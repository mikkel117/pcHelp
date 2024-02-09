<script setup lang="ts">
import { watch, ref, reactive } from "vue";
import { store } from "../store";
import moment from "moment";

const emit = defineEmits(["close"]);
const hour = ref(0);
const minute = ref(0);
const addrese = ref("");
const driver = ref("");
const passenger = ref("");
let time: any = reactive({
  startTime: null,
  endTime: null,
});
function submit(e) {
  let index = store.carBookingArray.length;
  if (index === 0) {
    index = 1;
  }

  let date = moment(e.target.date.value).format("DD/MM/YYYY");
  let booking = {
    Id: index,
    Address: e.target.addrese.value,
    DriverName: e.target.driver.value,
    PassengerName: e.target.passenger.value,
    Date: date,
    StartTime: e.target.startTime.value,
    EndTime: e.target.endTime.value,
    Hours: hour.value,
    Minutes: minute.value,
  };
  store.addCarBookingArray(booking);
  addrese.value = "";
  driver.value = "";
  passenger.value = "";
  emit("close");
}

watch(
  () => [time.startTime, time.endTime],
  () => {
    if (time.startTime === null || time.endTime === null) {
      return;
    }

    let date1 = moment(time.startTime, "HH:mm");
    let date2 = moment(time.endTime, "HH:mm");
    let hours = date2.diff(date1, "hours");
    let minutes = date2.diff(date1, "minutes") % 60;
    hour.value = hours;
    minute.value = minutes;
  }
);
</script>

<template>
  <form @submit.prevent="submit" id="test">
    <label for="addrese">Addrese:</label>
    <input type="text" name="addrese" required v-model="addrese" />
    <label for="driver">Føre:</label>
    <input type="text" name="driver" required v-model="driver" />
    <label for="passenger">Passager:</label>
    <input type="text" name="passenger" required v-model="passenger" />
    <div>
      <label for="date">Dato:</label>
      <input type="date" name="date" required />
      <label for="startTime">Start</label>
      <input type="time" name="startTime" v-model="time.startTime" required />
      <label for="endTime">Slut</label>
      <input type="time" name="endTime" v-model="time.endTime" required />
    </div>
    <p>
      det kommer til at tage Ca:
      <span>
        {{ hour }}
        <!-- {{ hour > 1 ? "timer" : hour === 1 ? "time" : "timer" }} -->
        t.
      </span>
      <!-- <span>&nbsp;</span> -->
      <span class="minute">
        {{ minute }}
        min.
      </span>
    </p>
    <button>book</button>
  </form>
</template>

<style scoped>
form {
  grid-column: 2;
  display: flex;
  flex-direction: column;
  height: 100%;
  justify-content: space-between;
}
label {
  font-weight: bold;
  font-size: larger;
}
input {
  width: 100%;
  height: 30px;
  border-radius: 5px;
  border: 1px solid black;
  font-size: large;
  padding-left: 5px;
}

form div {
  display: flex;
  justify-content: space-between;
  gap: 10px;
}

form div label {
  align-self: center;
}

p {
  font: 1em sans-serif;
  text-align: center;
}
</style>
