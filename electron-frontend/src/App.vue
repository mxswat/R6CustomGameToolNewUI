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
a {
  color: white;
}
#app {
  font-family: "Lato";
  font-weight: 300;
  color: white;
  display: flex;
  height: 100%;
  width: 100%;
}

input:focus {
  outline: none;
}

button:focus {
  outline: none;
}

::-webkit-scrollbar {
  width: 5px;
  height: 5px;
}
::-webkit-scrollbar-button {
  width: 0px;
  height: 0px;
}
::-webkit-scrollbar-thumb {
  background: #e1e1e1;
  border: 0px none #ffffff;
  border-radius: 50px;
}
::-webkit-scrollbar-thumb:hover {
  background: #ffffff;
}
::-webkit-scrollbar-thumb:active {
  background: #000000;
}
::-webkit-scrollbar-track {
  background: #666666;
  border: 0px none #ffffff;
  border-radius: 50px;
}
::-webkit-scrollbar-track:hover {
  background: #666666;
}
::-webkit-scrollbar-track:active {
  background: #333333;
}
::-webkit-scrollbar-corner {
  background: transparent;
}

.tooltips-on {
  [data-title] {
    position: relative;
  }

  [data-title]:hover::before {
    content: attr(data-title);
    position: absolute;
    display: inline-block;
    padding: 3px 6px;
    border-radius: 2px;
    background: #000;
    color: #fff;
    font-size: 12px;
    font-family: sans-serif;
    white-space: nowrap;
    top: -24px;
    right: -58px;
    z-index: 999;
  }
  [data-title]:hover::after {
    content: "";
    position: absolute;
    top: -4px;
    left: 8px;
    display: inline-block;
    color: #fff;
    border: 8px solid transparent;
    border-top: 8px solid #000;
    z-index: 999;
  }
}

.coming-soon-page {
  display: flex;
  flex: 1 1 auto;
  align-items: center;
  justify-content: center;
  span {
    font-size: 25px;
    text-transform: uppercase;
    font-weight: 500;
  }
}

.spacer {
  display: flex;
  flex: 1 1 auto;
}

.overflow-container {
  overflow: hidden;
  display: flex;
  flex-grow: 1;
  flex-shrink: 1;
  flex-basis: auto;
}
</style>

