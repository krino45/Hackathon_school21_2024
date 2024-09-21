<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';
import ToggleButton from '@src/components/ToggleButton.vue';
import HeaderCustom from '@src/components/Header.vue';

// Работа с данными через `ref`, более удобный подход для Composition API
const preferences = ref([]);
const selectedPreferences = ref([]);
const email = ref('');
const password = ref('');
const editEmail = ref(false);
const editPassword = ref(false);
const newsletter = ref(false);

// Загрузка данных при монтировании компонента
onMounted(async () => {
  fetchPreferences();
  const userData = localStorage.getItem('user');
  const user = JSON.parse(userData);
  email.value = user.email;
  password.value = user.password;
  localStorage.setItem('user', JSON.stringify(user));
});

// Функция для получения предпочтений
const fetchPreferences = async () => {
  try {
    const response = await axios.get(import.meta.env.VITE_NODE_API_HOST + '/api/preferences');
    preferences.value = JSON.parse(response.data).map(item => item.Tag);
  } catch (error) {
    console.error('Error fetching preferences:', error);
  }
};

// Функция для сохранения изменений
const saveChanges = async () => {
  try {
    const userData = localStorage.getItem('user');
    const user = JSON.parse(userData);
    const response = await axios.post(import.meta.env.VITE_NODE_API_HOST + '/api/settings', {
      id: user.id,
      email: email.value,
      password: password.value,
      preferences: selectedPreferences.value
    });
    console.log('Response:', response.data);
  } catch (error) {
    console.error('There was an error!', error);
  }
};

// Функция переключения предпочтений
const togglePreference = (preference) => {
  const index = selectedPreferences.value.indexOf(preference);
  if (index === -1) {
    selectedPreferences.value.push(preference);
  } else {
    selectedPreferences.value.splice(index, 1);
  }
};
</script>

<template>

  <HeaderCustom />

  <div class="container">
    <div class="settings-container">
      <h1>Настройки профиля</h1>

      <section class="settings-section">
        <div class="section-header">
          <h2>Почта</h2>
          <button @click="editEmail = !editEmail" class="edit-button">
            <span class="edit-icon">&#9998;</span>
          </button>
        </div>
        <div class="input-group">
          <input 
            type="email" 
            v-model="email" 
            :disabled="!editEmail" 
            class="settings-input" 
          />
        </div>
      </section>

      <section class="settings-section">
        <div class="section-header">
          <h2>Пароль</h2>
          <button @click="editPassword = !editPassword" class="edit-button">
            <span class="edit-icon">&#9998;</span>
          </button>
        </div>
        <div class="input-group">
          <input 
            type="password" 
            v-model="password" 
            placeholder="Новый пароль" 
            :disabled="!editPassword" 
            class="settings-input" 
          />
        </div>
      </section>

      <section class="settings-section">
        <h2>Предпочтения</h2>
        <div class="preferences">
          <toggleButton 
            v-for="(preference, index) in preferences" 
            :key="index" 
            :label="preference"
            @toggle="togglePreference(preference)"
            :active="selectedPreferences.includes(preference)"
          />
        </div>
      </section>

      <button @click="saveChanges()" class="save-button">Сохранить изменения</button>
    </div>
  </div>
</template>

<style scoped>
  .container {
    margin-top: 150px;
    background-color: white;
  }

  /* Общие стили */
  .settings-container {
    max-width: 500px;
    margin: 0 auto;
    padding: 20px;
    background-color: #f4f4f4;
    border-radius: 10px;
    box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
  }

  h1, h2 {
    text-align: center;
    color: #6a0dad;
  }

  /* Блоки настроек */
  .settings-section {
    margin-bottom: 20px;
    padding: 15px;
    background-color: #fff;
    border-radius: 8px;
    border: 1px solid #e0e0e0;
  }

  .section-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
  }

  .edit-icon {
    cursor: pointer;
    font-size: 18px;
    color: #6a0dad;
  }

  .input-group {
    margin-top: 10px;
    width: 100%;
  }

  .settings-input {
    width: 100%;
    padding: 10px;
    font-size: 14px;
    border-radius: 4px;
    border: 1px solid #ccc;
    background-color: #f9f9f9;
    color: #333;
  }

  .settings-input:disabled {
    background-color: #e0e0e0;
    color: #999;
  }

  .preferences {
    display: flex;
    gap: 10px;
    flex-wrap: wrap;
    justify-content: center;
  }

  .save-button {
    display: block;
    width: 100%;
    padding: 12px;
    background-color: #6a0dad;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    font-size: 16px;
  }

  .save-button:hover {
    background-color: #5b0ba3;
  }
</style>
