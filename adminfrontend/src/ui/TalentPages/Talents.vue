<script setup>
import { useRouter } from 'vue-router';
import axios from 'axios';
import { onMounted, ref } from 'vue';
import { API_URL, PROFILE_PHOTO_URL, UPLOAD_PROJECT_PHOTO_URL} from '../../config.js';

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
    // getUserLanguages();
  } catch (error) {
    console.error('Failed to get user info:', error);
    alert('Failed to get user info');
  }
};

const talent = ref({
    id: 0,
    name: '',
    iconUrl: '',
    category: '',
});

const talents = ref([]);
const talentCategories = ref([]);

// define v-models

const talentName = ref('');
const talentIconUrl = ref('');
const talentIconPreview = ref('');
const talentCategory = ref('');
const edittalentName = ref('');
const edittalentIconUrl = ref('');
const edittalentIconPreview = ref('');
const edittalentCategory = ref('');

// CRUD's

const getTalents = async () => {
    try {

        const response = await axios.get(`${API_URL}talents`);
        talents.value = response.data.map((talent) => ({
            id: talent.skill_ID,
            name: talent.skill_Name,
            iconUrl: talent.skill_IconUrl,
            category: talent.skillCategory_ID,
        }));

        // console.log('Talents:', talents);




    } catch (error) {
        console.error('Failed to get talents:', error);
        alert('Failed to get talents');
    }
}

const getTalentCategories = async () => {
    try {
        const response = await axios.get(`${API_URL}talentcategories`);
        talentCategories.value = response.data.map((talentCategory) => ({
            id: talentCategory.skillCategory_ID,
            name: talentCategory.skillCategory_Name,
            iconUrl: talentCategory.skillCategory_IconUrl,
        }));

        // console.log('Talent Categories:', talentCategories);
    } catch (error) {
        console.error('Failed to get talent categories:', error);
        alert('Failed to get talent categories');
    }
}

// POST
const addTalent = async () => {
    try {
        const addTalentDTO = {
            skill_Name: talentName.value,
            skill_IconUrl: talentIconUrl.value,
            skillCategory_ID: talentCategory.value,
        };

        const response = await axios.post(`${API_URL}talents`, addTalentDTO);
        // console.log(response.data);

        const newTalent = {
            id: response.data.skill_ID,
            name: response.data.skill_Name,
            iconUrl: response.data.skill_IconUrl,
            category: response.data.skillCategory_ID,
        };

        if (newTalent && newTalent.id) {
            talents.value.push({
                id: newTalent.id,
                name: newTalent.name,
                iconUrl: newTalent.iconUrl,
                category: newTalent.category,
            });
            console.log('New talent:', newTalent);
        }

        talent.value = {
            id: 0,
            name: '',
            iconUrl: '',
            category: '',
        };

        talentName.value = '';
        talentIconUrl.value = '';
        talentCategory.value = '';
        talentIconPreview.value = '';

        const modal = document.getElementById('addTalent');
        const modalInstance = bootstrap.Modal.getInstance(modal);
        modalInstance.hide();

    } catch (error) {
        console.error('Failed to add talent:', error);
        alert('Failed to add talent');
    }
};

// PUT 
const talentid = ref(0);
const selectedTalentForUpdate = (talentToUpdate) => {
    talentid.value = talentToUpdate.id;
    edittalentName.value = talentToUpdate.name;
    edittalentIconUrl.value = talentToUpdate.iconUrl;
    edittalentCategory.value = talentToUpdate.category;

    const modal = document.getElementById('updateTalent');
    const modalInstance = new bootstrap.Modal(modal);
    modalInstance.show();
}

