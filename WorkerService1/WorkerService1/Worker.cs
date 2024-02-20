using System;
using System.IO;
using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;



namespace WorkerService1
{


    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        int port = 9999;
        readonly IPAddress ip = IPAddress.Any;
        List<string> data = new List<string>();
        static string testData = "*HQ,9175279103,V1,090833,V,5702.2679,N,00958.9685,E,0.00,0,140224,fff7fbff,238,66,12002,17329#";



        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();

            _logger.LogInformation($"TCP Listener started on port {port}");
            /*var myTimer = new System.Timers.Timer();
            myTimer.Interval = 10000;
            myTimer.Start();*/
            while (!stoppingToken.IsCancellationRequested)
            {
                /*myTimer.Elapsed += (sender, e) =>
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                };*/
                try
                {
                    TcpClient client = await tcpListener.AcceptTcpClientAsync();

                    if (client.Available > 0)
                    {
                        _logger.LogInformation("Client connected");
                        using (NetworkStream networkStream = client.GetStream())
                        {
                            using (StreamReader reader = new StreamReader(networkStream, Encoding.UTF8))
                            {
                                string jsonData = reader.ReadToEnd();

                                if (!string.IsNullOrEmpty(jsonData))
                                {
                                    try
                                    {
                                        string pattern = @"^\*[A-Z]{2}," +  //  0 - maker
                                        "[0-9]+," +                   //  1 - serial           
                                        "V1," +                     //  2 - type
                                        "[0-9]{6}," +                 //  3 - time  
                                        "[VA]," +                   //  4 - signal
                                        @"[0-9]{4}\.[0-9]{4}," +         //  5 - latitude
                                        "[NS]," +                  //  6 - latitude direction
                                        @"[0-9]{5}\.[0-9]{4}," +        //  7 - longitude
                                        "[EW]," +                 //  8 - longitude direction
                                        @"[0-9]{0,3}\.[0-9]{2}," +      //  9 - speed
                                        "[0-9]{0,3}," +             // 10 - direction
                                        "[0-9]{6}," +              // 11 - date
                                        "[0-9a-fA-F]{8}," +         // 12 - status
                                        "[0-9]+," +              // 13 - mcc                             
                                        "[0-9]+";                // 14 - mnc
                                        bool isValid = Regex.IsMatch(testData, pattern);

                                        if (isValid)
                                        {
                                            Decoding decoding = new Decoding();
                                            _logger.LogInformation("Data is valid");
                                            data = testData.Split(',').ToList();
                                            _logger.LogInformation($"{data[5]}");

                                        }
                                        else
                                        {
                                            _logger.LogInformation("Data is invalid");
                                        }
                                        _logger.LogInformation($"jsonData: {jsonData}");
                                    }
                                    catch (JsonException ex)
                                    {
                                        _logger.LogError(ex, "Error deserializing JSON data");
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error accepting TCP client");
                }
            }
            tcpListener.Stop();
        }
    }


    class Decoding
    {
        public double Latitude(List<string> data)
        {
            int latDeg = int.Parse(data[5].Substring(0, 2));
            float latMin = float.Parse(data[5].Substring(2, 7));
            float latDirection = data[6] == "N" ? 1 : -1;
            double latitude = Math.Round(latDirection * (latDeg + latMin / 60), 5);
            /* _logger.LogInformation($"{latitude}"); */
            return latitude;
        }

        public double Longitude(List<string> data)
        {
            int lonDeg = int.Parse(data[7].Substring(0, 3));
            float lonMin = float.Parse(data[7].Substring(3, 7));
            float lonDirection = data[8] == "E" ? 1 : -1;
            double longitude = Math.Round(lonDirection * (lonDeg + lonMin / 60), 5);
            /* _logger.LogInformation($"{longitude}"); */
            return longitude;
        }

        public string Time(List<string> data)
        {
            string dateString = data[11] + data[3];
            DateTime date = DateTime.ParseExact(dateString, "ddMMyyHHmmss", CultureInfo.InvariantCulture);
            return date.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public double Speed(List<string> data)
        {
            float speed = float.Parse(data[9]);
            return Math.Round(speed * 1.852, 2);
        }

        public int Direction(List<string> data)
        {
            return int.Parse(data[10]);
        }
    }
}