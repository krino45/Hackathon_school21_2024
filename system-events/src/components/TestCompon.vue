<script setup>
    import { ref, onMounted } from 'vue'
    import axios from 'axios'

    const message = ref('')

    const fetchData = async () => {
    try {
            console.log(import.meta.env.VITE_NODE_API_HOST)
            const response = await axios.get(import.meta.env.VITE_NODE_API_HOST + '/message');
            if (response.status === 200) {
                message.value = response.data.message;
                console.log(response);
            } else {
                console.error('Ошибка на сервере:', response.status);
            }
        } catch (error) {
            console.error('Ошибка при получении данных:', error);
        }
    };

    onMounted(() => {
        fetchData()
    })

</script>

<template>
    <h1>{{message}}</h1>
</template>