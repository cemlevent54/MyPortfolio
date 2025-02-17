<script setup>
import { ref, onMounted, computed  } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';
import { API_URL, PROFILE_PHOTO_URL} from '../../config.js';

const router = useRouter();

const navigateToPlatform = () => {
    router.push('/educations/educationcategories');
};

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

const education = ref({
    id: 0,
    userId: 0,
    educationcategory: '',
    education: '',
    educationOrganization: '',
    educationSubject: '',
    educationStartDate: '',
    educationEndDate: '',
    educationCertificateUrl: ''
});

const educationCategory = ref('');
const educationStartDate = ref('');
const educationEndDate = ref('');


// define edit's
const editEducationCategory = ref('');
const editEducationTitle = ref('');
const editeducationCategory = ref('');
const editEducationOrganization = ref('');
const editEducationSubject = ref('');
const editEducationStartDate = ref('');
const editEducationEndDate = ref('');
const editEducationCertificateUrl = ref('');



// education reactive list 
const educations = ref([]);
const educationCategories = ref([]);

// CRUD operations

// GET Education Categories for combobox
const getEducationCategories = async () => {
    try {
        const response = await axios.get(`${API_URL}educationcategories`);
        // console.log('response: ', response.data);
        educationCategories.value = response.data.map(data => ({
            id: data.educationCategory_ID,
            name: data.educationCategory_Name
        }));

    } catch (error) {
        console.error('Failed to get education categories: ', error);
        alert('Failed to get education categories');
    }
}

// GET
const getEducations = async () => {
    try {
        const response = await axios.get(`${API_URL}educations`);
        // console.log('response: ', response.data);
        educations.value = response.data.map(data => ({
            id: data.education_ID,
            userId: data.user_ID,
            educationcategory: data.educationCategory_ID,
            education: data.education_Title,
            educationOrganization: data.education_Organization,
            educationSubject: data.education_Subject,
            educationStartDate: data.education_StartDate,
            educationEndDate: data.education_EndDate,
            educationCertificateUrl: data.education_CertificationUrl
        }));

    } catch (error) {
        console.error('Failed to get educations: ', error);
        alert('Failed to get educations');
    }
}

// POST
const addEducation = async () => {
    try {
        // console.log(user.value.id)
        const educationcategory = document.getElementById('educationCategory').value;
    
        const education = document.getElementById('educationTitle').value;
        const educationOrganization = document.getElementById('educationOrganization').value;
        const educationSubject = document.getElementById('educationSubject').value;
        const educationStartDate = document.getElementById('educationStartDate').value;
        const educationEndDate = document.getElementById('educationEndDate').value;
        const educationCertificateUrl = document.getElementById('educationCertificateUrl').value;
        
        const formattedStartDate = convertDateToDDMMYYYY(educationStartDate);
        const formattedEndDate = convertDateToDDMMYYYY(educationEndDate);

        // console.log(educationcategory);
        // console.log(education);
        // console.log(educationOrganization);
        // console.log(educationSubject);
        // console.log(educationStartDate, ' - ', educationEndDate);
        // console.log(typeof(educationStartDate));
        // console.log(formattedStartDate, ' - ', formattedEndDate);
        // console.log(typeof(formattedStartDate));
        
        if (!educationcategory.trim() || !education.trim() || !educationOrganization.trim() || !educationSubject.trim() || !educationStartDate.trim() || !educationEndDate.trim() || !educationCertificateUrl.trim()) {
            alert('Tüm alanlar zorunludur!');
            return;
        }

        

        const response = await axios.post(`${API_URL}educations`, {
            user_ID: user.value.id,
            educationCategory_ID: educationcategory,
            education_Title: education,
            education_Organization: educationOrganization,
            education_Subject: educationSubject,
            education_StartDate: formattedStartDate,
            education_EndDate: formattedEndDate,
            education_CertificationUrl: educationCertificateUrl
        });

        const newEducation = response.data;

        if (newEducation && newEducation.education_ID) {
            // Yeni eğitimi educations listesine ekle
            educations.value.push({
                id: newEducation.education_ID,
                userId: newEducation.user_ID,
                educationcategory: newEducation.educationCategory_ID,
                education: newEducation.education_Title,
                educationOrganization: newEducation.education_Organization,
                educationSubject: newEducation.education_Subject,
                educationStartDate: newEducation.education_StartDate,
                educationEndDate: newEducation.education_EndDate,
                educationCertificateUrl: newEducation.education_CertificationUrl
            });

            // Input alanını temizle
            document.getElementById('educationCategory').value = '';
            document.getElementById('educationTitle').value = '';
            document.getElementById('educationOrganization').value = '';
            document.getElementById('educationSubject').value = '';
            document.getElementById('educationStartDate').value = '';
            document.getElementById('educationEndDate').value = '';
            document.getElementById('educationCertificateUrl').value = '';



            // Bootstrap modalı kapat
            const modal = bootstrap.Modal.getInstance(document.getElementById('addEducation'));
            modal.hide();
        }

    } catch (error) {
        console.error('Failed to add education: ', error);
        alert('Failed to add education');
    }
}

