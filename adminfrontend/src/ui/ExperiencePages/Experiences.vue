<script setup>
import { useRouter } from 'vue-router';
import axios from 'axios';
import { onMounted, ref } from 'vue';
import { API_URL, PROFILE_PHOTO_URL} from '../../config.js';

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
  } catch (error) {
    console.error('Failed to get user info:', error);
    alert('Failed to get user info');
  }
};

const experience = ref({
    id: 0,
    userId: 0,
    title: '',
    company: '',
    about: '',
    startDate: '',
    endDate: ''
});

const experiences = ref([]);

// define v-models
const experienceTitle = ref('');
const experienceCompany = ref('');
const experienceDescription = ref('');
const educationStartDate = ref('');
const educationEndDate = ref('');
const editExperienceTitle = ref('');
const editExperienceCompany = ref('');
const editExperienceDescription = ref('');
const editEducationStartDate = ref('');
const editEducationEndDate = ref('');
const experienceId = ref(0);




// CRUD's

// GET
const getExperiences = async () => {
    try {
        const response = await axios.get(`${API_URL}experiences`);
        // console.log('Experiences:', response.data);
        experiences.value = response.data.map(data => ({
            id: data.experience_Id,
            userId: data.user_ID,
            title: data.experience_Title,
            company: data.experience_Company,
            about: data.experience_About,
            startDate: data.experience_StartDate,
            endDate: data.experience_EndDate
        }));

    } catch (error) {
        console.error('Failed to fetch experiences:', error);
        alert('Failed to fetch experiences');
    }
};

// POST
const addExperiences = async () => {
    try {
        const response = await axios.post(`${API_URL}experiences`, {
            user_ID: user.value.id,
            experience_Title: experienceTitle.value,
            experience_Company: experienceCompany.value,
            experience_About: experienceDescription.value,
            experience_StartDate: convertDateToDDMMYYYY(educationStartDate.value),
            experience_EndDate: convertDateToDDMMYYYY(educationEndDate.value),
        });
        // console.log('Experience added:', response.data);
        
        const newExperience = {
            id: response.data.experience_Id,
            userId: response.data.user_ID,
            title: response.data.experience_Title,
            company: response.data.experience_Company,
            about: response.data.experience_About,
            startDate: response.data.experience_StartDate,
            endDate: response.data.experience_EndDate
        };

        if(newExperience && newExperience.id) {
            experiences.value.push({
                id: newExperience.id,
                userId: newExperience.userId,
                title: newExperience.title,
                company: newExperience.company,
                about: newExperience.about,
                startDate: newExperience.startDate,
                endDate: newExperience.endDate
            });

            // Reset the form fields
            experienceTitle.value = '';
            experienceCompany.value = '';
            experienceDescription.value = '';
            educationStartDate.value = '';
            educationEndDate.value = '';

            const modal = bootstrap.Modal.getInstance(document.getElementById('addExperience'));
            modal.hide();
        }
    } catch (error) {
        console.error('Failed to add experience:', error);
        alert('Failed to add experience');
    }
}

// PUT 

const selectedExperienceForUpdate = (experienceToUpdate) => {
    // console.log(experienceToUpdate);
    experience.value = {...experienceToUpdate};
    experienceId.value = experienceToUpdate.id;
    editExperienceTitle.value = experienceToUpdate.title;
    editExperienceCompany.value = experienceToUpdate.company;
    editExperienceDescription.value = experienceToUpdate.about;
    editEducationStartDate.value = convertDDMMYYYYToInputDate(experienceToUpdate.startDate);
    editEducationEndDate.value = convertDDMMYYYYToInputDate(experienceToUpdate.endDate);

    const modal = new bootstrap.Modal(document.getElementById('updateExperience'));
    modal.show();
}

