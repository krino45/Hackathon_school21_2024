<script>
import axios from 'axios';
import ToggleButton from '@src/components/ToggleButton.vue'

export default {
  name: 'Settings',
  components: { ToggleButton },
  data() {
    return {
      preferences: ['something'],
      selectedPreferences: [],
      email: 'example@example.com', // брать почту из бд
      newPassword: '',
      editEmail: false,
      editPassword: false,
      newsletter: false,
    };
  },

  mounted() {
    this.fetchPreferences(); // Вызов метода при загрузке компонента
  },

  methods: {
    async fetchPreferences() {
      try {
        const response = await axios.get(import.meta.env.VITE_NODE_API_HOST + '/api/preferences'); // URL нашего бэкенда
        this.preferences = JSON.parse(response.data).map(item => item.Tag);
        
        console.info(JSON.parse(response.data).map(item => item.id));

      } catch (error) {
        console.error('Error fetching preferences:', error);
      }
    },

    togglePreference(preference) {
      const index = this.selectedPreferences.indexOf(preference);
      if (index === -1) {
        this.selectedPreferences.push(preference);
      } else {
        this.selectedPreferences.splice(index, 1);
      }
      console.log(this.selectedPreferences);
    }
  }
};
</script>

<template>
  <div class="container">
    <div class="settings-container">
      <h1>Настройки профиля</h1>

      <!-- Блок изменения почты -->
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

      <!-- Блок изменения пароля -->
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
            v-model="newPassword" 
            placeholder="Новый пароль" 
            :disabled="!editPassword" 
            class="settings-input" 
          />
        </div>
      </section>

      <!-- Блок предпочтений -->
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

      <!-- Кнопка сохранения изменений -->
      <button class="save-button">Сохранить изменения</button>
    </div>
  </div>
</template>

<style scoped>

.container {
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

/* Заголовки */
h1 {
  text-align: center;
  color: #6a0dad;
}

h2 {
  margin-bottom: 10px;
  color: #4b0082;
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

/* Иконка редактирования */
.edit-icon {
  cursor: pointer;
  font-size: 18px;
  color: #6a0dad;
}

/* Инпуты */
.input-group {
  margin-top: 10px;
  width: 95%;
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

/* Чекбоксы предпочтений */
.preferences-group {
  display: flex;
  flex-direction: column;
}

.preference-item {
  margin-bottom: 10px;
  font-size: 14px;
  color: #4b0082;
}

input[type="checkbox"] {
  margin-right: 10px;
}

.edit-button {
  background-color: white;
  border-radius: 10px;
  border-width: 2px;
  border-style: solid;
  border-color: #6a0dad;
}

/* Кнопка сохранения */
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

.preferences {
  display: flex;
  gap: 10px;
  flex-wrap: wrap;
  justify-content: center;
}
</style>
