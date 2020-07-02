<template>
  <div class="weapons">
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
          :list="gunslist"
          :Title="'weapons'"
        ></List>
        <List
          v-on:selectedElement="onSelectedItem($event, false)"
          :list="gadgetslist"
          :Title="'gadgets'"
        ></List>
        <div class="utils">
          <h3 class="list-title">Utility</h3>
          <div class="utils-list">
            <label class="randomize-lab">
              <span class="switch-label">Randomize all</span>
              <button @click="randomizeAll()" id="random" class="ibtt randomize"></button>
            </label>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { Component } from "vue-property-decorator";
import {
  startTool,
  BehaviorSubjects,
  changeWeapon,
  stopTimer,
  changeGadget,
  randomizeAll
} from "../services/ipcfront";

import { Subscription } from "rxjs";
import animationTabs from "../defaults/hosttabs";
import GUNS from "../defaults/guns";
import GADGETS from "../defaults/gadgets";
import List from "./List.vue";
import MxSwitch from "../components/Switch.vue";

@Component({
  components: {
    List,
    MxSwitch
  }
})
export default class Weapons extends Vue {
  selectedPlayer = 0;
  players: Array<any> = [];
  subscriptions: Array<Subscription> = [];
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
  }

  mounted() {
    animationTabs();
  }

  subscribeToSubjects() {
    const _this = this;
    this.subscriptions = [
      BehaviorSubjects.PlayerUpdated$.subscribe(this.onPlayerUpdated)
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

  randomizeAll() {
    randomizeAll();
  }
}
</script>

<style lang="scss" scoped>
.weapons {
  overflow: hidden;
  flex-direction: column;
  display: flex;
  flex-grow: 1;
  flex-shrink: 1;
  flex-basis: auto;
}

.overflow-container {
  overflow: hidden;
  display: flex;
  flex-grow: 1;
  flex-shrink: 1;
  flex-basis: auto;
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

.utils {
  .utils-list {
    padding: 16px;
  }

  .ibtt {
    font-size: 18px;
    font-weight: 400;
    border: 0;
  }

  .randomize-lab {
    display: flex;
    .switch-label {
      font-size: 18px;
      font-weight: 400;
      flex: 1 1 auto;
    }
  }

  .randomize {
    background: linear-gradient(
      124deg,
      #1ddde8,
      #2b1de8,
      #dd00f3,
      #ff2400,
      #e81d1d,
      #e8b71d,
      #e3e81d,
      #1de840,
      #1ddde8,
      #2b1de8,
      #dd00f3
    );
    height: 25px;
    width: 50px;
    padding: 0;
    border-radius: 15px;
    box-shadow: inset 0 0 5px rgba(0, 0, 0, 0.4);
    background-size: 600%;
    background-position: right;
    transition: all 0.8s;
    &:active,
    &:focus {
      animation: btn-color 500ms forwards linear;
    }
    cursor: pointer;
    &::after {
      content: "";
      background-image: url("data:image/svg+xml,%3Csvg height='300px' width='300px' fill='white' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink' version='1.1' x='0px' y='0px' viewBox='0 0 32 32' enable-background='new 0 0 32 32' xml:space='preserve'%3E%3Cpath d='M21.709,21.142c-0.618-0.195-1.407-0.703-2.291-1.588c-0.757-0.741-1.539-1.697-2.341-2.74 c-0.19,0.256-0.381,0.51-0.573,0.77c-0.523,0.709-1.059,1.424-1.604,2.127c1.903,2.312,3.88,4.578,6.809,4.952v2.701l7.556-4.362 l-7.556-4.361V21.142z M9.115,12.42c0.756,0.741,1.538,1.697,2.339,2.739c0.195-0.262,0.39-0.521,0.587-0.788 c0.521-0.703,1.051-1.412,1.592-2.11C11.602,9.798,9.5,7.354,6.237,7.236h-3.5v3.5h3.5C6.893,10.71,7.919,11.222,9.115,12.42z M21.709,10.828v2.535L29.265,9l-7.556-4.363v2.647c-1.904,0.219-3.425,1.348-4.751,2.644c-2.196,2.184-4.116,5.167-6.011,7.538 c-1.867,2.438-3.741,3.896-4.712,3.771h-3.5v3.5h3.5c2.185-0.029,3.879-1.266,5.34-2.693c2.194-2.184,4.116-5.166,6.009-7.538 C19.128,12.49,20.669,11.166,21.709,10.828z'%3E%3C/path%3E%3C/svg%3E");
      background-size: contain;
      background-repeat: no-repeat;
      background-position: center;
      display: block;
      height: 25px;
      width: 25px;
      margin-left: auto;
      margin-right: auto;
    }
  }
}

@keyframes btn-color {
  from {
    background-position: right;
  }
  to {
    background-position: left;
  }
}
</style>