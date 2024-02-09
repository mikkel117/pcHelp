using System;
using System.IO;
using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using NmeaParser;
using NmeaParser.Messages;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace WorkerService1
{


    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        int port = 9999;
        readonly System.Net.IPAddress ip = IPAddress.Any;
        static string testData = "*HQ,9175279103,V1,100348,V,5702.2818,N,00958.9676,E,0.00,325,080224,fbf7fbff,238,66,12002,17329#";



        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();

            _logger.LogInformation($"TCP Listener started on port {port}");
            while (!stoppingToken.IsCancellationRequested)
            {
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
                                        string pattern = @"^\*[A-Z]{2}," +
                "[0-9]+," +
                "V1," +
                "[0-9]{6}," +
                "[VA]," +
                @"[0-9]{4}\.[0-9]{4}," +
                "[NS]," +
                @"[0-9]{5}\.[0-9]{4}," +
                "[EW]," +
                @"[0-9]{0,3}\.[0-9]{2}," +
                "[0-9]{0,3}," +
                "[0-9]{6}," +
                "[0-9a-fA-F]{8}," +
                "[0-9]+," +
                "[0-9]+";

                                        // Use the Regex class to match the pattern
                                        bool isValid = Regex.IsMatch(testData, pattern);

                                        if (isValid)
                                        {
                                            _logger.LogInformation("Data is valid");
                                        }
                                        else
                                        {
                                            _logger.LogInformation("Data is invalid");
                                        }
                                        _logger.LogInformation($"{jsonData}");
                                    }
                                    catch (System.Text.Json.JsonException ex)
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
}
