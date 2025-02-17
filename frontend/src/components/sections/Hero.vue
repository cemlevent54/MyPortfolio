<template>
  <section id="hero" class="hero section dark-background">
    <!-- Background Image -->
    <img 
      :src="backgroundImage" 
      alt="Hero Background" 
      data-aos="fade-in"
    />

    <!-- Hero Content -->
    <div 
      class="container" 
      data-aos="fade-up" 
      data-aos-delay="100"
    >
      <h2>{{ name }}</h2>
      <p>
        I'm 
        <span 
          class="typed" 
          ref="typedElement" 
          data-typed-items="a junior computer engineering student, studying at Eskişehir Osmangazi University"
        ></span>
      </p>
    </div>
  </section>
</template>

<script>
import Typed from "typed.js";
import heroBackground from "@/assets/img/alpe-di-siusi-sunrise-with-sassolungo-or-langkofel-royalty-free-image-1623254127.jpg";

export default {
  name: "Hero",

  data() {
    return {
      name: "Cem Levent Avcı",
      backgroundImage: heroBackground,
    };
  },

  mounted() {
    this.initTyped();
    this.initAOS();
  },

  beforeUnmount() {
    if (this.typed) {
      this.typed.destroy();
    }
  },

  methods: {
    initTyped() {
      // Get typed strings from the `data-typed-items` attribute
      const typedElement = this.$refs.typedElement;
      const typedStrings = typedElement.getAttribute("data-typed-items");
      const stringsArray = typedStrings ? typedStrings.split(",") : [];

      // Initialize Typed.js with extracted strings
      this.typed = new Typed(typedElement, {
        strings: stringsArray,
        typeSpeed: 100,
        backSpeed: 50,
        backDelay: 2000,
        loop: true,
      });
    },

    initAOS() {
      if (window.AOS) {
        window.AOS.refresh();
      }
    },
  },
};
</script>

<style scoped>
/* Add styles here if needed */


</style>
