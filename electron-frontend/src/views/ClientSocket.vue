<template>
  <div>
    Hello client
    <a @click="sendMessage()"> Click me</a>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { Component } from 'vue-property-decorator'
import io from 'socket.io-client';
@Component
export default class ClientSocket extends Vue {
    socket: any;
    created() {
        const ipAddress = 'localhost';
        const port = '3000';
        this.socket = io('http://' + ipAddress + ':' + port);
    }

    sendMessage() {
        this.socket.emit('chat message', 'imAlive');
    }
}
</script>