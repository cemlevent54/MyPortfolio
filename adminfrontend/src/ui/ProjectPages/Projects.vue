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

const project = ref({
    id: 0,
    user_id: 0,
    category_id: 0,
    project_title: '',
    project_about: '',
    project_github_url: '',
    project_live_url: '',
    project_start_date: '',
    project_end_date: '',
    project_isactive: false,
    project_photo: '',
    project_technologies: [],
});

const projects = ref([]);

// define v-models
const projectTitle = ref('');
const projectAbout = ref('');
const projectGithubUrl = ref('');
const projectWebUrl = ref('');
const projectStartDate = ref('');
const projectEndDate = ref('');
const projectStatus = ref('');
const projectId = ref('');
const projectCategory = ref('');

const ProjectPhotoPreview = ref('');
const projectPhoto = ref('');
const projectPhotoUrl = ref('');
const editProjectPhoto = ref('');

// define v-models for update modal
const editProjectTitle = ref('');
const editProjectAbout = ref('');
const selectedTechnologies = ref([]);
const editProjectGithubUrl = ref('');
const editProjectWebUrl = ref('');
const editProjectStartDate = ref('');
const editProjectEndDate = ref('');
const editProjectStatus = ref('');
const editProjectPhotoPreview = ref('');
const editProjectCategory = ref('');

const photoFile = ref(null);
const editProjectPhotoUrl = ref('');
const editPhotoFile = ref(null);





// CRUD's

// GET
const getProjects = async () => {
    try {
        const response = await axios.get(`${API_URL}projects`);
        projects.value = response.data.map((project) => ({
            id: project.project_ID,
            user_id: project.user_ID,
            category_id: project.projectCategory_ID,
            project_title: project.project_Title,
            project_about: project.project_About,
            project_github_url: project.project_GithubUrl,
            project_live_url: project.project_LiveUrl,
            project_start_date: project.project_StartDate,
            project_end_date: project.project_EndDate,
            project_isactive: project.project_IsActive,
            project_photo: project.project_Photo,
            project_technologies: project.projectTechIDDTOs.map(tech => tech.projectTech_ID),
        }));
        // console.log('Projects:', projects.value); 
        // console.log(response.data);

        

    } catch (error) {
        console.error('Failed to get projects:', error);
        alert('Failed to get projects');
    }
}

// POST
const addProject = async () => {
    try {
        // console.log('Adding project...');
        // console.log(photoFile.value);
        if(photoFile.value) {
            let formData = new FormData();
            formData.append("file", photoFile.value);

            axios.post(UPLOAD_PROJECT_PHOTO_URL, formData)
            .then((response) => {
                if(response.data && response.data.photoUrl) {
                    projectPhotoUrl.value = response.data.photoUrl;
                    // console.log('Photo URL:', projectPhotoUrl.value);
                    // console.log('Photo uploaded successfully, proceeding to add project...');
                    sendAddProjectRequest();
                } else {
                    console.error('Failed to upload photo:', response.data);
                    alert('Failed to upload photo');
                }
            })
        }
        


    } catch (error) {
        console.error('Failed to add project:', error);
        alert('Failed to add project');
    }
}

