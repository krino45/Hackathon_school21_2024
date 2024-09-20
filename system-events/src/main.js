import { createApp } from 'vue'
import './style.css'
import App from './App.vue'

import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import * as brands from '@fortawesome/free-brands-svg-icons'
import * as regular from '@fortawesome/free-regular-svg-icons'
import * as solid from '@fortawesome/free-solid-svg-icons'

library.add(...Object.values(brands).filter(icon => icon.iconName));
library.add(...Object.values(regular).filter(icon => icon.iconName));
library.add(...Object.values(solid).filter(icon => icon.iconName));

const app = createApp(App);

app.component('font-awesome-icon', FontAwesomeIcon);

app.mount('#app');
