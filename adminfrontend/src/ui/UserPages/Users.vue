<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';
import { API_URL , PROFILE_PHOTO_URL, UPLOAD_FILE_URL } from '../../config.js';

const profile_photo_url = PROFILE_PHOTO_URL;
const user = ref({
  id: 0,
  Email: '',
  userName: '',
  user_Name: '',
  user_Surname: '',
  user_Email: '',
  user_Password: '',
  user_PhoneNumber: '',
  user_About: '',
  user_BirthDate: '',
  user_RegisteredAt: '',
  user_PhotoUrl: '',
  user_State: '',
  user_LivingCity: '',
  user_Cvurl: '',
  
  userRole: '',
})

const updatedUser = {
    id: 0,
    Email: '',
    userName: '',
    user_Name: '',
    user_Surname: '',
    user_Email: '',
    user_Password: '',
    user_PhoneNumber: '',
    user_About: '',
    user_BirthDate: '',
    user_RegisteredAt: '',
    user_PhotoUrl: '',
    user_State: '',
    user_LivingCity: '',
    user_Cvurl: '',
    
    userRole: '',
}

const userName = ref('');
const edituserName = ref('');
const userSurname = ref('');
const edituserSurname = ref('');
const userBirthdate = ref('');
const edituserBirthdate = ref('');
const userUsername = ref('');
const edituserUsername = ref('');
const userRole = ref('');
const edituserRole = ref('');
const userPhoneNumber = ref('');
const edituserPhone = ref('');
const userEmail = ref('');
const edituserEmail = ref('');
const userPassword = ref('');
const userJob = ref('');
const edituserJob = ref('');
const userLivingCity = ref('');
const edituserLivingCity = ref('');
const userCvUrl = ref('');
const edituserCvUrl = ref('');
const userAbout = ref('');
const edituserAbout = ref('');
const userPhotoUrl = ref('');
const photoFile = ref(null);
const editPhotoFile = ref(null);
const edituserPhotoPreview = ref('');
const edituserPhotoUrl = ref('');
// Fotoğraf preview için reaktif bir değişken
const userPhotoPreview = ref(''); // Varsayılan olarak boş

// Fotoğraf yüklendiğinde çağrılacak fonksiyon
function handlePhotoUpload(event) {
  const file = event.target.files[0]; // İlk seçilen dosyayı al
  if (file) {
    const reader = new FileReader(); // Dosyayı okumak için FileReader oluştur
    reader.onload = (e) => {
      userPhotoPreview.value = e.target.result; // Okunan dosyayı preview kaynağına ata
    };
    reader.readAsDataURL(file); // Dosyayı Data URL formatında oku
    photoFile.value = file; // Dosyayı güncelle
  }
}

// Fotoğraf yüklendiğinde çağrılacak fonksiyon
function handlePhotoUploadUpdate(event) {
  const file = event.target.files[0]; // İlk seçilen dosyayı al
  if (file) {
    const reader = new FileReader(); // Dosyayı okumak için FileReader oluştur
    reader.onload = (e) => {
      edituserPhotoPreview.value = e.target.result; // Okunan dosyayı preview kaynağına ata
    };
    reader.readAsDataURL(file); // Dosyayı Data URL formatında oku
    editPhotoFile.value = file; // Dosyayı güncelle
  }
}

// give me preview of img url
function getImgUrl(imgUrl){
//   console.log(imgUrl);
  return profile_photo_url + imgUrl;
}

const router = useRouter();

// Users array
const users = ref([]);

const getUsers = async () => { 
    try {
        const response = await axios.get(`${API_URL}user`);
        const datas = response.data;
        
        
        // For each user in the fetched data, push a new user object to the users array
        datas.forEach((element) => {
        users.value.push({
            id: element.id,
            Email: element.email,
            userName: element.userName,
            user_Name: element.user_Name,
            user_Surname: element.user_Surname,
            user_Email: element.user_Email,
            user_Password: element.user_Password,
            user_PhoneNumber: element.user_PhoneNumber,
            user_About: element.user_About,
            user_BirthDate: element.user_BirthDate,
            user_RegisteredAt: element.user_RegisteredAt,
            user_PhotoUrl: element.user_PhotoUrl,
            user_State: element.user_State,
            user_LivingCity: element.user_LivingCity,
            user_Cvurl: element.user_CvUrl,
            user_Job: element.user_Job,
            userRole: element.userRole,
        });
        });

        

        // console.log('Users:', users);

    } catch(error) {
        console.error('Failed to get users:', error);
        alert('Failed to get users');
    }
}

