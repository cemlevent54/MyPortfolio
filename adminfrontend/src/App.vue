<script setup>
import { ref, watch, onMounted, nextTick } from 'vue';
import { useRouter, useRoute } from "vue-router";
import "./ui-css/dashboard.css";

const router = useRouter();
const route = useRoute();

// Giriş bilgilerini kontrol etmek için reaktif değişkenler
const isLoggedIn = ref(false);
const username = ref('');

// LocalStorage'dan giriş bilgilerini güncelleyen fonksiyon
const updateAuthStatus = () => {
  isLoggedIn.value = localStorage.getItem('isLoggedIn') === 'true';
  username.value = localStorage.getItem('username') || '';
};

// Bileşen yüklendiğinde giriş durumunu güncelle
onMounted(() => {
  updateAuthStatus();
  
  // window.location.reload();
  nextTick(() => {
    if (isLoggedIn.value) {
      document.getElementById("sidebarMenu")?.classList.add("show");
    }
  });
});

// LocalStorage değişikliklerini izleme
watch(() => localStorage.getItem('isLoggedIn'), updateAuthStatus);

// Çıkış yapma fonksiyonu
const logout = () => {
  localStorage.removeItem('isLoggedIn');
  localStorage.removeItem('username');
  updateAuthStatus();
  router.push('/login');
  window.location.reload(); // Sayfayı tamamen yenileyerek Vue state'ini sıfırla
};
</script>

<template>
  <div>
    <header class="navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0 shadow">
      <a class="navbar-brand col-md-3 col-lg-2 me-0 px-3" @click="router.push('/home')">Admin Panel</a>

      <div class="navbar-nav ms-auto d-flex align-items-center">
        <div v-if="isLoggedIn" class="nav-item text-nowrap d-flex align-items-center">
          <span class="nav-link text-white me-3">{{ username }}</span>
          <a class="nav-link ms-3 me-3" @click="logout">Çıkış Yap</a>
        </div>
        <div v-else class="nav-item text-nowrap d-flex align-items-center">
          <router-link class="nav-link me-3" to="/login">Giriş Yap</router-link>
          <router-link class="nav-link me-3" to="/register">Kayıt Ol</router-link>
        </div>
      </div>
    </header>

    <div class="container-fluid">
      <div class="row">
        <nav id="sidebarMenu" class="col-md-3 col-lg-2 d-md-block bg-light sidebar collapse" :class="{ show: isLoggedIn }">
          <div class="position-sticky pt-3" v-if="isLoggedIn">
            <ul class="nav flex-column">
              <li class="nav-item m-2" v-for="item in menuItems" :key="item.path">
                <router-link class="nav-link" :to="item.path" :class="{ active: route.path === item.path }">
                  {{ item.label }}
                </router-link>
              </li>
            </ul>
          </div>
        </nav>

        <main class="col-md-9 ms-sm-auto col-lg-10 px-md-4">
          <br>
          <router-view></router-view>
        </main>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  computed: {
    menuItems() {
      return [
        { path: "/home", label: "Anasayfa" },
        { path: "/users", label: "Kullanıcılar" },
        { path: "/projects", label: "Projeler" },
        { path: "/technologies", label: "Teknolojiler" },
        { path: "/languages", label: "Yabancı Diller" },
        { path: "/talents", label: "Yetenekler" },
        { path: "/hobbies", label: "Hobiler" },
        { path: "/educations", label: "Eğitimler" },
        { path: "/experiences", label: "Deneyimler" },
        // { path: "/socialmedias", label: "Sosyal Medya" },
      ];
    }
  }
};
</script>

<style scoped>
/* Navbar */
.navbar-brand {
  font-size: 1.5rem;
  font-weight: bold;
  cursor: pointer;
}

.navbar-nav .nav-link {
  color: white;
  font-size: 0.9rem;
}

.navbar-nav .nav-link:hover {
  text-decoration: underline;
}

/* Sidebar */
.nav {
  padding-top: 1rem;
}

.nav .nav-item .nav-link {
  font-size: 1rem;
  color: #333;
  padding: 0.5rem 1rem;
  transition: background-color 0.3s ease, color 0.3s ease;
}

.nav .nav-item .nav-link:hover {
  background-color: #f8f9fa;
  color: #007bff;
  border-radius: 5px;
}

/* Aktif bağlantı */
.nav .nav-item .nav-link.active {
  font-weight: bold;
  background-color: #e9ecef;
  color: #007bff;
  border-radius: 5px;
}

/* Form */
.form-control {
  border-radius: 0;
  font-size: 0.9rem;
}
</style>
