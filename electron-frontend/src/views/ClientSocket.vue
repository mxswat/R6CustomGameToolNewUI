<template>
  <div class="client-socket">
    <div class="History">
      <router-link to="/" class="link-btt underline-from-left">Home</router-link>
      <router-link to="/credits" class="link-btt underline-from-left">Help</router-link>
      <div class="spacer"></div>
    </div>
    <div class="c-panel" v-if="!isConnected">
      <span class="welcome">Welcome to the client panel</span>
      <span class="todo">You can connect to another mod tool and request a custom loadout</span>
      <div class="ip-n-port">
        <label for="ip">Your uPlay name</label>
        <input
          type="text"
          class="r6inputTxt"
          placeholder="e.g. xXxDankSniper69420xXx"
          name="uplayName"
          id="uplayName"
          v-model="uplayName"
        />
        <br />
        <label for="ip">Host IP Address:</label>
        <input
          type="text"
          class="r6inputTxt"
          placeholder="e.g. 192.168.1.15"
          name="ip"
          id="ip"
          v-model="ipAddress"
        />
        <label for="port">Host Port:</label>
        <input
          type="text"
          class="r6inputTxt port"
          name="port"
          id="port"
          placeholder="Port"
          v-model="port"
        />
      </div>
      <button class="connectbtt" @click="connectToSocketIoServer()">Connect</button>
    </div>
    <div class="overflow-container">
      <div class="request-panel" v-if="isConnected">
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
        <div class="request-tab">
          <h3 class="list-title">Request options</h3>
          <div class="request-options">
            <button class="requestbbt">Request loadout</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { Component } from "vue-property-decorator";
import io from "socket.io-client";
import GUNS from "../defaults/guns";
import GADGETS from "../defaults/gadgets";
import List from "./List.vue";

@Component({
  components: {
    List
  }
})
export default class ClientSocket extends Vue {
  socket: any;
  ipAddress: string = "";
  port: string = "3000";
  uplayName: string = "";
  isConnected: boolean = false;
  gunslist: any = [];
  gadgetslist: any = [];
  created() {
    this.gunslist = GUNS;
    this.gadgetslist = GADGETS;
  }

  connectToSocketIoServer() {
    // TODO Reming me to use portfinder on server!
    // TODO Check status https://stackoverflow.com/questions/16518153/get-connection-status-on-socket-io-client
    if (this.ipAddress && this.uplayName && this.port) {
      this.isConnected = true; // todo handle correctly
      // this.socket = io(`http:// + ${this.ipAddress}:${this.port}`);
    }
  }

  sendMessage() {
    this.socket.emit("chat message", "imAlive");
  }

  onSelectedItem() {
    
  }
  // Todo kill socket on going back to home
}
</script>

<style lang="scss" scoped>
a {
  cursor: pointer;
}

.c-panel {
  display: flex;
  flex: 1 1 auto;
  flex-direction: column;
  align-items: center;
}

.client-socket {
  display: flex;
  flex: 1 1 auto;
  flex-direction: column;
  background: #36393f;
}

.r6inputTxt {
  background: #202225;
  border-radius: 10px;
  padding: 8px;
  border: 0px;
  font-size: 16px;
  color: white;
  margin: 8px;
}

.port {
  max-width: 50px;
}

.connectbtt,
.requestbbt {
  margin-top: 20px;
  color: white;
  text-transform: uppercase;
  padding: 8px 16px;
  border: 0;
  display: block;
  cursor: pointer;
  font-weight: 600;
  border-radius: 50px;
  background: linear-gradient(to right, #c33764, #1d2671);
}

.ip-n-port {
  text-align: center;
}

.request-panel {
  display: grid;
  grid-template-columns: repeat(3, [col] 1fr);
  grid-template-rows: repeat(1, [row] auto);
  gap: 8px;
  flex-grow: 1;
  flex-shrink: 1;
  flex-basis: auto;
}

.request-options {
  display: flex;
  flex: 1 1 auto;
  flex-direction: column;
  width: 100%;
}
.requestbbt {
  margin-left: auto;
  margin-right: auto;
}
// .todo {
//   text-transform: uppercase;
// }
</style>