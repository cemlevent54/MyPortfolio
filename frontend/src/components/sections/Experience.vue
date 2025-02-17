<template>
    <section id="experience" class="experience section">
      <!-- Section Title -->
      <div class="container section-title" data-aos="fade-up">
        <h2>Experience</h2>
        <p>{{ sectionTitle }}</p>
      </div>
  
      <div class="container">
        <div class="row">
          <div class="col-lg-12" data-aos="fade-up" data-aos-delay="100">
            <!-- Experience Items -->
            <div 
              v-for="experience in experiences" 
              :key="experience.id" 
              class="resume-item"
            >
              <h4>{{ experience.title }}</h4>
              <h5>{{ experience.period }}</h5>
              <p><em>{{ experience.company }}</em></p>
              <ul>
                <li v-for="(responsibility, index) in experience.responsibilities" 
                    :key="index"
                >
                  <i class="bi bi-chevron-right"></i>
                  {{ responsibility }}
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </section>
  </template>
  
  <script>
  export default {
    name: 'Experience',
  
    data() {
      return {
        sectionTitle: 'My Professional Experience',
        experiences: [
          {
            id: 1,
            title: 'Senior Graphic Design Specialist',
            period: '2019 - Present',
            company: 'Experion, New York, NY',
            responsibilities: [
              'Lead in the design, development, and implementation of the graphic, layout, and production communication materials',
              'Delegate tasks to the 7 members of the design team and provide counsel on all aspects of the project',
              'Supervise the assessment of all graphic materials in order to ensure quality and accuracy of the design',
              'Oversee the efficient use of production project budgets ranging from $2,000 - $25,000'
            ]
          },
          {
            id: 2,
            title: 'Graphic Design Specialist',
            period: '2017 - 2018',
            company: 'Stepping Stone Advertising, New York, NY',
            responsibilities: [
              'Developed numerous marketing programs (logos, brochures, infographics, presentations, and advertisements)',
              'Managed up to 5 projects or tasks at a given time while under pressure',
              'Recommended and consulted with clients on the most appropriate graphic design',
              'Created 4+ design presentations and proposals a month for clients and account managers'
            ]
          }
        ]
      }
    },
  
    mounted() {
      // Initialize AOS
      if (window.AOS) {
        window.AOS.refresh()
      }
    }
  }
  </script>
  
  <style scoped>
 
  </style>