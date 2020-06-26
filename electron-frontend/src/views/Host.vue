<template>
  <div class="host-page">
    <div class="History">
      <router-link to="/" class="link-btt underline-from-left">Home</router-link>
    </div>
    <div class="container">
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
      <div class="weapons">{{selectedPlayer}}</div>
      <div class="gadgets"></div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { startTool, PlayerUpdated$ } from "../services/ipcfront";
import { Subscription } from "rxjs";
import animationTabs from "../defaults/hosttabs";
@Component
export default class Host extends Vue {
  selectedPlayer = 0;
  players: Array<any> = [];
  subscriptions: Array<Subscription> = [];
  created() {
    for (let i = 0; i < 10; i++) {
      this.players.push({
        username: "Player " + (i + 1)
      });
    }
    // startTool();
    this.subscribeToSubjects();
  }
  mounted() {
    animationTabs();
  }
  subscribeToSubjects() {
    this.subscriptions.push(PlayerUpdated$.subscribe(this.onPlayerUpdated));
  }
  onPlayerUpdated(playerData) {
    if (playerData && playerData.name !== null) {
      const index = playerData.index;
      this.players[index].username = playerData.name;
      this.players[index].primaryWeapon = playerData.primaryweapon;
      this.players[index].secondaryWeapon = playerData.secondaryweapon;
      this.players[index].primaryGadget = playerData.primarygadget;
      this.players[index].secondaryGadget = playerData.secondarygadget;
    }
  }
  beforeDestroy() {
    this.subscriptions.forEach(x => x.unsubscribe());
  }
}
</script>

<style lang="scss" scoped>
.host-page {
  width: 100%;
}

.History {
  padding-left: 16px;
  padding-top: 16px;
}

.playerradio {
  display: none;
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
}
.player {
  max-width: 150px;
}

input[type="radio"]:checked + label {
  font-weight: 600;
  border-radius: 20px;
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
  z-index: -1;
}
</style>