// get roles 
const roles = ref([]);
const getRoles = async () => {
    try {
        const response = await axios.get(`${API_URL}roles`);
        roles.value = response.data.map(data => ({
            id: data.id,
            name: data.name,
            permissions: null
        }));
    } catch (error) {
        console.error('Failed to get roles: ', error);
        alert('Failed to get roles');
    }
};

// Add user
const addUser = async () => {
    try {
       if(photoFile.value) {
        let formData = new FormData();
        formData.append("file", photoFile.value);
        
        axios.post(UPLOAD_FILE_URL, formData)
        .then((response) => {
            console.log('Response from photo upload:', response);
            if(response.data && response.data.photoUrl) {
                userPhotoUrl.value = response.data.photoUrl;
                console.log(userPhotoUrl.value);
                console.log('Photo uploaded successfully, proceeding with registration...');
                sendAddUserRequest();
            } else {
                console.error('No photoUrl in response:', response);
                alert('Failed to retrieve photo URL.');
            }
        })
       }

    } catch(error) {
        console.error('Failed to add user:', error);
        alert('Failed to add user');
    }
}

// Send the user data to backend for registration
const sendAddUserRequest = async () => {
    const userDatas = {
        User_Name: userName.value,
        User_Surname: userSurname.value,
        User_BirthDate: userBirthdate.value,
        User_Username: userUsername.value,
        User_Role: userRole.value,
        User_PhoneNumber: userPhoneNumber.value,
        User_Email: userEmail.value,
        User_Password: userPassword.value,
        User_Job: userJob.value,
        User_LivingCity: userLivingCity.value,
        User_CvUrl: userCvUrl.value,
        User_About: userAbout.value,
        User_PhotoUrl: userPhotoUrl.value,
    }

    try {
        const response = await axios.post(`${API_URL}user`, userDatas);
        // console.log('User added successfully:', response.data);


        // Yeni kullanıcıyı users listesine ekleyin
        users.value.push({
            id: response.data.id,
            Email: response.data.addUserDto.User_Email,
            User_Name: response.data.addUserDto.User_Name,
            User_Surname: response.data.addUserDto.User_Surname,
            User_Email: response.data.addUserDto.User_Email,
            User_Password: response.data.addUserDto.User_Password,
            User_PhoneNumber: response.data.addUserDto.User_PhoneNumber,
            User_About: response.data.addUserDto.User_About,
            User_BirthDate: response.data.addUserDto.User_BirthDate,
            User_RegisteredAt: response.data.addUserDto.User_RegisteredAt,
            User_PhotoUrl: response.data.addUserDto.User_PhotoUrl,
            User_State: response.data.addUserDto.User_State,
            User_LivingCity: response.data.addUserDto.User_LivingCity,
            User_Cvurl: response.data.addUserDto.User_Cvurl,
            UserRole: response.data.addUserDto.User_Role,
        });

        if(response.data && response.data.id) {
            await getUsers();
            window.location.reload();
            // Modalı kapat
            $('#addUser').modal('hide');
        } else {
            throw new Error('User data is missing');
        }



    } catch (error) {
        console.error('Failed to add user:', error);
        alert('Failed to add user');
    }
}


const selectUserForUpdate = (userToUpdate) => {
    // console.log('User to update:', userToUpdate);

    // Güncellenecek kullanıcının bilgilerini modal formuna set et
    user.value = { ...userToUpdate };

    edituserName.value = userToUpdate.user_Name;
    edituserSurname.value = userToUpdate.user_Surname;
    // edituserBirthdate.value = userToUpdate.user_BirthDate;
    edituserUsername.value = userToUpdate.userName;
    edituserRole.value = userToUpdate.userRole;
    edituserPhone.value = userToUpdate.user_PhoneNumber;
    edituserEmail.value = userToUpdate.user_Email;
    edituserJob.value = userToUpdate.user_Job;
    edituserLivingCity.value = userToUpdate.user_LivingCity;
    edituserCvUrl.value = userToUpdate.user_Cvurl;
    edituserAbout.value = userToUpdate.user_About;
    edituserPhotoUrl.value = userToUpdate.user_PhotoUrl;

    // Profil fotoğrafı önizleme (varsa)
    if (userToUpdate.user_PhotoUrl) {
        edituserPhotoPreview.value = getImgUrl(userToUpdate.user_PhotoUrl);
    } else {
        edituserPhotoPreview.value = ''; // Eğer yoksa boş bırak
    }

    // Bootstrap modalı aç
    const modal = new bootstrap.Modal(document.getElementById('updateUser'));
    modal.show();
};


