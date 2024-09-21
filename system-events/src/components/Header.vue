<script setup>
import { ref, onMounted, onUnmounted, computed } from 'vue';
import { useRoute } from 'vue-router';
import Button from '@src/components/Button.vue';
import Modal from '@src/components/Modal.vue';
import ToggleButton from '@src/components/ToggleButton.vue';
import axios from 'axios';

const isDropdownOpen = ref(false);
const isModalOpen = ref(false);
const eventType = ref('t1'); // Set default as t1
const eventName = ref('');
const eventLocation = ref(''); // Holds the selected location
const venues = ref([]); // Stores the list of venues fetched from the API
const startTime = ref('');
const endTime = ref('');
const minAttendees = ref('');
const invitedAttendeesInput = ref(''); // Now it's a string input
const invitedAttendees = ref([]); // Will hold the array of emails
const roundtableId = ref('1'); // For t2 combobox
const preferences = ref([]); // For t3 and t4 preferences
const selectedPreferences = ref([]);

const route = useRoute();

const showControls = computed(() => {
    return !['/settings'].includes(route.path);
});

const toggleDropdown = () => {
    isDropdownOpen.value = !isDropdownOpen.value;
};

const toggleModal = () => {
    isModalOpen.value = !isModalOpen.value;
};

const handleClickOutside = (event) => {
    const dropdown = document.querySelector('.panel-nav');
    if (dropdown && !dropdown.contains(event.target)) {
        isDropdownOpen.value = false;
    }
};

const handleClickExit = () => {
    localStorage.removeItem('user');
};

const handleSubmit = async () => {
    let data = {
        eventType: eventType.value,
        Name: eventName.value,
        venue: eventLocation.value,
        startTime: startTime.value,
        endTime: endTime.value,
        minAttendees: minAttendees.value,
        AttendeesId: invitedAttendees.value,
        roundtableId: roundtableId.value, // t2
        tags: selectedPreferences.value, // t3, t4
    };

    invitedAttendees.value = invitedAttendeesInput.value
        .split(',')
        .map(email => email.trim())
        .filter(email => email !== '');

    invitedAttendees.value.push(localStorage.getItem('user'));
    data.AttendeesId = invitedAttendees.value.slice().reverse(); 

    let response = await axios.post(
        import.meta.env.VITE_NODE_API_HOST + '/api/post_event',
        data,
        {
            headers: {
                'Content-Type': 'application/json',
            },
        }
    );
    console.log(response.data);
    toggleModal();
};

// Fetch venues from the API
const fetchVenues = async () => {
    try {
        let response = await axios.get(import.meta.env.VITE_NODE_API_HOST + '/api/get_all_venues_strings');
        venues.value = response.data; // Assuming the response is an array of venue names
    } catch (error) {
        console.error('Error fetching venues:', error);
    }
};

const togglePreference = (preference) => {
    const index = selectedPreferences.value.indexOf(preference);
    if (index === -1) {
        selectedPreferences.value.push(preference);
    } else {
        selectedPreferences.value.splice(index, 1);
    }
};

const fetchPreferences = async () => {
    try {
        let response = await axios.get(import.meta.env.VITE_NODE_API_HOST + '/api/get_user_preferences');
        preferences.value = JSON.parse(response.data).map(item => item.Tag);
    } catch (error) {
        console.error('Error fetching preferences:', error);
    }
};

onMounted(() => {
    document.addEventListener('click', handleClickOutside);
    fetchVenues(); // Fetch venues when component is mounted
    fetchPreferences();
});

onUnmounted(() => {
    document.removeEventListener('click', handleClickOutside);
});
</script>


