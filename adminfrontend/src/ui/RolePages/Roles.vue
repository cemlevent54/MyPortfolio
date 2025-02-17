<script setup>
import { useRouter } from 'vue-router';
import axios from 'axios';
import { onMounted, ref } from 'vue';
import { API_URL, PROFILE_PHOTO_URL} from '../../config.js';



const router = useRouter();

const role = ref({
    id: 0,
    name: '',
    permissions: null
});

const roles = ref([]);

// CRUD's

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


const addRole = async () => {
    try {
        const roleName = document.getElementById('roleName').value;

        const response = await axios.post(`${API_URL}roles`, {
            roleName: roleName
        });

        const newRole = response.data;

        if (newRole && newRole.id) {
            // Backend'den gelen veriyi tam kullanmak için getRoles fonksiyonunu çağır
            await getRoles(); 

            // Modalı kapat
            $('#addRole').modal('hide');  
        } else {
            throw new Error('Role data is missing');
        }
    } catch (error) {
        console.error('Failed to add role: ', error);
        alert('Failed to add role');
    }
}



// Set the selected role and show the update modal
const selectRoleForUpdate = (roleToUpdate) => {
    role.value = { ...roleToUpdate }; // Ensure this updates the role object with the selected role's details
    const modal = new bootstrap.Modal(document.getElementById('updateRole')); // Initialize the modal
    modal.show(); // Show the modal
};



// Update role functionality
const updateRole = async () => {
    try {
        if (!role.value.name.trim()) {
            alert('Role name cannot be empty');
            return;
        }

        await axios.put(`${API_URL}roles/${role.value.id}`, {
            roleId: String(role.value.id),
            roleName: role.value.name.trim()
        });

        await getRoles(); // Güncellenen rolleri yeniden al

        $('#updateRole').modal('hide');
    } catch (error) {
        console.error('Failed to update role: ', error);
        alert('Failed to update role');
    }
}



const deleteRole = async (roleId) => {
    try {
        if (confirm('Are you sure you want to delete this role?')) {
            await axios.delete(`${API_URL}roles/${roleId}`);

            await getRoles(); // Silinen rolü listeden çıkarmak için tüm rolleri tekrar al
        }
    } catch (error) {
        console.error('Failed to delete role: ', error);
        alert('Failed to delete role');
    }
}




onMounted(() => {
    getRoles();
});


</script>

<template>
    <h3>Rol Yönetimi</h3>
    
    
    <br>

    <div style="font-size:16px">
        <table class="table table-striped table-responsive table-hover table-bordered text-center" style="width:100%">
            <thead class="table-dark">
                <tr class="col-md-2">
                    <th>ID</th>
                    <th>Role</th>
                    <th>İşlemler</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="role in roles" :key="role.id">
                    <td>{{role.id}}</td>
                    <td>{{role.name}}</td>
                    <td>
                        <button class="btn btn-primary btn-sm" @click="selectRoleForUpdate(role)">Güncelle</button>
                        <button class="btn btn-danger btn-sm" @click="deleteRole(role.id)">Sil</button>
                    </td>
                </tr>
            </tbody>
        </table>
        <button class="btn btn-success" data-bs-toggle="modal" data-bs-target="#addRole">Yeni Rol Ekle</button>
        
        <!-- Rol ekleme modal -->
        <div class="modal fade" id="addRole" tabindex="-1" aria-labelledby="addRoleLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" style="max-width: 40%;"> <!-- Genişliği artırmak için inline CSS kullanıldı -->
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="addRoleLabel">Yeni Rol Ekle</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="input-group mb-3">
                            <label for="roleName" class="me-4">Rol:</label>
                            <input type="text" class="form-control" id="roleName" aria-describedby="inputGroup-sizing-default" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                        <button type="button" class="btn btn-primary" @click="addRole">Ekle</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Rol güncelleme modal -->
        <div class="modal fade" id="updateRole" tabindex="-1" aria-labelledby="updateRoleLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" style="max-width: 40%;">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="updateRoleLabel">Rol Güncelle</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="input-group mb-3">
                            <label for="roleName" class="me-4">Rol:</label>
                            <input v-model="role.name" type="text" class="form-control" id="roleName" aria-label="Rol Adı" aria-describedby="inputGroup-sizing-default" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                        <button type="button" class="btn btn-primary" @click="updateRole">Güncelle</button>
                    </div>
                </div>
            </div>
        </div>
        
    </div>
</template>

<style scoped>

</style>
