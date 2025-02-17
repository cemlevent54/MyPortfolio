<template>
  <section id="skills" class="skills section">
    <div class="container" data-aos="fade-up">
      <h2 class="section-heading">Skills</h2>
    </div>

    <div class="container">
      <!-- Modern Category Tabs -->
      <div class="category-tabs" data-aos="fade-up">
        <div
          v-for="category in filteredCategories"
          :key="category.SkillCategory_ID"
          :class="['category-tab', { active: activeCategory === category.SkillCategory_Name }]"
          @click="setActiveCategory(category.SkillCategory_Name)"
        >
          {{ category.SkillCategory_Name }}
        </div>
      </div>

      <!-- Skills Grid -->
      <div class="row isotope-container" data-aos="fade-up" data-aos-delay="100">
        <transition-group name="fade-slide" tag="div" class="row">
          <div
            v-for="skill in filteredSkills"
            :key="skill.Skill_ID"
            class="col-lg-4 col-md-6 skill-item isotope-item"
          >
            <div class="resume-item">
              <div class="skill-icon">
                <img :src="skill.Skill_IconUrl" :alt="skill.Skill_Name">
              </div>
              <h4>{{ skill.Skill_Name }}</h4>
            </div>
          </div>
        </transition-group>
      </div>
    </div>
  </section>
</template>

<script>
import axios from "axios";
import { API_URL } from "../../config.js";

export default {
  name: "SkillsSection",

  data() {
    return {
      activeCategory: "All", // Varsayılan kategori
      categories: [],
      skills: [],
    };
  },

  computed: {
    filteredCategories() {
      // Altında en az bir yetenek olan kategorileri filtreler
      return [
        { SkillCategory_ID: 0, SkillCategory_Name: "All" }, // "All" kategorisi eklendi
        ...this.categories.filter(category =>
          this.skills.some(skill => skill.SkillCategory_ID === category.SkillCategory_ID)
        ),
      ];
    },

    filteredSkills() {
      if (this.activeCategory === "All") {
        return this.skills;
      }
      return this.skills.filter(skill =>
        this.categories.some(category =>
          category.SkillCategory_Name === this.activeCategory &&
          skill.SkillCategory_ID === category.SkillCategory_ID
        )
      );
    },
  },

  methods: {
    setActiveCategory(categoryName) {
      this.activeCategory = categoryName;
    },

    async fetchSkillsAndCategories() {
      try {
        // API'den yetenekleri çekme
        const skillsResponse = await axios.get(`${API_URL}Frontend/GetSkills`);
        this.skills = skillsResponse.data.map(skill => ({
          Skill_ID: skill.skill_ID,
          Skill_Name: skill.skill_Name,
          Skill_IconUrl: skill.skill_IconUrl,
          SkillCategory_ID: skill.skillCategory_ID,
        })) || [];

        console.log("Fetched Skills:", this.skills);

        // API'den kategorileri çekme
        const categoriesResponse = await axios.get(`${API_URL}Frontend/GetSkillCategories`);
        this.categories = categoriesResponse.data.map(category => ({
          SkillCategory_ID: category.skillCategory_ID,
          SkillCategory_Name: category.skillCategory_Name,
        })) || [];

        console.log("Fetched Categories:", this.categories);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    },
  },

  async mounted() {
    await this.fetchSkillsAndCategories();
  },
};
</script>

<style scoped>
/* Modern Minimalistic Skills Section */
#skills {
  padding: 60px 0;
  background-color: #fff;
  color: #000;
}

/* Section Title */
.section-heading {
  font-size: 2rem;
  font-weight: bold;
  margin-bottom: 20px;
  border-bottom: 3px solid #149ddd;
  display: inline-block;
  padding-bottom: 5px;
}

/* Centered Category Tabs */
.category-tabs {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 12px;
  flex-wrap: wrap;
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
  background: #f8f9fa;
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

/* Skills Grid */
.isotope-container {
  margin-top: 20px;
}

.skill-item {
  margin-bottom: 25px;
  text-align: center;
}

/* Skill Card */
/* Skill Card - Flexbox for Horizontal Alignment */
.resume-item {
  display: flex;
  align-items: center; /* Dikey hizalamayı ortalar */
  gap: 15px; /* İkon ve yazı arasına boşluk ekler */
  padding: 15px;
  border-left: 3px solid #000;
  background: #fff;
  border-radius: 8px;
  box-shadow: 0px 5px 15px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s ease;
}

.resume-item:hover {
  transform: translateY(-5px);
}

/* Skill Icon - Adjust Image Size */
.skill-icon img {
  width: 40px; /* Daha iyi orantılı bir görünüm için küçültüldü */
  height: 40px;
  object-fit: contain; /* Görselin kırpılmasını engeller */
}

/* Skill Text */
.resume-item h4 {
  font-size: 18px;
  font-weight: bold;
  margin: 0; /* Fazladan boşluğu kaldırır */
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
