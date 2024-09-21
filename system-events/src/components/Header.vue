<script setup>
    import { ref, onMounted, onUnmounted, computed } from 'vue';
    import { useRoute } from 'vue-router';
    import Button from '@src/components/Button.vue';
    import Modal from '@src/components/Modal.vue';

    const isDropdownOpen = ref(false);
    const isModalOpen = ref(false);
    const eventType = ref('');
    const eventName = ref('');
    const eventLocation = ref('');
    const startTime = ref('');
    const endTime = ref('');
    const minAttendees = ref('');

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
    }

    const handleSubmit = () => {
        console.log({
            eventType: eventType.value,
            eventName: eventName.value,
            eventLocation: eventLocation.value,
            startTime: startTime.value,
            endTime: endTime.value,
            minAttendees: minAttendees.value,
        });

        toggleModal();
    };

    onMounted(() => {
        document.addEventListener('click', handleClickOutside);
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
                        <a href="/settings">
                            Настройки
                        </a>
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
            <label for="event-type">Тип ивента:
                <select v-model="eventType" id="event-type" required>
                    <option value="t1">Т1</option>
                    <option value="t2">Т2</option>
                    <option value="t3">Т3</option>
                    <option value="t4">Т4</option>
                </select>
            </label>
            

            <label for="event-name">Название:
                <input type="text" v-model="eventName" id="event-name" required />
            </label>
            

            <label for="event-location">Место:
                <input type="text" v-model="eventLocation" id="event-location" required />
            </label>
            

            <label for="start-time">Время начала:
                <input type="datetime-local" v-model="startTime" id="start-time" required />
            </label>
            

            <label for="end-time">Время конца:
                <input type="datetime-local" v-model="endTime" id="end-time" />
            </label>
            

            <label for="min-attendees">Мин. кол-во посещаемых:
                <input type="number" v-model="minAttendees" id="min-attendees" min="5" />
            </label>

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

</style>