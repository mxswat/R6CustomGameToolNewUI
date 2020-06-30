<template>
  <div class="host-page">
    <div class="History">
      <router-link to="/" class="link-btt underline-from-left">Home</router-link>
      <span class="link-btt">{{BehaviorSubjects.R6SCGT_IsRunning$.value}}</span>
      <span class="link-btt">{{BehaviorSubjects.BattleyeIsRunning$.value}}</span>
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
        <List v-on:selectedElement="onSelectedItem($event)" :list="gunslist" :Title="'Weapons'"></List>
        <List v-on:selectedElement="onSelectedItem($event)" :list="gadgetslist" :Title="'Gadgets'"></List>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { startTool, BehaviorSubjects, changeWeapon } from "../services/ipcfront";

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
    this.subscriptions.push(BehaviorSubjects.PlayerUpdated$.subscribe(this.onPlayerUpdated));
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

  onSelectedItem(event: any) {
    changeWeapon(this.selectedPlayer.toString(), event.slotIndex, event.elementIndex);
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
</style>