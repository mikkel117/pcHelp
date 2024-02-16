<script setup lang="ts">
import { ref } from "vue";
import BookingSchema from "./BookingSchema.vue";
import CarTrackingMap from "./CarTrackingMap.vue";
let onCarBooking = ref<boolean>(true);
</script>

<template>
  <section class="carBookingContainer">
    <section class="carBookingHeader">
      <h2>Book bil</h2>
      <div class="bookTrackCarWrapper">
        <button
          class="bookCar"
          @click="onCarBooking = true"
          :class="{ active: onCarBooking }"
        >
          book bil
        </button>
        <button
          class="trackCar"
          @click="onCarBooking = false"
          :class="{ active: !onCarBooking }"
        >
          track bil
        </button>
      </div>
    </section>
    <BookingSchema v-if="onCarBooking" />
    <CarTrackingMap v-else />
  </section>
</template>

<style scoped>
.carBookingContainer {
  display: grid;
  grid-template-columns: 2% 1fr 2%;
  grid-template-rows: 10% 1fr;
  height: 100%;
  width: 100%;
}
.carBookingHeader {
  width: 100%;
  grid-column: 2;
  display: flex;
  flex-direction: column;
}

.carBookingHeader h2 {
  align-self: center;
  font-size: 2rem;
}

.bookTrackCarWrapper {
  display: flex;
  gap: 10px;
  align-items: center;
}

.active {
  background-color: gray;
}

@media (min-width: 1500px) {
  .bookingContainer,
  .carTrackingMapContainer {
    height: 840px;
  }
}
</style>
