<script setup>
import { ref, onMounted, onBeforeMount } from 'vue';
import axios from 'axios';

const isLogin = ref(true);
const login = ref('');
const password = ref('');
const FirstName = ref('');
const LastName = ref('');
const MiddleName = ref('');
const email = ref('');
const PasswordHash = ref('');
const responseMessage = ref(''); 

const showLogin = () => {
    isLogin.value = true;
};

const showRegister = () => {
    isLogin.value = false;
};

const handleLogin = async () => {
    const credentials = {
        login: login.value,
        password: password.value
    };

    try {
        const response = await fetch(import.meta.env.VITE_NODE_API_HOST + '/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(credentials)
        });

        if (!response.ok) {
            throw new Error('Login failed');
        }

        const data = await response.json();
        console.log('Login successful:', data);
        responseMessage.value = '';
        localStorage.setItem('user', data.message);
        window.location.href = "/events";

    } catch (error) {
        console.error('Error:', error);
        responseMessage.value = error.message;
    }
};

const handleRegistration = async () => {
    const registrationData = {
        FirstName: FirstName.value,
        LastName: LastName.value,
        MiddleName: MiddleName.value,
        email: email.value,
        PasswordHash: PasswordHash.value
    };

    try {
        const response = await fetch(import.meta.env.VITE_NODE_API_HOST + '/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(registrationData)
        });

        if (!response.ok) {
            throw new Error('Registration failed');
        }

        const data = await response.json();
        console.log('Registration successful:', data);
        responseMessage.value = '';
        localStorage.setItem('user', data.message);
        window.location.href = "/events";

    } catch (error) {
        console.error('Error:', error);
        responseMessage.value = error.message;
    }
};

const userId = ref(null);
const checkUserId = async () => {
    const storedUserId = localStorage.getItem('user');
    if (storedUserId && typeof storedUserId === 'string') {
        let response = await axios.get(import.meta.env.VITE_NODE_API_HOST + "/api/get_user", {
            params: { userJsonId: JSON.stringify({ userId: storedUserId }) },
        });
        console.log(response.data);

        if (JSON.parse(response.data).Success)
        {
            userId.value = storedUserId;
            console.log("userId: " + userId.value);
            window.location.href = "/events";
            console.log(buttonLink);
        }

    }
};

onBeforeMount(() => {
    checkUserId();
});
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
                <form @submit.prevent="handleLogin" class="fm-block">
                    <div :class="['err-msg', responseMessage ? 'visible' : '']">
                        <p>{{ responseMessage }}</p>
                    </div>


                    <div class="field-form">
                        <label for="">Почта</label>
                        <input type="text" v-model="login" required>
                    </div>

                    <div class="field-form">
                        <label for="">Пароль</label>
                        <input type="password" v-model="password" required>
                    </div>
                    
                    <input type="submit" id="sub-block" value="Вход">
                </form>
            </div>
            <div class="reg-block" v-else>
                <h1>Регистрация</h1>
                <form @submit.prevent="handleRegistration" class="fm-block">
                    <div :class="['err-msg', responseMessage ? 'visible' : '']">
                        <p>{{ responseMessage }}</p>
                    </div>
                    <div class="field-form">
                        <label for="">Фамилия</label>
                        <input type="text" v-model="LastName" required>
                    </div>
                    <div class="field-form">
                        <label for="">Имя</label>
                        <input type="text" v-model="FirstName" required>
                    </div>
                    <div class="field-form">
                        <label for="">Отчество</label>
                        <input type="text" v-model="MiddleName" required>
                    </div>
                    <div class="field-form">
                        <label for="">Почта</label>
                        <input type="email" v-model="email" required>
                    </div>
                    <div class="field-form">
                        <label for="">Пароль</label>
                        <input type="password" v-model="PasswordHash" required>
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
        font-family: "Aboreto", system-ui;
        font-style: normal;
        color: #fff;
    }

    .auth-block h1 {
        padding-bottom: 15px;
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
        height: 45vh;

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
        height: 60vh;

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

    .err-msg {
        display: flex;
        justify-content: center;
        align-items: center;
        flex-direction: column;
        margin-top: 0;
        margin-bottom: 15px;
        color: red;
        font-size: 20px;
        font-family: 'Courier New', Courier, monospace;
        height: 30px;
        opacity: 0%;
        transition: opacity 0.5s!important; 
    }

    .err-msg.visible {
        opacity: 100%; 
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