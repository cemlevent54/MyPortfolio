<script setup>
import { ref, onMounted } from 'vue'; // Correct import for onMounted
import { useRouter } from 'vue-router';
import { API_URL , PROFILE_PHOTO_URL } from '../../config.js';
import axios from 'axios';

// Define user state
const user = ref({
  user_Name: '',
  user_Surname: '',
  user_Email: '',
  user_PhoneNumber: '',
  userName: '',
  user_Role: '',
  user_BirthDate: '',
  user_RegisteredAt: '',
  user_PhotoUrl: 'https://via.placeholder.com/150', // Default image URL
});


// login olan kullanıcının bilgileri
const loginedUser = localStorage.getItem('username');

// Fetch user info based on the logged-in user
const getLoginedUserInfo = async () => {
  try {
    const response = await axios.get(`${API_URL}user/${loginedUser}`);
    const datas = response.data;

    // Map API data to the user state
    user.value = {
      user_Name: datas.user_Name,
      user_Surname: datas.user_Surname,
      user_Email: datas.user_Email,
      user_PhoneNumber: datas.user_PhoneNumber,
      userName: datas.userName,
      user_Role: datas.user_Role,
      user_BirthDate: datas.user_BirthDate,
      user_RegisteredAt: datas.user_RegisteredAt,
      user_PhotoUrl: datas.user_PhotoUrl, // Fallback for missing image
    };

    // console.log('User info:', user);  
  } catch (error) {
    console.error('Failed to get user info:', error);
    alert('Failed to get user info');
  }
};

const viewUserPhoto = (userphotourl) => {
  if (userphotourl) {
    // console.log(API_URL + userphotourl);
    return PROFILE_PHOTO_URL + userphotourl;
  } else {
    return '';
  }
}

// Call getLoginedUserInfo when the component is mounted
onMounted(() => {
  getLoginedUserInfo();
});

// Router to navigate to different pages
const router = useRouter();

const navigateToPlatform = (routeParam) => {
  router.push(`/${routeParam}`); // Navigate to the route with dynamic parameter
};
</script>

<template>
  <div class="container">
    <!-- Left Card with user info -->
    <div class="left-card" v-if="user.userName">
      <div class="card">
        <div class="card-header text-center">
          <h4>Kullanıcı Detay</h4>
        </div>
        <div class="card-body">
          <div class="user-info">
            <div class="text-center">
              <img :src="viewUserPhoto(user.user_PhotoUrl)" class="user-photo mt-2" />
            </div>
            <br>
            <div class="row mb-3">
              <div class="col-4"><strong>Username:</strong></div>
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
        </div>
      </div>
    </div>

    <!-- Loading state if user info is not loaded yet -->
    <div v-else class="loading">
      <p>Loading user data...</p>
    </div>

    <!-- Right Cards for navigation -->
    <div class="right-cards">
      <div class="card clickable" style="background-color: lightcoral;" @click="navigateToPlatform('experiences')">
        <div class="card-body">
          <h5>Deneyimlerim</h5>
        </div>
      </div>
      <div class="card clickable" style="background-color: #f0f8ff;" @click="navigateToPlatform('projects')">
        <div class="card-body">
          <h5>Projelerim</h5>
        </div>
      </div>

      <div class="card clickable" style="background-color: #ffebcd;" @click="navigateToPlatform('hobbies')">
        <div class="card-body">
          <h5>Hobilerim</h5>
        </div>
      </div>

      <div class="card clickable" style="background-color: blueviolet;" @click="navigateToPlatform('talents')">
        <div class="card-body">
          <h5>Yeteneklerim</h5>
        </div>
      </div>

      <div class="card clickable" style="background-color: gainsboro;" @click="navigateToPlatform('technologies')">
        <div class="card-body">
          <h5>Kullanıdığım Teknolojiler</h5>
        </div>
      </div>

      <div class="card clickable" style="background-color: chocolate;" @click="navigateToPlatform('languages')">
        <div class="card-body">
          <h5>Yabancı Diller</h5>
        </div>
      </div>

      <div class="card clickable" style="background-color: mediumaquamarine;" @click="navigateToPlatform('socialmedias')">
        <div class="card-body">
          <h5>Sosyal Medya</h5>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
const handleCardClick = (card) => {
  alert(`Tıklandı: ${card}`);
}
</script>

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

.user-info p {
  font-size: 16px;
  margin: 10px 0;
}

.user-info img {
  width: 100px;
  height: 100px;
  border-radius: 50%;
  border: 2px solid #ddd;
}

.user-photo {
  width: 120px;
  height: 120px;
  border-radius: 50%;
  border: 2px solid #ddd;
}

.clickable {
  cursor: pointer;
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
