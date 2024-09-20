<script setup>
import { ref, onMounted } from 'vue'

const day = ref('')
const next = ref('')
const date = ref('')
const nextDate = ref('')
const weekDays = [
  ' Воскресенье',
  ' Понедельник',
  ' Вторник',
  ' Среда',
  ' Четверг',
  ' Пятница',
  ' Суббота',
]
const months = [
    ' Января', 
    ' Февраля',
    ' Марта',
    ' Апреля',
    ' Мая',
    ' Июня',
    ' Июля',
    ' Августа',
    ' Сентября',
    ' Октября',
    ' Ноября',
    ' Декабря'
]

onMounted(() => {
  day.value = getDayOfWeek(0)
  next.value = getDayOfWeek(1)
  date.value = getDay(0)
  nextDate.value = getDay(1)
})

const getDayOfWeek = (shift) => {
    let day = new Date().getDay() + shift
    while (day >= 7){
        day = day - 7
    }
    return weekDays[day]
}

const getDay = (shift) => {
    let d = new Date(new Date().getTime() + 24 * 60 * 60 * 1000 * shift).getDate()  + months[new Date(new Date().getTime() + 24 * 60 * 60 * 1000 * shift).getMonth()]
    return d
}

</script>

<template>
  <div>
    <div id="np-container">
        <div class="date">
          <p>Today is {{ day }} {{ date }}</p>
        </div>  
        <div class="date">
            <p>Tomorrow is {{ next }} {{ nextDate }}</p>
        </div>
    </div>
  </div>
</template>

<style scoped>
#np-container {
  padding: 10px;
}
</style>