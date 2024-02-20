import { reactive } from "vue";
import { CarBookingArray } from "./types";

export const store = reactive({
  carBookingArray: [
    {
      Id: 1,
      Address: "1234 Main St",
      DriverName: "John Doe",
      PassengerName: "Jane Doe",
      Date: "19/02/2024",
      StartTime: "08:00",
      EndTime: "12:00",
      Hours: 4,
      Minutes: 0,
    },
    {
      Id: 2,
      Address: "5678 Main St",
      DriverName: "John Doe",
      PassengerName: "Jane Doe",
      Date: "19/02/2024",
      StartTime: "08:00",
      EndTime: "12:00",
      Hours: 4,
      Minutes: 0,
    },
    {
      Id: 3,
      Address: "91011 Main St",
      DriverName: "John Doe",
      PassengerName: "Jane Doe",
      Date: "19/02/2024",
      StartTime: "08:00",
      EndTime: "12:00",
      Hours: 4,
      Minutes: 0,
    },
    {
      Id: 4,
      Address: "121314 Main St",
      DriverName: "John Doe",
      PassengerName: "Jane Doe",
      Date: "19/02/2024",
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
