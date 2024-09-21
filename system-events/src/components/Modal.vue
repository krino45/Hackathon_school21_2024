<script setup>
  import { defineProps, defineEmits } from 'vue';
  
  const props = defineProps({
    title: {
      type: String,
      default: 'Модальное окно'
    },
    isVisible: {
      type: Boolean,
      required: true
    }
  });
  
  const emit = defineEmits(['close']);
  
  const close = () => {
    emit('close');
  };
</script>

<template>
    <div v-if="isVisible" class="modal">
      <div class="modal-content">
        <span class="close" @click="close">&times;</span>
        <h2>{{ title }}</h2>
        <slot></slot>
      </div>
    </div>
</template>
  
<style scoped>
    .modal {
        display: flex;
        justify-content: center;
        align-items: center;
        flex-direction: column;
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;

        background-color: rgba(0, 0, 0, 0.5);
        color: black;
        
        z-index: 1000;
    }
    
    .modal-content {
        width: 50%;
        padding: 20px;
        border-radius: 5px;
        background-color: white;
    }
    
    .close {
        color: #aaa;
        float: right;
        font-size: 28px;
        font-weight: bold;
    }
    
    .close:hover,
    .close:focus {
        color: black;
        text-decoration: none;
        cursor: pointer;
    }

    .modal-content h2 {
        padding-bottom: 18px;
    }
</style>
  