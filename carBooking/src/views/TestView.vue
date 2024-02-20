<script setup>
import moment from "moment";
import { ref } from "vue";
const year = ref(2027);
const week = ref(moment().isoWeek());
const getDate = () => {
  console.log(
    moment(2027, "YYYY")
      .locale("da")
      .dayOfYear(1)
      .day(1)
      .isoWeek(1)
      .format("dddd, MMMM Do YYYY")
  );
};

const incrementWeek = () => {
  week.value++;
};

const decrementWeek = () => {
  week.value--;
};
</script>

<template>
  <button @click="getDate">click me</button>
  <div>
    <button @click="decrementWeek">-</button>
    <p>{{ week }}</p>
    <button @click="incrementWeek">+</button>
  </div>
</template>

<style scoped></style>
