<template>
  <header id="header" class="header dark-background d-flex flex-column">
    <!-- Mobile Toggle Button -->
    <!-- <i class="header-toggle d-xl-none bi bi-list" @click="toggleMobileNav"></i> -->
     
    <!-- Profile Image -->
    <div class="profile-img">
      <img :src="profileImage" alt="Profile" class="img-fluid rounded-circle">
    </div>

    <!-- Logo/Name -->
    <a href="#" class="logo d-flex align-items-center justify-content-center">
      <h1 class="sitename">{{ siteName }}</h1>
    </a>

    <!-- Social Links -->
    <div class="social-links text-center">
      <a v-for="social in socialLinks" 
        :key="social.platform" 
        :href="social.url" 
        :class="social.platform"
        target="_blank" 
        rel="noopener noreferrer">
        <i :class="social.icon"></i>
      </a>
    </div>

    <!-- Navigation Menu -->
    <nav id="navmenu" class="navmenu" :class="{ 'mobile-nav-active': isMobileNavActive }">
      <ul>
        <li v-for="item in navItems" :key="item.id">
          <a href="#" 
             :class="{ active: item.isActive }" 
             @click.prevent="handleNavClick(item.id)">
            <i :class="['bi', item.icon, 'navicon']"></i>
            {{ item.text }}
          </a>
        </li>
      </ul>
    </nav>
  </header>
</template>

<script>
import heroProfileImage from '@/assets/img/icons8-person-96.png'

export default {
  name: 'Sidebar',
  
  data() {
    return {
      isMobileNavActive: false,
      profileImage: heroProfileImage,
      siteName: 'Cem Levent Avcı',
      socialLinks: [
        { platform: 'twitter', icon: 'bi-twitter-x', url: 'your-twitter-link' },
        { platform: 'instagram', icon: 'bi-instagram', url: 'your-instagram-link' },
        { platform: 'linkedin', icon: 'bi-linkedin', url: 'your-linkedin-link' },
        { platform: 'github', icon: 'bi-github', url: 'your-github-link' }
      ],
      navItems: [
        { id: 1, text: 'Home', href: '#hero', icon: 'bi-house', isActive: true },
        { id: 2, text: 'About', href: '#about', icon: 'bi-person', isActive: false },
        { id: 3, text: 'Education', href: '#education', icon: 'bi-mortarboard', isActive: false },
        // { id: 4, text: 'Experience', href: '#experience', icon: 'bi-briefcase', isActive: false },
        { id: 5, text: 'Skills', href: '#skills', icon: 'bi-gear', isActive: false },
        { id: 6, text: 'Portfolio', href: '#portfolio', icon: 'bi-images', isActive: false },
        { id: 7, text: 'Contact', href: '#contact', icon: 'bi-envelope', isActive: false }
      ]
    }
  },

  methods: {
    toggleMobileNav() {
      this.isMobileNavActive = !this.isMobileNavActive;
    },

    handleNavClick(id) {
      const targetItem = this.navItems.find(item => item.id === id);
      if (!targetItem) return;

      // Aktif durumu güncelle
      this.navItems.forEach(item => {
        item.isActive = item.id === id;
      });

      // Sayfayı ilgili bölüme kaydır
      const section = document.querySelector(targetItem.href);
      if (section) {
        section.scrollIntoView({ behavior: 'smooth' });
      }

      // URL'de değişiklik yapmadan kalmasını sağla
      history.pushState(null, null, ' ');

      // Mobil menüyü kapat
      if (this.isMobileNavActive) {
        this.isMobileNavActive = false;
      }
    }
  },

  mounted() {
    // Scroll izleyicisini ekle
    window.addEventListener('scroll', () => {
      const sections = document.querySelectorAll('section');
      const scrollPosition = window.scrollY;

      sections.forEach((section) => {
        const sectionTop = section.offsetTop - 100;
        const sectionHeight = section.offsetHeight;
        const sectionId = section.getAttribute('id');

        if (scrollPosition >= sectionTop && scrollPosition < sectionTop + sectionHeight) {
          this.navItems.forEach(item => {
            item.isActive = item.href === `#${sectionId}`;
          });
        }
      });
    });
  }
}
</script>

<style scoped>
@import '@/assets/css/main.css';

/* Sidebar Stilleri */
.header {
  position: fixed;
  top: 0;
  left: 0;
  bottom: 0;
  width: 250px;
  padding: 40px 20px;
  z-index: 996;
  transition: all 0.3s;
}

.header .profile-img {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
}

.header .profile-img img {
  display: flex;
  justify-content: center;
  align-items: center;
  object-fit: cover; /* Resmi tam oturtur */
  width: 145px;  /* Daha büyük çerçeve */
  height: 145px; /* Daha büyük çerçeve */
  border-radius: 50%;
  border: 5px solid rgba(255, 255, 255, 0.2); /* Hafif bir çerçeve efekti */
}




.header .sitename {
  font-size: 18px;
  margin: 0;
  padding: 0;
  font-weight: 600;
  color: #fff;
}

.social-links a {
  font-size: 14px;
  padding: 6px 0;
  margin-right: 4px;
  width: 30px;
  height: 30px;
}

.navmenu a {
  display: flex;
  align-items: center;
  color: #a8a9b4;
  padding: 8px 12px;
  margin-bottom: 6px;
  font-size: 14px;
}

.navmenu a i {
  font-size: 20px;
  padding-right: 6px;
}

@media (max-width: 1199px) {
  .header {
    left: -250px;
  }

  .header.mobile-nav-active {
    left: 0;
  }

  .header-toggle {
    font-size: 20px;
    width: 35px;
    height: 35px;
  }
}
</style>
