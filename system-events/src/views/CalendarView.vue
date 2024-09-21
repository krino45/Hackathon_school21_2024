<script setup>
    import Header from '@src/components/Header.vue'
</script>

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
        todayTime: [

        ],
        tomorrowTime: [

        ],
        eventsId: [
        ],
        todayEvents: [
            'Ничего не запланировано'
        ],
        tomorrowEvents: [
            'Ничего не запланировано'
        ],
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
        this.next = this.getDayOfWeek(this.shift + 1);
        this.date = this.getDay(this.shift);
        this.nextDate = this.getDay(this.shift + 1);
        let data;
        let events = []
        localStorage.setItem('user', JSON.stringify({userId: "66ed6be90840463b66a487fa", email: "hello13224@yandex.ru", password: "some_password" }));
        let user = localStorage.getItem('user');
        try {
            const response = await axios.get(import.meta.env.VITE_NODE_API_HOST + '/api/get_user', {
                params:{
                    userJsonId: user},})    
                    data = JSON.parse(response.data)
                    for(let i = 0; i < data.User.Events.length; i++)
                    {
                        events.push(data.User.Events[i])
                    }
        }
        catch(error) {
            console.error('Error fetching preferences:', error);
        }

        try{
            let obj = {}
            obj.id = events[1]
            console.log(obj)
            const response = await axios.get(import.meta.env.VITE_NODE_API_HOST + '/api/get_event', {
                params:{
                    json: JSON.stringify(obj)
                }
            })
            console.log(this.eventsId[0])
            let data = JSON.parse(response.data)
            console.log(data)
            for(let i = 0; i < data.User.Events.length; i++)
                {
                    this.eventsId.push(data.User.Events[i])
                }

        }
        catch(error)
        {

        }


        // for(let i = 0; i < events.length; i++)
        // {
        //     let time = events[i].start_time.split(' ');

        //     if(time[1] + ' ' + time[0] == this.date)
        //     {
        //         this.todayTime.push(time[2] + " - " + events[i].end_time.split(' ')[2])
        //     }
        //     else if(time[1] + ' ' + time[0] == this.nextDate)
        //     {
        //         this.tomorrowTime.push(time[2] + " - " + events[i].end_time.split(' ')[2])
        //     }
        // }

        for(let i = 0; i < this.eventsId.length; i++)
        {
            this.todayEvents[i] = this.eventsId[i];
        }

    },

        getDay(shift){
            let d = new Date(new Date().getTime() + 24 * 60 * 60 * 1000 * shift).getDate()  + this.months[new Date(new Date().getTime() + 24 * 60 * 60 * 1000 * shift).getMonth()]
            return d
        }
    }
}
</script>

<template>
    <Header/>
    <div class="calendar">
        <div class="date">
          <p color="#ffdfdf">Сегодня</p>
          <p>{{ day }} {{ date }}</p>
          <p v-for="(id, index) in todayTime"> {{ id }}</p>
          
        </div>  
        <div class="date">
            <p>Завтра</p>
            <p>{{ next }} {{ nextDate }}</p>
            <p v-for="(id, index) in tomorrowTime"> {{ id }}</p>

        </div>
    </div>
</template>

<style scoped>

    .calendar
    {
        display: flex;
        position: relative;
        flex-direction: column;
        flex-wrap: wrap;
        justify-content: flex-start;
        min-height: 350px;
        max-height: 600px;
        margin-top: 7%;
        align-items: center;
        top: 0px;
        width: 80%;
        left: 10%;
    }

    .date
    {
        width: 35%;
        height:fit-content;
        min-height: 200px;
        display: flex;
        flex-direction: column;
        background-color: #15263f;
        color: #fff;
        border-radius: 10%;
        margin-top: 2%;
        text-align: center;
    }


</style>