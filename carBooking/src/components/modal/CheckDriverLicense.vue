<script setup lang="ts">
const emit = defineEmits(["close"]);
function closeModal() {
  emit("close");
}
</script>

<template>
  <div class="modal">
    <div class="modal-content">
      <button class="close" @click="closeModal">x</button>
      <h1>Check driver license</h1>
    </div>
  </div>
</template>

<style></style>
