<script setup>
import { useRouter } from 'vue-router';
import axios from 'axios';
import { onMounted, ref } from 'vue';
import { API_URL, PROFILE_PHOTO_URL} from '../../config.js';


// Sample data for hobbies 
const samplehobbies = ref([
  { id: 1, name: 'Kitap Okumak', iconUrl: 'path/to/icon1.png' },
  { id: 2, name: 'Kod Yazmak', iconUrl: 'path/to/icon2.png' },
  { id: 3, name: 'Gitar Çalmak', iconUrl: 'path/to/icon3.png' },
]);

// Sample data for logged-in user hobbies
const sampleuserHobbies = ref([
  { id: 1, name: 'Kitap Okumak' },
  { id: 3, name: 'Gitar Çalmak' },
]);

// define v-models
const hobbieName = ref('');
const hobbieIcon = ref('');
const edithobbieName = ref('');
const edithobbieIcon = ref('');

// Methods to handle hobby actions (e.g., delete)
const removeHobby = (hobbyId) => {
  const index = sampleuserHobbies.value.findIndex(hobby => hobby.id === hobbyId);
  if (index !== -1) {
    sampleuserHobbies.value.splice(index, 1);
  }
};

const addUserHobby = () => {
  const selectedHobbies = Array.from(document.querySelectorAll('input[name="hobbyCheckbox"]:checked')).map(checkbox => {
    const hobbyId = parseInt(checkbox.value);
    return samplehobbies.value.find(hobby => hobby.id === hobbyId);
  });

  selectedHobbies.forEach(newHobby => {
    if (newHobby && !sampleuserHobbies.value.some(hobby => hobby.id === newHobby.id)) {
        sampleuserHobbies.value.push(newHobby);
    }
  });
};

/// samples end

const router = useRouter();
// login olan kullanıcının bilgileri
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
    getUserHobbies();
  } catch (error) {
    console.error('Failed to get user info:', error);
    alert('Failed to get user info');
  }
};

const hobbie = ref({
    name: '',
    iconUrl: '',
});

const hobbies = ref([]);
const hobbyid = ref(0);

// CRUD's

// GET
const getHobbies = async () => {
    try {
        const response = await axios.get(`${API_URL}hobbies`);
        // console.log(response.data);
        hobbies.value = response.data.map(hobby => ({
            id: hobby.hobby_ID,
            name: hobby.hobby_Name,
            iconUrl: hobby.hobby_IconUrl,
        }));


    } catch (error) {
        console.error('Failed to get hobbies:', error);
        alert('Failed to get hobbies');
    }
};

// POST
const addHobby = async () => {
    try {
        const response = await axios.post(`${API_URL}hobbies`, {
            hobby_Name: hobbieName.value,
            hobby_IconUrl: hobbieIcon.value,
        });
        // console.log(response.data);

        const newHobby = {
            id: response.data.hobby_ID,
            name: response.data.hobby_Name,
            iconUrl: response.data.hobby_IconUrl,
        };

        if (newHobby && newHobby.id) {
            hobbies.value.push({
                id: newHobby.id,
                name: newHobby.name,
                iconUrl: newHobby.iconUrl,
            });

            // Clear the input fields
            hobbieName.value = '';
            hobbieIcon.value = '';
            
            const modal = bootstrap.Modal.getInstance(document.getElementById('addHobby'));
            modal.hide();
        }

    } catch (error) {
        console.error('Failed to add hobby:', error);
        alert('Failed to add hobby');
    }
};

// PUT
const selectedHobbyForUpdate = (hobbyToUpdate) => {
    // console.log(hobbyToUpdate);
    hobbyid.value = hobbyToUpdate.id;
    edithobbieName.value = hobbyToUpdate.name;
    edithobbieIcon.value = hobbyToUpdate.iconUrl;

    const modal = new bootstrap.Modal(document.getElementById('updateHobby'));
    modal.show();
}

const updateHobby = async () => {
    try {

        const updatedHobbyData = {
            hobby_ID: hobbyid.value,
            hobby_Name: edithobbieName.value,
            hobby_IconUrl: edithobbieIcon.value,
        }

        const response = await axios.put(`${API_URL}hobbies/${hobbyid.value}`, updatedHobbyData);

        const updatedHobby = response.data;

        const hobbyIndex = hobbies.value.findIndex(hobby => hobby.id === updatedHobby.hobby_ID);

        if (hobbyIndex !== -1) {
            hobbies.value[hobbyIndex] = {
                id: updatedHobby.hobby_ID,
                name: updatedHobby.hobby_Name,
                iconUrl: updatedHobby.hobby_IconUrl,
            };
        }

        const modal = bootstrap.Modal.getInstance(document.getElementById('updateHobby'));
        modal.hide();

    }catch (error) {
        console.error('Failed to update hobby:', error);
        alert('Failed to update hobby');
    }
}

