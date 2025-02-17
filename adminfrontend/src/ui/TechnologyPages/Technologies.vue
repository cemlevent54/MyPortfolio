<script setup>
import { useRouter } from 'vue-router';
import axios from 'axios';
import { onMounted, ref } from 'vue';
import { API_URL, PROFILE_PHOTO_URL} from '../../config.js';


const router = useRouter();

const technology = ref({
    id: 0,
    name: '',
    iconUrl: ''
});
const technologies = ref([]);

// define v-models
const technologyName = ref('');
const technologyIconUrl = ref('');
const editTechnologyName = ref('');
const editTechnologyIconUrl = ref('');


// CRUD's

// GET
const getTechnologies = async () => {
    try {
        const response = await axios.get(`${API_URL}technology`);
        // console.log(response.data);
        technologies.value = response.data.map((technology) => {
            return {
                id: technology.technology_ID,
                name: technology.technology_Name,
                iconUrl: technology.technology_IconUrl,
            };
        });
        // console.log(technologies);

    } catch (error) {
        console.error('Failed to get technologies: ', error);
        alert('Failed to get technologies');    
    }
}

// POST
const addTechnology = async () => {
    try {
        const addTechnologyDTO = {
            technology_Name: technologyName.value,
            technology_IconUrl: technologyIconUrl.value,
        };
        const response = await axios.post(`${API_URL}technology`, addTechnologyDTO);
        // console.log(response.data);

        const newTechnology = {
            id: response.data.technology_ID,
            name: response.data.technology_Name,
            iconUrl: response.data.technology_IconUrl,
        };

        if (newTechnology && newTechnology.id) {
            technologies.value.push({
                id: newTechnology.id,
                name: newTechnology.name,
                iconUrl: newTechnology.iconUrl,
            });

            technologyName.value = '';
            technologyIconUrl.value = '';

            technology.value = {
                id: 0,
                name: '',
                iconUrl: ''
            };

            const modal = bootstrap.Modal.getInstance(document.getElementById('addTechnology'));
            modal.hide();


            

        }
    } catch (error) {
        console.error('Failed to add technology: ', error);
        alert('Failed to add technology');
    }
}

// PUT
const techid = ref(0);
const selectedTechnologyForUpdate = (technologyToUpdate) => {
    // console.log(technologyToUpdate);
    techid.value = technologyToUpdate.id;
    editTechnologyName.value = technologyToUpdate.name;
    editTechnologyIconUrl.value = technologyToUpdate.iconUrl;

    const modalElement = document.getElementById('updateTechnology');
    const modal = bootstrap.Modal.getOrCreateInstance(modalElement);
    modal.show();
}

const updateTechnology = async () => {
    try {
        const updateTechnologyDTO = {
            technology_ID: techid.value,
            technology_Name: editTechnologyName.value,
            technology_IconUrl: editTechnologyIconUrl.value,
        };
        const response = await axios.put(`${API_URL}technology/${techid.value}`, updateTechnologyDTO);
        // console.log(response.data);

        const updatedTechnology = {
            id: response.data.technology_ID,
            name: response.data.technology_Name,
            iconUrl: response.data.technology_IconUrl,
        };

        if (updatedTechnology && updatedTechnology.id) {
            const index = technologies.value.findIndex((technology) => technology.id === updatedTechnology.id);
            technologies.value[index] = updatedTechnology;

            editTechnologyName.value = '';
            editTechnologyIconUrl.value = '';

            techid.value = 0;

            const modal = bootstrap.Modal.getInstance(document.getElementById('updateTechnology'));
            modal.hide();
        }
    } catch (error) {
        console.error('Failed to update technology: ', error);
        alert('Failed to update technology');
    }
}

// DELETE 
const deleteTechnology = async (technologyToDelete) => {
    const confirmDelete = confirm('Are you sure you want to delete this technology?');
    if (!confirmDelete) {
        return;
    }
    console.log(technologyToDelete);
    try {
        const response = await axios.delete(`${API_URL}technology/${technologyToDelete.id}`);
        // console.log(response.data);

        if (response.status === 200) {
            technologies.value = technologies.value.filter((technology) => technology.id !== technologyToDelete.id);
        }
        else {
            throw new Error('Failed to delete technology');
        }
    } catch (error) {
        console.error('Failed to delete technology: ', error);
        alert('Failed to delete technology');
    }
}



