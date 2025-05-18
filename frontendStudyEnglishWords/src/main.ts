import './assets/main.css'
import "element-plus/dist/index.css";

import ElementPlus from 'element-plus'
import {createApp} from 'vue'

import Router from "@/router/index";
import App from './App.vue'
import {createPinia} from 'pinia'




const app = createApp(App);

app.use(ElementPlus)
app.use(Router);
app.use(createPinia());

app.mount('#app')

