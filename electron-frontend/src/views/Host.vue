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
      <MxSwitch
        :id="'timerStop'"
        :label="'Stop timer'"
        v-model="timerCheck"
        v-on:changed="stopTimer"
      ></MxSwitch>
      <div class="spacer"></div>
      <MxSwitch
        :id="'rickRoll'"
        :label="'Unlock All'"
        v-model="rickRoll"
        v-on:changed="rickRolling"
      ></MxSwitch>
    </div>
    <div class="router-container">
      <div class="sidebar" :class="{ 'collapsed': collapsed, 'centerIcons' : centerIcons }">
        <router-link to="/host/weapons" class="item">
          <span class="name">Weapons</span>
          <img src="../assets/weapon.png" class="icon" />
        </router-link>
        <router-link to="/host/maps" class="item">
          <span class="name">Maps & Gamemodes</span>
          <img src="../assets/map.png" class="icon" />
        </router-link>
        <router-link to="/host/outfits" class="item">
          <span class="name">Outfits</span>
          <img src="../assets/operator.png" class="icon" />
        </router-link>
        <router-link to="/host/requests" class="item">
          <span class="name">Loadout Requests</span>
          <img src="../assets/connection.png" class="icon" />
          <span class="countReq" v-if="getRequestsCount()">{{getRequestsCount()}}</span>
        </router-link>
        <div class="item collapse-btt" @click="collapse()">
          <span class="name">Collapse</span>
          <img src="../assets/collapseleft.png" />
        </div>
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
import RequestManagerInst from "../services/RequestManager";

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
  rickRoll: boolean = false;
  loadoutRequestsCount: number = 0;
  collapsed: boolean = false;
  centerIcons: boolean = false;

  created() {
    console.log("Host.vue");
    startTool();
    this.BehaviorSubjects = BehaviorSubjects;
    this.subscribeToSubjects();
    this.collapsed =
      localStorage.getItem("collapsed") === "true" ? true : false;
    this.centerIcons = this.collapsed;
  }

  subscribeToSubjects() {
    const _this = this;
    this.subscriptions = [
      BehaviorSubjects.BattleyeIsRunning$.subscribe((value: any) => {
        _this.BattleyeIsRunning = value;
      }),
      BehaviorSubjects.R6SCGT_IsRunning$.subscribe((value: any) => {
        _this.R6SCGT_IsRunning = value;
      }),
      RequestManagerInst.getRequests$().subscribe((value: Array<any>) => {
        _this.loadoutRequestsCount = value.length;
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

  getRequestsCount() {
    return this.loadoutRequestsCount ? this.loadoutRequestsCount : false;
  }

  rickRolling() {
    window.open("https://youtu.be/dQw4w9WgXcQ", "_blank");
  }

  collapse() {
    this.collapsed = !this.collapsed;
    localStorage.setItem("collapsed", this.collapsed + "");
    if (this.collapsed) {
      setTimeout(() => {
        this.centerIcons = this.collapsed;
      }, 350);
    } else {
      this.centerIcons = this.collapsed;
    }
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
  display: flex;
  flex-direction: column;
  &.collapsed {
    &.centerIcons {
      .icon {
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
      }
    }
    .item {
      min-width: 28px;
      .name {
        max-width: 0;
        transition: all 350ms ease-in-out;
        margin-right: 0px;
      }
      &.collapse-btt {
        img {
          transform: rotate(180deg);
          transition: transform 350ms ease-in-out;
        }
      }
    }
  }
  .item {
    text-transform: uppercase;
    padding: 8px 16px;
    cursor: pointer;
    font-weight: 500;
    text-decoration: none;
    border-radius: 20px;
    margin-bottom: 8px;
    position: relative;
    min-width: 210px;
    height: 30px;
    transition: all 350ms ease-in-out;
    &.collapse-btt {
      margin-top: auto;
      img {
        transition: transform 350ms ease-in-out;
        float: right;
      }
    }
    .name {
      display: inline-block;
      transition: all 350ms ease-in-out;
      overflow: hidden;
      max-width: 250px;
      line-height: 30px;
      white-space: nowrap;
      margin-right: 8px;
    }
    .icon {
      right: 14px;
      position: absolute;
    }
    &:hover {
      background: gray;
    }
    &.router-link-active {
      background: linear-gradient(to right, #8a2387, #e94057, #f27121);
      font-weight: 600;
    }
    .countReq {
      background: #f04747;
      border-radius: 50%;
      font-weight: 600;
      position: absolute;
      right: 0;
      bottom: 0;
      width: 20px;
      height: 20px;
      text-align: center;
      border: 3px solid #2f3136;
    }
  }
}

.disabled {
  cursor: not-allowed;
  pointer-events: none;
  opacity: 0.5;
}
</style>