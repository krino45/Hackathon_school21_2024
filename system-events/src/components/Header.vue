<script setup>
    let activenk = "";
</script>

<template>

    <header>
        <div class="logo">
            <img src="../assets/logo.svg" alt="logo"/>
        </div>
        <div class="menu">
            <button style="color: #a064b9;" onclick="showMenu()">Menu</button>
            <div id="buttons" class="content">
                <button class="menuButton">a</button>
                <button class="menuButton">b</button>
                <button class="menuButton">c</button>
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
        right: 100%;
        top: 20%;
        right:-70%;
        display: inline;
    }

    .content
    {
        background-color: rgba(0, 0, 0, 0.25);
        width: 100%;
        position: absolute;
        right: 0;
        display: none;
        flex-direction: column;
    }
    .menuButton
    {
        background-color: transparent;
        color:aqua;
        border: none;
        border-radius: 10%;
    }

</style>