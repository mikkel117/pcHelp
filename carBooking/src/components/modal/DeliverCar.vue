<script setup lang="ts">
const emit = defineEmits(["close"]);
function closeModal() {
  emit("close");
}
</script>

<template>
  <div class="modal">
    <div class="modal-content">
      <h2>Deliver car</h2>
      <button class="close" @click="closeModal">x</button>
      <div>
        <p>Are you sure you want to deliver the car?</p>
        <button @click="closeModal">Yes</button>
        <button @click="closeModal">No</button>
      </div>
    </div>
  </div>
</template>

<style>
h2 {
  grid-row: 1;
  grid-column: 2;
  text-align: center;
}
.modal-content div {
  justify-self: center;
  align-self: center;
  grid-row: 2;
  grid-column: 2;
  width: 50%;
}
</style>
