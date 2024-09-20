import { defineConfig } from 'vite'
import { fileURLToPath, URL } from "url" 
import vue from '@vitejs/plugin-vue'


// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  server: {
    proxy: {
      '/api': {
        target: 'http://localhost:5258',
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/api/, ''),
      },
    }
  },
  resolve: {
    alias: [
      {find: '@src', replacement: fileURLToPath(new URL('./src', import.meta.url))},
      {find: '@assets', replacement: fileURLToPath(new URL('./src/assets', import.meta.url))}
    ]
  }
})
