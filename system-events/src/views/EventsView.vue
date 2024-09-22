<script setup>
import { ref, onMounted, nextTick, computed } from 'vue'; // Добавлен импорт computed
import HeaderCustom from '@src/components/Header.vue'
import ToggleButton from '@src/components/ToggleButton.vue';
import axios from 'axios';

const events = ref([]);
const preferences = ref([]);
const showAllPreferences = ref(false);

onMounted(async () => {
  fetchPreferences();
});

// Функция для получения предпочтений
const fetchPreferences = async () => {
  let response = await axios.get(import.meta.env.VITE_NODE_API_HOST + '/api/get_user_preferences');
  preferences.value = JSON.parse(response.data).map(item => item.Tag);

  response = await axios.get(import.meta.env.VITE_NODE_API_HOST + '/api/get_all_events');
  events.value = JSON.parse(response.data);
  console.info(events.value[0].Id)
  await nextTick();
  animateEvents();
  window.addEventListener('scroll', animateEvents); // добавляем обработчик скролла
  }

const animateEvents = async () => {
  const threshold = window.innerHeight * 0.75; // Увеличиваем порог видимости
    events.value.forEach((event, index) => {
      const element = document.getElementsByClassName('event-block')[index];
      if (element) {
        const rect = element.getBoundingClientRect();
        // Проверяем, попадает ли блок в видимую область
        event.visible = rect.top < threshold && rect.bottom > 0;
      }
    });
};

const togglePreferences = () => {
  showAllPreferences.value = !showAllPreferences.value;
};

Date.prototype.minusHours = function(hours) {
  const date = new Date(this.getTime()); // Создаем новую дату на основе текущей
  date.setUTCHours(date.getUTCHours() - hours); // Вычитаем часы
  return date;
};

Date.prototype.minusMinutes = function(minutes) {
  const date = new Date(this.getTime());
  date.setUTCMinutes(date.getUTCMinutes() - minutes); 
  return date;
};

const getDuration = (endTimeStr, startTimeStr) => {
  let startTime = new Date(startTimeStr);
  let endTime = new Date(endTimeStr); // Создаем новую дату на основе endTime
  let duration = endTime.minusHours(startTime.getUTCHours());
  duration = duration.minusMinutes(startTime.getUTCMinutes());
  const hours = duration.getUTCHours();
  const minutes = duration.getUTCMinutes();
  return `${hours} ч ${minutes} мин`;
}

const getStringDate = (date) => {
  const day = date.getDay() < 10? '0' + date.getDay() : date.getDay();
  const month = date.getMonth() < 10? '0' +date.getMonth() :date.getMonth();
  const year = date.getFullYear();
  const hours = date.getUTCHours() < 10? '0' + date.getUTCHours() : date.getUTCHours();
  const minutes = date.getUTCMinutes() < 10? '0' + date.getUTCMinutes() : date.getUTCMinutes();
  return `${day}.${month}.${year} в ${hours}:${minutes}`;
}

const subscribeUser = async (eventId) => {
  const userId = localStorage.getItem('user');
  let response = await axios.get(import.meta.env.VITE_NODE_API_HOST + "/api/get_user", {
      params: { userJsonId: JSON.stringify({ userId: userId }) },
    });
    let dbUser = JSON.parse(response.data).User;
  response = await axios.post(
    import.meta.env.VITE_NODE_API_HOST + '/api/post_subscribe_user', 
    {
        eventId: eventId`н`, 
        user: dbUser,
    },
    {
        headers: {
            'Content-Type': 'application/json',
        },
    });
}

const displayedPreferences = computed(() => {
  return showAllPreferences.value ? preferences.value : preferences.value.slice(0, 3);
});
</script>

<template>
  <HeaderCustom />

  <div class="events-container">
    <div 
      v-for="(event, index) in events" 
      :key="event.Id" 
      class="event-block" 
    >
      <h3 class="event-title">{{ event.Name }}</h3>
      <div class="preferences">
        <toggleButton
          v-for="(preference, index) in displayedPreferences" 
          :key="index" 
          :label="preference"
          :active="true"
          disabled="true"
        />
        <button v-if="preferences.length > 3" @click="togglePreferences" class="toggle-button">
          {{ showAllPreferences ? 'Скрыть' : '...' }}
        </button>
      </div>
      <div class="event-details">
        <p class="event-participants">{{ event.AttendeesId ? "Участников: " + event.AttendeesId.length : "Участников: 0" }}</p>
        <p class="event-date">Начало: {{ getStringDate(new Date(event.StartTime)) }}</p>
        <p class="event-duration">Продолжительность: {{ getDuration(event.EndTime, event.StartTime) }}</p>
        <!-- <p class="event-organizer">Организатор: {{ event. }}</p> -->
      </div>
      <button @click="subscribeUser(event.Id)" class="join-button">Подписаться</button>
    </div>
  </div>
</template>

<style scoped>
.events-container {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  padding: 20px;
}

.event-block {
  background-color: #7b68ee;
  color: white;
  border-radius: 8px;
  margin: 20px;
  padding: 20px; /* Уменьшен отступ */
  width: 350px;
  height: auto; /* Изменяем на auto для гибкости */
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2); 
  display: flex;
  flex-direction: column; /* Вертикальное расположение элементов */
  align-items: flex-start; /* Выровнять по началу */
}

.event-title {
  font-size: 18px;
  font-weight: bold;
  margin-bottom: 10px; /* Расстояние между заголовком и предпочтениями */
}

.preferences {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 10px;
  margin-bottom: 10px; /* Расстояние под предпочтениями */
}

.event-details {
  margin-bottom: 10px; /* Расстояние между деталями события и кнопкой */
}

.event-participants,
.event-date,
.event-duration,
.event-organizer {
  font-size: 14px;
  margin: 2px 0; /* Уменьшаем отступы между строками */
}

.join-button {
  margin-top: 10px;
  padding: 10px;
  background-color: #4a0082;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  width: 100%; /* Кнопка занимает всю ширину */
}

.join-button:hover {
  background-color: #6a0082;
}

.toggle-button {
  background: none;
  color: white;
  border: none;
  cursor: pointer;
  font-size: 14px;
  margin-left: auto; /* Выравнивание кнопки "..." вправо */
}
</style>
