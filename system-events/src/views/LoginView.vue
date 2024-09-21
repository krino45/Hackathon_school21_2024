<script setup>
    import { ref } from 'vue'

    const isLogin = ref(true)
    const email = ref('')
    const password = ref('')
    const firstName = ref('')
    const lastName = ref('')
    const surname = ref('')

    const showLogin = () => {
        isLogin.value = true
    };

    const showRegister = () => {
        isLogin.value = false
    };

    const handleSubmit = async (event) => {
        event.preventDefault()

        let url = `${import.meta.env.VITE_NODE_API_HOST}/login`;
        let payload = isLogin.value 
        ? { email: email.value, password: password.value }
        : { firstName: firstName.value, lastName: lastName.value, surname: surname.value, email: email.value, password: password.value }

        try {
            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(payload),
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json()
            console.log('Response Data:', data)
        } catch (error) {
                console.error('Error:', error)
        }
    };

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
                <form @submit.prevent="handleSubmit" class="fm-block">
                    <div class="field-form">
                        <label for="login-email">Почта</label>
                        <input type="text" id="login-email" v-model="email">
                    </div>

                    <div class="field-form">
                        <label for="login-password">Пароль</label>
                        <input type="password" id="login-password" v-model="password">
                    </div>
                    
                    <input type="submit" id="sub-block" value="Вход">
                </form>
            </div>
            <div class="reg-block" v-else>
                <h1>Регистрация</h1>
                <form @submit.prevent="handleSubmit" class="fm-block">
                    <div class="field-form">
                        <label for="first-name">Имя</label>
                        <input type="text" id="first-name" v-model="firstName" required>
                    </div>
                    <div class="field-form">
                        <label for="last-name">Фамилия</label>
                        <input type="text" id="last-name" v-model="lastName" required>
                    </div>
                    <div class="field-form">
                        <label for="last-name">Отчество</label>
                        <input type="text"  id="surname" v-model="surname" required>
                    </div>
                    <div class="field-form">
                        <label for="register-email">Почта</label>
                        <input type="email" id="register-email" v-model="email" required>
                    </div>
                    <div class="field-form">
                        <label for="register-password">Пароль</label>
                        <input type="password" id="register-password" v-model="password" required>
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