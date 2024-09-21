<script setup>
import { ref, onMounted } from 'vue'

const shift = 0
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
  day.value = getDayOfWeek(shift)
  next.value = getDayOfWeek(shift + 1)
  date.value = getDay(shift)
  nextDate.value = getDay(shift + 1)
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
    <div class="calendar">
        <div class="date">
          <p>Сегодня</p>
          <p>{{ day }} {{ date }}</p>
          
        </div>  
        <div class="date">
            <p>Завтра</p>
            <p>{{ next }} {{ nextDate }}</p>
        </div>
    </div>
</template>

<style scoped>

    .calendar
    {
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        min-height: 350px;
        height: fit-content;
        width: 120%;
    }

    .date
    {
        width: 100%;
        height:fit-content;
        min-height: 40%;
        display: flex;
        flex-direction: column;
        background-color: #a940b9;
        border-radius: 10%;
    }


</style>