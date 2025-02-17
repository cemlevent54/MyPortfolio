<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router'; // Importing useRouter
import axios from 'axios'; // Importing axios

const router = useRouter();

// Function to navigate to a given path
const navigateToPlatform = (path) => {
  router.push(path);
};

const userName = ref('');
const userPassword = ref('');

// Login function that sends a POST request
const loginUser = async () => {
  const userData = {
    username: userName.value,
    password: userPassword.value,
  };

  console.log(userData);

  try {
    const response = await axios.post('http://localhost:5000/api/account/login', userData);
    console.log(response);

    // Store login status and user info in localStorage
    localStorage.setItem('isLoggedIn', 'true');
    localStorage.setItem('username', userName.value);
    window.location.reload();
    router.push('/home'); // Navigate to the home page on success
  } catch (error) {
    console.log(error); // Log any errors
  }
};

</script>


<template>
  <div class="container-fluid vh-80 d-flex justify-content-center align-items-center" style="font-size: 16px;">
    <div class="row w-100">
      <div class="col-md-6 offset-md-3">
        <div class="card">
          <form class="card-body cardbody-color p-lg-5" @submit.prevent="loginUser">
            <h1 class="text-center mb-4">Giriş Yap</h1>

            <div class="text-center">
              <img
                src="https://cdn.pixabay.com/photo/2016/03/31/19/56/avatar-1295397__340.png"
                class="img-fluid profile-image-pic img-thumbnail rounded-circle my-3"
                alt="profile"
              />
            </div>

            <div class="mb-3">
              <input
                type="text"
                class="form-control"
                id="userName"
                v-model="userName" 
                placeholder="Kullanıcı Adı"
              />
            </div>

            <div class="mb-3">
              <input
                type="password"
                class="form-control"
                id="userPassword"
                v-model="userPassword"
                placeholder="Şifre"
              />
            </div>

            <div class="text-center">
              <button type="submit" class="btn btn-color px-5 mb-4 w-100">
                Giriş Yap
              </button>
            </div>

            <div id="emailHelp" class="form-text text-center mb-4 text-dark">
              Üye olmadınız mı? <a class="text-dark fw-bold" @click.prevent="navigateToPlatform('/register')">Hesap Oluştur</a>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* Button style */
.btn-color {
  background-color: #0e1c36;
  color: #fff;
}

/* Profile image style */
.profile-image-pic {
  height: 120px;
  width: 120px;
  object-fit: cover;
}

/* Card background */
.cardbody-color {
  background-color: #ebf2fa;
}

/* Full-height container */
.container-fluid {
  height: 100%;
}

/* No underline on links */
a {
  text-decoration: none;
}
</style>
