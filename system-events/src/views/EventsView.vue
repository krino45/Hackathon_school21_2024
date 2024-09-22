<script setup>
import HeaderCustom from '@src/components/Header.vue'
import { ref, onMounted, onBeforeMount } from 'vue';
import axios from 'axios';
const checkUserId = async () => {
    const storedUserId = localStorage.getItem('user');
    if (storedUserId && typeof storedUserId === 'string') {
        let response = await axios.get(import.meta.env.VITE_NODE_API_HOST + "/api/get_user/safe", {
            params: { userJsonId: JSON.stringify({ userId: storedUserId }) },
        });
        console.log(response.data);

        if (!JSON.parse(response.data).Success)
        {
            userId.value = null;
            localStorage.setItem('user', null);
            window.location.href = "/login";
        }

    }
    else
    {
        window.location.href = "/login";
    }
};

onBeforeMount(() => {
    checkUserId();
});
</script>

<template>
    <HeaderCustom />  

    <div class="all-events">

    </div>

</template>

<style scoped>
    
</style>