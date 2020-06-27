<template>
  <div class="host-page">
    <div class="History">
      <router-link to="/" class="link-btt underline-from-left">Home</router-link>
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
        <div class="list">
          <div class="list-container">
            <h3 class="list-title">Weapons</h3>
            <input class="search" type="text" name="" id="" placeholder="Search...">
            <div class="list-inner">
              <div
                class="category"
                v-for="gunCategory in gunslibrary"
                v-bind:key="gunCategory.name"
              >
                <input
                  class="css-dropdown"
                  :id="'check'+ gunCategory.name"
                  type="checkbox"
                  name="menu"
                />
                <label class="css-dropdown" :for="'check'+ gunCategory.name">
                  {{gunCategory.name}}
                  <div class="arrow-down"></div>
                </label>
                <div class="nodes" v-for="gun in gunCategory.children" v-bind:key="gun.index">
                  <span class="node-name">{{gun.name}}</span>
                  <div class="select-buttons">
                    <input
                      class="radio primary"
                      type="radio"
                      :id="'primary-' + gun.index"
                      name="primary"
                      :value="gun.index"
                    />
                    <label class="label primary" :for="'primary-' + gun.index" data-title="Set as Primary weapon">P</label>
                    <input
                      class="radio secondary"
                      type="radio"
                      :id="'secondary-' + gun.index"
                      name="secondary"
                      :value="gun.index"
                    />
                    <label class="label secondary" :for="'secondary-' + gun.index" data-title="Set as Secondary weapon">S</label>
                    <input
                      class="radio everyone"
                      type="radio"
                      :id="'everyone-' + gun.index"
                      name="everyone"
                      :value="gun.index"
                    />
                    <label class="label everyone" :for="'everyone-' + gun.index">E</label>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { startTool, PlayerUpdated$ } from "../services/ipcfront";
import { Subscription } from "rxjs";
import animationTabs from "../defaults/hosttabs";
import GUNS from "../defaults/guns";

@Component
export default class Host extends Vue {
  selectedPlayer = 0;
  players: Array<any> = [];
  subscriptions: Array<Subscription> = [];
  // Definition copied from the VS UI
  // TODO DEFINE TYPE
  gunslibrary: {
    name: string;
    children: {
      name: string;
      index: string;
      type: string;
    }[];
  }[] = [];
  created() {
    for (let i = 0; i < 10; i++) {
      this.players.push({
        username: "Player " + (i + 1)
      });
    }
    // startTool();
    this.gunslibrary = GUNS;
    this.subscribeToSubjects();
  }
  mounted() {
    animationTabs();
  }
  subscribeToSubjects() {
    this.subscriptions.push(PlayerUpdated$.subscribe(this.onPlayerUpdated));
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

.History {
  padding: 8px 16px;
  background: #202225;
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

// LIST COMMON CODE
.list-title {
  text-transform: uppercase;
  margin-left: 8px;
}
.search {
  background: #202225;
  border-radius: 10px;
  padding: 8px;
  border: 0px;
  font-size: 16px;
  color: white;
  margin: 8px;
}

.nodes {
  display: none;
  padding: 4px;
  padding-left: 16px;
  max-width: 100%;
  align-items: center;
  .node-name {
    font-size: 18px;
  }
  input.radio {
    display: none;
    &:checked + label {
      font-weight: 600;
      opacity: 1;
      &.primary {
        background: linear-gradient(141.54deg, #64e8de 0%, #8a64eb 100%);
      }
      &.secondary {
        background: linear-gradient(141.54deg, #ff9482 0%, #7d77ff 100%);
      }
      &.everyone {
        background: linear-gradient(141.54deg, #7bf2e9 0%, #b65eba 100%);
      }
    }
  }
  label.label {
    // padding: 8px;
    // cursor: pointer;
    // font-weight: 400;
    // width: 19px;
    // text-align: center;
    // border-radius: 10px;
    // margin-right: 8px;
    // opacity: 0.5;
    // border: 1px solid gray;
    padding: 4px;
    cursor: pointer;
    font-weight: 400;
    width: 24px;
    text-align: center;
    border-radius: 10px;
    margin-right: 8px;
    opacity: 0.5;
    border: 1px solid gray;
    font-size: 20px;
  }
  .select-buttons {
    display: flex;
    margin-left: auto;
    &:hover {
      label.label {
        opacity: 1;
      }
    }
  }
  &:hover {
    .select-buttons {
      display: flex;
    }
  }
}

input.css-dropdown {
  display: none;
  &:checked {
    ~ div.nodes {
      display: flex;
    }
    ~ label .arrow-down {
      transform: rotate(180deg);
    }
  }
}

label.css-dropdown {
  padding: 8px;
  display: flex;
  cursor: pointer;
}

.arrow-down {
  height: 14px;
  width: 14px;
  background: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='14' fill='white' height='10' role='presentation' class='vs__open-indicator'%3E%3Cpath d='M9.211364 7.59931l4.48338-4.867229c.407008-.441854.407008-1.158247 0-1.60046l-.73712-.80023c-.407008-.441854-1.066904-.441854-1.474243 0L7 5.198617 2.51662.33139c-.407008-.441853-1.066904-.441853-1.474243 0l-.737121.80023c-.407008.441854-.407008 1.158248 0 1.600461l4.48338 4.867228L7 10l2.211364-2.40069z'%3E%3C/path%3E%3C/svg%3E");
  background-repeat: no-repeat;
  margin-left: auto;
  background-position: bottom;
  transition: transform 300ms;
}

.list-inner {
  overflow-y: scroll;
  overflow-x: hidden;
  height: 100%;
  width: 100%;
}

.list {
  display: flex;
  flex-grow: 1;
  flex-shrink: 1;
  flex-basis: auto;
  overflow: hidden;
}

.list-container {
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  flex-shrink: 1;
  flex-basis: auto;
}
</style>