// DELETE
const deleteHobby = async (hobbyToDelete) => {
    const confirmDelete = confirm('Are you sure you want to delete this hobby?');
    if(!confirmDelete) {
        return;
    }
    try {
        const response = await axios.delete(`${API_URL}hobbies/${hobbyToDelete.id}`);
        if(response.status === 200) {
            hobbies.value = hobbies.value.filter(hobby => hobby.id !== hobbyToDelete.id);
        }
        else {
            throw new Error('Failed to delete hobby');
        }

    }catch (error) {
        console.error('Failed to delete hobby:', error);
        alert('Failed to delete hobby');
    }
}
onMounted(() => {
    getLoginedUserInfo();
    getHobbies();
    // getUserHobbies();
});

// User Hobby CRUD's
const userHobby = ref({
    user_ID: user.value.id,
    hobby_ID: 0,
});
const userHobbies = ref([]);


// GET
const getUserHobbies = async () => {
    try {
        const userid = user.value.id;
        // console.log(userid);
        // console.log(`${API_URL}hobbies/hobbie/${userid}`);
        const response = await axios.get(`${API_URL}hobbies/hobbie/${user.value.id}`);
        // console.log(response.data);
        userHobbies.value = response.data.map(userHobby => ({
            id: userHobby.hobby_ID,
            name: userHobby.hobby_Name,
        }));

    } catch (error) {
        console.error('Failed to get user hobbies:', error);
        alert('Failed to get user hobbies');
    }
};

// POST
const addHobbytoUser = async () => {
    try {
        const selectedHobbies = Array.from(document.querySelectorAll('input[name="hobbyCheckbox"]:checked'))
        .map(checkbox => {
          return { hobby_ID: parseInt(checkbox.value) }
        });

        // console.log(selectedHobbies);

        if (selectedHobbies.length === 0) {
            alert('Please select at least one hobby to add');
            return;
        }
        
        const uid = user.value.id;
        const response = await axios.post(`${API_URL}hobbies/${uid}`,{
            user_id: uid,
            HobbyItemsForUser: selectedHobbies,
        });

        if(response.status === 200) {
          selectedHobbies.forEach(s => {
            const h = hobbies.value.find(h => h.id === s.hobby_ID);
            if(h && !userHobbies.value.some(userHobby => userHobby.id === h.id)) {
              userHobbies.value.push(h);
            }
          });

          const modal = bootstrap.Modal.getInstance(document.getElementById('addUserHobby'));
          modal.hide();
        } else {
          throw new Error('Failed to add hobby to user');
        }


    } catch (error) {
        console.error('Failed to add hobby to user:', error);
        alert('Failed to add hobby to user');
    }

}

// DELETE 
const removeHobbyFromUser = async (userHobbyToDelete) => {
    // Kullanıcıdan onay alalım
    const confirmDelete = window.confirm('Are you sure you want to remove this hobby from the user?');
    if (!confirmDelete) {
        return; // Kullanıcı onay vermediyse, işlemi durduruyoruz
    }

    try {
        const deleteHobbyid = userHobbyToDelete.id;
        // console.log(`Deleting hobby with ID: ${deleteHobbyid}`);

        const deletehobbydto = {
            Hobby_ID: deleteHobbyid, // Backend'deki isimle uyumlu olmalı
        };

        const uid = user.value.id;
        console.log(`User ID: ${uid}`);

        // API'ye DELETE isteği gönderiyoruz
        const response = await axios.delete(`${API_URL}hobbies/delete/${uid}`, {
            headers: {
                'Content-Type': 'application/json', // Verinin JSON formatında gönderileceğini belirtiyoruz
            },
            data: deletehobbydto, // Gönderdiğimiz veri
        });

        // console.log('API Response:', response);

        // Silme işlemi başarılıysa, kullanıcı hobisinden kaldırıyoruz
        if (response.status === 200) {
            userHobbies.value = userHobbies.value.filter(hobby => hobby.id !== userHobbyToDelete.id);
            
        } else {
            console.log('Failed to remove hobby from user:', response.status);
            throw new Error('Failed to remove hobby from user');
        }

    } catch (error) {
        console.error('Failed to remove hobby from user:', error);
        alert('Failed to remove hobby from user');
    }
};







</script>

