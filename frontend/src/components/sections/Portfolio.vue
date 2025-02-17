<template>
    <section id="portfolio" class="portfolio section">
        <div class="container section-title" data-aos="fade-up">
            <h2>Portfolio</h2>
            <p>Browse through my projects by category.</p>
        </div>

        <div class="container">
            <!-- Category Filters -->
            <ul class="portfolio-filters" data-aos="fade-up" data-aos-delay="100">
                <li 
                    v-for="filter in filteredCategories"
                    :key="filter.id"
                    :class="{ 'filter-active': activeFilter === filter.id }"
                    @click="filterItems(filter.id)"
                >
                    {{ filter.name }}
                </li>
            </ul>

            <!-- Portfolio Items with Smooth Transition -->
            <transition-group name="fade" tag="div" class="row gy-4">
                <div 
                    v-for="project in filteredProjects" 
                    :key="project.id"
                    class="col-lg-4 col-md-6"
                >
                    <div class="portfolio-content h-100">
                        <img :src="PROFILE_PHOTO_URL + project.photo" :alt="project.title" class="img-fluid">
                        <div class="portfolio-info">
                            <h4>{{ project.title }}</h4>
                            <a href="#" title="More Details" class="details-link" @click.prevent="showDetails(project)">
                                <i class="bi bi-link-45deg"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </transition-group>
        </div>

        <!-- Portfolio Details Modal -->
        <PortfolioDetails 
            v-if="isModalVisible" 
            :project="selectedProject" 
            :isVisible="isModalVisible" 
            :photoBaseUrl="PROFILE_PHOTO_URL"
            @close="isModalVisible = false"
        />
    </section>
</template>

<style scoped>
/* Fade transition */
.fade-enter-active, .fade-leave-active {
    transition: opacity 0.5s ease, transform 0.5s ease;
}
.fade-enter, .fade-leave-to {
    opacity: 0;
    transform: translateY(10px);
}
</style>



<script>
import axios from 'axios';
import PortfolioDetails from './PortfolioDetails.vue';
import { API_URL, PROFILE_PHOTO_URL } from "../../config.js";

export default {
    name: 'Portfolio',
    components: {
        PortfolioDetails
    },
    data() {
        return {
            activeFilter: "*",
            categories: [],
            portfolioProjects: [],
            selectedProject: null,
            isModalVisible: false,
            PROFILE_PHOTO_URL
        };
    },

    computed: {
        filteredCategories() {
            return [{ id: "*", name: "All" }, ...this.categories];
        },

        filteredProjects() {
            return this.activeFilter === "*"
                ? this.portfolioProjects
                : this.portfolioProjects.filter(project => project.categoryId === this.activeFilter);
        }
    },

    methods: {
        filterItems(filterId) {
            this.activeFilter = filterId;
        },

        async fetchProjects() {
            try {
                const response = await axios.get(`${API_URL}Frontend/GetProjects`);
                this.portfolioProjects = response.data.map(project => ({
                    id: project.project_ID.toString(),
                    title: project.project_Title,
                    photo: project.project_Photo,
                    categoryId: project.projectCategory_ID ? project.projectCategory_ID.toString() : null,
                    about: project.project_About || "No description available.",
                    githubUrl: project.project_GithubUrl || "",
                    liveUrl: project.project_LiveUrl || "",
                    startDate: project.project_StartDate || "Unknown",
                    endDate: project.project_EndDate || "Ongoing",
                    isActive: project.project_IsActive,
                    userId: project.user_ID,
                    client: project.project_Client || "N/A",
                    videoUrl: project.project_VideoURL || "",
                    category: project.project_Category || "Uncategorized",
                    techStack: []
                }));
            } catch (error) {
                console.error('Error fetching projects:', error);
            }
        },

        async fetchCategories() {
            try {
                const response = await axios.get(`${API_URL}Frontend/GetProjectCategories`);
                const projectCategoryIds = new Set(this.portfolioProjects.map(project => project.categoryId));

                this.categories = response.data
                    .map(category => ({
                        id: category.projectCategory_ID.toString(),
                        name: category.projectCategory_Name
                    }))
                    .filter(category => projectCategoryIds.has(category.id));
            } catch (error) {
                console.error('Error fetching categories:', error);
            }
        },

        async fetchProjectTechnologies(projectId) {
            try {
                const response = await axios.get(`${API_URL}Frontend/GetProjectTechnologies/${projectId}`);
                return response.data.map(tech => ({
                    id: tech.technology_ID.toString(),
                    name: tech.technology_Name,
                    icon: tech.technology_IconUrl
                }));
            } catch (error) {
                console.error('Error fetching project technologies:', error);
                return [];
            }
        },

        async showDetails(project) {
            this.selectedProject = { ...project, techStack: [] };
            this.isModalVisible = true;

            // Teknoloji listesini API'den çekip projeye ekle
            this.selectedProject.techStack = await this.fetchProjectTechnologies(project.id);
        }
    },

    async mounted() {
        await this.fetchProjects();  
        await this.fetchCategories();  
    }
};
</script>
