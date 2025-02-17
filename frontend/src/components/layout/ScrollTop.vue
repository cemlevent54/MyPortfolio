<template>
    <a 
      href="#" 
      id="scroll-top" 
      class="scroll-top d-flex align-items-center justify-content-center"
      :class="{ 'active': isVisible }"
      @click.prevent="scrollToTop"
    >
      <i class="bi bi-arrow-up-short"></i>
    </a>
  </template>
  
  <script>
  export default {
    name: 'ScrollTop',
    
    data() {
      return {
        isVisible: false
      }
    },
  
    mounted() {
      window.addEventListener('scroll', this.toggleVisibility)
    },
  
    unmounted() {
      window.removeEventListener('scroll', this.toggleVisibility)
    },
  
    methods: {
      toggleVisibility() {
        // Sayfa 100px'den fazla scroll edildiğinde butonu göster
        this.isVisible = window.scrollY > 100
      },
  
      scrollToTop() {
        window.scrollTo({
          top: 0,
          behavior: 'smooth'
        })
      }
    }
  }
  </script>
  
  <style scoped>
  .scroll-top {
    position: fixed;
    visibility: hidden;
    opacity: 0;
    right: 15px;
    bottom: 15px;
    z-index: 99999;
    background: var(--primary-color);
    width: 40px;
    height: 40px;
    border-radius: 4px;
    transition: all 0.4s;
    text-decoration: none;
  }
  
  .scroll-top i {
    font-size: 24px;
    color: #fff;
    line-height: 0;
  }
  
  .scroll-top:hover {
    background: var(--primary-color-hover);
  }
  
  .scroll-top.active {
    visibility: visible;
    opacity: 1;
  }
  </style>