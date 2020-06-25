<template>
  <div>
    <h1>Hosting panel</h1>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
@Component({
  components: {},
  created() {
    window.ipcRenderer.send('start-tool', 'start');
  }
})
export default class Host extends Vue {}
</script>

<style>
</style>