<script setup>
    import MyHeader from '@src/components/Header.vue'
    import MyBasement from '@src/components/Basement.vue'
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
        this.day = this.getDayOfWeek(this.shift +6);
        this.next = this.getDayOfWeek(this.shift);
        this.date = this.getDay(this.shift);
        this.nextDate = this.getDay(this.shift + 1);
        let data;
        let events = [];
        try {
            let userid = localStorage.getItem('user');
            let userObj = {}
            userObj.userId = userid
            const response = await axios.get(import.meta.env.VITE_NODE_API_HOST + '/api/get_user', {
                params: {
                    userJsonId: JSON.stringify(userObj)},}) 
                    data = JSON.parse(response.data)
                    console.log(data)
                    for(let i = 0; i < data.User.Events.length; i++)
                    {
                        events.push(data.User.Events[i])
                    }
        }
        catch(error) {
            console.error('Error fetching user:', error);
        }

        try{
            for(let i = 0; i < events.length; i++)
            {
            let obj = {}
            obj.id = events[i]
            const response = await axios.get(import.meta.env.VITE_NODE_API_HOST + '/api/get_event', {
                params:{
                    json: JSON.stringify(obj)
                }
            })
            let data = JSON.parse(response.data)
            let day = new Date(data.data.StartTime).getUTCDate()
            let month = this.months[Number(new Date(data.data.StartTime).getUTCMonth())]
            let hour = new Date(data.data.StartTime).getUTCHours()
            let minute = new Date(data.data.StartTime).getUTCMinutes()
            let end_hour = new Date(data.data.EndTime).getUTCHours()
            let end_minute = new Date(data.data.EndTime).getUTCMinutes()

            if(day + month == this.date)
            {
                this.todayTime[this.todayTime.length] = hour + ':' + minute + ' - ' + end_hour + ':' + end_minute + ' ' + '\"' + data.data.Name + '\" ' + ' площадка:\n' + data.data.Venue
            }
            else if (day + month == this.nextDate)
            {
                this.tomorrowTime[this.todayTime.length] = hour + ':' + minute + ' - ' + end_hour + ':' + end_minute + '\" ' + data.data.Name + '\" ' + ' площадка:\n' + data.data.Venue
            }
            }
            if(this.todayTime.length == 0)
            {
                this.todayTime.push('Событий нет')
            }
            if(this.tomorrowTime.length == 0)
            {
                this.tomorrowTime.push('Событий нет')
            }
            this.todayTime.sort()
            this.tomorrowTime.sort()
        }
        catch(error)
        {
            console.error('Error fetching event:', error);
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
    <MyHeader />
    <div class="calendar">
        <div class="date">
            <div class="header">
                <p class="title">Сегодня</p>
                <p class="subtitle">{{ day }} {{ date }}</p>
            </div>
            <div class="wrapper">
                <ul class="event-list">
                    <li v-for="(event, index) in todayTime" :key="index" class="event-item">
                        {{ event }}
                    </li>
                </ul>
            </div>
        </div>

        <div class="date">
            <div class="header">
                <p class="title">Завтра</p>
                <p class="subtitle">{{ next }} {{ nextDate }}</p>
            </div>
            <div class="wrapper">
                <ul class="event-list">
                    <li v-for="(event, index) in tomorrowTime" :key="index" class="event-item">
                        {{ event }}
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <MyBasement />
</template>


<style scoped>
    .calendar {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        margin-top: 4rem;
        width: 100%;
    }

    .date {
        width: 80%;
        background-color: #fff;
        border-radius: 12px;
        box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
        margin-bottom: 2rem;
        padding: 1.5rem;
    }

    .header {
        text-align: center;
        margin-bottom: 1.5rem;
    }

    .title {
        font-size: 1.5rem;
        font-weight: bold;
        color: #3d155f;
    }

    .subtitle {
        font-size: 1.2rem;
        color: #8e44ad;
    }

    .wrapper {
        max-width: 100%;
        margin: 0 auto;
        text-align: left;
    }

    .event-list {
        list-style-type: none;
        padding: 0;
        margin: 0;
    }

    .event-item {
        font-size: 1rem;
        color: #4b0082;
        padding: 0.8rem;
        background-color: #f0e6fa;
        border-left: 4px solid #8e44ad;
        margin-bottom: 0.5rem;
        border-radius: 8px;
        transition: background-color 0.3s ease;
    }

    .event-item:hover {
        background-color: #e0d4f5;
    }
</style>
