<script setup lang="ts">
import { onMounted, toRefs } from "vue";
import ModalContentBookCar from "./ModalContentBookCar.vue";

const emit = defineEmits(["close"]);

function closeModal() {
  emit("close");
}
</script>

<template>
  <div class="modal">
    <div class="modal-content">
      <button class="close" @click="closeModal">x</button>
      <p class="headerText">booking af bil</p>
      <ModalContentBookCar @close="closeModal" />
    </div>
  </div>
</template>

<style scoped>
.modal {
  display: flex;
  justify-content: center;
  align-items: center;
  position: absolute;
  z-index: 1;
  left: 0;
  top: 0;
  width: 100%;
  height: 100%;
  overflow: auto;
  background-color: rgb(0, 0, 0);
  background-color: rgba(0, 0, 0, 0.8);
}

.modal-content {
  background-color: #fefefe;
  border: 1px solid #888;
  width: 50%;
  height: 50%;
  display: grid;
  grid-template-columns: 10% 1fr 10%;
  grid-template-rows: 10% 1fr;
  grid-gap: 10px;
}

.headerText {
  grid-column: 2;
  grid-row: 1;
  text-align: center;
  align-self: center;
  font-weight: bold;
  font-size: 20px;
}

.close {
  grid-column: 3;
  grid-row: 1;
  height: 100%;
  justify-self: end;
  align-self: start;
  font-weight: bold;
  color: #aaaaaa;
  cursor: pointer;
  font-size: large;
}
</style>
