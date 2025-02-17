<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { API_URL, PROFILE_PHOTO_URL} from '../../config.js';
import axios from 'axios';

const router = useRouter();

const educationCategory = ref({
    id: 0,
    name: ''
});

const editeducationCategoryName = ref('');

// Sample categories
const sampleCategories = ref([
    { id: 1, name: 'universities' },
    { id: 2, name: 'courses' },
    { id: 3, name: 'certificates' }
]);

// category reactive list
const categories = ref([]);

// CRUD operations

// GET
const getEducationCategories = async () => {
  try {
    const response = await axios.get(`${API_URL}educationcategories`);
    // console.log('response: ', response.data);
    categories.value = response.data.map(data => ({
      id: data.educationCategory_ID,
      name: data.educationCategory_Name
    }));

  }catch(error) {
    console.error('Failed to get education categories: ', error);
    alert('Failed to get education categories');
  }
}

// POST
const addEducationCategory = async () => {
  try {
    const educationcategoryname = document.getElementById('educationCategoryName').value;

    if (!educationcategoryname.trim()) {
      alert('Kategori adı boş olamaz!');
      return;
    }

    const response = await axios.post(`${API_URL}educationcategories`, {
      educationCategory_Name: educationcategoryname
    });

    const newCategory = response.data;

    if (newCategory && newCategory.educationCategory_ID) {
      // Yeni kategoriyi categories listesine ekle
      categories.value.push({
        id: newCategory.educationCategory_ID,
        name: newCategory.educationCategory_Name
      });

      // Input alanını temizle
      document.getElementById('educationCategoryName').value = '';

      // Bootstrap modalı kapat
      const modal = bootstrap.Modal.getInstance(document.getElementById('addLanguage'));
      modal.hide();
    } else {
      throw new Error('Kategori eklenirken hata oluştu');
    }
  } catch (error) {
    console.error('Failed to add category:', error);
    alert('Kategori eklenirken hata oluştu');
  }
};


// Fill update modal with selected category
const selectedCategoryForUpdate = (categoryToUpdate) => {
    educationCategory.value = { ...categoryToUpdate };
    editeducationCategoryName.value = categoryToUpdate.name; // Set the input field's value
    const modal = new bootstrap.Modal(document.getElementById('updateEducationCategory')); // Initialize the modal
    modal.show(); // Show the modal
}

// PUT
const updateEducationCategory = async () => {
  try {
    if (!editeducationCategoryName.value.trim()) {
      alert('Kategori adı boş olamaz!');
      return;
    }

    const response = await axios.put(`${API_URL}educationcategories/${educationCategory.value.id}`, {
      educationCategory_Name: editeducationCategoryName.value
    });

    const updatedCategory = response.data;

    if (updatedCategory && updatedCategory.educationCategory_ID) {
      // Kategoriyi listede bul ve güncelle
      const categoryIndex = categories.value.findIndex(category => category.id === educationCategory.value.id);
      
      if (categoryIndex !== -1) {
        categories.value[categoryIndex] = {
          id: updatedCategory.educationCategory_ID,
          name: updatedCategory.educationCategory_Name
        };
      }

      // Modalı kapat
      const modalElement = document.getElementById('updateEducationCategory');
      const modalInstance = bootstrap.Modal.getInstance(modalElement) || new bootstrap.Modal(modalElement);
      modalInstance.hide();
    } else {
      throw new Error('Kategori güncellenirken hata oluştu');
    }
  } catch (error) {
    console.error('Failed to update category:', error);
    alert('Kategori güncellenirken hata oluştu');
  }
};

const deleteEducationCategory = async (categoryToDelete) => {
  const confirmDelete = window.confirm(`"${categoryToDelete.name}" kategorisini silmek istediğinize emin misiniz?`);
  
  if (!confirmDelete) {
    return; // Kullanıcı "Hayır" dediği için işlem durduruldu
  }

  try {
    const response = await axios.delete(`${API_URL}educationcategories/${categoryToDelete.id}`);
    
    if (response.status === 200) {
      // Kategoriyi listeden sil
      categories.value = categories.value.filter(category => category.id !== categoryToDelete.id);
      // alert('Kategori başarıyla silindi!');
    } else {
      throw new Error('Kategori silinirken hata oluştu');
    }
  } catch (error) {
    console.error('Failed to delete category:', error);
    alert('Kategori silinirken hata oluştu');
  }
};



onMounted(() => {
  getEducationCategories();
})



</script>

<template>
  <h3>Eğitim Kategoriler</h3>
  <br>

  <div style="font-size:20px">
    <table class="table table-striped table-responsive table-hover table-bordered text-center" style="width:50%">
      <thead class="table-dark">
        <tr class="col-md-2">
          <th>ID</th>
          <th>Name</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="category in categories" :key="category.id">
          <td>{{ category.id }}</td>
          <td>{{ category.name }}</td>
          <td>
            <button type="button" class="btn btn-primary" @click="selectedCategoryForUpdate(category)">
              Güncelle
            </button>
            <button type="button" class="btn btn-danger" @click="deleteEducationCategory(category)">
              Sil
            </button>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- Eğitim Kategori ekleme modal -->
    <div class="d-flex justify-content-between mt-3">
      <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addLanguage">
        Ekle
      </button>

      <!-- Modal -->
      <div class="modal fade" id="addLanguage" tabindex="-1" aria-labelledby="addLanguageLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" style="max-width: 40%;">
          <div class="modal-content">
            <div class="modal-header">
              <h1 class="modal-title fs-5" id="addLanguageLabel">Yeni Kategori Ekle</h1>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <div class="input-group mb-3">
                <label for="educationCategoryName" class="me-4">Eğitim Kategori:</label>
                <input type="text" class="form-control" id="educationCategoryName" aria-label="Eğitim Kategori Adı" aria-describedby="inputGroup-sizing-default" />
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
              <button type="button" class="btn btn-primary" @click="addEducationCategory">Ekle</button>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Eğitim Kategori güncelleme modal -->
    <div class="d-flex justify-content-between mt-3">
      <!-- Modal -->
      <div class="modal fade" id="updateEducationCategory" tabindex="-1" aria-labelledby="updateEducationCategoryLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" style="max-width: 40%;">
          <div class="modal-content">
            <div class="modal-header">
              <h1 class="modal-title fs-5" id="updateEducationCategoryLabel">Eğitim Kategori Güncelle</h1>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <div class="input-group mb-3">
                <label for="editeducationCategoryName" class="me-4">Eğitim Kategori:</label>
                <input v-model="editeducationCategoryName" type="text" class="form-control" id="editeducationCategoryName" aria-label="Eğitim Kategori Adı" aria-describedby="inputGroup-sizing-default" />
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
              <button type="button" class="btn btn-primary" @click="updateEducationCategory">Güncelle</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
</style>
