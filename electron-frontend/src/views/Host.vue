<template>
  <div>
    <h1>Hosting panel</h1>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { startTool } from "../services/ipcfront";
@Component({
  components: {},
  created() {
    startTool();
  }
})
export default class Host extends Vue {}
</script>

<style>
</style>