<script setup>
    import { defineProps } from 'vue';

    const props = defineProps({
        label: {
            type: String,
            required: true
        },
        link: {
            type: String,
            default: null
        },
        action: {
            type: Function,
            default: null
        }
    })

    const handleClick = () => {
        if (props.action) {
            props.action();
        }

        if (props.link) {
            window.location.href = props.link;
        }

    }

</script>

<template>
    <button @click="handleClick">{{ label }}</button>
</template>

<style lang="css" scoped>
    button {
        padding: 10px 20px;
        border: none;
        color: rgb(0, 0, 0);
        cursor: pointer;
        border-radius: 5px;
    }

    button:hover {
        background-color: #0056b3;
    }
</style>