onMounted(() => {
    getTechnologies();
});


</script>

<template>
    <h3>Kullanılan Teknolojiler</h3>
    
    
    <br>

    <div style="font-size:20px">
        <table class="table table-striped table-responsive table-hover table-bordered text-center" style="width:80%">
            <thead class="table-dark">
                <tr class="col-md-2">
                    <th>ID</th>
                    <th>Teknoloji</th>
                    <th>
                        Icon Preview
                    </th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="tech in technologies" :key="tech.id">
                    <td>{{ tech.id }}</td>
                    <td>{{ tech.name }}</td>
                    <td>
                        <img :src="tech.iconUrl" alt="icon" style="width: 50px; height: 50px;border:1px solid black;" />
                    </td>
                    <td>
                        <button type="button" class="btn btn-primary" @click="selectedTechnologyForUpdate(tech)">
                            Güncelle
                        </button>
                        <button type="button" class="btn btn-danger" @click="deleteTechnology(tech)">
                            Sil
                        </button>
                    </td>

                </tr>
            </tbody>
        </table>
        
        <!-- Teknoloji ekleme modal -->
        <div class="d-flex justify-content-between mt-3">
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addTechnology">
            Ekle
            </button>

            <!-- Modal -->
            <div class="modal fade" id="addTechnology" tabindex="-1" aria-labelledby="addTechnologyLabel" aria-hidden="true" >
                <div class="modal-dialog modal-dialog-centered" style="max-width: 70%;"> <!-- Genişliği artırmak için inline CSS kullanıldı -->
                    <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="addTechnologyLabel">Yeni Teknoloji Ekle</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="mb-2 d-flex align-items-center">
                            <label for="technologyName" class="me-4 col-3">Teknoloji:</label>
                            <input type="text" class="form-control col-max" id="technologyName" aria-label="Teknoloji Adı" 
                            aria-describedby="inputGroup-sizing-default" v-model="technologyName"/>
                        </div>
                        <div class="mb-2 d-flex align-items-center">
                            <label for="technologyIconUrl" class="me-4 col-3">Teknoloji Icon Url:</label>
                            <input type="text" class="form-control col-max" id="technologyIconUrl" aria-label="Teknoloji Icon Url" 
                            aria-describedby="inputGroup-sizing-default" v-model="technologyIconUrl"/>
                        </div>
                        <!-- preview -->
                        <div class="mb-2 d-flex align-items-center">
                            <label for="technologyIconPreview" class="me-4 col-3">Icon Preview:</label>
                            <img :src="technologyIconUrl" alt="" style="width: 50px; height: 50px;border:1px solid black;" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                        <button type="button" class="btn btn-primary" @click="addTechnology">Ekle</button>
                    </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Teknoloji güncelleme modal -->
        <div class="d-flex justify-content-between mt-3">

            <!-- Modal -->
            <div class="modal fade" id="updateTechnology" tabindex="-1" aria-labelledby="updateTechnologyLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" style="max-width: 70%;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h1 class="modal-title fs-5" id="updateTechnologyLabel">Teknoloji Güncelle</h1>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="mb-2 d-flex align-items-center">
                                <label for="edittechnologyName" class="me-4 col-3">Teknoloji:</label>
                                <input type="text" class="form-control col-max" id="edittechnologyName" aria-label="Teknoloji Adı" 
                                aria-describedby="inputGroup-sizing-default" v-model="editTechnologyName"/>
                            </div>
                            <div class="mb-2 d-flex align-items-center">
                                <label for="edittechnologyIconUrl" class="me-3 col-3">Teknoloji Icon Url:</label>
                                <input type="text" class="form-control col-max" id="edittechnologyIconUrl" 
                                aria-label="Teknoloji Icon Url" aria-describedby="inputGroup-sizing-default" v-model="editTechnologyIconUrl" />
                            </div>
                            <!-- preview -->
                            <div class="mb-2 d-flex align-items-center">
                                <label for="edittechnologyIconPreview" class="me-4 col-3">Icon Preview:</label>
                                <img :src="editTechnologyIconUrl" alt="" style="width: 50px; height: 50px;border:1px solid black;" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                            <button type="button" class="btn btn-primary" @click="updateTechnology">Güncelle</button>
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
