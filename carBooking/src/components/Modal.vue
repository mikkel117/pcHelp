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

<style scoped></style>