// update user
const updateUser = async () => {
    console.log('Updated user:', user.value);
    try {
        if (editPhotoFile.value) {
            // Eğer yeni bir fotoğraf seçilmişse, fotoğrafı yükle
            let formData = new FormData();
            formData.append("file", editPhotoFile.value);

            axios.post(UPLOAD_FILE_URL, formData)
                .then((response) => {
                    console.log('Response from photo upload:', response);
                    if (response.data && response.data.photoUrl) {
                        edituserPhotoUrl.value = response.data.photoUrl; // Yeni fotoğraf URL'sini güncelle
                        // console.log(edituserPhotoUrl.value);
                        // console.log('Photo uploaded successfully, proceeding with registration...');
                        sendUpdateUserRequest(); // Kullanıcıyı güncelle
                    } else {
                        console.error('No photoUrl in response:', response);
                        alert('Failed to retrieve photo URL.');
                    }
                })
                .catch((error) => {
                    console.error('Failed to upload photo:', error);
                    alert('Failed to upload photo.');
                });
        } else {
            // Eğer yeni bir fotoğraf seçilmemişse, direkt olarak kullanıcıyı güncelle
            console.log('No new photo selected, proceeding with user update...');
            sendUpdateUserRequest();
        }
    } catch (error) {
        console.error('Failed to update user:', error);
        alert('Failed to update user');
    }
}

const sendUpdateUserRequest = async () => {
    const updateUserDatas = {
        User_Name: edituserName.value,
        User_Surname: edituserSurname.value,
        User_Username: edituserUsername.value,
        User_Role: edituserRole.value,
        User_PhoneNumber: edituserPhone.value,
        User_Email: edituserEmail.value,
        User_Job: edituserJob.value,
        User_LivingCity: edituserLivingCity.value,
        User_CvUrl: edituserCvUrl.value,
        User_About: edituserAbout.value,
        User_PhotoUrl: edituserPhotoUrl.value,
    }

    try {
        const response = await axios.put(`${API_URL}user/${user.value.id}`, updateUserDatas);
        // console.log('User updated successfully:', response.data);
        // Update the user in the users array
        const index = users.value.findIndex(u => u.id === user.value.id);
        if (index !== -1) {
            users.value[index] = { ...users.value[index], ...updateUserDatas };
        }
        // alert('User updated successfully');

        const modal = new bootstrap.Modal(document.getElementById('updateUser'));
        modal.hide();
        window.location.reload();
    } catch (error) {
        console.error('Failed to update user:', error);
        alert('Failed to update user');
    }
}

// delete user
const deleteUser = async (userId) => {
    try {
        await axios.delete(`${API_URL}user/${userId}`);
        
        // Remove the user from the reactive state
        users.value = users.value.filter(user => user.id !== userId);
        
        alert('User deleted successfully');
    } catch (error) {
        console.error('Failed to delete user:', error);
        alert('Failed to delete user');
    }
}


onMounted(() => {
    getUsers();
    getRoles();
})



const navigateToRoles = () => {
  router.push('/roles'); // Navigate to the defined route
};

const viewUserDetails = (userName) => {
  if (!userName) {
    console.error("User name is missing!");
    return;
  }
  router.push(`/user/${encodeURIComponent(userName)}`);
};



</script>