const sendAddProjectRequest = async () => {
    try {
        // Map selected technologies into the correct format for submission
        let selectedTechStack = Array.from(document.querySelectorAll('input[type="checkbox"]:checked'))
            .map(tech => ({ ProjectTech_ID: parseInt(tech.value) }));  // Format as { ProjectTech_ID: ID }

        // Remove duplicates
        selectedTechStack = selectedTechStack.filter((tech, index, self) =>
            index === self.findIndex((t) => t.ProjectTech_ID === tech.ProjectTech_ID)
        );

        // console.log('Selected technologies:', selectedTechStack);

        // Create the project data object
        const projectData = {
            User_ID: user.value.id,  // Ensure this is correct (from the logged-in user)
            Project_Title: projectTitle.value,
            Project_About: projectAbout.value,
            Project_GithubUrl: projectGithubUrl.value,
            Project_LiveUrl: projectWebUrl.value,
            Project_StartDate: convertDateToDDMMYYYY(projectStartDate.value), // Ensure the date is formatted correctly
            Project_EndDate: convertDateToDDMMYYYY(projectEndDate.value),     // Ensure the date is formatted correctly
            Project_IsActive: projectStatus.value,
            Project_Photo: projectPhotoUrl.value,  // Ensure projectPhotoUrl is updated
            ProjectTechIDDTOs: selectedTechStack,  // Technologies as an array of { ProjectTech_ID }
            ProjectCategory_ID: projectCategory.value,
        };

        // console.log('Project data:', projectData);

        // Send the project data to the API
        const response = await axios.post(`${API_URL}projects`, projectData);
        // console.log('Project added:', response.data);

        // Handle response and update UI
        const newProject = response.data.project;
        const newTechStack = response.data.technologies;
        // console.log('New tech stack:', newTechStack);
        // console.log('New project:', newProject);

        if (newProject && newProject.project_ID) {
            // Add the new project to the list of projects
            projects.value.push({
                id: newProject.project_ID,
                user_id: newProject.user_ID,
                category_id: newProject.projectCategory_ID,
                project_title: newProject.project_Title,
                project_about: newProject.project_About,
                project_github_url: newProject.project_GithubUrl,
                project_live_url: newProject.project_LiveUrl,
                project_start_date: newProject.project_StartDate,
                project_end_date: newProject.project_EndDate,
                project_isactive: newProject.project_IsActive,
                project_photo: newProject.project_Photo,
                project_technologies: newTechStack.map(tech => tech.projectTech_ID),
            });
            alert('Project added successfully!');

            // Reset the form fields
            projectTitle.value = '';
            projectAbout.value = '';
            projectGithubUrl.value = '';
            projectWebUrl.value = '';
            projectStartDate.value = '';
            projectEndDate.value = '';
            projectStatus.value = '';
            projectPhotoUrl.value = '';
            selectedTechnologies.value = [];
            ProjectPhotoPreview.value = '';
            photoFile.value = null;
            projectId.value = '';

            // Hide the modal after adding the project
            const modal = bootstrap.Modal.getInstance(document.getElementById('addProject'));
            modal.hide();
        }

    } catch (error) {
        console.error('Failed to add project:', error);
        alert('Failed to add project. Please try again later.');
    }
};

// PUT
const selectedProjectForUpdate = async (projectToUpdate) => {
    // console.log('Selected project for update:', projectToUpdate);
    projectId.value = projectToUpdate.id;
    editProjectTitle.value = projectToUpdate.project_title;
    editProjectAbout.value = projectToUpdate.project_about;
    editProjectGithubUrl.value = projectToUpdate.project_github_url;
    editProjectWebUrl.value = projectToUpdate.project_live_url;
    editProjectStartDate.value = convertDDMMYYYYToInputDate(projectToUpdate.project_start_date);
    editProjectEndDate.value = convertDDMMYYYYToInputDate(projectToUpdate.project_end_date);
    editProjectStatus.value = projectToUpdate.project_isactive;
    if (projectToUpdate.project_photo) {
        editProjectPhotoPreview.value = getImgUrl(projectToUpdate.project_photo);
    } else {
        editProjectPhotoPreview.value = ''; // Eğer yoksa boş bırak
    }
    editProjectPhoto.value = projectToUpdate.project_photo;
    selectedTechnologies.value = projectToUpdate.project_technologies;
    editProjectCategory.value = projectToUpdate.category_id;

    const modalElement = document.getElementById('updateProject');
    const modal = bootstrap.Modal.getOrCreateInstance(modalElement);
    modal.show();
}

const updateProject = async () => {
    // console.log('Updating user: ', user.value.id);
    try {
        if(editPhotoFile.value) {
            let formData = new FormData();
            formData.append("file", editPhotoFile.value);

            axios.post(UPLOAD_PROJECT_PHOTO_URL, formData)
            .then((response) => {
                if(response.data && response.data.photoUrl) {
                    editProjectPhotoUrl.value = response.data.photoUrl;
                    // console.log('Photo URL:', editProjectPhotoUrl.value);
                    // console.log('Photo uploaded successfully, proceeding to update project...');
                    sendUpdateProjectRequest();
                } else {
                    console.error('Failed to upload photo:', response.data);
                    alert('Failed to upload photo');
                }
            })
            .catch((error) => {
                console.error('Failed to upload photo:', error);
                alert('Failed to upload photo');
            });
        } else {
            console.log('No photo uploaded, proceeding to update project...');
            sendUpdateProjectRequest();
        }
    } catch (error) {
        console.error('Failed to update project:', error);
        alert('Failed to update project. Please try again later.');
    }
}