<template>
  

  <div class="d-flex">
    <!-- Admin Hobby Table on the left -->
    <div class="admin-table" style="width: 50%; padding-right: 10px;">
      <h3>Hobiler</h3>
      <table class="table table-striped table-responsive table-hover table-bordered text-center">
        <thead class="table-dark">
          <tr class="col-md-2">
            <th>ID</th>
            <th>Hobi</th>
            <th>Icon</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="hobby in hobbies" :key="hobby.id">
            <td>{{ hobby.id }}</td>
            <td>{{ hobby.name }}</td>
            <td>{{ hobby.iconUrl }}</td>
            <td>
              <button class="btn btn-primary btn-sm" @click="selectedHobbyForUpdate(hobby)">Güncelle</button>
              <button class="btn btn-danger btn-sm" @click="deleteHobby(hobby)">Delete</button>
            </td>
          </tr>
        </tbody>
      </table>

      <!-- Add Hobby Modal -->
      <div class="d-flex justify-content-between mt-3">
        <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addHobby">
          Ekle
        </button>
      </div>
    </div>

    <!-- User Hobby Table on the right -->
    <div class="user-table" style="width: 45%; padding-left: 10px;">
      <h3>Kullanıcı Hobileri</h3>
      <table class="table table-striped table-responsive table-hover table-bordered text-center">
        <thead class="table-dark">
          <tr class="col-md-2">
            <th>Hobi</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="hobby in userHobbies" :key="hobby.id">
            <td>{{ hobby.name }}</td>
            <td>
              <button class="btn btn-danger btn-sm" @click="removeHobbyFromUser(hobby)">Delete</button>
            </td>
          </tr>
        </tbody>
      </table>

      <!-- Add Hobby Button for User -->
      <div class="d-flex justify-content-between mt-3">
        <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addUserHobby">
          Hobi Ekle
        </button>
      </div>
    </div>
  </div>

  <!-- Add Hobby Modal -->
  <div class="modal fade" id="addHobby" tabindex="-1" aria-labelledby="addHobbyLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" style="max-width: 40%;">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="addHobbyLabel">Yeni Hobi Ekle</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div class="mb-2 d-flex align-items-center">
            <label for="hobbieName" class="me-4 col-3">Hobi: </label>
            <input type="text" class="form-control col-max" id="hobbieName" 
            aria-label="Hobi Adı" v-model="hobbieName"/>
          </div>
          <div class="mb-2 d-flex align-items-center">
            <label for="hobbieIcon" class="me-4 col-3">Icon: </label>
            <!-- hobbie icon will be link -->
            <input type="text" class="form-control col-max" id="hobbieIcon" v-model="hobbieIcon">
          </div>
            
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
          <button type="button" class="btn btn-primary" @click="addHobby">Ekle</button>
        </div>
      </div>
    </div>
  </div>

<!-- Add Hobby Modal for User -->
<div class="modal fade" id="addUserHobby" tabindex="-1" aria-labelledby="addUserHobbyLabel" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" style="max-width: 40%;">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="addUserHobbyLabel">Kullanıcı Hobi Ekle</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <div>
            <div v-for="hobby in hobbies.filter(hobby => !userHobbies.some(userHobby => userHobby.id === hobby.id))" :key="hobby.id">
            <!-- Show checkboxes only for unselected hobbies -->
            <input 
                type="checkbox" 
                :id="'checkbox-' + hobby.id" 
                :value="hobby.id" 
                name="hobbyCheckbox"
            />
            <label :for="'checkbox-' + hobby.id">{{ hobby.name }}</label>
            </div>
        </div>
      </div>

      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
        <button type="button" class="btn btn-primary" @click="addHobbytoUser">
          Ekle
        </button>
      </div>
    </div>
  </div>
</div>



  <!-- Update Hobby Modal -->
  <div class="modal fade" id="updateHobby" tabindex="-1" aria-labelledby="updateHobbyLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" style="max-width: 40%;">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="updateHobbyLabel">Hobi Güncelle</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div class="mb-2 d-flex align-items-center">
            <label for="edithobbieName" class="me-4 col-3">Hobi:</label>
            <input type="text" class="form-control col-max" id="edithobbieName" aria-label="Hobi Adı" v-model="edithobbieName"/>
          </div>
          <div class="mb-2 d-flex align-items-center">
            <label for="edithobbieIcon" class="me-4 col-3">Icon: </label>
            <!-- hobbie icon will be link -->
            <input type="text" class="form-control col-max" id="edithobbieIcon" v-model="edithobbieIcon">
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
          <button type="button" class="btn btn-primary" @click="updateHobby">Ekle</button>
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
