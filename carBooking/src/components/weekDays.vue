<script setup lang="ts">
import { onMounted, ref, watch } from "vue";
import moment from "moment";
import { store } from "@/store";
import { CarBookingArray } from "@/types";
interface Props {
  weekday?: string;
  weekNumber?: number;
  year?: number;
}

const props: Props = defineProps({
  weekday: String,
  weekNumber: Number,
  year: Number,
});
let date = ref<string>("");
let bookingsToday = ref<CarBookingArray[]>([]);
onMounted(() => {
  getDate();
  bookingsToday.value = store.carBookingArray.filter(
    (item) => item.Date === date.value
  );
});

watch(
  () => [store.carBookingArray.length, props.weekNumber, props.year],
  () => {
    getDate();
    bookingsToday.value = store.carBookingArray.filter(
      (item) => item.Date === date.value
    );
  }
);

function getDate() {
  let weekday: string;
  switch (props.weekday) {
    case "Mandag":
      weekday = "Monday";
      break;
    case "Tirsdag":
      weekday = "Tuesday";
      break;
    case "Onsdag":
      weekday = "Wednesday";
      break;
    case "Torsdag":
      weekday = "Thursday";
      break;
    case "Fredag":
      weekday = "Friday";
      break;
  }
  date.value = moment()
    .isoWeekYear(props.year)
    .locale("da")
    .day(weekday)
    .isoWeek(props.weekNumber)
    .format("DD/MM/YYYY");
}
</script>

<template>
  <div class="weekday">
    <section class="weekdayHeader">
      <p class="weekday">{{ weekday }}</p>
      <p class="date">{{ date }}</p>
      <p class="line" />
    </section>
    <section class="weekdayContent">
      <div
        v-for="booking in bookingsToday"
        :key="booking.Id"
        class="bookingCard"
      >
        <p>
          <span>{{ booking.StartTime }}-{{ booking.EndTime }} </span>
        </p>
        <p>
          <span>{{ booking.Date }}</span>
        </p>
        <p><span> Addrese: </span> {{ booking.Address }}.</p>
        <p>
          <span> Personer: </span> {{ booking.DriverName }},
          {{ booking.PassengerName }}.
        </p>
      </div>
    </section>
  </div>
</template>

<style scoped>
.weekday {
  flex: 1;
  background-color: black;
  color: white;
  overflow-x: hidden;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.weekdayHeader .weekday {
  font-size: 1.5rem;
  font-weight: bolder;
}

.weekdayHeader .date {
  font-size: 1.1rem;
  align-self: flex-end;
  margin-right: 5px;
}

.weekdayHeader {
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 100%;
}

.date {
  font-size: 1rem;
  text-align: end;
}

.line {
  border-bottom: 1px solid white;
  width: 100%;
  margin: 10px 0;
}

.bookingCard {
  display: flex;
  justify-content: space-between;
  flex-direction: column;
  border-bottom: 1px solid white;
  font-size: 1.3rem;
  border: 1px solid white;
  background-color: gray;
  padding: 10px;
  margin: 5px;
  border-radius: 1em;
}

.weekdayContent {
  width: 100%;
  overflow: auto;
  padding: 0 5px;
}

.weekdayContent div p span {
  display: block;
  font-weight: bolder;
  font-size: 1.5;
}

.weekdayContent div p {
  padding: 2px;
  padding-bottom: 10px;
}
.weekdayContent div {
  margin-bottom: 10px;
}
</style>
