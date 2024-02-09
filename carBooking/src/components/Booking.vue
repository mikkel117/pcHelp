<script setup lang="ts">
import weekDays from "./weekDays.vue";
import Modal from "./Modal.vue";
import { Icon } from "@iconify/vue";
import { ref } from "vue";
import moment from "moment";
import { store } from "@/store";
let week = ["Mandag", "Tirsdag", "Onsdag", "Torsdag", "Fredag"];
let showModel = ref(false);
let weekNumber = ref(moment().week());

const closeModel = () => {
  showModel.value = false;
};
</script>

<template>
  <section class="carBookingContainer">
    <h2>Book bil</h2>
    <section class="bookingContainer">
      <div class="bookingWeekWrapper">
        <button @click="showModel = true">book</button>
        <div class="weekNavigator">
          <Icon
            icon="raphael:arrowleft2"
            @click="weekNumber = weekNumber - 1"
          />
          <p>uge: {{ weekNumber }}</p>
          <Icon icon="raphael:arrowright2" @click="weekNumber++" />
        </div>
      </div>
      <div class="weekdaysWrapper">
        <weekDays
          v-for="day in week"
          :key="day"
          :week-number="weekNumber"
          :weekday="day"
        />
      </div>
    </section>
  </section>
  <!-- <div class="test">
    <Icon icon="raphael:arrowleft2" />
    <p>uge: {{ weekNumber }}</p>
    <Icon icon="raphael:arrowright2" />
  </div> -->
  <Modal v-if="showModel" @close="closeModel" />
</template>

<style scoped>
.carBookingContainer {
  display: grid;
  grid-template-columns: 10% 1fr 10%;
  grid-template-rows: 10% 1fr;
  height: 100%;
  width: 100%;
}
h2 {
  grid-column: 2;
  justify-self: center;
  align-self: center;
  font-size: 2rem;
}

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
  gap: 10px;
  align-items: center;
  background-color: white;
}

@media (min-width: 1500px) {
  .bookingContainer {
    height: 840px;
  }
}
</style>
