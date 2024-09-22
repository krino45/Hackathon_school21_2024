<script setup>
import { ref, onMounted, onBeforeMount } from 'vue';
import axios from 'axios';
import ToggleButton from '@src/components/ToggleButton.vue';
import HeaderCustom from '@src/components/Header.vue';

const checkUserId = async () => {
    const storedUserId = localStorage.getItem('user');
    if (storedUserId && typeof storedUserId === 'string') {
        let response = await axios.get(import.meta.env.VITE_NODE_API_HOST + "/api/get_user", {
            params: { userJsonId: JSON.stringify({ userId: storedUserId }) },
        });
        console.log(response.data);

        if (!JSON.parse(response.data).Success)
        {
            userId.value = null;
            localStorage.removeItem('user');
            window.location.href = "/login";
        }

  }
  else
    {
        window.location.href = "/login";
    }
};


// Работа с данными через `ref`, более удобный подход для Composition API
const preferences = ref([]);
const selectedPreferences = ref([]);
const email = ref('');
const oldPassword = ref('');
const newPassword = ref('');
const editEmail = ref(false);
const editPassword = ref(false);
const oldPasswordVisible = ref(false);
const newPasswordVisible = ref(false);

onBeforeMount(async () => {
  console.log('Component mounted');
  checkUserId();
});
// Загрузка данных при монтировании компонента
onMounted(async () => {
  fetchPreferences();
});

// Функция для получения предпочтений
const fetchPreferences = async () => {
  try {
    let id = localStorage.getItem('user');
    let response = await axios.get(import.meta.env.VITE_NODE_API_HOST + '/api/get_user_preferences');
    preferences.value = JSON.parse(response.data).map(item => item.Tag);

    response = await axios.get(import.meta.env.VITE_NODE_API_HOST + "/api/get_user", {
      params: { userJsonId: JSON.stringify({ userId: id }) },
    });
    let dbUser = JSON.parse(response.data).User;
    console.info(dbUser);
    selectedPreferences.value = dbUser.Preferences;
    email.value = dbUser.Email;

  } catch (error) {
    console.error('Error fetching preferences:', error);
  }
};

// Функция для сохранения изменений
const saveChanges = async () => {
  try {
    const id = localStorage.getItem('user');
    let response = await axios.post(
      import.meta.env.VITE_NODE_API_HOST + '/api/post_user_email', 
      {
          userId: id,
          newEmail: email.value,
      },
      {
          headers: {
              'Content-Type': 'application/json',
          },
      });

  response = await axios.post(
      import.meta.env.VITE_NODE_API_HOST + '/api/post_user_password', 
      {
          userId: id,
          currentPassword: oldPassword.value,
          newPassword: newPassword.value,
      },
      {
          headers: {
              'Content-Type': 'application/json',
          },
      });

  response = await axios.post(
    import.meta.env.VITE_NODE_API_HOST + '/api/post_user_preferences', 
    {
        userId: id,
        preferences: selectedPreferences.value,
    },
    {
        headers: {
            'Content-Type': 'application/json',
        },
    });
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
            :type="oldPasswordVisible ? 'text' : 'password'"
            v-model="oldPassword"
            placeholder="Текущий пароль"
            :disabled="!editPassword"
            class="settings-input"
          />
          <button @click="oldPasswordVisible = !oldPasswordVisible" class="eye-button" :disabled="!editPassword"
          :style="{ cursor: editPassword ? 'pointer' : 'default' }">
            <font-awesome-icon 
              :icon="oldPasswordVisible ? 'fas fa-eye-slash' : 'fas fa-eye'" 
              :style="{ color: editPassword ? '#6a0dad' : 'grey' }"
            />
          </button>
        </div>

        <div class="input-group">
          <input
            :type="newPasswordVisible ? 'text' : 'password'"
            v-model="newPassword"
            placeholder="Новый пароль"
            :disabled="!editPassword" 
            class="settings-input"
          />
          <button @click="newPasswordVisible = !newPasswordVisible" class="eye-button" :disabled="!editPassword"
          :style="{ cursor: editPassword ? 'pointer' : 'default' }">
            <font-awesome-icon :icon="newPasswordVisible ? 'fas fa-eye-slash' : 'fas fa-eye'" 
            :style="{ color: editPassword ? '#6a0dad' : 'grey' }"
            />
          </button>
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
  color: #15263f;
  padding-bottom: 20px;
}

h2 {
  margin-bottom: 10px;
  color: #15263f;
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
  color: #15263f;
}

/* Инпуты */
.input-group {
  position: relative;
  margin-top: 10px;
  margin-bottom: 15px;
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
  width: calc(100% - 40px); /* Уменьшаем ширину, чтобы оставить место для кнопки */
  padding-right: 40px; /* Добавляем отступ справа для кнопки */
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
  color: #15263f;
}

input[type="checkbox"] {
  margin-right: 10px;
}

.edit-button {
  background-color: white;
  border-radius: 10px;
  border-width: 2px;
  border-style: solid;
  border-color: #15263f;
}

/* Кнопка сохранения */
.save-button {
  display: block;
  width: 100%;
  padding: 12px;
  background-color: #15263f;
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
  color: #15263f;
}

h2 {
  margin-bottom: 10px;
  color: #15263f;
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
  color: #15263f;
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
  color: #15263f;
}

input[type="checkbox"] {
  margin-right: 10px;
}

.edit-button {
  background-color: white;
  border-radius: 10px;
  border-width: 2px;
  border-style: solid;
  border-color: #15263f;
}

/* Кнопка сохранения */
.save-button {
  display: block;
  width: 100%;
  padding: 12px;
  background-color: #15263f;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
}

.save-button:hover {
  background-color: #0a121e;
}

.preferences {
  display: flex;
  gap: 10px;
  flex-wrap: wrap;
  justify-content: center;
}

.eye-button {
  position: absolute;
  left: 95%;
  top: 50%;
  transform: translateY(-50%);
  background: transparent;
  border: none;
  outline: none;
}

.eye-button i {
  font-size: 16px; /* Размер иконки */
}

</style>