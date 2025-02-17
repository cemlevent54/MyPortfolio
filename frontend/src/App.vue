<script setup>
import { RouterLink, RouterView } from 'vue-router'
import 'bootstrap';
import AOS from 'aos';
import Typed from 'typed.js';
import Glightbox from 'glightbox';
import Swiper from 'swiper';
import Isotope from 'isotope-layout';

// components
import Sidebar from './components/layout/Sidebar.vue';
import Hero from './components/sections/Hero.vue';
import About from './components/sections/About.vue';
import Education from './components/sections/Education.vue';
import Experience from './components/sections/Experience.vue';
import Skills from './components/sections/Skills.vue';
import Portfolio from './components/sections/Portfolio.vue';
import Contact from './components/sections/Contact.vue';
import ScrollTop from './components/layout/ScrollTop.vue';
import FooterBar from './components/layout/FooterBar.vue';


</script>

<template>
  <div class="index-page">
    <Sidebar />

    <main class="main">
      <Hero />
      <About />
      <Education />
      <!-- <Experience /> -->
      <Skills />
      <Portfolio />
      <Contact />
      <FooterBar />
      
    </main>
    
    <ScrollTop />
    
  </div>
</template>

<script>
export default {
  name: 'App',
};
</script>

<style scoped>
@import 'assets/vendor/bootstrap/css/bootstrap.min.css';
@import 'assets/vendor/bootstrap-icons/bootstrap-icons.css';
@import 'assets/vendor/aos/aos.css';
@import 'assets/vendor/glightbox/css/glightbox.min.css';
@import 'assets/vendor/swiper/swiper-bundle.min.css';
@import 'assets/css/main.css';
@import 'assets/css/Skills/skills.css';

@import 'assets/css/main.css';
@import 'assets/css/Skills/skills.css';



/* nav {
  background-color: #333;
  padding: 1rem;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  display: inline;
  margin-right: 1rem;
}

a {
  color: white;
  text-decoration: none;
}

a:hover {
  text-decoration: underline;
}

main {
  padding: 1rem;
}

h1 {
  color: #333;
}

p {
  font-size: 1rem;
  color: #666;
} */
</style>