const updateTalent = async () => {
    try {
        const updateTalentDTO = {
            skill_ID: talentid.value,
            skill_Name: edittalentName.value,
            skill_IconUrl: edittalentIconUrl.value,
            skillCategory_ID: edittalentCategory.value,
        };

        const response = await axios.put(`${API_URL}talents/${talentid.value}`, updateTalentDTO);
        // console.log(response.data);

        const updatedTalent = {
            id: response.data.skill_ID,
            name: response.data.skill_Name,
            iconUrl: response.data.skill_IconUrl,
            category: response.data.skillCategory_ID,
        };

        if (updatedTalent && updatedTalent.id) {
            const index = talents.value.findIndex((talent) => talent.id === updatedTalent.id);
            talents.value[index] = {
                id: updatedTalent.id,
                name: updatedTalent.name,
                iconUrl: updatedTalent.iconUrl,
                category: updatedTalent.category,
            };
        }

        talent.value = {
            id: 0,
            name: '',
            iconUrl: '',
            category: '',
        };

        const modal = document.getElementById('updateTalent');
        const modalInstance = bootstrap.Modal.getInstance(modal);
        modalInstance.hide();

    } catch (error) {
        console.error('Failed to update talent:', error);
        alert('Failed to update talent');
    }
}

// DELETE
const deleteTalent = async (talentToDelete) => {
    const confirmDelete = confirm('Are you sure you want to delete this talent?');
    if (!confirmDelete) {
        return;
    }

    try {
        const response = await axios.delete(`${API_URL}talents/${talentToDelete.id}`);
        // console.log(response.data);

        if (response.status === 200) {
            talents.value = talents.value.filter((talent) => talent.id !== talentToDelete.id);
        }
        else {
            throw new Error('Failed to delete talent');
        }

    } catch (error) {
        console.error('Failed to delete talent:', error);
        alert('Failed to delete talent');
    }
}


onMounted(() => {
  getLoginedUserInfo().then(() => {
    getUserTalents();
    getTalents();
    getTalentCategories();
  });
});

// User Talent CRUD's

// GET
const userTalents = ref([]);

const getUserTalents = async () => {
    try {
        // console.log('User:', user.value.id);
        const response = await axios.get(`${API_URL}talents/talent/${user.value.id}`);
        // console.log('User talents:', response.data);
        userTalents.value = response.data.map((talent) => ({
            id: talent.skill_ID,
            name: talent.skill_Name,
            iconUrl: talent.skill_IconUrl,
            category: talent.skillCategory_ID,
        }));
        // console.log('User talents fetched successfully:', userTalents.value);
    } catch (error) {
        console.error('Failed to fetch user talents:', error);
    }
};

// POST
const addTalentToUser = async () => {
    try {
        const selectedTalents = Array.from(document.querySelectorAll('input[name=talentCheckbox]:checked'))
        .map((checkbox) => {
            return { skill_ID: parseInt(checkbox.value) };
        });

        if (selectedTalents.length === 0) {
            alert('Please select at least one talent to add');
            return;
        }

        const response = await axios.post(`${API_URL}talents/talent/${user.value.id}`, {
            userid: user.value.id,
            talentItemsForUser: selectedTalents,
        });

        if ( response.status === 200) {
            selectedTalents.forEach(s => {
                const h = talents.value.find(t => t.id === s.skill_ID);
                if (h && !userTalents.value.some(ut => ut.id === h.id)) {
                    userTalents.value.push(h);
                }
            });

            const modal = bootstrap.Modal.getInstance(document.getElementById('addUserTalent'));
            modal.hide();

        }
        else {
            throw new Error('Failed to add talents to user');
        }

        

    } catch (error) {
        console.error('Failed to add talent to user:', error);
        alert('Failed to add talent to user');
    }
}

