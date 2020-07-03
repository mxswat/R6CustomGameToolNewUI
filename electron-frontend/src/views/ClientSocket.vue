<template>
  <div>
    Hello client
    <input type="text" name="ip" id="ip" v-model="ipAddress"/>
    <br />
    <a @click="connectToSocketIoServer()">Connect to socket.io server</a>
    <br />
    <a @click="sendMessage">sendMessage</a>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { Component } from "vue-property-decorator";
import io from "socket.io-client";
@Component
export default class ClientSocket extends Vue {
  socket: any;
  ipAddress: string = 'ipgoeshere';
  created() {
    // const ipAddress = 'localhost';
    // // TODO Reming me to use portfinder!
    // const port = '3000';
    // this.socket = io('http://' + ipAddress + ':' + port);
  }

  connectToSocketIoServer() {
    const ipAddress = "localhost";
    // TODO Reming me to use portfinder!
    const port = "3000";
    this.socket = io("http://" + this.ipAddress);
  }

  sendMessage() {
    this.socket.emit("chat message", "imAlive");
  }
}
</script>

<style lang="scss" scoped>
a {
  cursor: pointer;
}
</style>