const sendUpdateProjectRequest = async () => {
    try {
        let selectedTechStack = Array.from(document.querySelectorAll('input[type="checkbox"]:checked'))
            .map(tech => ({ ProjectTech_ID: parseInt(tech.value) }));  // Format as { ProjectTech_ID: ID }

        // Remove duplicates
        selectedTechStack = selectedTechStack.filter((tech, index, self) =>
            index === self.findIndex((t) => t.ProjectTech_ID === tech.ProjectTech_ID)
        );

        const updateProjectDatas = {
            Project_ID: projectId.value,
            User_ID: user.value.id,
            Project_Title: editProjectTitle.value,
            Project_About: editProjectAbout.value,
            Project_GithubUrl: editProjectGithubUrl.value,
            Project_LiveUrl: editProjectWebUrl.value,
            Project_StartDate: convertDateToDDMMYYYY(editProjectStartDate.value),
            Project_EndDate: convertDateToDDMMYYYY(editProjectEndDate.value),
            Project_IsActive: editProjectStatus.value,
            Project_Photo: editProjectPhotoUrl.value,
            ProjectTechIDDTOs: selectedTechStack,
            ProjectCategory_ID: editProjectCategory.value,
        };

        // console.log('Update project data:', updateProjectDatas);

        const response = await axios.put(`${API_URL}projects/${projectId.value}`, updateProjectDatas);

        const updatedProject = response.data;
        // console.log('Updated project:', updatedProject);

        if (updatedProject) {
            const projectIndex = projects.value.findIndex((project) => project.id === projectId.value);

            if (projectIndex !== -1) {
                projects.value[projectIndex] = {
                    id: updatedProject.project_ID,
                    user_id: updatedProject.user_ID,
                    category_id: updatedProject.projectCategory_ID,
                    project_title: updatedProject.project_Title,
                    project_about: updatedProject.project_About,
                    project_github_url: updatedProject.project_GithubUrl,
                    project_live_url: updatedProject.project_LiveUrl,
                    project_start_date: updatedProject.project_StartDate,
                    project_end_date: updatedProject.project_EndDate,
                    project_isactive: updatedProject.project_IsActive,
                    project_photo: updatedProject.project_Photo,
                    project_technologies: updatedProject.projectTechIDDTOs.map(tech => tech.ProjectTech_ID),
                };
                
            }

            // Reset the form fields
            resetUpdateFormFields();

            // Hide the modal after updating the project
            const modal = bootstrap.Modal.getInstance(document.getElementById('updateProject'));
            modal.hide();
            window.location.reload();
        } else {
            console.error('Failed to update project:', response.data);
            alert('Failed to update project');
        }
    } catch (error) {
        console.error('Failed to update project:', error);
        alert('Failed to update project. Please try again later.');
    }
};

const resetUpdateFormFields = () => {
    editProjectTitle.value = '';
    editProjectAbout.value = '';
    editProjectGithubUrl.value = '';
    editProjectWebUrl.value = '';
    editProjectStartDate.value = '';
    editProjectEndDate.value = '';
    editProjectStatus.value = '';
    editProjectPhotoUrl.value = '';
    selectedTechnologies.value = [];
    editProjectPhotoPreview.value = '';
    editPhotoFile.value = null;
    projectId.value = '';
};

// DELETE 
const deleteProject = async (id) => {
    const confirm = window.confirm('Are you sure you want to delete this project?');
    if (!confirm) return;

    try {
        
        const response = await axios.delete(`${API_URL}projects/${id}`);
        // console.log('Project deleted:', response.data);

        if (response.status === 200) {
            projects.value = projects.value.filter((project) => project.id !== id);
            // alert('Project deleted successfully!');
        } else {
            throw new Error('Failed to delete project');
        }
    } catch (error) {
        console.error('Failed to delete project:', error);
        alert('Failed to delete project. Please try again later.');
    }
};




// GET techs
const technologies = ref([]);
const getTechs = async () => {
    try {
        const response = await axios.get(`${API_URL}technology`);
        technologies.value = response.data.map((tech) => ({
            id: tech.technology_ID,
            technology_name: tech.technology_Name,
        }));
        // console.log('Technologies:', technologies.value);
        

    } catch (error) {
        console.error('Failed to get technologies:', error);
        alert('Failed to get technologies');
    }
}

