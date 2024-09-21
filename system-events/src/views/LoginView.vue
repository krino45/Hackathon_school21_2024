<script setup>
    import { ref } from 'vue';

    const isLogin = ref(true);

    const showLogin = () => {
        isLogin.value = true;
    }

    const showRegister = () => {
        isLogin.value = false;
    }

</script>

<template>
    <div class="auth-block">
        <div class="content">
            <div class="swapper">
                <button class="sw-btn" id="sw-auth" @click="showLogin">Войти</button>
                <button class="sw-btn" id="sw-reg" @click="showRegister">Зарегистрироваться</button>
            </div>

            <div class="login-block" v-if="isLogin">
                <h1>Вход</h1>
                <form action="" class="fm-block">
                    <div class="field-form">
                        <label for="">Почта</label>
                        <input type="text">
                    </div>

                    <div class="field-form">
                        <label for="">Пароль</label>
                        <input type="password">
                    </div>
                    
                    <input type="submit" id="sub-block" value="Вход">
                </form>
            </div>
            <div class="reg-block" v-else>
                <h1>Регистрация</h1>
                <form action="" class="fm-block">
                    <div class="field-form">
                        <label for="">Имя</label>
                        <input type="text" required>
                    </div>
                    <div class="field-form">
                        <label for="">Фамилия</label>
                        <input type="text" required>
                    </div>
                    <div class="field-form">
                        <label for="">Отчество</label>
                        <input type="text" required>
                    </div>
                    <div class="field-form">
                        <label for="">Почта</label>
                        <input type="email" required>
                    </div>
                    <div class="field-form">
                        <label for="">Пароль</label>
                        <input type="password" required>
                    </div>

                    <input type="submit" id="sub-block" value="Зарегистрироваться">
                </form>
            </div>
        </div>
    </div>
</template>

<style scoped>
    .auth-block {
        height: 90vh;
        display: flex;
        justify-content: center;
        align-items: center;
        color: #fff;
    }

    .auth-block h1 {
        padding-bottom: 30px;
    }

    .swapper {
        display: flex;
        justify-content: center;
    }

    .swapper .sw-btn {
        width: 100%;
        border: none;
        padding: 18px;
        border-radius: 0px;
        background-color: #1a1a1a;

        cursor: pointer;
        transition: all 0.3s;
    }

    .swapper .sw-btn:hover {
        border: none;
        outline: none;

        background-color: #141414;
    }

    .swapper .sw-btn:active {
        background-color: #0e0e0e;
    }

    #sw-auth {
        border-top-left-radius: 10px;
    }

    #sw-reg {
        border-top-right-radius: 10px;
    }

    .login-block{
        width: 50vh;
        height: 40vh;

        display: flex;
        flex-direction: column;
        align-items: center;
        padding-top: 40px;
        border-bottom-left-radius: 10px;
        border-bottom-right-radius: 10px;
        
        background-color: #0F1C2E;
    }

    .reg-block {
        width: 50vh;
        height: 50vh;

        display: flex;
        flex-direction: column;
        align-items: center;
        padding-top: 20px;
        padding-bottom: 40px;
        border-bottom-left-radius: 10px;
        border-bottom-right-radius: 10px;
        
        background-color: #0F1C2E;
    }    

    .fm-block {
        display: flex;
        justify-content: center;
        align-items: center;
        flex-direction: column;
        gap: 20px;
    }

    .field-form {
        display: flex;
        justify-content: space-between;
        width: 280px;
        gap: 10px;
    }

    .field-form label {
        padding: 5px;
    }

    .field-form input {
        width: 180px;
        border-radius: 12px;
        border: none;

        padding-left: 10px;
        color:black;
        background-color: #fff;
    }

    #sub-block {    
        height: 40px;
        margin-top: 20px;
        padding: 0 50px 0 50px;
    }

</style>