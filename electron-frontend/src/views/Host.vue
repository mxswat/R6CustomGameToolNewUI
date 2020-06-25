<template>
  <div class="host-page">
    <div class="History">
      <router-link to="/" class="link-btt underline-from-left">Home</router-link>
    </div>
    <div class="container">
      <div class="players">
        <span></span>
      </div>
      <div class="weapons"></div>
      <div class="gadgets"></div>
    </div>
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

<style lang="scss" scoped>
.History {
  padding-left: 16px;
  padding-top: 16px;
}

.container {

}
</style>