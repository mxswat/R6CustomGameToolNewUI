<template>
  <div id="app">
    <router-view></router-view>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component({
  components: {}
})
export default class App extends Vue {}
</script>

<style lang="scss">
.gradme {
  // background: linear-gradient(230deg, #a24bcf, #4b79cf, #4bc5cf);
  // background-size: 300% 300%;
  // -webkit-animation: MoveBG 60s ease infinite;
  // -moz-animation: MoveBG 60s ease infinite;
  // -o-animation: MoveBG 60s ease infinite;
  // animation: MoveBG 60s ease infinite;
  height: 100%;
  width: 100%;
}
@keyframes MoveBG {
  0% {
    background-position: 0% 84%;
  }
  50% {
    background-position: 100% 16%;
  }
  100% {
    background-position: 0% 84%;
  }
}
body {
  margin: 0;
  height: 100%;
  width: 100%;
}
h1,
h2,
h3,
h4 {
  font-family: "Poppins";
  font-weight: 400;
  margin: 0;
}
#app {
  font-family: "Lato";
  font-weight: 300;
  color: white;
  display: flex;
  height: 100%;
  width: 100%;
}
</style>