// DELETE
const deleteEducation = async (educationToDelete) => {
    const confirmDelete = window.confirm(`${educationToDelete} eğitimini silmek istiyor musunuz? `);

    if(!confirmDelete) {
        return;
    }

    try {
        const response = await axios.delete(`${API_URL}educations/${educationToDelete.id}`);

        if(response.status === 200) {
            educations.value = educations.value.filter(education => education.id !== educationToDelete.id);

        }
        else {
            throw new Error('Eğitim silinirken hata oluştu');
        }

    }catch(error) {
        console.error('Failed to delete education: ',error);
        alert('Eğitim silinirken hata oluştu');

    }
}


// PUT

// Fill update modal with selected education data
const selectedEducationForUpdate = (educationToUpdate) => {
    education.value = { ...educationToUpdate };
    // console.log(education.value);
    editEducationCategory.value = education.value.educationcategory;
    editEducationTitle.value = education.value.education;
    editEducationOrganization.value = education.value.educationOrganization;
    editEducationSubject.value = education.value.educationSubject;
    editEducationStartDate.value = convertDDMMYYYYToInputDate(education.value.educationStartDate);
    // console.log(editEducationStartDate.value);
    editEducationEndDate.value = convertDDMMYYYYToInputDate(education.value.educationEndDate);
    editEducationCertificateUrl.value = education.value.educationCertificateUrl;

    const modal = new bootstrap.Modal(document.getElementById('updateEducation'));
    modal.show();
}

const updateEducation = async () => {
    try {
        const updatedDatas = {
            education_Title: editEducationTitle.value,
            education_Organization: editEducationOrganization.value,
            education_Subject: editEducationSubject.value,
            education_StartDate: convertDateToDDMMYYYY(editEducationStartDate.value),
            education_EndDate: convertDateToDDMMYYYY(editEducationEndDate.value),
            education_CertificationUrl: editEducationCertificateUrl.value,
            user_ID: user.value.id,
            educationCategory_ID: editEducationCategory.value,
        };

        const eduid = education.value.id;
        // console.log(eduid);
        // console.log(updatedDatas);

        // PUT isteği gönderiyoruz
        const response = await axios.put(`${API_URL}educations/${eduid}`, updatedDatas);

        const updateDatas = response.data;
        // console.log(updateDatas);

        if (updateDatas && eduid) {
            // Find the index of the education in the educations list
            // Directly update the education data in the educations list
            const updatedEducations = educations.value.map(education => {
                if (education.id === eduid) {
                    return {
                        ...education, // Preserve the existing properties
                        ...updateDatas // Update with new data
                    };
                }
                return education; // Keep the rest unchanged
            });

            educations.value = updatedEducations; // Reassign updated list to trigger reactivity
            window.location.reload();
            // Close the modal
            const modalElement = document.getElementById('updateEducation');
            const modalInstance = bootstrap.Modal.getInstance(modalElement) || new bootstrap.Modal(modalElement);
            modalInstance.hide();
        } else {
            throw new Error('Eğitim güncellenirken hata oluştu');
        }
    } catch (error) {
        console.error('Failed to update education: ', error);
        alert("Eğitim güncellenirken hata oluştu.");
    }
}



