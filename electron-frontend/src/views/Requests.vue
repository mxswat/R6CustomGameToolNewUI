<template>
  <div class="requests-main">
    <div class="requests-list">
      <div v-for="(loadoutRequest, idx) in loadoutRequests" v-bind:key="idx">
        <span>
          {{loadoutRequest.uplayName}}
          {{loadoutRequest.loadout}}
        </span>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { Component } from "vue-property-decorator";
import RequestManagerInst from "../services/RequestManager";

@Component
export default class Requests extends Vue {
  loadoutRequests: Array<any> = [];
  created() {
    RequestManagerInst.getRequests$().subscribe((value: any) => {
      this.loadoutRequests = value;
    });
  }
}
</script>