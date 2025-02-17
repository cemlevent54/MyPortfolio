<script setup>
import { useRouter } from 'vue-router';
import axios from 'axios';
import { onMounted, ref } from 'vue';
import { API_URL, PROFILE_PHOTO_URL} from '../../config.js';

const router = useRouter();

const user = ref({
    id: 0,
});
const loginedUser = localStorage.getItem('username');
// Fetch user info based on the logged-in user
const getLoginedUserInfo = async () => {
  try {
    const response = await axios.get(`${API_URL}user/${loginedUser}`);
    const datas = response.data;

    // Map API data to the user state
    user.value = {
      id: datas.id, 
    };
    // console.log(user.value.id); // OK
    
    // console.log('User info:', user);  
    getUserLanguages();
  } catch (error) {
    console.error('Failed to get user info:', error);
    alert('Failed to get user info');
  }
};

const language = ref({
    name: '',
});

// define v-models
const languageName = ref('');
const editLanguageName = ref('');

const languages = ref([]);
const languageid = ref(0);

// CRUD's

// GET
const getLanguages = async () => {
    try {
        const response = await axios.get(`${API_URL}languages`);
        languages.value = response.data.map((language) => ({
            id: language.language_ID,
            name: language.language_Name,
        }));
        // console.log('Languages:', languages);

    }catch (error) {
        console.error('Failed to get languages:', error);
        alert('Failed to get languages');
    }
};

// POST
const addLanguage = async () => {
    try {
        const response = await axios.post(`${API_URL}languages`, {
            language_Name: languageName.value,
        });
        // console.log('Language added:', response.data);

        const newLanguage = {
            id: response.data.language_ID,
            name: response.data.language_Name,
        };

        if (newLanguage && newLanguage.id) {
            languages.value.push({
                    id: newLanguage.id,
                    name: newLanguage.name,
            });

            languageName.value = '';

            const modal = bootstrap.Modal.getInstance(document.getElementById('addLanguage'));
            modal.hide();
        }




    }catch (error) {
        console.error('Failed to add language:', error);
        alert('Failed to add language');
    }
};

// PUT
const selectedLanguageForUpdate = (languageToUpdate) => {
    // console.log('Selected language:', languageToUpdate); 
    languageid.value = languageToUpdate.id;
    editLanguageName.value = languageToUpdate.name;

    const modalElement = document.getElementById('updateLanguages');
    const modal = bootstrap.Modal.getOrCreateInstance(modalElement);
    modal.show();
};

const updateLanguage = async () => {
    try {
        const updatedLanguageData = {
            language_ID: languageid.value,
            language_Name: editLanguageName.value,
        };

        const response = await axios.put(`${API_URL}languages/${languageid.value}`, updatedLanguageData);
        // console.log('Language updated:', response.data);

        const updatedLanguage = response.data;
        const languageIndex = languages.value.findIndex((language) => language.id === updatedLanguage.language_ID);

        if (languageIndex !== -1) {
            languages.value[languageIndex] = {
                id: updatedLanguage.language_ID,
                name: updatedLanguage.language_Name,
            };

            editLanguageName.value = '';

            // Modal'ı kapatma işlemi
            const modalElement = document.getElementById('updateLanguages');
            if (modalElement) {
                const modal = bootstrap.Modal.getOrCreateInstance(modalElement);
                modal.hide();
            } else {
                console.error('Modal not found');
            }
        }
    } catch (error) {
        console.error('Failed to update language:', error);
        alert('Failed to update language');
    }
};

// DELETE
const deleteLanguage = async (languagetoDelete) => {
    const confirmDelete = confirm('Are you sure you want to delete this language?');
    if (!confirmDelete) {
        return;
    }

    try {
        const response = await axios.delete(`${API_URL}languages/${languagetoDelete.id}`);
        // console.log('Language deleted:', response.data);

        if (response.status === 200) {
            languages.value = languages.value.filter((language) => language.id !== languagetoDelete.id);
        } else {
            throw new Error('Failed to delete language');
        }

    } catch (error) {
        console.error('Failed to delete language:', error);
        alert('Failed to delete language');
    }
};

onMounted(() => {
    getLoginedUserInfo();
    getLanguages();
});

// User Language CRUD's
const userLanguage = ref({
    user_ID: user.value.id,
    language_ID: 0,
});
const userLanguages = ref([]);

// GET
const getUserLanguages = async () => {
    try {
        const userid = user.value.id;
        const response = await axios.get(`${API_URL}languages/language/${userid}`);
        // console.log('User Languages:', response.data);

        userLanguages.value = response.data.map((language) => ({
            id: language.language_ID,
            name: language.language_Name,
        }));
        
        

        if (userLanguages.value.length === 0) {
            console.log('No languages found for user');
            alert('You have no languages assigned.');
        }

    } catch (error) {
        console.error('Failed to get user languages:', error);
        alert('Failed to get user languages');
    }
};

// POST
const addLanguagetoUser = async () => {
    try {
        const selectedLanguages = Array.from((document.querySelectorAll('input[name="languageCheckbox"]:checked')))
        .map((checkbox) => {
            return { language_ID: parseInt(checkbox.value) };
        });
        // console.log(selectedLanguages);

        if (selectedLanguages.length === 0) {
            alert('Please select a language to add');
            return;
        }

        const uid = user.value.id;
        const response = await axios.post(`${API_URL}languages/language/${uid}`, {
            user_ID: uid,
            languageItemsForUser: selectedLanguages,
        });

        if (response.status === 200) {
            selectedLanguages.forEach(l => {
                const lang = languages.value.find(language => language.id === l.language_ID);
                if (lang && !userLanguages.value.some(userLanguage => userLanguage.id === lang.id)) {
                    userLanguages.value.push(lang);
                }
            });

            const modal = bootstrap.Modal.getInstance(document.getElementById('addUserLanguage'));
            modal.hide();
        } else {
            throw new Error('Failed to add language to user');
        }

    } catch (error) {
        console.error('Failed to add language to user:', error);
        alert('Failed to add language to user');
    }
};

