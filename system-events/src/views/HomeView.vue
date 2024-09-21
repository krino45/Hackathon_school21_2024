<script setup>
    import Button from '@src/components/Button.vue'
</script>

<template>
    <div class="block-start">
        <div class="block-header">
            <div class="logo">
                <img src="@assets/logo.svg" alt="logo">
            </div>

            <div class="social-media">
                <nav>
                    <ul>
                        <li><a href="https://t.me/krona_businessclub"><font-awesome-icon icon="fa-brands fa-telegram" /></a></li>
                        <li><a href="https://vk.com/krona_club_nsk"><font-awesome-icon icon="fa-brands fa-vk" /></a></li>
                        <li><a href="https://wa.me/79139409888"><font-awesome-icon icon="fa-brands fa-whatsapp" /></a></li>
                        <li><a href="https://go.2gis.com/1ehge0"><font-awesome-icon icon="fa-solid fa-map-pin" /></a></li>
                    </ul>
                </nav>
            </div>
        </div>


        <div class="body-start">
            <div class="disc-comp">
                <p>бизнес-клуб</p>
                <h1>krona</h1>
            </div>

            <div class="tagline">
                <p>создай мероприятие</p>
                <p>для единомышленников</p>
            </div>

        </div>

        
        <div class="logout-next">
            <Button label="Приступить ->" class="btn-start" />
        </div>
    </div>
</template>

<style scoped>
    .block-start {
        position: relative;
        display: flex;
        align-items: center;
        flex-direction: column;

        height: 100vh;
        padding: 20px;
        background: url('@assets/bg-main.svg') no-repeat;
        background-size: cover;
        background-position: center;

        font-family: "Aboreto", system-ui;
        font-weight: 400;
        font-style: normal;
    }

    .block-header {
        display: flex;
        justify-content: space-between;
        align-items: center;

        width: calc(100% - 158px);
        height: 60px;
        margin-top: 30px;
    }

    .social-media {
        display: flex;
        justify-content: center;
        align-items: center;
        flex-direction: row;
    }

    .social-media ul {
        display: flex;
        gap: 30px;
        list-style: none;
        font-size: 25px;
    }

    .social-media ul li a {
        color: #fff;
        transition: color 0.3s;
    }

    .social-media ul li a:hover {
        color: rgb(238, 238, 0);
    }

    .body-start {
        width: calc(100% - 158px);
        height: calc(100% - 350px);

        display: flex;
        flex-direction: column;
        align-content: center;
        justify-content: center;
        position: relative;
    }

    .body-start .disc-comp h1 {
        font-size: 90px;
        padding: 0;
        margin: 0;
    }

    .body-start .disc-comp p {
        font-size: 18px;
        font-weight: bold;
        line-height: 10px;
        text-transform: uppercase;
        margin: 0;
        padding: 0 0 0 10px;
    }

    .body-start .tagline {
        position: absolute;
        left: 30%;
        bottom: 15%;
    }

    .body-start .tagline p {
        font-weight: 600;
        line-height: 40px;
        text-transform: uppercase;
        font-size: 40px;
        padding: 0;
        margin: 0;
    }

    .body-start .tagline p:last-child {
        padding-left: 100px;
    }

    .logout-next {
        position: absolute;
        bottom: 10%;
        right: 5%;
    }

    .btn-start {
        width: 250px;
        height: 60px;
        border-radius: 30px;
        font-size: 20px;
        background-color: #FBD495;
        transition: all 0.3s;
    }

    .btn-start:hover {
        background-color: #c4a575;
    }

</style>