// GET categories
const categories = ref([]);
const getCategories = async () => {
    try {
        const response = await axios.get(`${API_URL}projectcategory`);
        categories.value = response.data.map((category) => ({
            id: category.projectCategory_ID,
            category_name: category.projectCategory_Name,
        }));
        // console.log('Categories:', categories.value);
        // console.log(response.data); 
        

    } catch (error) {
        console.error('Failed to get categories:', error);
        alert('Failed to get categories');
    }
}



onMounted(() => {
    getLoginedUserInfo();
    getProjects();
    getTechs();
    getCategories();
});


// PHOTO
function handlePhotoUpload(event) {
  const file = event.target.files[0]; // İlk seçilen dosyayı al
  if (file) {
    const reader = new FileReader(); // Dosyayı okumak için FileReader oluştur
    reader.onload = (e) => {
        ProjectPhotoPreview.value = e.target.result; // Okunan dosyayı preview kaynağına ata
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
       editProjectPhotoPreview.value = e.target.result; // Okunan dosyayı preview kaynağına ata
    };
    reader.readAsDataURL(file); // Dosyayı Data URL formatında oku
    editPhotoFile.value = file; // Dosyayı güncelle
  }
}

function getImgUrl(photo) {
    return `${PROFILE_PHOTO_URL}${photo}`;
}

// DATE
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


const navigateToPlatform = () => {
    router.push('/projects/categories');
}

const navigatetodetails = (projectToDetails) => {
    router.push(`/projects/${projectToDetails.id}`);
}

</script>

