<script setup>
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <div>
    <nav>
      <ul>
        <li><a href="#">Home</a></li>
        <li><a href="#">About</a></li>
        <li><a href="#">Contact</a></li>
      </ul>
    </nav>
    <main>
      <h1>Welcome to My Project</h1>
      <p>This is a test page with a simple navbar and some text content. Feel free to edit and expand on this template to fit your needs.</p>
    </main>
  </div>
</template>

<script>
export default {
  name: 'App',
};
</script>

<style scoped>
nav {
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
}
</style>

