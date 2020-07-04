<template>
  <div class="client-socket">
    <div class="History">
      <router-link to="/" class="link-btt underline-from-left">Home</router-link>
      <router-link to="/credits" class="link-btt underline-from-left">Help</router-link>
      <div class="spacer"></div>
    </div>
    <div class="c-panel" v-if="connectionStatus !== connectionStatuses.Connected">
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
      <span
        v-if="connectionStatus === connectionStatuses.Connecting"
        class="connectionfail"
      >Connecting to {{ipAddress}}:{{port}} as {{uplayName}}</span>
      <span v-if="connectionStatus === connectionStatuses.Error" class="connectionfail">
        Connection Error
        <br />Check the IP Address and Port,
        <br />if they are correct the host probably has the firewall blocking the port
      </span>
    </div>
    <div class="overflow-container">
      <div class="request-panel" v-if="connectionStatus === connectionStatuses.Connected">
        <List
          v-on:selectedElement="onSelectedItem($event, item_type.Weapon)"
          :list="gunslist"
          :Title="'weapons'"
        ></List>
        <List
          v-on:selectedElement="onSelectedItem($event, item_type.Gadget)"
          :list="gadgetslist"
          :Title="'gadgets'"
        ></List>
        <div class="request-tab">
          <h3 class="list-title">Request options</h3>
          <div class="request-options">
            <button @click="requestLoadout()" class="requestbbt">Request loadout</button>
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
import { SLOT, ITEM_TYPE } from "../defaults/general";
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
  gunslist: any = [];
  gadgetslist: any = [];
  connectionStatus: ConnectionStatuses = ConnectionStatuses.New;
  connectionStatuses = ConnectionStatuses;
  item_type = ITEM_TYPE;
  loadout: any = {
    Weapon: [],
    Gadget: []
  };
  created() {
    this.gunslist = GUNS;
    this.gadgetslist = GADGETS;
    this.uplayName = localStorage.getItem("uplayName") || "";
    this.ipAddress = localStorage.getItem("ipAddress") || "";
    this.port = localStorage.getItem("port") || this.port;
  }

  // TODO Reming me to use portfinder on server!
  connectToSocketIoServer() {
    localStorage.setItem("uplayName", this.uplayName);
    localStorage.setItem("ipAddress", this.ipAddress);
    localStorage.setItem("port", this.port);
    console.log("Attempting Connection to socket.io server");
    this.connectionStatus = ConnectionStatuses.Connecting;
    if (this.ipAddress && this.uplayName && this.port) {
      this.socket = io(`http://${this.ipAddress}:${this.port}`, {
        reconnection: false
      });
      const _this = this;
      this.socket.on("connect", () => {
        console.log("connect", this.socket.connected);
        _this.connectionStatus = ConnectionStatuses.Connected;
      });

      this.socket.on("connect_error", () => {
        console.log("connect_error", this.socket.connected);
        _this.connectionStatus = ConnectionStatuses.Error;
      });
    }
  }

  sendMessage() {
    this.socket.emit("chat message", "imAlive");
  }

  onSelectedItem(item: any, GorW: ITEM_TYPE) {
    this.loadout[ITEM_TYPE[GorW]] = this.loadout[ITEM_TYPE[GorW]] || {};
    this.loadout[ITEM_TYPE[GorW]][item.slotIndex] = item;
  }

  requestLoadout() {
    console.log(this.loadout);
  }
  // Todo kill socket on going back to home
}

enum ConnectionStatuses {
  New,
  Connecting,
  Connected,
  Error
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
.connectionfail {
  margin-top: 40px;
  text-align: center;
  font-size: 18px;
}
</style>