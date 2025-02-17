<template>
  <section id="education" class="education section">
    <div class="container " data-aos="fade-up">
      <h2 class="section-heading">Education</h2>
    </div>

    <div class="container">
      <!-- Modern Category Filter -->
      <div class="category-tabs" data-aos="fade-up" >
        <div
          v-for="filter in categoryFilters"
          :key="filter.id"
          :class="['category-tab', { active: activeFilter === filter.dataFilter }]"
          @click="setActiveFilter(filter.dataFilter)"
        >
          {{ filter.text }}
        </div>
      </div>

      <!-- Education Items -->
      <div class="row isotope-container" data-aos="fade-up" data-aos-delay="100">
        <transition-group name="fade-slide" tag="div" class="row">
          <div
            v-for="item in filteredItems"
            :key="item.id"
            class="col-lg-6 col-md-6 education-item isotope-item"
          >
            <div class="resume-item">
              <h4>{{ item.title }}</h4>
              <h5>{{ item.period }}</h5>
              <p><em>{{ item.institution }}</em></p>
              <p>{{ item.description }}</p>
            </div>
          </div>
        </transition-group>
      </div>
    </div>
  </section>
</template>

<script>
import axios from "axios";
import { API_URL } from '../../config.js';

export default {
  name: "EducationSection",

  data() {
    return {
      activeFilter: "*", // Default to show all categories
      categoryFilters: [],
      educationItems: [],
    };
  },

  computed: {
    filteredItems() {
      return this.activeFilter === "*"
        ? this.educationItems
        : this.educationItems.filter(item => item.filterClass === this.activeFilter);
    },
  },

  methods: {
    setActiveFilter(filter) {
      this.activeFilter = filter;
    },

    async fetchEducationData() {
      try {
        const response = await axios.get(`${API_URL}Frontend/GetEducations`);
        this.educationItems = response.data.map(item => ({
          id: item.education_ID,
          title: item.education_Title,
          period: `${item.education_StartDate} - ${item.education_EndDate}`,
          institution: item.education_Organization,
          description: item.education_Subject,
          filterClass: `filter-${item.educationCategory_ID}`,
          categoryId: item.educationCategory_ID,
        }));

        const existingCategoryIds = [...new Set(this.educationItems.map(item => item.categoryId))];
        await this.fetchEducationCategories(existingCategoryIds);
      } catch (error) {
        console.error("Error fetching education data:", error);
      }
    },

    async fetchEducationCategories(existingCategoryIds) {
      try {
        const response = await axios.get(`${API_URL}Frontend/GetEducationCategories`);
        this.categoryFilters = [
          { id: 0, text: "All", dataFilter: "*" },
          ...response.data
            .filter(category => existingCategoryIds.includes(category.educationCategory_ID))
            .map(category => ({
              id: category.educationCategory_ID,
              text: category.educationCategory_Name,
              dataFilter: `filter-${category.educationCategory_ID}`,
            })),
        ];
      } catch (error) {
        console.error("Error fetching education categories:", error);
      }
    },
  },

  async mounted() {
    await this.fetchEducationData();
  },
};
</script>

<style scoped>
/* Modern Minimalistic Education Section */
#education {
  padding: 60px 0;
  background-color: #fff;
  color: #000;
}

/* Section Title */
.section-heading {
  font-size: 2rem;
  font-weight: bold;
  margin-bottom: 20px;
  /* text-transform: uppercase; */
  border-bottom: 3px solid #149ddd;
  display: inline-block;
  padding-bottom: 5px;
}

/* Centered Category Tabs */
.category-tabs {
  display: flex;
  justify-content: center; /* Center the buttons */
  align-items: center;
  gap: 12px;
  flex-wrap: wrap; /* Ensures responsiveness */
  padding-bottom: 15px;
  margin-bottom: 30px;
}

/* Category Buttons */
.category-tab {
  padding: 12px 24px;
  font-size: 14px;
  font-weight: 600;
  cursor: pointer;
  border-radius: 30px;
  transition: all 0.3s ease-in-out;
  background: #f8f9fa; /* Soft white background */
  color: #000;
  border: 2px solid #e0e0e0;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.05);
  display: inline-flex;
  align-items: center;
  justify-content: center;
}

.category-tab:hover {
  background: #000;
  color: #fff;
  border-color: #000;
  box-shadow: 0 6px 15px rgba(0, 0, 0, 0.15);
}

.category-tab.active {
  background: #000;
  color: #fff;
  border-color: #000;
  box-shadow: 0 6px 15px rgba(0, 0, 0, 0.15);
}

/* Mobile Friendly */
@media (max-width: 768px) {
  .category-tabs {
    flex-direction: row;
    flex-wrap: wrap;
    justify-content: center;
  }

  .category-tab {
    font-size: 13px;
    padding: 10px 20px;
  }
}


/* Education Items */
.isotope-container {
  margin-top: 20px;
}

.education-item {
  margin-bottom: 25px;
}

.resume-item {
  padding: 20px;
  border-left: 3px solid #000;
  background: #fff;
  border-radius: 8px;
  box-shadow: 0px 5px 15px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s ease;
}

.resume-item:hover {
  transform: translateY(-5px);
}

.resume-item h4 {
  font-size: 20px;
  font-weight: bold;
}

.resume-item h5 {
  font-size: 14px;
  color: #444;
  margin-bottom: 5px;
}

.resume-item p {
  font-size: 14px;
  color: #333;
}

/* Transition Animations */
.fade-slide-enter-active,
.fade-slide-leave-active {
  transition: all 0.4s ease-in-out;
}

.fade-slide-enter-from {
  opacity: 0;
  transform: translateY(10px);
}

.fade-slide-leave-to {
  opacity: 0;
  transform: translateY(-10px);
}
</style>
