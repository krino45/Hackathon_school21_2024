<script>
import axios from 'axios';
import { ref, onMounted } from 'vue'
export default {

    data(){
        return {
        shift: 0,
        day: '',
        next: '',
        date: '',
        nextDate: '',
        eventId: 0,
        weekDays: [
        ' Воскресенье',
        ' Понедельник',
        ' Вторник',
        ' Среда',
        ' Четверг',
        ' Пятница',
        ' Суббота',
        ],
        months: [
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
        };
    },

    mounted(){
        this.fetch()
    },

methods:{

    
    getDayOfWeek(shift){
        let day = new Date().getDay() + shift
        while (day >= 7){
            day = day - 7
        }
        return this.weekDays[day]
    },
    async fetch()
    {
        this.day = this.getDayOfWeek(this.shift);
        this.next = this.getDayOfWeek(this.shift + 1)
        this.date = this.getDay(this.shift)
        this.nextDate = this.getDay(this.hift + 1)
        try {
        const response = await axios.get(import.meta.env.VITE_NODE_API_HOST + '/api/events'); // URL нашего бэкенда
        this.preferences = JSON.parse(response.data).map(item => item.EventId);
        
        console.info(JSON.parse(response.data).map(item => item.EventId));

      } catch (error) {
        console.error('Error fetching preferences:', error);
      }

    },

        getDay(shift){
            let d = new Date(new Date().getTime() + 24 * 60 * 60 * 1000 * this.shift).getDate()  + this.months[new Date(new Date().getTime() + 24 * 60 * 60 * 1000 * this.shift).getMonth()]
            return d
        }
    }
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
            <p>{{ next }} {{ nextDate }} Ивент:{{ eventId }}</p>

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
        width: 20%;
        height:fit-content;
        min-height: 40%;
        display: flex;
        flex-direction: column;
        background-color: #a940b9;
        border-radius: 10%;
    }


</style>