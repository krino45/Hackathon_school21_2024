<script setup>

import CustomHeader from '@src/components/Header.vue'
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
        localStorage.setItem('user', JSON.stringify({userId: "66ed6be90840463b66a487fa", email: "hello13224@yandex.ru", password: "some_password" }));
        let user = localStorage.getItem('user');
        try {
            const response = await axios.get(import.meta.env.VITE_NODE_API_HOST + '/api/events', {
                params:{
                    userId: user},})
                    let data = JSON.parse(response.data)
                    for(let i = 0; i < data.User.Events.length; i++)
                    {
                        this.eventsId.push(data.User.Events[i])
                    }
        }
        catch(error) {
            console.error('Error fetching preferences:', error);
        }

        
        const event0 = {}
        event0.start_time = "Сентября 22 13:00"
        event0.end_time = "Сентября 22 15:00"
        
        const event1 = {}
        event1.start_time = "Сентября 23 12:00"
        event1.end_time = "Сентября 23 14:20"

        const event2 = {}
        event2.start_time = "Сентября 24 12:00"
        event2.end_time = "Сентября 24 14:30"

        const event3 = {}
        event3.start_time = "Сентября 22 14:00"
        event3.end_time = "Сентября 22 17:00"

        const event4 = {}
        event4.start_time = "Сентября 22 14:00"
        event4.end_time = "Сентября 22 17:00"

        const event5 = {}
        event5.start_time = "Сентября 22 14:00"
        event5.end_time = "Сентября 22 17:00"

        const event6 = {}
        event6.start_time = "Сентября 22 14:00"
        event6.end_time = "Сентября 22 17:00"

        const event7 = {}
        event7.start_time = "Сентября 22 14:00"
        event7.end_time = "Сентября 22 17:00"

        const event8 = {}
        event8.start_time = "Сентября 22 14:00"
        event8.end_time = "Сентября 22 17:00"

        const event9 = {}
        event9.start_time = "Сентября 22 14:00"
        event9.end_time = "Сентября 22 17:00"

        const event10 = {}
        event10.start_time = "Сентября 22 14:00"
        event10.end_time = "Сентября 22 17:00"
        
        const events = [
            event0, 
            event1,
            event2,
            event3,
            event4,
            event5,
            event6,
            event7,
            event8,
            event9,
            event10
        ]

        console.log(events)

        for(let i = 0; i < events.length; i++)
        {
            let time = events[i].start_time.split(' ');

            if(time[1] + ' ' + time[0] == this.date)
            {
                this.todayTime.push(time[2] + " - " + events[i].end_time.split(' ')[2])
            }
            else if(time[1] + ' ' + time[0] == this.nextDate)
            {
                this.tomorrowTime.push(time[2] + " - " + events[i].end_time.split(' ')[2])
            }
        }


        // try{
        //     const response = await axios.get(import.meta.env.VITE_NODE_API_HOST + '/api/getEvent', {
        //         params:{
        //             id: this.eventsId[0]
        //         }
        //     })
        //     console.log(this.eventsId[0])
        //     console.log(response)
        //     // let data = JSON.parse(response.data)
        //     // console.log(data)
        //     for(let i = 0; i < data.User.Events.length; i++)
        //         {
        //             this.eventsId.push(data.User.Events[i])
        //         }

        // }
        // catch(error)
        // {

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

<CustomHeader/>

    <div class="calendar">
        <div class="date">
          <p>Сегодня</p>
          <p style="min-height: 30px;">{{ day }} {{ date }}</p>
          <p style="min-height: 35px;" v-for="(id, index) in todayTime"> {{ id }}</p>
          
        </div>  
        <div class="date">
            <p>Завтра</p>
            <p style="min-height: 30px;">{{ next }} {{ nextDate }}</p>
            <p style="min-height: 35px;" v-for="(id, index) in tomorrowTime"> {{ id }}</p>

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
        max-height: 500px;
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
        min-height: 160px;
        display: flex;
        flex-direction: column;
        justify-content:flex-start;
        background-color: #0F1C2E;
        color: #fafbf3;
        border-radius: 10%;
        margin-top: 2%;
        text-align: center;
    }
</style>