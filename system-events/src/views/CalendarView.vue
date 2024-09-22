<script setup>
    import MyHeader from '@src/components/Header.vue'
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
        this.day = this.getDayOfWeek(this.shift);
        this.next = this.getDayOfWeek(this.shift + 1);
        this.date = this.getDay(this.shift);
        this.nextDate = this.getDay(this.shift + 1);
        let data;
        let events = []
        try {
            
            let userid = localStorage.getItem('user');
            let userObj = {}
            userObj.userId = userid
            const response = await axios.get(import.meta.env.VITE_NODE_API_HOST + '/api/get_user', {
                params:{
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
    <MyHeader/>
    <div class="calendar">
        <div class="date">
               <p>Сегодня</p>
                <p>{{ day }} {{ date }}</p>
                <div class="wrapper">
                <ul class="table" v-for="(id, index) in todayTime">
                    {{ id }}
                </ul>
                </div>
        </div>
        <div class="date">
               <p>завтра</p>
                <p>{{ next }} {{ nextDate }}</p>
                <div class="wrapper">
                <ul class="table" v-for="(id, index) in tomorrowTime">
                    {{ id }}
                </ul>
                </div>
        </div>
    </div>
</template>

<style scoped>

    .calendar
    {
        display: flex;
        position: relative;
        flex-direction: column;
        justify-content: center;
        margin-top: 7%;
        align-items: center;
        top: 0px;
        width: 80%;
        left: 10%;
    }
    
    .date
    {
        box-sizing: border-box;
        padding-top: 1%;
        padding-bottom: 1%;
        min-width: 50%;
        min-height: 150px;
        display: inline-block;
        background-color: #68148f;
        color: #fff;
        border-radius: 30px;
        margin-top: 2%;
        text-align: center;
        justify-self: center;
    }

    .table
    {
        display: flex;
        flex-direction: column;
        margin-top: 3%;
        align-items: flex-start;
        flex-wrap: wrap;
        background-color: #68148f;
    }
    
    .wrapper
    {
        display: flex;
        flex-wrap: wrap;
        align-items: center;
        justify-content: space-evenly;
    }

</style>