const updateExperience = async () => {
    try {
        // Prepare updated experience data
        const updatedExperienceData = {
            experience_Id: experience.value.id, // The ID of the experience being updated
            experience_Title: editExperienceTitle.value,
            experience_Company: editExperienceCompany.value,
            experience_About: editExperienceDescription.value,
            experience_StartDate: convertDateToDDMMYYYY(editEducationStartDate.value),
            experience_EndDate: convertDateToDDMMYYYY(editEducationEndDate.value),
            user_ID: user.value.id
        };

        // Send PUT request to update the experience
        const response = await axios.put(`${API_URL}experiences/${experience.value.id}`, updatedExperienceData);

        // Update the experiences list with the new data
        const updatedExperience = response.data;

        // Find the index of the experience in the array
        const experienceIndex = experiences.value.findIndex(exp => exp.id === updatedExperience.experience_Id);

        // Update the experience in the array
        if (experienceIndex !== -1) {
            experiences.value[experienceIndex] = {
                id: updatedExperience.experience_Id,
                userId: updatedExperience.user_ID,
                title: updatedExperience.experience_Title,
                company: updatedExperience.experience_Company,
                about: updatedExperience.experience_About,
                startDate: updatedExperience.experience_StartDate,
                endDate: updatedExperience.experience_EndDate
            };
        }

        // Close the modal after updating
        const modal = bootstrap.Modal.getInstance(document.getElementById('updateExperience'));
        modal.hide();
    } catch (error) {
        console.error('Failed to update experience:', error);
        alert('Failed to update experience');
    }
};


// DELETE 
const deleteExperience = async (experienceToDelete) => {
    const confirmDelete = confirm(`${experienceToDelete} deneyiminizi silmek istiyor musunuz? `);
    if (!confirmDelete) return;
    try {
        const response = await axios.delete(`${API_URL}experiences/${experienceToDelete.id}`);

        if(response.status === 200) {
            experiences.value = experiences.value.filter(e => e.id !== experienceToDelete.id);

        }
        else {
            throw new Error('Deneyim silinirken hata oluştu');
        }

    }catch (error) {
        console.error('Failed to delete experience:', error);
        alert('Failed to delete experience');
    }
}


function convertDateToDDMMYYYY(date) {
    if (!date) return '';

    // console.log('convertDateToDDMMYYYY input:', date); // Gelen veriyi kontrol et

    // Eğer tarih string olarak gelirse, Date objesine çevir
    const d = typeof date === 'string' ? new Date(date + 'T00:00:00Z') : new Date(date);

    if (isNaN(d)) {
        console.error('Geçersiz tarih:', date);
        return '';
    }

    const day = String(d.getUTCDate()).padStart(2, '0');
    const month = String(d.getUTCMonth() + 1).padStart(2, '0'); // Ay 0 tabanlı olduğu için +1 eklenmeli
    const year = String(d.getUTCFullYear()).padStart(4, '0'); // 4 basamaklı yıl formatı

    return `${day}.${month}.${year}`;
}

function handleDateInput(event, field) {
    const dateValue = event.target.value; // "YYYY-MM-DD" formatında gelir
    // console.log('handleDateInput received:', dateValue); // Gelen değeri kontrol et
    education.value[field] = convertDateToDDMMYYYY(dateValue);
    // console.log('Converted Date:', typeof education.value[field], ' - ', education.value[field]);
}

function convertDDMMYYYYToInputDate(dateStr) {
    if (!dateStr) return ''; // Return an empty string for invalid input
    const [day, month, year] = dateStr.split('.').map(Number); // Convert to numbers

    if (!day || !month || !year || year < 1000) return ''; // Invalid date

    // Return the date in YYYY-MM-DD format
    return `${year}-${String(month).padStart(2, '0')}-${String(day).padStart(2, '0')}`;
}





onMounted(() => {
    getLoginedUserInfo();
    getExperiences();
});

</script>

