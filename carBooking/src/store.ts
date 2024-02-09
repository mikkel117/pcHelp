import { reactive } from "vue";
interface CarBookingArray {
  id: number;
  Address: string;
  DriverName: string;
  PassengerName: string;
  Date: string;
  StartTime: string;
  EndTime: string;
  Hours: number;
  Minutes: number;
}

export const store = reactive({
  carBookingArray: [
    {
      id: 1,
      Address: "1234 Main St",
      DriverName: "John Doe",
      PassengerName: "Jane Doe",
      Date: "05/02/2024",
      StartTime: "08:00",
      EndTime: "12:00",
      Hours: 4,
      Minutes: 0,
    },
    {
      id: 2,
      Address: "5678 Main St",
      DriverName: "John Doe",
      PassengerName: "Jane Doe",
      Date: "05/02/2024",
      StartTime: "08:00",
      EndTime: "12:00",
      Hours: 4,
      Minutes: 0,
    },
    {
      id: 3,
      Address: "91011 Main St",
      DriverName: "John Doe",
      PassengerName: "Jane Doe",
      Date: "05/02/2024",
      StartTime: "08:00",
      EndTime: "12:00",
      Hours: 4,
      Minutes: 0,
    },
    {
      id: 4,
      Address: "121314 Main St",
      DriverName: "John Doe",
      PassengerName: "Jane Doe",
      Date: "05/02/2024",
      StartTime: "08:00",
      EndTime: "12:00",
      Hours: 4,
      Minutes: 0,
    },
  ] as CarBookingArray[],
  carBookingArrayLength: 0,
  addCarBookingArray: (carBookingArray: CarBookingArray) => {
    store.carBookingArray.push(carBookingArray);
    store.carBookingArrayLength = store.carBookingArray.length;
  },
});
