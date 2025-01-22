import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue'; // Doğru paket yolu
import vueDevTools from 'vite-plugin-vue-devtools'; // Geliştirme araçları

export default defineConfig({
  plugins: [
    vue(),
    vueDevTools() // Geliştirme araçlarını ekliyoruz
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url)), // '@' alias'ını ayarlıyoruz
    },
  },
  server: {
    watch: {
      usePolling: true, // Docker ortamında dosya değişikliklerini algılamak için gerekli
    },
    host: true, // Tüm ağ arabirimlerinde erişim için
    port: 3000, // Geliştirme sunucusu portu
  },
});
