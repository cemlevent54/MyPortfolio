<script setup>
import { useRouter } from 'vue-router';
import axios from 'axios';
import { onMounted, ref } from 'vue';
import { API_URL, PROFILE_PHOTO_URL} from '../../config.js';


const router = useRouter();

const projectCat = ref({
    id: 0,
    name: '',
});

const projectCats = ref([]);

// define v-models
const projectCategoryName = ref('');
const editProjectCategoryName = ref('');


// CRUD's

// GET
const getProjectCategories = async () => {
    try {
        const response = await axios.get(`${API_URL}ProjectCategory`);
        // console.log(response.data);
        projectCats.value = response.data.map((category) => {
            return {
                id: category.projectCategory_ID,
                name: category.projectCategory_Name,
            };
        });
        // console.log(projectCats.value); 

        projectCat.value = {
            id: 0,
            name: '',
        };

    } catch (error) {
        console.error('Failed to get project categories: ', error);
        alert('Failed to get project categories');    
    }
}

// POST
const addProjectCategory = async () => {
    try {
        const addProjectCategoryDTO = {
            projectCategory_Name: projectCategoryName.value,
        };
        const response = await axios.post(`${API_URL}projectcategory`, addProjectCategoryDTO);
        // console.log(response.data);

        const newProjectCategory = {
            id: response.data.projectCategory_ID,
            name: response.data.projectCategory_Name,
        };

        if (newProjectCategory && newProjectCategory.id) {
            projectCats.value.push({
                id: newProjectCategory.id,
                name: newProjectCategory.name,
            });

            projectCategoryName.value = '';

            projectCat.value = {
                id: 0,
                name: '',
            };

            const modal = bootstrap.Modal.getInstance(document.getElementById('addProjectCategory'));
            modal.hide();


            

        }
    } catch (error) {
        console.error('Failed to add project category: ', error);
        alert('Failed to add project category');
    }
}

// PUT
const projectcatid = ref(0);
const selectedProjectCategoryForUpdate = (projectCategoryToUpdate) => {
    // console.log(projectCategoryToUpdate);
    projectcatid.value = projectCategoryToUpdate.id;
    editProjectCategoryName.value = projectCategoryToUpdate.name;

    const modalElement = document.getElementById('updateProjectCategory');
    const modal = bootstrap.Modal.getOrCreateInstance(modalElement);
    modal.show();
}

const updateProjectCategory = async () => {
    try {
        const updateProjectCategoryDTO = {
            projectCategory_ID: projectcatid.value,
            projectCategory_Name: editProjectCategoryName.value,
        };
        const response = await axios.put(`${API_URL}projectcategory/${projectcatid.value}`, updateProjectCategoryDTO);
        // console.log(response.data);

        const updatedProjectCategory = {
            id: response.data.projectCategory_ID,
            name: response.data.projectCategory_Name,
        };

        if (updatedProjectCategory && updatedProjectCategory.id) {
            const index = projectCats.value.findIndex((category) => category.id === updatedProjectCategory.id);
            projectCats.value[index] = updatedProjectCategory;

            editProjectCategoryName.value = '';
            
            projectcatid.value = 0;

            const modal = bootstrap.Modal.getInstance(document.getElementById('updateProjectCategory'));
            modal.hide();
        }
    } catch (error) {
        console.error('Failed to update project category: ', error);
        alert('Failed to update project category');
    }
}

// DELETE 
const deleteProjectCategory = async (projectCategoryToDelete) => {
    const confirmDelete = confirm('Are you sure you want to delete this category?');
    if (!confirmDelete) {
        return;
    }
    // console.log(projectCategoryToDelete);
    try {
        const response = await axios.delete(`${API_URL}projectcategory/${projectCategoryToDelete.id}`);
        // console.log(response.data);

        if (response.status === 200) {
            projectCats.value = projectCats.value.filter((category) => category.id !== projectCategoryToDelete.id);
        }
        else {
            throw new Error('Failed to delete project category');
        }
    } catch (error) {
        console.error('Failed to delete project category: ', error);
        alert('Failed to delete project category');
    }
}




onMounted(() => {
    getProjectCategories();
});


</script>

<template>
    <h3>Proje Kategorileri</h3>
    
    
    <br>

    <div style="font-size:20px">
        <table class="table table-striped table-responsive table-hover table-bordered text-center" style="width:80%">
            <thead class="table-dark">
                <tr class="col-md-2">
                    <th>ID</th>
                    <th>Proje Kategorisi</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="category in projectCats" :key="category.id">
                    <td>{{ category.id }}</td>
                    <td>{{ category.name }}</td>
                    <td>
                        <button type="button" class="btn btn-primary" @click="selectedProjectCategoryForUpdate(category)">
                            Güncelle
                        </button>
                        <button type="button" class="btn btn-danger" @click="deleteProjectCategory(category)">
                            Sil
                        </button>
                    </td>

                </tr>
            </tbody>
        </table>
        
        <!-- Proje Kategorisi ekleme modal -->
        <div class="d-flex justify-content-between mt-3">
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addProjectCategory">
                Proje Kategorisi Ekle
            </button>
            

            <!-- Modal -->
            <div class="modal fade" id="addProjectCategory" tabindex="-1" aria-labelledby="addProjectCategoryLabel" aria-hidden="true" >
                <div class="modal-dialog modal-dialog-centered" style="max-width: 70%;"> <!-- Genişliği artırmak için inline CSS kullanıldı -->
                    <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="addProjectCategoryLabel">Yeni Proje Kategorisi Ekle</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="mb-2 d-flex align-items-center">
                            <label for="projectCategoryName" class="me-4 col-3">Proje Kategorisi:</label>
                            <input type="text" class="form-control col-max" id="projectCategoryName" aria-label="Proje Kategorisi Adı" 
                            aria-describedby="inputGroup-sizing-default" v-model="projectCategoryName"/>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                        <button type="button" class="btn btn-primary" @click="addProjectCategory">Ekle</button>
                    </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Teknoloji güncelleme modal -->
        <div class="d-flex justify-content-between mt-3">

            <!-- Modal -->
            <div class="modal fade" id="updateProjectCategory" tabindex="-1" aria-labelledby="updateProjectCategoryLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" style="max-width: 70%;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h1 class="modal-title fs-5" id="updateProjectCategoryLabel">Proje Kategorisi Güncelle</h1>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="mb-2 d-flex align-items-center">
                                <label for="editProjectCategoryName" class="me-4 col-3">Proje Kategorisi:</label>
                                <input type="text" class="form-control col-max" id="editProjectCategoryName" aria-label="Proje Kategorisi Adı" 
                                aria-describedby="inputGroup-sizing-default" v-model="editProjectCategoryName"/>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                            <button type="button" class="btn btn-primary" @click="updateProjectCategory">Güncelle</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        
        
    </div>
</template>

<style scoped>
thead {
    background-color: black;
    color: white;
}

</style>