<template>
    <h3>Deneyimler</h3>
    
    
    <br>

    <div style="font-size:20px">
        <table class="table table-striped table-responsive table-hover table-bordered text-center" style="width:100%">
            <thead class="table-dark">
                <tr class="col-md-2">
                    <th>ID</th>
                    <th>Kullanıcı ID</th>
                    <th>Deneyim Adı: </th>
                    <th>Deneyim Kurum: </th>
                    <th>Deneyim Hakkında: </th>
                    <th>Deneyim Başlangıç Tarihi: </th>
                    <th>Deneyim Bitiş Tarihi: </th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="experience in experiences" :key="experience.id">
                    <td>{{ experience.id }}</td>
                    <td>{{ experience.userId }}</td>
                    <td>{{ experience.title }}</td>
                    <td>{{ experience.company }}</td>
                    <td>{{ experience.about }}</td>
                    <td>{{ experience.startDate }}</td>
                    <td>{{ experience.endDate }}</td>
                    <td>
                        <button type="button" class="btn btn-primary" @click="selectedExperienceForUpdate(experience)">
                            Güncelle
                        </button>
                        <button type="button" class="btn btn-danger" @click="deleteExperience(experience)">
                            Sil
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>
        
        <!-- Deneyim Ekle Modal -->
    <div class="d-flex justify-content-between mt-3">
      <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addExperience">
        Ekle
      </button>

      <div class="modal fade" id="addExperience" tabindex="-1" aria-labelledby="addExperienceLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" style="max-width: 60%;">
          <div class="modal-content">
            <div class="modal-header">
              <h1 class="modal-title fs-5" id="addExperienceLabel">Deneyim Ekle</h1>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <div class="mb-2 d-flex align-items-center">
                <label for="experienceTitle" class="me-4 col-3">Deneyim:</label>
                <input type="text" class="form-control col-max" id="experienceTitle" v-model="experienceTitle" />
              </div>
              <div class="mb-3 d-flex align-items-center">
                <label for="experienceCompany" class="me-4 col-3">Kurum:</label>
                <input type="text" class="form-control col-max" id="experienceCompany" v-model="experienceCompany" />
              </div>
              <div class="mb-3 d-flex align-items-center">
                <label for="experienceDescription" class="me-4 col-3">Hakkında:</label>
                <textarea class="form-control col-max" id="experienceDescription" v-model="experienceDescription" rows="4"></textarea>
              </div>
              <div class="mb-3 d-flex align-items-center">
                <label for="educationStartDate" class="me-4 col-3">Başlangıç Tarihi:</label>
                <input type="date" class="form-control col-max" id="educationStartDate" v-model="educationStartDate" />
              </div>
              <div class="mb-3 d-flex align-items-center">
                <label for="educationEndDate" class="me-4 col-3">Bitiş Tarihi:</label>
                <input type="date" class="form-control col-max" id="educationEndDate" v-model="educationEndDate" />
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
              <button type="button" class="btn btn-primary" @click="addExperiences">Ekle</button>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Deneyim Güncelle Modal -->
    <div class="modal fade" id="updateExperience" tabindex="-1" aria-labelledby="updateExperienceLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" style="max-width: 60%;">
        <div class="modal-content">
          <div class="modal-header">
            <h1 class="modal-title fs-5" id="updateExperienceLabel">Deneyim Güncelle</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <div class="mb-3 d-flex align-items-center">
              <label for="experienceTitle" class="me-4 col-3">Deneyim:</label>
              <input type="text" class="form-control col-max" id="experienceTitle" v-model="editExperienceTitle" />
            </div>
            <div class="mb-3 d-flex align-items-center">
              <label for="experienceCompany" class="me-4 col-3">Kurum:</label>
              <input type="text" class="form-control col-max" id="experienceCompany" v-model="editExperienceCompany" />
            </div>
            <div class="mb-3 d-flex align-items-center">
              <label for="experienceDescription" class="me-4 col-3">Hakkında:</label>
              <textarea class="form-control col-max" id="experienceDescription" v-model="editExperienceDescription" rows="4"></textarea>
            </div>
            <div class="mb-3 d-flex align-items-center">
              <label for="educationStartDate" class="me-4 col-3">Başlangıç Tarihi:</label>
              <input type="date" class="form-control col-max" id="educationStartDate" v-model="editEducationStartDate" />
            </div>
            <div class="mb-3 d-flex align-items-center">
              <label for="educationEndDate" class="me-4 col-3">Bitiş Tarihi:</label>
              <input type="date" class="form-control col-max" id="educationEndDate" v-model="editEducationEndDate" />
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
            <button type="button" class="btn btn-primary" @click="updateExperience">Güncelle</button>
          </div>
        </div>
      </div>
    </div>
</div>


        
        
</template>

<style scoped>

</style>
