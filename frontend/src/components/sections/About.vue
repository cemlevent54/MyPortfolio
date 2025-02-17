<template>
  <section id="about" class="about section">
    <!-- Section Title -->
    <div class="container section-title" data-aos="fade-up">
      <h2>About</h2>
      <p>{{ aboutTitle }}</p>
    </div>

    <div class="container" data-aos="fade-up" data-aos-delay="100">
      <div class="row gy-4 justify-content-center">
        <div class="col-lg-12 content">
          <h2>{{ profession }}</h2>
          <p class="fst-italic py-3">
            {{ shortDescription }}
          </p>

          <div class="row">
            <!-- Personal Info Left Column -->
            <div class="col-lg-6">
              <ul>
                <li v-for="(value, key) in personalInfoLeft" :key="key">
                  <i class="bi bi-chevron-right"></i>
                  <strong>{{ key }}:</strong>
                  <span>{{ value }}</span>
                </li>
              </ul>
            </div>

            <!-- Personal Info Right Column -->
            <div class="col-lg-6">
              <ul>
                <li v-for="(value, key) in personalInfoRight" :key="key">
                  <i class="bi bi-chevron-right"></i>
                  <strong>{{ key }}:</strong>
                  <span>{{ value }}</span>
                </li>
              </ul>
            </div>
          </div>

          <p class="py-3">
            {{ detailedDescription }}
          </p>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import axios from 'axios';
import { API_URL } from '../../config.js';

export default {
  name: 'About',

  data() {
    return {
      aboutTitle: '',
      profession: '',
      shortDescription: '',
      detailedDescription: '',
      
      personalInfoLeft: {},
      personalInfoRight: {}
    };
  },

  methods: {
    // ✅ Corrected async GET function
    async getUserAbout() {
      try {
        const response = await axios.get(`${API_URL}Frontend/GetAbout`);
        console.log('API Response:', response.data);

        // Assuming the response has a proper structure
        const about = response.data;
        this.aboutTitle = about.user_About ;
        this.profession = about.user_Job || 'Junior Year Computer Engineering Student';
        // this.detailedDescription = about.detailedDescription || 'Default detailed description';

        this.personalInfoLeft = {
          'Birthday': about.user_BirthDate,
          'Phone': about.user_Phone,
          'City': about.user_LivingCity
        };

        this.personalInfoRight = {
          'Age': 21,
          'Email': about.user_Email,
        };

      } catch (error) {
        console.error('Failed to fetch user about:', error);
      }
    }
  },

  mounted() {
    // ✅ Corrected function call
    this.getUserAbout();

    // Initialize AOS animations
    if (window.AOS) {
      window.AOS.refresh();
    }
  }
};
</script>

<style scoped>
</style>
