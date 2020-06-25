<template>
  <div class="home">
    <h1>Welcome to R6S Custom game tool</h1>
    <h2>What do you want to do?</h2>
    <router-link to="/host">Host a game</router-link>
    <router-link to="/join">Join a custom game</router-link>
  </div>
</template>

<script>
// @ is an alias to /src
export default {
  name: 'Home',
  components: {
    
  }
}
</script>
