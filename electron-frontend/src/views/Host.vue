<template>
  <div class="host-page">
    <div class="History">
      <router-link to="/" class="link-btt underline-from-left">Home</router-link>
      <router-link to="/credits" class="link-btt underline-from-left">Help</router-link>
    </div>
    <div class="toolbar">
      <span class="link-btt">Battle eye: {{BattleyeIsRunning ? 'ON' : 'OFF'}}</span>
      <span class="link-btt">R6SCGT: {{BattleyeIsRunning ? 'ON' : 'OFF'}}</span>
    </div>
    <div class="tabs-container">
      <div class="players tabs">
        <div class="selector"></div>
        <span class="player" v-for="(player, index) in players" :key="player.username">
          <input
            v-model="selectedPlayer"
            class="playerradio"
            type="radio"
            :id="'player-' + index"
            name="player"
            :value="index"
          />
          <label class="playerlabel" :for="'player-' + index">{{player.username}}</label>
        </span>
      </div>
    </div>
    <div class="overflow-container">
      <div class="container">
        <List
          v-on:selectedElement="onSelectedItem($event, true)"
          :enabled="timerCheck"
          :list="gunslist"
          :Title="'weapons'"
        ></List>
        <List
          v-on:selectedElement="onSelectedItem($event, false)"
          :enabled="timerCheck"
          :list="gadgetslist"
          :Title="'gadgets'"
        ></List>
        <div class="utils">
          <h3 class="list-title">Utility</h3>
          <div class="switch">
            <div>
              <input type="checkbox" id="1" v-model="timerCheck" @change="stopTimer($event)" />
              <label for="1">
                <span class="switch-label">Stop timer</span>
                <span></span>
              </label>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import {
  startTool,
  BehaviorSubjects,
  changeWeapon,
  stopTimer,
  changeGadget
} from "../services/ipcfront";

import { Subscription } from "rxjs";
import animationTabs from "../defaults/hosttabs";
import GUNS from "../defaults/guns";
import GADGETS from "../defaults/gadgets";
import List from "./List.vue";

@Component({
  components: {
    List
  }
})
export default class Host extends Vue {
  selectedPlayer = 0;
  players: Array<any> = [];
  subscriptions: Array<Subscription> = [];
  // Definition copied from the VS UI
  // TODO DEFINE TYPE
  gunslist: any = [];
  gadgetslist: any = [];
  BehaviorSubjects: any;
  BattleyeIsRunning: boolean;
  R6SCGT_IsRunning: boolean;
  timerCheck: boolean = false;

  created() {
    for (let i = 0; i < 10; i++) {
      this.players.push({
        username: "Player " + (i + 1)
      });
    }
    startTool();
    this.gunslist = GUNS;
    this.gadgetslist = GADGETS;
    this.BehaviorSubjects = BehaviorSubjects;
    this.subscribeToSubjects();
  }
  mounted() {
    animationTabs();
  }
  subscribeToSubjects() {
    this.subscriptions = [
      BehaviorSubjects.PlayerUpdated$.subscribe(this.onPlayerUpdated),
      BehaviorSubjects.BattleyeIsRunning$.subscribe((value: boolean) => {
        this.BattleyeIsRunning = value;
      }),
      BehaviorSubjects.R6SCGT_IsRunning$.subscribe((value: boolean) => {
        this.R6SCGT_IsRunning = value;
      })
    ];
  }
  onPlayerUpdated(playerData: any) {
    if (playerData && playerData.name !== null) {
      const index = playerData.index;
      this.players[index].username = playerData.name;
      this.players[index].primary = playerData.primary;
      this.players[index].secondary = playerData.secondary;
      this.players[index].primaryGadget = playerData.primarygadget;
      this.players[index].secondaryGadget = playerData.secondarygadget;
    }
  }
  beforeDestroy() {
    this.subscriptions.forEach(x => x.unsubscribe());
  }

  onSelectedItem(event: any, isWeapon: boolean) {
    if (isWeapon) {
      changeWeapon(
        this.selectedPlayer.toString(),
        event.slotIndex,
        event.elementIndex
      );
    } else {
      changeGadget(
        this.selectedPlayer.toString(),
        event.slotIndex,
        event.elementIndex
      );
    }
  }

  stopTimer() {
    stopTimer(this.timerCheck);
  }
}
</script>

<style lang="scss" scoped>
.overflow-container {
  overflow: hidden;
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

.playerradio {
  display: none;
}

.tabs-container {
  padding: 8px;
  background: #2f3136;
  overflow: hidden;
  min-height: 35px;
}

.players {
  display: flex;
  position: relative;
}
.playerlabel {
  text-transform: uppercase;
  padding: 8px 16px;
  margin-right: 8px;
  display: block;
  cursor: pointer;
  font-weight: 500;
}
.player {
  max-width: 150px;
  z-index: 2;
}

input.playerradio[type="radio"]:checked + label {
  font-weight: 600;
  border-radius: 20px;
}

.container {
  display: grid;
  grid-template-columns: repeat(3, [col] 1fr);
  grid-template-rows: repeat(1, [row] auto);
  gap: 8px;
  flex-grow: 1;
  flex-shrink: 1;
  flex-basis: auto;
}

.selector {
  height: 100%;
  display: inline-block;
  position: absolute;
  left: 0px;
  top: 0px;
  z-index: 1;
  border-radius: 50px;
  transition-duration: 0.6s;
  transition-timing-function: cubic-bezier(0.68, -0.55, 0.265, 1.55);
  background: linear-gradient(45deg, #05abe0 0%, #8200f4 100%);
  z-index: 0;
}

.toolbar {
  padding: 8px 16px;
}

$white: #e8e9ed;
$gray: #ff4b77;
$blue: #18172c;
$green: #00d084;
$pink: #ff4b77;

.utils {
  label {
    cursor: pointer;
  }

  [type="checkbox"] {
    position: absolute;
    left: -9999px;
  }

  .switch li::before {
    content: counter(switchCounter);
    position: absolute;
    top: 50%;
    left: -30px;
    transform: translateY(-50%);
    font-size: 2rem;
    font-weight: bold;
    color: $pink;
  }

  .switch-label {
    font-size: 18px;
    font-weight: 400;
  }

  .switch label {
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 15px;
  }

  .switch span:last-child {
    position: relative;
    width: 50px;
    height: 26px;
    border-radius: 15px;
    box-shadow: inset 0 0 5px rgba(0, 0, 0, 0.4);
    background: $gray;
    transition: all 0.3s;
  }

  .switch span:last-child::before,
  .switch span:last-child::after {
    content: "";
    position: absolute;
  }

  .switch span:last-child::before {
    left: 1px;
    top: 1px;
    width: 24px;
    height: 24px;
    background: $white;
    border-radius: 50%;
    z-index: 1;
    transition: transform 0.3s;
  }

  .switch span:last-child::after {
    top: 50%;
    right: 8px;
    width: 12px;
    height: 12px;
    transform: translateY(-50%);
    background: url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/162656/uncheck-switcher.svg);
    background-size: 12px 12px;
  }

  .switch [type="checkbox"]:checked + label span:last-child {
    background: $green;
  }

  .switch [type="checkbox"]:checked + label span:last-child::before {
    transform: translateX(24px);
  }

  .switch [type="checkbox"]:checked + label span:last-child::after {
    width: 14px;
    height: 14px;
    /*right: auto;*/
    left: 8px;
    background-image: url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/162656/checkmark-switcher.svg);
    background-size: 14px 14px;
  }
}
</style>