<template>
    <h3>Kullanıcılar</h3>

    <br>

    <div style="font-size:16px">
        <table class="table table-striped table-responsive table-hover table-bordered text-center" style="width:100%">
            <thead class="table-dark">
                <tr class="col-md-2">
                    <th>ID</th>
                    <th>Username</th>
                    <th>Email</th>
                    <th>Rolü</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <!-- Dynamically rendered rows -->
                <tr v-for="user in users" :key="user.id">
                    <td>{{ user.id }}</td>
                    <td>{{ user.userName }}</td> <!-- Updated to use user.userName -->
                    <td>{{ user.user_Email }}</td> <!-- Updated to use user.user_Email -->
                    <td>{{ user.userRole }}</td> <!-- Updated to use user.userRole -->
                    
                    <td>
                        <button class="btn btn-secondary btn-sm" @click="viewUserDetails(user.userName)">Detay</button>
                        <button class="btn btn-primary btn-sm" @click="selectUserForUpdate(user)">Güncelle</button>
                        <button class="btn btn-danger btn-sm" @click="deleteUser(user.id)">Sil</button>
                    </td>
                </tr>
            </tbody>
        </table>

        
        <!-- Kullanıcı ekleme modal -->
        <div class="d-flex justify-content-between mt-3">
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addUser">
            Ekle
            </button>
            <button type="button" class="btn btn-success" @click="navigateToRoles">
            Roller
            </button>

            <!-- Modal -->
            <div class="modal fade" id="addUser" tabindex="-1" aria-labelledby="addUserLabel" aria-hidden="true" >
                <div class="modal-dialog modal-dialog-centered" style="max-width: 60%;"> <!-- Genişliği artırmak için inline CSS kullanıldı -->
                    <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="addUserLabel">Yeni Kullanıcı Ekle</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="mb-3 row">
                                <label for="userName" class="col-sm-3 col-form-label">Ad:</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="userName" v-model="userName"/>
                                </div>
                            </div>
                            
                            <div class="mb-3 row">
                                <label for="userSurname" class="col-sm-3 col-form-label">Soyad:</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="userSurname" v-model="userSurname"/>
                                </div>
                            </div>

                            <div class="mb-3 row">
                                <label for="userBirthdate" class="col-sm-3 col-form-label">Doğum Tarihi:</label>
                                <div class="col-sm-9">
                                    <input type="date" class="form-control" id="userBirthdate" v-model="userBirthdate"/>
                                </div>
                            </div>

                            <div class="mb-3 row">
                                <label for="userUsername" class="col-sm-3 col-form-label">Username:</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="userUsername" v-model="userUsername"/>
                                </div>
                            </div>

                            <div class="mb-3 row">
                                <label for="userRole" class="col-sm-3 col-form-label">Rol:</label>
                                <div class="col-sm-9">
                                    <!-- do it with roles array, v-for-->
                                    <select class="form-select" id="userRole" v-model="userRole">
                                        <option v-for="role in roles" :key="role.id" :value="role.name">
                                            {{ role.name }}
                                        </option>
                                    </select>
                                    
                                </div>
                            </div>

                            <div class="mb-3 row">
                                <label for="userPhone" class="col-sm-3 col-form-label">Telefon:</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="userPhone" v-model="userPhoneNumber"/>
                                </div>
                            </div>

                            <div class="mb-3 row">
                                <label for="userEmail" class="col-sm-3 col-form-label">Email:</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="userEmail" v-model="userEmail"/>
                                </div>
                            </div>

                            <!-- user password -->
                            <div class="mb-3 row">
                                <label for="userPassword" class="col-sm-3 col-form-label">Şifre:</label>
                                <div class="col-sm-9">
                                    <input type="password" class="form-control" id="userPassword" v-model="userPassword"/>
                                </div>
                            </div>

                            <!-- user job -->
                            <div class="mb-3 row">
                                <label for="userJob" class="col-sm-3 col-form-label">Meslek:</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="userJob" v-model="userJob"/>
                                </div>
                            </div>
                            
                            <!-- user living city -->
                            <div class="mb-3 row">
                                
                                <label for="userLivingCity" class="col-sm-3 col-form-label">Yaşadığı Şehir:</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="userLivingCity" v-model="userLivingCity"/>
                                </div>
                            </div>

                            <!-- user cv url -->
                            <div class="mb-3 row">
                                <label for="userCvUrl" class="col-sm-3 col-form-label">CV Url:</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="userCvUrl" v-model="userCvUrl"/>
                                </div>
                            </div>

                            <!-- user about -->
                            <div class="mb-3 row">
                                <label for="userAbout" class="col-sm-3 col-form-label">Hakkında:</label>
                                <div class="col-sm-9">
                                    <textarea class="form-control" id="userAbout" rows="4" v-model="userAbout" ></textarea>
                                </div>
                            </div>

                            <!-- Fotoğraf Preview -->
                            <div class="mb-3 row align-items-center">
                                <label for="userPhotoPreview" class="col-sm-3 col-form-label">Fotoğraf:</label>
                                <div class="col-sm-9 d-flex align-items-center">
                                    <img :src="userPhotoPreview" alt="Fotoğraf" width="100" height="100" class="border rounded me-3" />
                                    <input type="file" class="form-control" id="userPhoto" accept="image/*" @change="handlePhotoUpload" />
                                </div>
                            </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                        <button type="button" class="btn btn-primary" @click="addUser">Ekle</button>
                    </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Kullanıcı güncelleme modal -->
        <div class="d-flex justify-content-between mt-3">

            <!-- Modal -->
            <div class="modal fade" id="updateUser" tabindex="-1" aria-labelledby="updateUserLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" style="max-width: 60%;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h1 class="modal-title fs-5" id="updateUserLabel">Kullanıcı Güncelle</h1>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="mb-3 row">
                                <label for="edituserName" class="col-sm-3 col-form-label">Ad:</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="edituserName" v-model="edituserName"/>
                                </div>
                            </div>
                            
                            <div class="mb-3 row">
                                <label for="edituserSurname" class="col-sm-3 col-form-label">Soyad:</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="edituserSurname" v-model="edituserSurname"/>
                                </div>
                            </div>

                            <!-- <div class="mb-3 row">
                                <label for="edituserBirthdate" class="col-sm-3 col-form-label">Doğum Tarihi:</label>
                                <div class="col-sm-9">
                                    <input type="date" class="form-control" id="edituserBirthdate" v-model="edituserBirthdate"/>
                                </div>
                            </div> -->

                            <div class="mb-3 row">
                                <label for="edituserUsername" class="col-sm-3 col-form-label">Username:</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="edituserUsername" v-model="edituserUsername"/>
                                </div>
                            </div>

                            <div class="mb-3 row">
                                <label for="edituserRole" class="col-sm-3 col-form-label">Rol:</label>
                                <div class="col-sm-9">
                                    <!-- do it with roles array, v-for-->
                                    <select class="form-select" id="edituserRole" v-model="edituserRole">
                                        <option v-for="role in roles" :key="role.id" :value="role.name">
                                            {{ role.name }}
                                        </option>
                                    </select>
                                    
                                </div>
                            </div>

                            <div class="mb-3 row">
                                <label for="edituserPhone" class="col-sm-3 col-form-label">Telefon:</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="edituserPhone" v-model="edituserPhone"/>
                                </div>
                            </div>

                            <div class="mb-3 row">
                                <label for="edituserEmail" class="col-sm-3 col-form-label">Email:</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="edituserEmail" v-model="edituserEmail"/>
                                </div>
                            </div>

                            <!-- user job -->
                            <div class="mb-3 row">
                                <label for="edituserJob" class="col-sm-3 col-form-label">Meslek:</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="edituserJob" v-model="edituserJob"/>
                                </div>
                            </div>
                            
                            <!-- user living city -->
                            <div class="mb-3 row">
                                
                                <label for="edituserLivingCity" class="col-sm-3 col-form-label">Yaşadığı Şehir:</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="edituserLivingCity" v-model="edituserLivingCity"/>
                                </div>
                            </div>

                            <!-- user cv url -->
                            <div class="mb-3 row">
                                <label for="edituserCvUrl" class="col-sm-3 col-form-label">CV Url:</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="edituserCvUrl" v-model="edituserCvUrl"/>
                                </div>
                            </div>

                            <!-- user about -->
                            <div class="mb-3 row">
                                <label for="edituserAbout" class="col-sm-3 col-form-label">Hakkında:</label>
                                <div class="col-sm-9">
                                    <textarea class="form-control" id="edituserAbout" rows="4" v-model="edituserAbout"></textarea>
                                </div>
                            </div>

                            <!-- Fotoğraf Preview -->
                            <div class="mb-3 row align-items-center">
                                <label for="edituserPhotoPreview" class="col-sm-3 col-form-label">Fotoğraf:</label>
                                <div class="col-sm-9 d-flex align-items-center">
                                    <img :src="edituserPhotoPreview" alt="Fotoğraf" width="100" height="100" class="border rounded me-3" />
                                    <input type="file" class="form-control" id="edituserPhoto" accept="image/*" @change="handlePhotoUploadUpdate" />
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                            <button type="button" class="btn btn-primary" @click="updateUser">Güncelle</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        
        
    </div>
</template>

<style scoped>

</style>

