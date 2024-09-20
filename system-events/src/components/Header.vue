<script setup>
    //let activenk = "";
</script>

<template>

    <header>
        <div class="logo">
            <img src="../assets/logo.svg" alt="logo"/>
        </div>
        <div class="credits">
            <div class="credit">
                <a href="Адрес ссылки" style="color: inherit;">
                    <font-awesome-icon :icon="['fab', 'vk']" size="2x" />
                </a> 
            </div>
            <div class="credit">
                <a href="Адрес ссылки" style="color: inherit; ">
                    <font-awesome-icon :icon="['fab', 'telegram']" size="2x" />
                </a>
            </div>
            <div class="credit">
                <a href="Адрес ссылки" style="color: inherit; ">
                    <font-awesome-icon :icon="['fab', 'whatsapp']" size="2x" />
                </a>
            </div>
            <div class="credit">
                <a href="Адрес ссылки" style="color: inherit; ">
                    <font-awesome-icon :icon="['fas', 'location-dot']" size="2x" />
                </a>
            </div>
        </div>
        <div class="menu">
            <button style="color: #a064b9;" onclick="showMenu()">Menu</button>
            <div class="content">
                <button class="menuButton">settings</button>
                <button class="menuButton">exit</button>
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
    }

    .menu
    {
        width:fit-content;
        position: relative;
        top: 20%;
        display: inline;
    }

    .content
    {
        background-color: rgba(0, 0, 0, 0.25);
        width: 100%;
        position: absolute;
        display: flex;
        flex-direction: column;
    }
    .menuButton
    {
        background-color: transparent;
        color:aqua;
        border: none;
    }

    .credits
    {
        position: absolute;
        width: 10%;
        top:30%;
        left: 17%;
        display: inline-flex;
        flex-direction: row;
        justify-content: space-between;
        color: white;
    }

</style>