const removeLanguageFromUser = async (userLanguageToDelete) => {
    const confirmDelete = confirm('Are you sure you want to delete this language?');
    if (!confirmDelete) {
        return;
    }

    try {
        const uid = user.value.id;

        // Create the DTO to send in the body of the DELETE request
        const languageIdDTO = {
            Language_ID: userLanguageToDelete.id, // Pass the correct language ID
        };

        const response = await axios.delete(`${API_URL}languages/delete/${uid}`, {
            data: languageIdDTO, // Include the DTO in the request body
            headers: {
                'Content-Type': 'application/json', // Ensure the server understands the body format
            },
        });

        // console.log('User Language deleted:', response.data);

        if (response.status === 200) {
            // Update the UI by removing the language from the list
            userLanguages.value = userLanguages.value.filter((language) => language.id !== userLanguageToDelete.id);
        } else {
            throw new Error('Failed to delete user language');
        }

    } catch (error) {
        console.error('Failed to delete user language:', error);
        alert('Failed to delete user language');
    }
};




</script>

<template>
  

    <div class="d-flex">
      <!-- Admin Hobby Table on the left -->
      <div class="admin-table" style="width: 50%; padding-right: 10px;">
        <h3>Yabancı Diller</h3>
        <table class="table table-striped table-responsive table-hover table-bordered text-center">
          <thead class="table-dark">
            <tr class="col-md-2">
              <th>ID</th>
              <th>Yabancı Dil</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="language in languages" :key="language.id">
              <td>{{ language.id }}</td>
              <td>{{ language.name }}</td>
              <td>
                <button class="btn btn-primary btn-sm" @click="selectedLanguageForUpdate(language)">Güncelle</button>
                <button class="btn btn-danger btn-sm" @click="deleteLanguage(language)">Delete</button>
              </td>
            </tr>
          </tbody>
        </table>
  
        <!-- Add Language Modal -->
        <div class="d-flex justify-content-between mt-3">
          <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addLanguage">
            Ekle
          </button>
        </div>
      </div>
  
      <!-- User Language Table on the right -->
      <div class="user-table" style="width: 45%; padding-left: 10px;">
        <h3>Kullanıcı Dilleri</h3>
        <table class="table table-striped table-responsive table-hover table-bordered text-center">
          <thead class="table-dark">
            <tr class="col-md-2">
              <th>Dil</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="language in userLanguages" :key="language.id">
              <td>{{ language.name }}</td>
              <td>
                <button class="btn btn-danger btn-sm" @click="removeLanguageFromUser(language)">Delete</button>
              </td>
            </tr>
          </tbody>
        </table>
  
        <!-- Add Language Button for User -->
        <div class="d-flex justify-content-between mt-3">
          <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addUserLanguage">
            Dil Ekle
          </button>
        </div>
      </div>
    </div>
  
    <!-- Add Language Modal -->
    <div class="modal fade" id="addLanguage" tabindex="-1" aria-labelledby="addLanguageLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" style="max-width: 40%;">
        <div class="modal-content">
          <div class="modal-header">
            <h1 class="modal-title fs-5" id="addLanguageLabel">Yeni Dil Ekle</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <div class="mb-2 d-flex align-items-center">
              <label for="languageName" class="me-4 col-3">Dil: </label>
              <input type="text" class="form-control col-max" id="languageName" 
              aria-label="Dil Adı" v-model="languageName"/>
            </div>
              
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
            <button type="button" class="btn btn-primary" @click="addLanguage">Ekle</button>
          </div>
        </div>
      </div>
    </div>


  
  <!-- Add Language Modal for User -->
  <div class="modal fade" id="addUserLanguage" tabindex="-1" aria-labelledby="addUserLanguageLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" style="max-width: 40%;">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="addUserLanguageLabel">Kullanıcı Dil Ekle</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div>
              <div v-for="language in languages.filter(language => !userLanguages.some(userLanguage => userLanguage.id === language.id))" :key="language.id">
              <!-- Show checkboxes only for unselected languages -->
              <input 
                  type="checkbox" 
                  :id="'checkbox-' + language.id" 
                  :value="language.id" 
                  name="languageCheckbox"
              />
              <label :for="'checkbox-' + language.id">{{ language.name }}</label>
              </div>
          </div>
        </div>
  
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
          <button type="button" class="btn btn-primary" @click="addLanguagetoUser">
            Ekle
          </button>
        </div>
      </div>
    </div>
  </div>
  
  
  
    <!-- Update Language Modal -->
    <div class="modal fade" id="updateLanguages" tabindex="-1" aria-labelledby="updateLanguagesLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" style="max-width: 40%;">
        <div class="modal-content">
          <div class="modal-header">
            <h1 class="modal-title fs-5" id="updateLanguagesLabel">Dil Güncelle</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <div class="mb-2 d-flex align-items-center">
              <label for="editLanguageName" class="me-4 col-3">Dil:</label>
              <input type="text" class="form-control col-max" id="editLanguageName" aria-label="Dil Adı" v-model="editLanguageName"/>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
            <button type="button" class="btn btn-primary" @click="updateLanguage">Güncelle</button>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <style scoped>
  /* Flex layout for the two tables side by side */
  .d-flex {
    display: flex;
    justify-content: space-between;
  }
  
  .admin-table, .user-table {
    box-sizing: border-box;
  }
  </style>