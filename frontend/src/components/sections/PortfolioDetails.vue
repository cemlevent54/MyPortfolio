<template>
  <div v-if="isVisible" class="portfolio-modal" @click.self="close">
      <div class="modal-content">
          <h2 class="portfolio-title">{{ project.title }}</h2>
          <button @click="close" class="close-button" aria-label="Close modal">&times;</button>
          
          <section id="portfolio-details" class="portfolio-details">
              <div class="container">
                  <div class="row gy-4">
                      <div class="col-lg-6">
                          <div class="portfolio-details-image">
                            <img :src="photoBaseUrl + project.photo" :alt="project.title" class="img-fluid">
                          </div>
                      </div>

                      <div class="col-lg-6">
                          <div class="portfolio-info">
                              <h3>Project Information</h3>
                              <ul>
                                  <li><strong>Category:</strong> {{ project.categoryId }}</li>
                                  <li><strong>Project Date:</strong> {{ project.startDate }} - {{ project.endDate }}</li>
                                  <li v-if="project.liveUrl">
                                      <strong>Project URL:</strong> 
                                      <a :href="project.liveUrl" target="_blank">View on Youtube</a>
                                  </li>
                                  <li v-if="project.githubUrl">
                                      <strong>Github URL:</strong> 
                                      <a :href="project.githubUrl" target="_blank">View on Github</a>
                                  </li>
                              </ul>
                          </div>
                      </div>

                      <!-- about -->
                      <div class="col-lg-12">
                          <div class="portfolio-info">
                              <h3>About</h3>
                              <p>{{ project.about }}</p>
                          </div>
                      </div>

                      <div class="col-lg-12" v-if="project.techStack.length">
                          <div class="portfolio-info">
                              <h3>Tech Stack</h3>
                              <div class="tech-stack">
                                  <div v-for="tech in project.techStack" :key="tech.id" class="tech-item">
                                      <img :src="tech.icon" class="tech-icon me-3" style="width: 40px; height: 40px;">
                                      <span>{{ tech.name }}</span>
                                  </div>
                              </div>
                          </div>
                      </div>
                  </div>
              </div>
          </section>
      </div>
  </div>
</template>

<script>
export default {
  props: {
      project: Object,
      isVisible: Boolean,
      photoBaseUrl: String
  },
  methods: {
      close() {
          this.$emit('close')
      }
  }
}
</script>

<style scoped>
.portfolio-modal {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.8);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
  overflow-y: auto;
}

.modal-content {
  background-color: white;
  padding: 20px;
  border-radius: 8px;
  width: 90%;
  max-width: 800px;
  max-height: 90vh;
  overflow-y: auto;
  position: relative;
  box-shadow: 0px 5px 15px rgba(0, 0, 0, 0.3);
}

/* Çarpı butonu sağ üstte olacak şekilde ayarlandı */
.close-button {
  position: absolute;
  top: 15px;
  right: 15px;
  font-size: 28px;
  cursor: pointer;
  background: none;
  border: none;
  color: #333; /* Siyah tonları */
  transition: color 0.3s ease;
}

.close-button:hover {
  color: #000; /* Daha koyu siyah */
}

/* Tech Stack Görünümü */
.tech-stack {
  display: flex;
  flex-wrap: wrap;
  gap: 1rem;
  padding: 0.5rem 0;
}

.tech-item {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.5rem 1rem;
  border-radius: 8px;
  transition: background-color 0.3s ease;
  min-width: 120px;
}

.tech-item:hover {
  background-color: rgba(0, 0, 0, 0.05);
}

.tech-icon {
  width: 40px;
  height: 40px;
}

@media (max-width: 768px) {
  .modal-content {
      width: 95%;
      padding: 15px;
  }
}
</style>
