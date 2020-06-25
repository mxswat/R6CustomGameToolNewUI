<template>
  <div class="host-page">
    <div class="History">
      <router-link to="/" class="link-btt underline-from-left">Home</router-link>
    </div>
    <div class="container">
      <div class="players">
        <span class="player" v-for="(player, index) in players" :key="player.username">
          <input v-model="selectedPlayer" type="radio" :id="'player-' + index" name="player" :value="index" />
          <label :for="'player-' + index">{{player.username}}</label>
        </span>
      </div>
      <div class="weapons">
        {{selectedPlayer}}
      </div>
      <div class="gadgets"></div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { startTool } from "../services/ipcfront";

@Component({
  components: {},
  data() {
    return {
      selectedPlayer: null,
      players: []
    };
  },
  created() {
    for (let i = 0; i < 10; i++) {
      this.players.push({
        username: "Player " + (i + 1)
      });
    }
    startTool();
  }
})
export default class Host extends Vue {}
</script>

<style lang="scss" scoped>
.History {
  padding-left: 16px;
  padding-top: 16px;
}

.container {
}
</style>