// don't touch
// for datetime
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

// don't touch
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
    getEducations();
    getEducationCategories();
    getLoginedUserInfo();
});
</script>

<template>
    <h3>Eğitimler</h3>
    
    
    <br>

    <div style="font-size:15px">
        <table class="table table-striped table-responsive table-hover table-bordered text-center" style="width:100%">
            <thead class="table-dark">
                <tr class="col-md-2">
                    <th>ID</th>
                    <th>User ID</th>
                    <th>Eğitim Adı</th>
                    <th>Eğitim Kategori</th>
                    <th>Eğitim Org.</th>
                    <th>Eğitim Subject</th>
                    <th>Başlangıç Tarihi</th>
                    <th>Bitiş Tarihi</th>
                    <th>Sertifika Url</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="education in educations" :key="education.id">
                    <td>{{ education.id }}</td>
                    <td>{{ education.userId }}</td>
                    <td>{{ education.education }}</td>
                    <td>{{ education.educationcategory }}</td>
                    <td>{{ education.educationOrganization }}</td>
                    <td>{{ education.educationSubject }}</td>
                    <td>{{ education.educationStartDate }}</td>
                    <td>{{ education.educationEndDate }}</td>
                    <td>{{ education.educationCertificateUrl }}</td>
                    <td>
                        <button type="button" class="btn btn-secondary" @click="selectedEducationForUpdate(education)">Güncelle</button>
                        <button type="button" class="btn btn-danger" @click="deleteEducation(education)">Sil</button>
                    </td>
                </tr>
            </tbody>
        </table>
        
        <!-- Eğitim ekleme modal -->
    <div class="d-flex justify-content-between mt-3">
        <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addEducation">
            Ekle
        </button>
        <button type="button" class="btn btn-success" @click="navigateToPlatform">Eğitim Kategorisi ekle</button>

        <!-- Modal -->
        <div class="modal fade" id="addEducation" tabindex="-1" aria-labelledby="addEducationLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" style="max-width: 40%;"> <!-- Genişliği artırmak için inline CSS kullanıldı -->
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="addEducationLabel">Yeni Eğitim Ekle</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                <div class="modal-body">
                    <!-- education category combobox -->
                    <div class="mb-3 row">
                        <label for="educationCategory" class="col-sm-3 col-form-label">Rol:</label>
                        <div class="col-sm-9">
                            <select class="form-select" id="educationCategory" v-model="educationCategory">
                                <option v-for="cat in educationCategories" :key="cat.id" :value="cat.id">
                                    {{ cat.name }}
                                </option>
                            </select>     
                        </div>
                    </div>
                    <div class="mb-3 row">
                        <label for="educationTitle" class="col-sm-3 col-form-label">Eğitim Adı:</label>
                        <div class="col-sm-9">
                            <input type="text" class="form-control" id="educationTitle" aria-label="Eğitim Adı" aria-describedby="inputGroup-sizing-default" />
                        </div>
                        </div>
                        <div class="mb-3 row">
                            <label for="educationOrganization" class="col-sm-3 col-form-label">Eğitim Kurumu:</label>
                            <div class="col-sm-9">
                                <input type="text" class="form-control" id="educationOrganization" aria-label="Eğitim Kurumu" aria-describedby="inputGroup-sizing-default" />
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <label for="educationSubject" class="col-sm-3 col-form-label">Eğitim Konusu:</label>
                            <div class="col-sm-9">
                                <textarea class="form-control" id="educationSubject" rows="4" aria-label="Eğitim Konusu" aria-describedby="inputGroup-sizing-default"></textarea>
                            </div>
                        </div>

                        <div class="mb-3 row">
                            <label for="educationStartDate" class="col-sm-3 col-form-label">Başlangıç Tarihi:</label>
                            <div class="col-sm-9">
                                <input type="date" class="form-control" id="educationStartDate" 
                                 aria-describedby="inputGroup-sizing-default" v-model="educationStartDate" 
                                @change="(e) => handleDateInput(e,'educationStartDate')" />
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <label for="educationEndDate" class="col-sm-3 col-form-label">Bitiş Tarihi:</label>
                            <div class="col-sm-9">
                                <input type="date" class="form-control" id="educationEndDate" v-model="educationEndDate" 
                                aria-describedby="inputGroup-sizing-default" @change="(e) => handleDateInput(e,'educationEndDate')"/>
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <label for="educationCertificateUrl" class="col-sm-3 col-form-label">Sertifika Url:</label>
                            <div class="col-sm-9">
                                <input type="text" class="form-control" id="educationCertificateUrl" aria-label="Sertifika Url" aria-describedby="inputGroup-sizing-default" />
                            </div>
                        </div>
                </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                        <button type="button" class="btn btn-primary" @click="addEducation">Ekle</button>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!-- Eğitim güncelleme modal -->
    <div class="d-flex justify-content-between mt-3">

        <!-- Modal -->
        <div class="modal fade" id="updateEducation" tabindex="-1" aria-labelledby="updateEducationLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" style="max-width: 40%;">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="updateEducationLabel">Eğitim Güncelle</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <!-- education category combobox -->
                        <div class="mb-3 row">
                            <label for="editEducationCategory" class="col-sm-3 col-form-label">Rol:</label>
                            <div class="col-sm-9">
                                <select class="form-select" id="editEducationCategory" v-model="editEducationCategory">
                                    <option v-for="cat in educationCategories" :key="cat.id" :value="cat.id">
                                        {{ cat.name }}
                                    </option>
                                </select>     
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <label for="editEducationTitle" class="col-sm-3 col-form-label">Eğitim Adı:</label>
                            <div class="col-sm-9">
                                <input type="text" class="form-control" id="editEducationTitle" 
                                aria-label="Eğitim Adı" aria-describedby="inputGroup-sizing-default"
                                v-model="editEducationTitle" />
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <label for="editEducationOrganization" class="col-sm-3 col-form-label">Eğitim Kurumu:</label>
                            <div class="col-sm-9">
                                <input type="text" class="form-control" id="editEducationOrganization" 
                                aria-label="Eğitim Kurumu" aria-describedby="inputGroup-sizing-default"
                                v-model="editEducationOrganization" />
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <label for="editEducationSubject" class="col-sm-3 col-form-label">Eğitim Konusu:</label>
                            <div class="col-sm-9">
                                <textarea class="form-control" id="editEducationSubject" rows="4" 
                                aria-label="Eğitim Konusu" aria-describedby="inputGroup-sizing-default"
                                v-model="editEducationSubject"></textarea>
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <label for="editEducationStartDate" class="col-sm-3 col-form-label">Başlangıç Tarihi:</label>
                            <div class="col-sm-9">
                                <input type="date" class="form-control" id="editEducationStartDate" 
                                aria-label="Başlangıç Tarihi" aria-describedby="inputGroup-sizing-default" 
                                v-model="editEducationStartDate" />
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <label for="editEducationEndDate" class="col-sm-3 col-form-label">Final Tarihi:</label>
                            <div class="col-sm-9">
                                <input type="date" class="form-control" id="editEducationEndDate" 
                                aria-label="Final Tarihi" aria-describedby="inputGroup-sizing-default" 
                                v-model="editEducationEndDate" />
                            </div>
                        </div>
                        <div class="mb-3 row">
                            <label for="editEducationCertificateUrl" class="col-sm-3 col-form-label">Sertifika Url:</label>
                            <div class="col-sm-9">
                                <input type="text" class="form-control" id="editEducationCertificateUrl" 
                                aria-label="Sertifika Url" aria-describedby="inputGroup-sizing-default" 
                                v-model="editEducationCertificateUrl"/>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                        <button type="button" class="btn btn-primary" @click="updateEducation">Güncelle</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    </div>
</template>

<style scoped>

</style>
