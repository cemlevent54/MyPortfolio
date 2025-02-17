<script setup>
import { ref, onMounted, computed } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import axios from 'axios';
import { API_URL ,PROFILE_PHOTO_URL } from '../../config';

const user = ref(null); // Başlangıçta boş nesne yerine null
const route = useRoute();
const router = useRouter();

// Route paramını computed ile almak daha güvenlidir
const userName = computed(() => route.params.userName);
const userPhotoUrl = '';

const navigateToPlatform = (routeParam) => {
  router.push(`/${routeParam}`);
};

const getUserDetails = async () => {
  if (!userName.value) return;

  try {
    const response = await axios.get(`${API_URL}user/${encodeURIComponent(userName.value)}`);
    user.value = response.data;
  } catch (error) {
    console.error('Error fetching user details:', error);
    user.value = null; // Kullanıcı bulunamazsa boş bırak
  }
};

// view user photo
const viewUserPhoto = (userphotourl) => {
  if (userphotourl) {
    console.log(API_URL + userphotourl);
    return PROFILE_PHOTO_URL + userphotourl;
  } else {
    return '';
  }
}

onMounted(() => {
  getUserDetails();
});
</script>

<template>
  <div class="container">
    <!-- Kullanıcı Kartı -->
    <div class="left-card" v-if="user">
      <div class="card">
        <div class="card-header text-center">
          <h4>Kullanıcı Detay</h4>
        </div>
        <div class="card-body">
          <div class="user-info text-center">
            <div class="user-photo-container">
              <img 
                v-if="user.user_PhotoUrl" 
                :src="viewUserPhoto(user.user_PhotoUrl)" 
                class="user-photo" 
                alt="User Photo"
              />
            </div>
          </div>
          <br />
          <div v-if="user.userName">
            <div class="row mb-3">
              <div class="col-4"><strong>Kullanıcı Adı:</strong></div>
              <div class="col-8">{{ user.userName }}</div>
            </div>
            <div class="row mb-3">
              <div class="col-4"><strong>E-posta:</strong></div>
              <div class="col-8">{{ user.user_Email }}</div>
            </div>
            <div class="row mb-3">
              <div class="col-4"><strong>Ad:</strong></div>
              <div class="col-8">{{ user.user_Name }}</div>
            </div>
            <div class="row mb-3">
              <div class="col-4"><strong>Soyad:</strong></div>
              <div class="col-8">{{ user.user_Surname }}</div>
            </div>
            <div class="row mb-3">
              <div class="col-4"><strong>Telefon:</strong></div>
              <div class="col-8">{{ user.user_PhoneNumber }}</div>
            </div>
            <div class="row mb-3">
              <div class="col-4"><strong>Rol:</strong></div>
              <div class="col-8">{{ user.user_Role }}</div>
            </div>
            <div class="row mb-3">
              <div class="col-4"><strong>Doğum Tarihi:</strong></div>
              <div class="col-8">{{ user.user_BirthDate }}</div>
            </div>
            <div class="row mb-3">
              <div class="col-4"><strong>Kayıt Tarihi:</strong></div>
              <div class="col-8">{{ user.user_RegisteredAt }}</div>
            </div>
          </div>
          <div v-else class="text-center text-muted">Kullanıcı bilgisi bulunamadı.</div>
        </div>
      </div>
    </div>

    <!-- Yükleme Durumu -->
    <div v-else class="loading">
      <p>Loading user data...</p>
    </div>

    <!-- Navigasyon Kartları -->
    <div class="right-cards">
      <div class="card clickable" style="background-color: lightcoral;" @click="navigateToPlatform('experiences')">
        <div class="card-body"><h5>Deneyimlerim</h5></div>
      </div>
      <div class="card clickable" style="background-color: #f0f8ff;" @click="navigateToPlatform('projects')">
        <div class="card-body"><h5>Projelerim</h5></div>
      </div>
      <div class="card clickable" style="background-color: #ffebcd;" @click="navigateToPlatform('hobbies')">
        <div class="card-body"><h5>Hobilerim</h5></div>
      </div>
      <div class="card clickable" style="background-color: blueviolet;" @click="navigateToPlatform('talents')">
        <div class="card-body"><h5>Yeteneklerim</h5></div>
      </div>
      <div class="card clickable" style="background-color: gainsboro;" @click="navigateToPlatform('technologies')">
        <div class="card-body"><h5>Kullandığım Teknolojiler</h5></div>
      </div>
      <div class="card clickable" style="background-color: chocolate;" @click="navigateToPlatform('languages')">
        <div class="card-body"><h5>Yabancı Diller</h5></div>
      </div>
      <div class="card clickable" style="background-color: mediumaquamarine;" @click="navigateToPlatform('socialmedias')">
        <div class="card-body"><h5>Sosyal Medya</h5></div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.container {
  display: flex;
  justify-content: space-between;
  gap: 20px;
  margin: 20px;
}

.left-card {
  flex: 2;
  max-width: 50%;
}

.right-cards {
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.card {
  border: 1px solid #ccc;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.card-header {
  padding: 10px;
  background-color: #f7f7f7;
  border-bottom: 1px solid #ddd;
  text-align: center;
}

.card-body {
  padding: 20px;
}

.user-photo-container {
  width: 120px;
  height: 120px;
  border-radius: 50%;
  border: 2px solid #000;
  overflow: hidden;
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 0 auto;
  background-color: #f0f0f0;
}

.user-photo {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.user-photo-placeholder {
  width: 60%;
  height: 60%;
  color: #666;
}

.clickable {
  cursor: pointer;
  transition: opacity 0.3s;
}

.clickable:hover {
  opacity: 0.8;
}

.loading {
  text-align: center;
  font-size: 18px;
  color: gray;
}
</style>