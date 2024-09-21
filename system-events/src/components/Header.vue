<script setup>
<<<<<<< HEAD
=======
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

    onMounted(() => {
        document.addEventListener('click', handleClickOutside);
    });

    onUnmounted(() => {
        document.removeEventListener('click', handleClickOutside);
    });

>>>>>>> 4a0afc74f6ed63de4938b5b00f19b2e98a59c745
</script>

<template>
    <header>
        <div class="logo">
<<<<<<< HEAD
            <a href="../views/HomeView.vue" style="color: inherit; ">
            <img src="../assets/logo.svg" alt="logo"/>
            </a>
        </div>
        <div class="credits">
            <div class="credit">
                <a href="https://t.me/krona_businessclub" style="color: inherit; ">
                    <font-awesome-icon :icon="['fab', 'telegram']" size="2x" />
                </a>
            </div>
            <div class="credit">
                <a href="https://vk.com/krona_club_nsk" style="color: inherit;">
                    <font-awesome-icon :icon="['fab', 'vk']" size="2x" />
                </a> 
            </div>
            <div class="credit">
                <a href="https://wa.me/79139409888" style="color: inherit; ">
                    <font-awesome-icon :icon="['fab', 'whatsapp']" size="2x" />
                </a>
            </div>
            <div class="credit">
                <a href="https://go.2gis.com/4x5en" style="color: inherit; ">
                    <font-awesome-icon :icon="['fas', 'location-dot']" size="2x" />
                </a>
            </div>
        </div>
        <div class="menu">
            <button style="color: #a064b9;" >Menu</button>
            <div class="active" >
                <form class="menuButton" action="../views/LoginView.vue"><button class="menuButton">log in</button></form>
                <form class="menuButton" action=""><button class="menuButton">exit</button></form>
            </div>
        </div>
    </header>

</template>

<style scoped>

    header
    {
        width:100%;
        height: 8%;
        background-color: #602469;
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        position: fixed;
        top: 0;
        left: 0;
    }

    .logo
    {
        position: relative;
        width: 20%;
        top: 30%;
        right: -1%;
    }

    .menu
    {
        width:fit-content;
        position: relative;
        top: 20%;
        right: 3%;
        display: inline;
    }  

    .menu:hover .active {display: block;}

    .active
    {
        background-color: rgba(255, 255, 255, 0.25);
        width: 100%;
        position: absolute;
        display: none;
        flex-direction: column;
    }

    .menuButton
    {
        background-color: transparent;
        color: #a064b9;
        border: none;
    }

    .credits
    {
        position: absolute;
        width: 10%;
        top:30%;
        left: 20%;
        display: inline-flex;
        flex-direction: row;
        justify-content: space-between;
        color: white;
    }   

=======
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
                    <li>
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
>>>>>>> 4a0afc74f6ed63de4938b5b00f19b2e98a59c745
</style>