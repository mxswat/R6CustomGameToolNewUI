<template>
  <div class="home" id="home">
    <h1 class="welcome">Welcome to -Unnamed project- </h1>
    <h2 class="todo">What do you want to do?</h2>
    <div class="links">
      <router-link to="/host" class="home-link underline-from-left">Mod a custom game</router-link>
      <br />
      <span class="buttonsub">Mod your custom game or THUNT <br> Only directx, vulkan coming soon!</span>
    </div>
    <div class="links">
      <router-link to="/client" class="join underline-from-left">Join a modded game</router-link>
      <br />
      <span class="buttonsub">custom loadout as client and more!</span>
    </div>
    <div class="links">
      <router-link to="/credits" class="home-link underline-from-left">Credits and Help</router-link>
      <br />
      <span class="buttonsub">Meet the R6 modding team</span>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import { Component, Vue } from "vue-property-decorator";
@Component({
  components: {},
  mounted() {
    VANTA.RINGS({
      el: "#home",
      mouseControls: true,
      touchControls: true,
      minHeight: 200.0,
      minWidth: 200.0,
      scale: 1.0,
      scaleMobile: 1.0
    });
  }
})
export default class Home extends Vue {}
</script>

<style lang="scss">
.home {
  align-self: center;
  width: 100%;
  max-height: 100%;
  height: 100%;
  padding-left: 30px;
  display: flex;
  flex-direction: column;
}

.History {
  padding: 8px 16px;
  background: #202225;
  display: flex;
}

.welcome {
  margin-top: 41px;
  font-size: 40px;
}

.links {
  margin-top: 40px;
}

.home-link,
.join {
  margin: 24px 0px;
  color: white;
  text-decoration: none;
  text-transform: uppercase;
  font-weight: 500;
  font-size: 21px;
  border-radius: 13px;
}

.link-btt {
  color: white;
  text-decoration: none;
  text-transform: uppercase;
  font-weight: 500;
  margin-right: 8px;
}

.buttonsub {
  font-size: 13px;
  text-transform: uppercase;
}

.home-link {
  margin-top: 60px;
}

.todo {
  font-family: "Lato";
  font-weight: 400;
}

.underline-from-left {
  transform: translateZ(0);
  backface-visibility: hidden;
  position: relative;
  overflow: hidden;
  &:before {
    content: "";
    position: absolute;
    z-index: -1;
    left: 0;
    right: 100%;
    bottom: -1px;
    background: #2980b9;
    height: 4px;
    transition-property: right;
    transition-duration: 0.3s;
    transition-timing-function: ease-out;
  }
  &:hover:before {
    right: 0;
  }
}
</style>
