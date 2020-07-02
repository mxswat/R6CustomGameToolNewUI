<template>
  <div class="host-page">
    <div class="History">
      <router-link to="/" class="link-btt underline-from-left">Home</router-link>
      <router-link to="/credits" class="link-btt underline-from-left">Help</router-link>
      <div class="spacer"></div>
      <span class="link-btt">Battle eye: {{BattleyeIsRunning ? 'ON' : 'OFF'}}</span>
      <span class="link-btt">Attached: {{R6SCGT_IsRunning ? 'Yes' : 'No'}}</span>
    </div>
    <div class="toolbar">
      <MxSwitch :id="'timerStop'" :label="'Stop timer'" v-model="timerCheck" v-on:changed="stopTimer"></MxSwitch>
    </div>
    <div class="router-container">
      <div class="sidebar">
        <router-link to="/host/weapons" class>Weapons</router-link>
        <router-link to="/host/maps" class="disabled">Maps & Gamemodes</router-link>
        <router-link to="/host/outfits" class="disabled">Outfits</router-link>
      </div>
      <router-view></router-view>
      <div v-if="$route.name === 'Host'" class="helper">
        <span class="welcome">Welcome to the host mod panel</span>
        <span class="message">Start by choosing one option from the menu on the left side</span>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import MxSwitch from "../components/Switch.vue";
import {
  startTool,
  BehaviorSubjects,
  changeWeapon,
  stopTimer,
  changeGadget,
  randomizeAll
} from "../services/ipcfront";
import { Subscription } from "rxjs";

@Component({
  components: {
    MxSwitch
  }
})
export default class Host extends Vue {
  BattleyeIsRunning: boolean = false;
  R6SCGT_IsRunning: boolean = false;
  BehaviorSubjects: any;
  subscriptions: Array<Subscription> = [];
  timerCheck: boolean = false;
  created() {
    startTool();
    this.BehaviorSubjects = BehaviorSubjects;
    this.subscribeToSubjects();
  }

  subscribeToSubjects() {
    const _this = this;
    this.subscriptions = [
      BehaviorSubjects.BattleyeIsRunning$.subscribe((value: any) => {
        _this.BattleyeIsRunning = value;
      }),
      BehaviorSubjects.R6SCGT_IsRunning$.subscribe((value: any) => {
        _this.R6SCGT_IsRunning = value;
      })
    ];
  }

  beforeDestroy() {
    this.subscriptions.forEach(x => x.unsubscribe());
  }

  stopTimer() {
    this.timerCheck = !this.timerCheck;
    stopTimer(this.timerCheck);
  }
}
</script>

<style lang="scss" scoped>
.router-container {
  overflow: hidden;
  flex-direction: row;
  display: flex;
  flex-grow: 1;
  flex-shrink: 1;
  flex-basis: auto;
}

.host-page {
  width: 100%;
  background: #36393f;
  max-height: 100vh;
  overflow: hidden;
  display: flex;
  flex-direction: column;
}

.toolbar {
  padding: 8px 16px;
  background: #282828;
  display: flex;
}

.helper {
  display: flex;
  flex: 1 1 auto;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  .welcome {
    margin-top: 0;
    font-weight: 500;
  }
  .message {
    font-size: 20px;
  }
}

.sidebar {
  padding: 8px;
  background: #2f3136;
  a {
    display: block;
    text-transform: uppercase;
    padding: 8px 16px;
    display: block;
    cursor: pointer;
    font-weight: 500;
    text-decoration: none;
    border-radius: 20px;
    margin-bottom: 8px;
    &:hover {
      background: gray;
    }
    &.router-link-active {
      background: linear-gradient(to right, #8a2387, #e94057, #f27121);
      font-weight: 600;
    }
  }
}

.disabled {
  cursor: not-allowed;
  pointer-events: none;
  opacity: 0.5;
}
</style>