<script setup>
</script>

<template>
    <h3>Sosyal Medya</h3>
    
    
    <br>

    <div style="font-size:20px">
        <table class="table table-striped table-responsive table-hover table-bordered text-center" style="width:100%">
            <thead class="table-dark">
                <tr class="col-md-2">
                    <th>Sosyal Medya ID</th>
                    <th>Kullanıcı ID</th>
                    <th>Sosyal Medya Adı</th>
                    <th>Sosyal Medya Url</th>
                    <th>İşlemler</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>1</td>
                    <td>1</td>
                    <td>Facebook</td>
                    <td>www.facebook.com</td>
                    <td>
                        <button class="btn btn-primary btn-sm" data-bs-toggle="modal" data-bs-target="#updateSocialMedia">Güncelle</button>
                        <button class="btn btn-danger btn-sm">Delete</button>
                    </td>
                </tr>
                <tr>
                    <td>2</td>
                    <td>1</td>
                    <td>Instagram</td>
                    <td>www.instagram.com</td>
                    <td>
                        <button class="btn btn-primary btn-sm" data-bs-toggle="modal" data-bs-target="#updateSocialMedia">Güncelle</button>
                        <button class="btn btn-danger btn-sm">Delete</button>
                    </td>
                </tr>
                <!-- do it v-if -->
                <tr v-if="true">
                    <td>3</td>
                    <td>1</td>
                    <td>Twitter</td>
                    <td>www.twitter.com</td>
                    <td>
                        <button class="btn btn-primary btn-sm" data-bs-toggle="modal" data-bs-target="#updateSocialMedia">Güncelle</button>
                        <button class="btn btn-danger btn-sm">Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
        
        <!-- Sosyal Medya ekleme modal -->
        <div class="d-flex justify-content-between mt-3">
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addSocialMedia">
            Ekle
            </button>
            <button type="button" class="btn btn-success" @click="navigateToPlatform">Sosyal Medya Platformu ekle</button>

            <!-- Modal -->
            <div class="modal fade" id="addSocialMedia" tabindex="-1" aria-labelledby="addSocialMediaLabel" aria-hidden="true" >
                <div class="modal-dialog modal-dialog-centered" style="max-width: 40%;"> <!-- Genişliği artırmak için inline CSS kullanıldı -->
                    <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="addSocialMediaLabel">Sosyal Medya Ekle</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="input-group mb-3">
                            <div class="d-flex w-100 align-items-center">
                                <div class="dropdown me-2">
                                    <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                        Sosyal Medya Seç
                                    </button>
                                    <ul class="dropdown-menu">
                                        <li><button class="dropdown-item" type="button">Facebook</button></li>
                                        <li><button class="dropdown-item" type="button">Twitter</button></li>
                                        <li><button class="dropdown-item" type="button">Instagram</button></li>
                                    </ul>
                                </div>
                                <input type="text" class="form-control" id="socialMediaUrl" aria-label="Sosyal Medya Adı" aria-describedby="inputGroup-sizing-default" />
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                        <button type="button" class="btn btn-primary">Ekle</button>
                    </div>
                    </div>
                </div>
            </div>
        </div>
        

        <!-- Sosyal Medya güncelleme modal -->
        <div class="d-flex justify-content-between mt-3">

            <!-- Modal -->
            <div class="modal fade" id="updateSocialMedia" tabindex="-1" aria-labelledby="updateSocialMediaLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" style="max-width: 40%;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h1 class="modal-title fs-5" id="updateSocialMediaLabel">Sosyal Medya Güncelle</h1>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="input-group mb-3">
                                <div class="d-flex w-100 align-items-center">
                                    <div class="dropdown me-2">
                                        <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                            Sosyal Medya Seç
                                        </button>
                                        <ul class="dropdown-menu">
                                            <li><button class="dropdown-item" type="button">Facebook</button></li>
                                            <li><button class="dropdown-item" type="button">Twitter</button></li>
                                            <li><button class="dropdown-item" type="button">Instagram</button></li>
                                        </ul>
                                    </div>
                                    <input type="text" class="form-control" id="socialMediaUrl" aria-label="Sosyal Medya Adı" aria-describedby="inputGroup-sizing-default" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                            <button type="button" class="btn btn-primary">Ekle</button>
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
      router.push('/socialmediaplatform'); // Navigate to the defined route
    };

    return {
      navigateToPlatform,
    };
  },
};
</script>
