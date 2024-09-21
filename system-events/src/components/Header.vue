<script setup>
    import { ref, onMounted, onUnmounted } from 'vue';

    const isDropdownOpen = ref(false);

    const toggleDropdown = () => {
        isDropdownOpen.value = !isDropdownOpen.value;
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
    </header>
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
</style>