<template>
    <header>
        <div class="logo">
            <a href="/events">
                <img src="@assets/logo.svg" alt="logo">
            </a>
        </div>

        <div class="control-panel">
            <div class="controls" v-if="showControls">
                <Button label="Создать" class="btn-controls" @click="toggleModal" />
                <Button label="Мои мероприятия" class="btn-controls" />
            </div>

            <div class="panel-nav">
                <div class="dropdown-btn" @click="toggleDropdown">
                <img src="@assets/avatar.svg" class="avatar" alt="avatar">
                <p><font-awesome-icon icon="fa-solid fa-angle-down" /></p>
                </div>

                <div class="dropdown-list" v-if="isDropdownOpen">
                    <ul>
                        <li>
                            <a href="/settings">Настройки</a>
                        </li>
                        <li @click="handleClickExit">
                            <a href="/exit">Выход</a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </header>

    <Modal :isVisible="isModalOpen" title="Создать мероприятие" @close="toggleModal">
        <form @submit.prevent="handleSubmit" class="create-event-modal">
            <label for="event-type">Тип мероприятия:
                <select v-model="eventType" id="event-type" required>
                    <option value="t1">Индивидуальная встреча</option>
                    <option value="t2">Собрание круглого стола</option>
                    <option value="t3">Отраслевое мероприятие</option>
                    <option value="t4">Публичное мероприятие</option>
                </select>
            </label>

            <label for="event-name">Название:
                <input type="text" v-model="eventName" id="event-name" required />
            </label>

            <label for="event-location">Место:
                <select v-model="eventLocation" id="event-location" required>
                    <option value="" disabled>Выберите место</option>
                    <option v-for="venue in venues" :key="venue" :value="venue">
                        {{ venue }}
                    </option>
                </select>
            </label>

            <label for="start-time">Время начала:
                <input type="datetime-local" v-model="startTime" id="start-time" required />
            </label>

            <label for="end-time">Время конца:
                <input type="datetime-local" v-model="endTime" id="end-time" />
            </label>

            <label for="min-attendees" v-if="!(eventType === 't1')">Мин. кол-во посещаемых:
                <input type="number" v-model="minAttendees" id="min-attendees" min="5" />
            </label>

            <label for="invited-attendees" v-if="eventType === 't1'">Приглашенные (email):
                <input type="text" v-model="invitedAttendeesInput" id="invited-attendees" placeholder="email1@example.com, email2@example.com" />
            </label>

            <label v-if="eventType === 't2'" for="roundtable-id">Круглый стол (ID):
                <select v-model="roundtableId" id="roundtable-id">
                    <option v-for="n in 6" :key="n" :value="n">{{ n }}</option>
                </select>
            </label>

            <div v-if="eventType === 't3' || eventType === 't4'">
                <h3>Предпочтения:</h3>
                <div class="preferences-wrap">
                    <div class="preferences">
                        <toggleButton 
                            v-for="(preference, index) in preferences" 
                            :key="index" 
                            :label="preference"
                            @toggle="togglePreference(preference)"
                            :active="selectedPreferences.includes(preference)"
                            class="pref-button"
                            type="button"
                        />
                    </div>
                </div>
            </div>

            <button type="submit" id="modal-btn">Создать</button>
        </form>
    </Modal>
</template>

<style scoped>
    header {
        height: 60px;
        display: flex;
        justify-content: space-between;
        align-items: center;
        gap: 20px;
        padding: 0 50px;
        background-color: #0F1C2E;
    }

    .dropdown-btn {
        display: flex;
        justify-content: center;
        align-items: center;
        padding: 10px;
        gap: 10px;
        cursor: pointer;
        transition: all 0.3s;
    }

    .dropdown-btn:hover {
        background-color: #15263f;
    }

    .dropdown-list {
        position: absolute;
        top: 65px;
        right: 50px;
        background-color: #0F1C2E;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        border-radius: 0px;
    }

    .dropdown-list ul {
        list-style: none;
        margin: 0;
        padding: 10px 0;
        cursor: pointer;
    }

    .dropdown-list li {
        padding: 10px 10px;
    }

    .dropdown-list li a {
        padding: 20px;
        color: #fff;
        text-decoration: none;
    }

    .dropdown-list li:hover {
        background-color: #f0f0f0;
    }

    .dropdown-list li:hover a {
        color: #333;
    }

    .avatar {
        width: 40px;
        border-radius: 50px;
    }

    .control-panel {
        display: flex;
        justify-content: center;
        align-items: center;
        gap: 30px;
    }

    .controls {
        display: flex;
        gap: 25px;
    }

    .btn-controls {
        font-weight: 500;
        background-color: #f0f0f0;
        transition: all 0.3s;
    }

    .btn-controls:hover {
        background-color: #cccccc;
    }

    .btn-controls:active {
        box-shadow: 0px 6px 13px 1px rgba(34, 60, 80, 0.2);
        background-color: #b9b9b9;
    }

    .create-event-modal{
        width: 100%;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        gap: 15px;
    }

    .create-event-modal label {
        width: 100%;
        display: flex;
        justify-content: space-between;
        flex-direction: column;
        gap: 5px;
    }

    .create-event-modal label[for="min-attendees"] {
        flex-direction: row;
        align-items: center;
    }

    .create-event-modal input, select {
        padding: 10px;
        border-radius: 8px;
        border: none;
        background-color: #f0f0f0;
        color: #333;
    }

    #min-attendees {
        width: 40%;
    }

    #modal-btn {
        border: none;
        border-radius: 8px;
        margin-top: 12px;
        padding: 10px 18px;
        background-color: #15263f;
    }
    .preferences {
        margin-top: 15px;
        position: relative; 
        max-height: 150px;
        overflow-y: auto; 
        padding: 15px;
        border-radius: 8px;
        scrollbar-width: thin;
        scrollbar-color: #15263f2f transparent;
    }
    .preferences:hover {
        scrollbar-color: #0f1c2fb9 transparent;
    }

    .preferences-wrap {
        position: relative;
        max-height: 150px;
    }
    .preferences-wrap::before,
    .preferences-wrap::after {
        z-index: 2;
        content: '';
        position: absolute;
        left: 0;
        right: 0;
        height: 12px;
        pointer-events: none;
    }

    /* Top gradient */
    .preferences-wrap::before {
        top: 0;
        background: linear-gradient(to bottom, rgb(255, 255, 255), rgba(255, 255, 255, 0));
    }

    /* Bottom gradient */
    .preferences-wrap::after {
        bottom: 0;
        background: linear-gradient(to top, rgb(252, 252, 252), rgba(255, 255, 255, 0));
    }
    .pref-button {
        margin: 5px;
        vertical-align: middle;
    }


</style>
