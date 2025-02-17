<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';

// URL's
const REGISTER_URL = 'http://localhost:5000/api/account/register';
const FILE_URL = 'http://localhost:5000/api/account/upload-photo';

// Reactive variables
const userName = ref('');
const userSurname = ref('');
const userBirthdate = ref('');
const userUsername = ref('');
const userPhone = ref('');
const userEmail = ref('');
const userPhotoPreview = ref('');
const userPassword = ref('');
const userPhotoUrl = ref('');
const PhotoFile = ref(null);

// Handle photo upload
function handlePhotoUpload(event) {
  const file = event.target.files[0];
  if (file) {
    const reader = new FileReader();
    reader.onload = (e) => {
      userPhotoPreview.value = e.target.result;
    };
    reader.readAsDataURL(file);
    PhotoFile.value = file; // Update PhotoFile
  }
}

const router = useRouter();

// Navigate to login
const navigateToLogin = () => {
  router.push('/login');
};

// Function to handle registration
const registerUser = () => {
  console.log('Attempting to register user...');
  
  // Upload photo first
  if (PhotoFile.value) {
    let formData = new FormData();
    formData.append("file", PhotoFile.value);

    axios.post(FILE_URL, formData)
      .then((response) => {
        console.log('Response from photo upload:', response);
        if (response.data && response.data.photoUrl) { // photoUrl kullanılıyor
          userPhotoUrl.value = response.data.photoUrl; // Get the photo URL
          console.log('Photo uploaded successfully, proceeding with registration...');
          sendRegisterRequest(); // Call register after photo upload
        } else {
          console.error('No photoUrl in response:', response);
          alert('Failed to retrieve photo URL.');
        }
      })
      .catch((error) => {
        console.error('Image upload failed:', error);
        alert('Failed to upload image');
      });
  } else {
    console.log('No photo uploaded, proceeding with registration...');
    sendRegisterRequest(); // If no image, just register
  }
}


// Function to send registration request
function sendRegisterRequest() {
  console.log(userName.value);
  const userDatas = {
    User_Username: userUsername.value,
    User_Email: userEmail.value,
    User_Password: userPassword.value,
    User_Name: userName.value,
    User_Surname: userSurname.value,
    User_PhoneNumber: userPhone.value,
    User_About: 'kullanıcı hakkında',
    User_BirthDate: userBirthdate.value,
    User_PhotoUrl: userPhotoUrl.value, 
    User_LivingCity: 'your_living_city',
    User_CvUrl: 'your_cv_url',
  }
  // console.log('Sending registration data:', userDatas);

  axios.post(REGISTER_URL, userDatas)
    .then((response) => {
      // console.log('Registration successful:', response);
      alert('User registered successfully');
      router.push('/login');
    })
    .catch((error) => {
      console.error('Failed to register user:', error);
      alert('Failed to register user');
    });
}

</script>

<template>
  <div class="container-fluid vh-100 d-flex justify-content-center align-items-center">
    <div class="row w-100">
      <div class="col-md-6 offset-md-3">
        <div class="card">
          <form class="card-body cardbody-color p-lg-5" @submit.prevent="registerUser">
            <h1 class="text-center mb-4">Register</h1>

            <div class="text-center">
              <img
                :src="userPhotoPreview || 'https://cdn.pixabay.com/photo/2016/03/31/19/56/avatar-1295397__340.png'"
                class="img-fluid profile-image-pic img-thumbnail rounded-circle my-3"
                alt="profile"
              />
            </div>

            <!-- Name -->
            <div class="mb-3 row">
              <label for="userName" class="col-sm-3 col-form-label text-end">Ad:</label>
              <div class="col-sm-9">
                <input type="text" class="form-control" id="userName" v-model="userName" />
              </div>
            </div>

            <!-- Surname -->
            <div class="mb-3 row">
              <label for="userSurname" class="col-sm-3 col-form-label text-end">Soyad:</label>
              <div class="col-sm-9">
                <input type="text" class="form-control" id="userSurname" v-model="userSurname" />
              </div>
            </div>

            <!-- Birthdate -->
            <div class="mb-3 row">
              <label for="userBirthdate" class="col-sm-3 col-form-label text-end">Doğum Tarihi:</label>
              <div class="col-sm-9">
                <input type="date" class="form-control" id="userBirthdate" v-model="userBirthdate" />
              </div>
            </div>

            <!-- Username -->
            <div class="mb-3 row">
              <label for="userUsername" class="col-sm-3 col-form-label text-end">Username:</label>
              <div class="col-sm-9">
                <input type="text" class="form-control" id="userUsername" v-model="userUsername" />
              </div>
            </div>

            <!-- Password -->
            <div class="mb-3 row">
              <label for="userPassword" class="col-sm-3 col-form-label text-end">Parola:</label>
              <div class="col-sm-9">
                <input type="password" class="form-control" id="userPassword" v-model="userPassword" />
              </div>
            </div>

            <!-- Phone -->
            <div class="mb-3 row">
              <label for="userPhone" class="col-sm-3 col-form-label text-end">Telefon:</label>
              <div class="col-sm-9">
                <input type="text" class="form-control" id="userPhone" v-model="userPhone" />
              </div>
            </div>

            <!-- Email -->
            <div class="mb-3 row">
              <label for="userEmail" class="col-sm-3 col-form-label text-end">Email:</label>
              <div class="col-sm-9">
                <input type="email" class="form-control" id="userEmail" v-model="userEmail" />
              </div>
            </div>

            <!-- Photo Preview -->
            <!-- <div class="mb-3 row">
              <label for="userPhotoPreview" class="col-sm-3 col-form-label text-end">Fotoğraf:</label>
              <div class="col-sm-9 d-flex align-items-center">
                <img
                  :src="userPhotoPreview || 'https://cdn.pixabay.com/photo/2016/03/31/19/56/avatar-1295397__340.png'"
                  alt="Fotoğraf"
                  width="100"
                  height="100"
                  class="border rounded"
                />
              </div>
            </div> -->

            <!-- Photo Upload -->
            <div class="mb-3 row">
              <label for="userPhoto" class="col-sm-3 col-form-label text-end">Yükle:</label>
              <div class="col-sm-9">
                <input type="file" class="form-control" id="userPhoto" accept="image/*" @change="handlePhotoUpload" />
              </div>
            </div>

            <div class="text-center">
              <button type="submit" class="btn btn-color px-5 mb-4 w-100">Register</button>
            </div>
            <div id="emailHelp" class="form-text text-center mb-4 text-dark">
              Already Registered? <a class="text-dark fw-bold" @click="navigateToLogin">Login</a>
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

label {
  font-weight: bold;
  font-size: 16px;
}
</style>
