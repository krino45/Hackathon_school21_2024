<template>
  <button 
    @click="toggle" 
    :class="['preference', { active: active }]">
    {{ label }}
  </button>
</template>

<script>
export default {
  name: 'ToggleButton',
  props: {
    label: String,
    active: Boolean
  },
  methods: {
    toggle() {
      this.$emit('toggle');
    }
  }
};
</script>

<style scoped>
button {
  width: 200px;
}

.preference {
  padding: 10px 20px;
  border-radius: 20px;
  border: 2px solid #15263f;
  background-color: white;
  color: #15263f;
  font-size: 16px;
  cursor: pointer;
  transition: background-color 0.3s, color 0.3s;
}

.preference.active {
  background-color: #15263f;
  color: white;
}

.preference:hover {
  background-color: #2e4c78;
  color:white;
}
</style>
