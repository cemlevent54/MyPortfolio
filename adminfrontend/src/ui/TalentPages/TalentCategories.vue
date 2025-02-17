<script setup>
import { useRouter } from 'vue-router';
import axios from 'axios';
import { onMounted, ref } from 'vue';
import { API_URL, PROFILE_PHOTO_URL} from '../../config.js';

const router = useRouter();

const talentCat = ref({
    id: 0,
    name: '',
    iconurl: '',    
});

const talentCats = ref([]);

// define v-models
const talentCategoryName = ref('');
const editTalentCategoryName = ref('');


// CRUD's

// GET
const getTalentCategories = async () => {
    try {
        const response = await axios.get(`${API_URL}TalentCategories`);
        // console.log(response.data);
        talentCats.value = response.data.map((category) => {
            return {
                id: category.skillCategory_ID,
                name: category.skillCategory_Name,
                iconurl: category.skillCategory_IconURL,
            };
        });
        // console.log(talentCats.value); 

        talentCat.value = {
            id: 0,
            name: '',
            iconurl: '',
        };

    } catch (error) {
        console.error('Failed to get talent categories: ', error);
        alert('Failed to get talent categories');    
    }
}

// POST
const addTalentCategory = async () => {
    try {
        const addTalentCategoryDTO = {
            skillCategory_Name: talentCategoryName.value,
            skillCategory_IconURL: '',
        };

        const response = await axios.post(`${API_URL}talentcategories`,addTalentCategoryDTO);
        // console.log(response.data);

        const newTalentCategory = {
            id: response.data.skillCategory_ID,
            name: response.data.skillCategory_Name,
            iconurl: response.data.skillCategory_IconURL,
        };

        if (newTalentCategory && newTalentCategory.id) {
            talentCats.value.push({
                id: newTalentCategory.id,
                name: newTalentCategory.name,
                iconurl: newTalentCategory.iconurl,
            });

            talentCategoryName.value = '';

            talentCat.value = {
                id: 0,
                name: '',
                iconurl: '',
            };

            const modal = bootstrap.Modal.getInstance(document.getElementById('addTalentCategory'));
            modal.hide();
        }


    } catch (error) {
        console.error('Failed to add talent category: ', error);
        alert('Failed to add talent category');    
    }
}

// PUT
const talentcatid = ref(0);
const selectedTalentCategoryForUpdate = async (categoryToUpdate) => {
    // console.log(categoryToUpdate);
    talentcatid.value = categoryToUpdate.id;
    editTalentCategoryName.value = categoryToUpdate.name;

    const modalElement = document.getElementById('updateTalentCategory');
    let modal = bootstrap.Modal.getInstance(modalElement);
    if (!modal) {
        modal = new bootstrap.Modal(modalElement);
    }
    modal.show();
}

const updateTalentCategory = async () => {
    try {
        const updateTalentCategoryDTO = {
            skillCategory_ID: talentcatid.value,
            skillCategory_Name: editTalentCategoryName.value,
            skillCategory_IconURL: '',
        };

        const response = await axios.put(`${API_URL}talentcategories/${talentcatid.value}`, updateTalentCategoryDTO);
        // console.log(response.data);

        const updatedTalentCategory = {
            id: response.data.skillCategory_ID,
            name: response.data.skillCategory_Name,
            iconurl: response.data.skillCategory_IconURL,
        };

        if (updatedTalentCategory && updatedTalentCategory.id) {
            const index = talentCats.value.findIndex((category) => category.id === updatedTalentCategory.id);
            talentCats.value[index] = {
                id: updatedTalentCategory.id,
                name: updatedTalentCategory.name,
                iconurl: updatedTalentCategory.iconurl,
            };

            editTalentCategoryName.value = '';

            const modal = bootstrap.Modal.getInstance(document.getElementById('updateTalentCategory'));
            modal.hide();
        }

    } catch (error) {
        console.error('Failed to update talent category: ', error);
        alert('Failed to update talent category');    
    }
}

// DELETE
const deleteTalentCategory = async (talentCategory) => {
    const confirmDelete = confirm('Are you sure you want to delete this talent category?');
    if (!confirmDelete) {
        return;
    }

    console.log(talentCategory);

    try {
        const response = await axios.delete(`${API_URL}talentcategories/${talentCategory.id}`);
        // console.log(response.data);

        if(response.status === 200) {
            talentCats.value = talentCats.value.filter((category) => category.id !== talentCategory.id);
        }
        else {
            throw new Error('Failed to delete talent category');
        }

    } catch (error) {
        console.error('Failed to delete talent category: ', error);
        alert('Failed to delete talent category');    
    }
}

onMounted(() => {
    getTalentCategories();
});

</script>

<template>
    <h3>Yetenek Kategori</h3>
    
    
    <br>

    <div style="font-size:16px">
        <table class="table table-striped table-responsive table-hover table-bordered text-center" style="width:60%">
            <thead class="table-dark">
                <tr class="col-md-2">
                    <th>ID</th>
                    <th>Yetenek Kategori</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="category in talentCats" :key="category.id">
                    <td>{{ category.id }}</td>
                    <td>{{ category.name }}</td>
                    <td>
                        <button class="btn btn-primary btn-sm" @click="selectedTalentCategoryForUpdate(category)">Güncelle</button>
                        <button class="btn btn-danger btn-sm" @click="deleteTalentCategory(category)">Sil</button>
                    </td>
                </tr>
            </tbody>
        </table>
        
        <!-- Yetenek ekleme modal -->
        <div class="d-flex justify-content-between mt-3">
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addTalentCategory">
            Ekle
            </button>

            <!-- Modal -->
            <div class="modal fade" id="addTalentCategory" tabindex="-1" aria-labelledby="addTalentCategoryLabel" aria-hidden="true" >
            <div class="modal-dialog modal-dialog-centered" style="max-width: 40%;">
                <div class="modal-content">
                <div class="modal-header">
                <h1 class="modal-title fs-5" id="addTalentCategoryLabel">Yeni Yetenek Kategorisi Ekle</h1>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                <div class="input-group mb-3">
                    <label for="talentCategoryName" class="me-4">Yetenek Kategori:</label>
                    <input type="text" class="form-control" v-model="talentCategoryName" aria-label="Yabancı Dil Adı" aria-describedby="inputGroup-sizing-default" />
                </div>
                </div>
                <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                <button type="button" class="btn btn-primary" @click="addTalentCategory">Ekle</button>
                </div>
                </div>
            </div>
            </div>
        </div>

        <!-- Yetenek güncelleme modal -->
        <div class="d-flex justify-content-between mt-3">

            <!-- Modal -->
            <div class="modal fade" id="updateTalentCategory" tabindex="-1" aria-labelledby="updateTalentCategoryLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" style="max-width: 40%;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h1 class="modal-title fs-5" id="updateTalentCategoryLabel">Yetenek Kategorisi Güncelle</h1>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="input-group mb-3">
                                <label for="edittalentCategoryName" class="me-4">Yetenek Kategori:</label>
                                <input type="text" class="form-control" id="edittalentCategoryName"
                                 v-model="editTalentCategoryName"
                                 aria-label="Yabancı Dil Adı" aria-describedby="inputGroup-sizing-default" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                            <button type="button" class="btn btn-primary" @click="updateTalentCategory">Güncelle</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

<!-- talentcategories -->
        
        
    </div>
</template>

<style scoped>

</style>

