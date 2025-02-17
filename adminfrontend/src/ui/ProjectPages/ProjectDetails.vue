<script setup>
import { useRouter } from 'vue-router';
import axios from 'axios';
import { onMounted, ref } from 'vue';
import { API_URL, PROFILE_PHOTO_URL } from '../../config.js';

const router = useRouter();

const project = ref({
  id: '',
  title: '',
  about: '',
  selectedTechnologies: [], // Array of technology IDs for selected technologies
  githubUrl: '',
  webUrl: '',
  startDate: '',
  endDate: '',
  status: '',
  photoUrl: '',
  category: '',
  photoPreview: ''
});

const projectTechnologies = ref([]); // All available technologies
const projectCategories = ref(''); // All available categories


const getProjectDetails = async () => {
  try {
    const response = await axios.get(`${API_URL}projects/${router.currentRoute.value.params.id}/`);
    const projectData = response.data;

    project.value.id = projectData.project_ID;
    project.value.title = projectData.project_Title;
    project.value.about = projectData.project_About;
    project.value.selectedTechnologies = await getProjectTechnologies(parseInt(projectData.project_ID));
    project.value.githubUrl = projectData.project_GithubUrl;
    project.value.webUrl = projectData.project_LiveUrl;
    project.value.startDate = projectData.project_StartDate;
    project.value.endDate = projectData.project_EndDate;
    project.value.category = await getProjectCategory(parseInt(projectData.project_ID));
    project.value.status = projectData.project_IsActive ? 'Aktif' : 'Pasif';

    if (projectData.project_Photo) {
      project.value.photoUrl = `${PROFILE_PHOTO_URL}${projectData.project_Photo}`;
      project.value.photoPreview = `${PROFILE_PHOTO_URL}${projectData.project_Photo}`;
    }

    // console.log('Project details:', projectData);
  } catch (error) {
    console.error('Failed to fetch project details:', error);
    alert('An error occurred while fetching project details.');
  }
};

const getProjectCategory = async (projectid) => {
  try {
    // console.log(`${API_URL}projects/projectcategory/${projectid}`);
    const response = await axios.get(`${API_URL}projects/projectcategory/${projectid}`);
    // console.log(response.data.projectCategory_Name);
    projectCategories.value = response.data;
    // console.log('Project category:', projectCategories.value);
    return response.data.projectCategory_Name;
  } catch (error) {
    console.error('Failed to fetch project categories:', error);
    alert('An error occurred while fetching project categories.');
  }
};

const getProjectTechnologies = async (projectid) => {
  try {
    const response = await axios.get(`${API_URL}projects/projecttechnology/${projectid}`);
    projectTechnologies.value = response.data;
    // console.log('Project technologies:', projectTechnologies.value);
    
  } catch (error) {
    console.error('Failed to fetch project technologies:', error);
    alert('An error occurred while fetching project technologies.');
  }
}






onMounted(() => {
  getProjectDetails();
});
</script>


<template>
  <div class="container mt-5">
    <div class="card shadow-lg">
      <div class="card-header bg-primary text-white text-center">
        <h3>Proje Detayları</h3>
      </div>
      <div class="card-body">
        <div v-if="project.photoPreview" class="text-center mb-3">
          <img :src="project.photoPreview" alt="Project Image" class="img-fluid rounded shadow-sm" style="max-height: 250px;">
        </div>

        <h4 class="mb-3">{{ project.title }}</h4>
        <p class="text-muted">{{ project.about }}</p>

        <div class="mb-3">
          <strong>Kullanılan Teknolojiler:</strong>
          <ul class="list-group mt-2">
            <!-- Using getSelectedTechnologies to map the selected technologies -->
            <li v-for="tech in projectTechnologies" :key="tech.technology_ID" class="list-group-item">
              <img :src="tech.technology_IconUrl" alt="Technology Icon" class="me-2" style="width: 20px; height: 20px;"> <!-- Icon added -->
              {{ tech.technology_Name }}
            </li>
            
          </ul>
        </div>

        <p>
          <strong>Proje Github URL:</strong>
          <a :href="project.githubUrl" target="_blank" class="text-primary">{{ project.githubUrl }}</a>
        </p>

        <p>
          <strong>Proje Web URL:</strong>
          <a :href="project.webUrl" target="_blank" class="text-primary">{{ project.webUrl }}</a>
        </p>

        <p><strong>Başlangıç Tarihi:</strong> {{ project.startDate }}</p>
        <p><strong>Bitiş Tarihi:</strong> {{ project.endDate }}</p>
        <p><strong class="me-3">Proje Durumu:     </strong> 
          <span v-if="project.status === 'Aktif'" class="text-success">{{ project.status }}</span>
          <span v-else class="text-danger">{{ project.status }}</span>
        </p>
        <p><strong>Kategori:</strong> {{ project.category }}</p>

        <div class="d-flex justify-content-end mt-4">
          <button class="btn btn-primary" @click="router.push('/projects')">Geri Dön</button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.card {
  max-width: 700px;
  margin: auto;
}
</style>
