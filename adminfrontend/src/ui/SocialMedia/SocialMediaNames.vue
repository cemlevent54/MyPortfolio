<script setup>
</script>

<template>
    <h3>Sosyal Medya Platform</h3>
    
    
    <br>

    <div style="font-size:20px">
        <table class="table table-striped table-responsive table-hover table-bordered text-center" style="width:100%">
            <thead class="table-dark">
                <tr class="col-md-2">
                    <th>Sosyal Medya Platform ID</th>
                    <th>Sosyal Medya Platform Adı</th>
                    <th>Sosyal Medya Platform IconUrl</th>
                    <th>Icon Preview</th>
                    <th>İşlemler</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>1</td>
                    <td>Facebook</td>
                    <!-- icon url -->
                    <td>https://cdn-icons-png.flaticon.com/512/124/124010.png</td>
                    <td><img src="https://cdn-icons-png.flaticon.com/512/124/124010.png" alt="Facebook Icon" style="width: 50px; height: 50px;"></td>
                    <td>
                        <button class="btn btn-primary btn-sm" data-bs-toggle="modal" data-bs-target="#updateSocialMediaPlatorm">Güncelle</button>
                        <button class="btn btn-danger btn-sm">Delete</button>
                    </td>
                </tr>
                <tr>
                    <td>2</td>
                    <td>Instagram</td>
                    <!-- icon url -->
                    <td>https://cdn-icons-png.flaticon.com/512/2111/2111463.png</td>
                    <td><img src="https://cdn-icons-png.flaticon.com/512/2111/2111463.png" alt="Instagram Icon" style="width: 50px; height: 50px;"></td>
                    <td>
                        <button class="btn btn-primary btn-sm" data-bs-toggle="modal" data-bs-target="#updateSocialMediaPlatorm">Güncelle</button>
                        <button class="btn btn-danger btn-sm">Delete</button>
                    </td>
                </tr>
                <!-- do it v-if -->
                 <tr>
                    <td>3</td>
                    <td>Twitter</td>
                    <!-- icon url -->
                    <td>https://cdn-icons-png.flaticon.com/512/733/733579.png</td>
                    <td><img src="https://cdn-icons-png.flaticon.com/512/733/733579.png" alt="Twitter Icon" style="width: 50px; height: 50px;"></td>
                    <td>
                        <button class="btn btn-primary btn-sm" data-bs-toggle="modal" data-bs-target="#updateSocialMediaPlatorm">Güncelle</button>
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
                            <label for="socialMediaPlatformName: " class="me-4">Sosyal Medya Platform adı:</label>
                            <input type="text" class="form-control" id="socialMediaPlatformName" aria-label="Yabancı Dil Adı" aria-describedby="inputGroup-sizing-default" />
                        </div>
                        <div class="input-group mb-3">
                            <label for="socialMediaPlatformIconUrl" class="me-4">Sosyal Medya Platform Icon Url:</label>
                            <input type="text" class="form-control" id="socialMediaPlatformIconUrl" aria-label="Sosyal Medya Platform Icon Url" aria-describedby="inputGroup-sizing-default" />
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
            <div class="modal fade" id="updateSocialMediaPlatorm" tabindex="-1" aria-labelledby="updateSocialMediaPlatormLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" style="max-width: 40%;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h1 class="modal-title fs-5" id="updateSocialMediaPlatormLabel">Sosyal Medya Güncelle</h1>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="input-group mb-3">
                                <label for="socialMediaPlatformName: " class="me-4">Sosyal Medya Platform adı:</label>
                                <input type="text" class="form-control" id="socialMediaPlatformName" aria-label="Yabancı Dil Adı" aria-describedby="inputGroup-sizing-default" />
                            </div>
                            <div class="input-group mb-3">
                                <label for="socialMediaPlatformIconUrl" class="me-4">Sosyal Medya Platform Icon Url:</label>
                                <input type="text" class="form-control" id="socialMediaPlatformIconUrl" aria-label="Sosyal Medya Platform Icon Url" aria-describedby="inputGroup-sizing-default" />
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
