<script setup>
import HelloWorld from './components/HelloWorld.vue'
import TestComp from './components/TestCompon.vue'
import Header from './components/Header.vue'
</script>

<template>

  <Header/>

  <div>
    <a href="https://vitejs.dev" target="_blank">
      <img src="/vite.svg" class="logo" alt="Vite logo" />
    </a>
    <a href="https://vuejs.org/" target="_blank">
      <img src="./assets/vue.svg" class="logo vue" alt="Vue logo" />
    </a>
  </div>
  
  <TestComp/>
</template>

<style scoped>

</style>