// DELETE
const removeTalentFromUser = async (talentToRemove) => {
    const confirmDelete = confirm('Are you sure you want to remove this talent from user?');
    if (!confirmDelete) {
        return;
    }

    try {
        const deleteTalentid = talentToRemove.id;
        

        const deleteTalentDto = {
            skill_ID: deleteTalentid,
        };
        const uid = user.value.id;
        // console.log('User ID:', uid);

        const response = await axios.delete(`${API_URL}talents/delete/${user.value.id}`, {
            headers : {
                'Content-Type': 'application/json',
            },
            data: deleteTalentDto,
        });
        // console.log(response.data);

        


        if (response.status === 200) {
            userTalents.value = userTalents.value.filter((talent) => talent.id !== talentToRemove.id);
        }
        else {
            console.log('Failed to remove talent from user',response);
            throw new Error('Failed to remove talent from user');
        }

    } catch (error) {
        console.error('Failed to remove talent from user:', error);
        alert('Failed to remove talent from user');
    }
}

const navigateToPlatform = () => {
    router.push('/talentcategories'); // Navigate to the defined route
};
</script>

<template>
    <div class="d-flex">
        <!-- Admin Talent Table on the left -->
        <div class="admin-table" style="width: 50%; padding-right: 10px;">
        <h3>Yetenekler</h3>
        <table class="table table-striped table-responsive table-hover table-bordered text-center">
            <thead class="table-dark">
            <tr class="col-md-2">
                <th>ID</th>
                <th>Yetenek</th>
                <th>Icon</th>
                <th>Kategori</th>
                <th>Actions</th>
            </tr>
            </thead>
            <tbody>
            <tr v-for="talent in talents" :key="talent.id">
                <td>{{ talent.id }}</td>
                <td>{{ talent.name }}</td>
                <td><img :src="talent.iconUrl" style="width: 50px; height: 50px;"/></td>
                <td>{{ talentCategories.find(category => category.id === talent.category)?.name }}</td>
                <td>
                <button class="btn btn-primary btn-sm" @click="selectedTalentForUpdate(talent)">Güncelle</button>
                <button class="btn btn-danger btn-sm" @click="deleteTalent(talent)">Delete</button>
                </td>
            </tr>
            </tbody>
        </table>
        </div>

        <!-- User Talent Table on the right -->
        <div class="user-table" style="width: 45%; padding-left: 10px;">
        <h3>Kullanıcı Yetenekleri</h3>
            <table class="table table-striped table-responsive table-hover table-bordered text-center">
                <thead class="table-dark">
                <tr class="col-md-2">
                    <th>Yetenek</th>
                    <th>Kategori</th>
                    <th>Actions</th>
                </tr>
                </thead>
                <tbody>
                <tr v-for="talent in userTalents" :key="talent.id">
                    <td>{{ talent.name }}</td>
                    <td>{{ talentCategories.find(category => category.id === talent.category)?.name }}</td>
                    <td>
                    <button class="btn btn-danger btn-sm" @click="removeTalentFromUser(talent)">Delete</button>
                    </td>
                </tr>
                </tbody>
            </table>

                <!-- Add Talent Button for User -->
                <div class="d-flex justify-content-between mt-3">
                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addUserTalent">
                    Yetenek Ekle
                    </button>
                </div>
            </div>
    </div>
    
    <div>
        <!-- Yetenek ekleme modal -->
        <div class="d-flex justify-content-between mt-3">
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addTalent">
            Ekle
            </button>

            

            <!-- Modal -->
            <div class="modal fade" id="addTalent" tabindex="-1" aria-labelledby="addTalentLabel" aria-hidden="true" >
                <div class="modal-dialog modal-dialog-centered" style="max-width: 50%;"> <!-- Genişliği artırmak için inline CSS kullanıldı -->
                    <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="addTalentLabel">Yetenek Ekle</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="mb-3 d-flex align-items-center">
                            <label for="talentName" class="me-4 col-3">Yetenek:</label>
                            <input type="text" class="form-control col-max" id="talentName" aria-label="Yetenek Adı" aria-describedby="inputGroup-sizing-default" v-model="talentName"/>
                        </div>
                        
                        <div class="mb-3 d-flex align-items-center">
                            <label for="talentIconUrl" class="me-4 col-3">Yetenek Icon Url:</label>
                            <input type="text" class="form-control col-max" id="talentIconUrl" v-model="talentIconUrl" aria-label="Yetenek Icon Url" aria-describedby="inputGroup-sizing-default" />
                        </div>
                        <div class="mb-3 d-flex align-items-center">
                            <label for="talentIconPreview" class="me-4 col-3">Yetenek Icon Preview:</label>
                            <img :src="talentIconUrl" style="width: 50px; height: 50px;border:1px solid black" />
                        </div>
                        <div class="mb-3 d-flex align-items-center">
                            <label for="talentCategory" class="me-4 col-3">Yetenek Kategori:</label>
                            <select class="form-select" id="talentCategory" v-model="talentCategory" aria-label="Yetenek Kategori">
                                <option v-for="category in talentCategories" :key="category.id" :value="category.id">{{ category.name }}</option>
                            </select>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                        <button type="button" class="btn btn-primary" @click="addTalent">Ekle</button>
                    </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Yetenek güncelleme modal -->
        <div class="d-flex justify-content-between mt-3">

            <!-- Modal -->
            <div class="modal fade" id="updateTalent" tabindex="-1" aria-labelledby="updateTalentLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" style="max-width: 50%;">
                <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="updateTalentLabel">Yetenek Güncelle</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3 d-flex align-items-center">
                        <label for="edittalentName" class="me-4 col-3">Yetenek:</label>
                        <input type="text" class="form-control col-max" id="edittalentName" v-model="edittalentName" aria-label="Yetenek Adı" aria-describedby="inputGroup-sizing-default" />
                    </div>
                    
                    <div class="mb-3 d-flex align-items-center">
                        <label for="edittalentIconUrl" class="me-4 col-3">Yetenek Icon Url:</label>
                        <input type="text" class="form-control col-max" id="edittalentIconUrl" v-model="edittalentIconUrl" aria-label="Yetenek Icon Url" aria-describedby="inputGroup-sizing-default" />
                    </div>
                    <div class="mb-3 d-flex align-items-center">
                        <label for="edittalentIconPreview" class="me-4 col-3">Yetenek Icon Preview:</label>
                        <img :src="edittalentIconUrl" style="width: 50px; height: 50px;border:1px solid black" />
                    </div>
                    <div class="mb-3 d-flex align-items-center">
                        <label for="edittalentCategory" class="me-4 col-3">Yetenek Kategori:</label>
                        <select class="form-select" id="edittalentCategory" v-model="edittalentCategory" aria-label="Yetenek Kategori">
                            <option v-for="category in talentCategories" :key="category.id" :value="category.id">{{ category.name }}</option>
                        </select>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                    <button type="button" class="btn btn-primary" @click="updateTalent">Güncelle</button>
                </div>
                </div>
            </div>
            </div>
        </div>

        <!-- addUserTalent -->
        <div class="modal fade" id="addUserTalent" tabindex="-1" aria-labelledby="addUserTalentLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" style="max-width: 40%;">
            <div class="modal-content">
            <div class="modal-header">
                <h1 class="modal-title fs-5" id="addUserTalentLabel">Kullanıcı Yetenek Ekle</h1>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <div>
                <div v-for="talent in talents.filter(talent => !userTalents.some(userTalent => userTalent.id === talent.id))" :key="talent.id">
                <!-- Show checkboxes only for unselected talents -->
                <input 
                    type="checkbox" 
                    :id="'checkbox-' + talent.id" 
                    :value="talent.id" 
                    name="talentCheckbox"
                />
                <label :for="'checkbox-' + talent.id">{{ talent.name }}</label>
                </div>
                </div>
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                <button type="button" class="btn btn-primary" @click="addTalentToUser">
                Ekle
                </button>
            </div>
            </div>
            </div>
        </div>
    </div>
</template>

<style scoped>

</style>