<template>
    <h3>Projeler</h3>
    
    
    <br>

    <div style="font-size:14px">
        <table class="table table-striped table-responsive table-hover table-bordered text-center" style="width:100%">
            <thead class="table-dark">
                <tr class="col-md-2">
                    <th>ID</th>
                    <th>Kullanıcı ID</th>
                    <th>Proje Adı</th>
                    <!-- <th>Proje Hakkında</th> -->
                    <th>Proje Github Url</th>
                    <th>Proje Web Url</th>
                    <th>Proje Başlangıç Tarihi</th>
                    <th>Proje Bitiş Tarihi</th>
                    <th>Proje Durumu</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="project in projects" :key="project.id">
                    <td>{{ project.id }}</td>
                    <td>{{ project.user_id }}</td>
                    <td>{{ project.project_title }}</td>
                    <!-- <td>{{ project.project_about }}</td> -->
                    <td>{{ project.project_github_url }}</td>
                    <td>{{ project.project_live_url }}</td>
                    <td>{{ project.project_start_date }}</td>
                    <td>{{ project.project_end_date }}</td>
                    <td>{{ project.project_isactive }}</td>
                    <td>
                        <!-- detay buton -->
                        <button type="button" class="btn btn-info" @click="navigatetodetails(project)">
                            Detay
                        </button>
                        <button type="button" class="btn btn-warning" @click="selectedProjectForUpdate(project)">
                            Güncelle
                        </button>
                        <button type="button" class="btn btn-danger" @click="deleteProject(project.id)">
                            Sil
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>
        
        <!-- Proje ekleme modal -->
        <div class="d-flex justify-content-between mt-3">
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addProject">
            Ekle
            </button>
            <button type="button" class="btn btn-success" @click="navigateToPlatform">Proje Kategorisi ekle</button>

            <!-- Modal -->
            <div class="modal fade" id="addProject" tabindex="-1" aria-labelledby="addProjectLabel" aria-hidden="true" >
                <div class="modal-dialog modal-dialog-centered" style="max-width: 60%;"> <!-- Genişliği artırmak için inline CSS kullanıldı -->
                    <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="addProjectLabel">Yeni Proje Ekle</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="mb-3 d-flex align-items-center">
                            <label for="projectTitle" class="me-4 col-3">Proje:</label>
                            <input type="text" class="form-control col-max" id="projectTitle" 
                            aria-label="Proje Adı" aria-describedby="inputGroup-sizing-default" v-model="projectTitle"/>
                        </div>
                        <div class="mb-3 d-flex align-items-center">
                            <label for="projectAbout" class="me-4 col-3">Proje Hakkında:</label>
                            <textarea class="form-control col-max" id="projectAbout" 
                            aria-label="Proje Hakkında" aria-describedby="inputGroup-sizing-default" v-model="projectAbout"></textarea>
                        </div>
                        <div class="mb-3 d-flex align-items-center">
                            <label class="me-4 col-3">Kullanılan Teknolojiler:</label>
                            <div class="d-flex flex-wrap">
                                <div v-for="tech in technologies" :key="tech.id" class="form-check me-3">
                                    <input class="form-check-input" type="checkbox" :id="'tech' + tech.id" 
                                        :value="tech.id" v-model="selectedTechnologies" />
                                    <label class="form-check-label" :for="'tech' + tech.id">{{ tech.technology_name }}</label>
                                </div>
                            </div>
                        </div>


                        
                        <div class="mb-3 d-flex align-items-center">
                            <label for="projectGithubUrl" class="me-4 col-3">Proje Github Url:</label>
                            <input type="text" class="form-control col-max" id="projectGithubUrl" 
                            aria-label="Proje Github Url" aria-describedby="inputGroup-sizing-default" v-model="projectGithubUrl"/>
                        </div>
                        <div class="mb-3 d-flex align-items-center">
                            <label for="projectWebUrl" class="me-4 col-3">Proje Web Url:</label>
                            <input type="text" class="form-control col-max" id="projectWebUrl" aria-label="Proje Web Url" 
                            aria-describedby="inputGroup-sizing-default" v-model="projectWebUrl" />
                        </div>
                        <div class="mb-3 d-flex align-items-center">
                            <label for="projectStartDate" class="me-4 col-3">Proje Başlangıç Tarihi:</label>
                            <input type="date" class="form-control col-max" id="projectStartDate" 
                            aria-label="Proje Başlangıç Tarihi" aria-describedby="inputGroup-sizing-default" v-model="projectStartDate"/>
                        </div>
                        <div class="mb-3 d-flex align-items-center">
                            <label for="projectEndDate" class="me-4 col-3">Proje Bitiş Tarihi:</label>
                            <input type="date" class="form-control col-max" id="projectEndDate" 
                            aria-label="Proje Bitiş Tarihi" aria-describedby="inputGroup-sizing-default" v-model="projectEndDate"/>
                        </div>
                        <!-- Aktif/Pasif -->
                        <div class="mb-3 d-flex align-items-center">
                            <label for="projectStatus" class="me-4 col-3">Proje Durumu:</label>
                            <select class="form-select" id="projectStatus" aria-label="Proje Durumu" v-model="projectStatus">
                                <option :value="true">Aktif</option>
                                <option :value="false">Pasif</option>
                            </select>
                        </div>
                        <!-- kategori combobox (ai/ml/web) -->
                        
                        <div class="mb-3 d-flex align-items-center">
                            <label for="projectCategory" class="me-4 col-3">Kategori:</label>
                            <select class="form-select col-max" id="projectCategory" aria-label="Proje Kategorisi" v-model="projectCategory">
                                <option v-for="category in categories" :key="category.id" :value="category.id">{{ category.category_name }}</option>
                            </select>
                        </div>

                        <!-- project photo -->
                        <!-- Fotoğraf Preview -->
                        <div class="mb-3 row align-items-center">
                            <label for="ProjectPhotoPreview" class="col-sm-3 col-form-label">Fotoğraf:</label>
                            <div class="col-sm-9 d-flex align-items-center">
                                <img :src="ProjectPhotoPreview" alt="Fotoğraf" width="100" height="100" class="border rounded me-3" />
                                <input type="file" class="form-control" id="ProjectPhoto" accept="image/*" @change="handlePhotoUpload" />
                            </div>
                        </div>
                        
                        <input type="hidden" v-model="projectId" /> <!-- added hidden input for project ID -->
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                        <button type="button" class="btn btn-primary" @click="addProject">Ekle</button> <!-- Added method to handle project addition -->
                    </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Proje güncelleme modal -->
        <div class="d-flex justify-content-between mt-3">

            <!-- Modal -->
            <div class="modal fade" id="updateProject" tabindex="-1" aria-labelledby="updateProjectLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" style="max-width: 60%;">
                <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="updateProjectLabel">Proje Güncelle</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3 d-flex align-items-center">
                    <label for="editProjectTitle" class="me-4 col-3">Proje:</label>
                    <input type="text" class="form-control col-max" id="editProjectTitle" 
                    aria-label="Proje Adı" aria-describedby="inputGroup-sizing-default" v-model="editProjectTitle"/>
                    </div>
                    <div class="mb-3 d-flex align-items-center">
                    <label for="editProjectAbout" class="me-4 col-3">Proje Hakkında:</label>
                    <textarea class="form-control col-max" id="editProjectAbout" 
                    aria-label="Proje Hakkında" aria-describedby="inputGroup-sizing-default" v-model="editProjectAbout"></textarea>
                    </div>
                    <div class="mb-3 d-flex align-items-center">
                    <label class="me-4 col-3">Kullanılan Teknolojiler:</label>
                    <div class="d-flex flex-wrap">
                        <div v-for="tech in technologies" :key="tech.id" class="form-check me-3">
                        <input class="form-check-input" type="checkbox" :id="'editTech' + tech.id" 
                            :value="tech.id" v-model="selectedTechnologies" />
                        <label class="form-check-label" :for="'editTech' + tech.id">{{ tech.technology_name }}</label>
                        </div>
                    </div>
                    </div>
                    <div class="mb-3 d-flex align-items-center">
                    <label for="editProjectGithubUrl" class="me-4 col-3">Proje Github Url:</label>
                    <input type="text" class="form-control col-max" id="editProjectGithubUrl" 
                    aria-label="Proje Github Url" aria-describedby="inputGroup-sizing-default" v-model="editProjectGithubUrl"/>
                    </div>
                    <div class="mb-3 d-flex align-items-center">
                    <label for="editProjectWebUrl" class="me-4 col-3">Proje Web Url:</label>
                    <input type="text" class="form-control col-max" id="editProjectWebUrl" aria-label="Proje Web Url" 
                    aria-describedby="inputGroup-sizing-default" v-model="editProjectWebUrl" />
                    </div>
                    <div class="mb-3 d-flex align-items-center">
                    <label for="editProjectStartDate" class="me-4 col-3">Proje Başlangıç Tarihi:</label>
                    <input type="date" class="form-control col-max" id="editProjectStartDate" 
                    aria-label="Proje Başlangıç Tarihi" aria-describedby="inputGroup-sizing-default" v-model="editProjectStartDate"/>
                    </div>
                    <div class="mb-3 d-flex align-items-center">
                    <label for="editProjectEndDate" class="me-4 col-3">Proje Bitiş Tarihi:</label>
                    <input type="date" class="form-control col-max" id="editProjectEndDate" 
                    aria-label="Proje Bitiş Tarihi" aria-describedby="inputGroup-sizing-default" v-model="editProjectEndDate"/>
                    </div>
                    <!-- Aktif/Pasif -->
                    <div class="mb-3 d-flex align-items-center">
                    <label for="editProjectStatus" class="me-4 col-3">Proje Durumu:</label>
                    <select class="form-select" id="editProjectStatus" aria-label="Proje Durumu" v-model="editProjectStatus">
                        <option :value="true">Aktif</option>
                        <option :value="false">Pasif</option>
                    </select>
                    </div>
                    <!-- kategori combobox (ai/ml/web) -->
                    <div class="mb-3 d-flex align-items-center">
                    <label for="editProjectCategory" class="me-4 col-3">Kategori:</label>
                    <select class="form-select col-max" id="editProjectCategory" aria-label="Proje Kategorisi" v-model="editProjectCategory">
                        <option v-for="category in categories" :key="category.id" :value="category.id">{{ category.category_name }}</option>
                    </select>
                    </div>
                    <!-- edit project photo -->
                    <!-- Fotoğraf Preview -->
                    <div class="mb-3 row align-items-center">
                    <label for="editProjectPhotoPreview" class="col-sm-3 col-form-label">Fotoğraf:</label>
                    <div class="col-sm-9 d-flex align-items-center">
                        <img :src="editProjectPhotoPreview" alt="Fotoğraf" width="100" height="100" class="border rounded me-3" />
                        <input type="file" class="form-control" id="editProjectPhoto" accept="image/*" @change="handlePhotoUploadUpdate" />
                    </div>
                    </div>
                    <input type="hidden" v-model="projectId" /> <!-- added hidden input for project ID -->
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                    <button type="button" class="btn btn-primary" @click="updateProject">Güncelle</button>
                </div>
                </div>
            </div>
            </div>
        </div>


        
        
    </div>
</template>

<style scoped>

</style>

<script>
import { useRouter } from 'vue-router';

export default {
  setup() {
    const router = useRouter();

    const navigateToPlatform = () => {
      router.push('/projects/1'); // Navigate to the defined route
    };

    return {
      navigateToPlatform,
    };
  },
};
</script>