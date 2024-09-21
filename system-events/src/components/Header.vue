<script setup>
</script>

<template>
    <header>
        <div class="logo">
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

</style>