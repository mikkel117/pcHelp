<script setup lang="ts">
import weekDays from "./weekDays.vue";
import carBooking from "./modal/Carbooking.vue";
import DeliverCar from "./modal/DeliverCar.vue";
import { Icon } from "@iconify/vue";
import { ref } from "vue";
import moment from "moment";
let week = ["Mandag", "Tirsdag", "Onsdag", "Torsdag", "Fredag"];
let showBookingCar = ref<boolean>(false);
let showDeliverCar = ref<boolean>(false);
let weekNumber = ref<number>(moment().week());
let year = ref<number>(moment().year());
let user = ref<string>("bob");

function incrementWeek() {
  weekNumber.value++;
  if (
    weekNumber.value > moment(year.value, "YYYY").locale("da").isoWeeksInYear()
  ) {
    weekNumber.value = 1;
    year.value++;
  }
}

function decrementWeek() {
  weekNumber.value = weekNumber.value - 1;
  if (weekNumber.value < 1) {
    year.value--;
    weekNumber.value = moment(year.value, "YYYY").locale("da").isoWeeksInYear();
  }
}
</script>

<template>
  <section class="bookingContainer">
    <div class="bookingWeekWrapper">
      <button @click="showBookingCar = true" v-if="user != 'bob'">book</button>
      <button @click="showDeliverCar = true" v-if="user == 'bob'">
        aflever billen
      </button>
      <div class="weekNavigator">
        <button @click="decrementWeek">
          <Icon icon="raphael:arrowleft2" />
        </button>
        <p>uge: {{ weekNumber }}</p>
        <button @click="incrementWeek">
          <Icon icon="raphael:arrowright2" />
        </button>
      </div>
    </div>
    <div class="weekdaysWrapper">
      <weekDays
        v-for="day in week"
        :key="day"
        :week-number="weekNumber"
        :weekday="day"
        :year="year"
      />
    </div>
  </section>
  <carBooking v-if="showBookingCar" @close="showBookingCar = false" />
  <DeliverCar v-if="showDeliverCar" @close="showDeliverCar = false" />
</template>

<style>
.bookingContainer {
  height: 650px;
  grid-column: 2;
  grid-row: 2;
  padding: 1rem;
  background-color: gray;
  display: flex;
  flex-direction: column;
  overflow-y: hidden;
}

.bookingWeekWrapper {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-bottom: 10px;
}
.bookingWeekWrapper div {
  display: flex;
  align-items: center;
}
.weekdaysWrapper {
  height: 100%;
  max-height: 100%;
  width: 100%;
  overflow: hidden;
  display: flex;
  gap: 10px;
}

.weekNavigator {
  display: flex;
  gap: 20px;
  align-items: center;
  justify-content: center;
  background-color: white;
}
.weekNavigator p {
  margin: 0;
  padding: 0;
  font-weight: bolder;
}
.weekNavigator button {
  border: none;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: white;
  color: black;
}

.weekNavigator button:hover {
  background-color: black;
  color: white;
}
</style>
