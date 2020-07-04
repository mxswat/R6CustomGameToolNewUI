<template>
  <div class="requests-main">
    <!-- <h3 class="list-title">Requests</h3> -->
    <div class="requests-list">
      <div
        class="request"
        v-for="(loadoutRequest, idx) in loadoutRequests"
        v-bind:key="loadoutRequest.uplayName + idx"
      >
        <span class="username">{{loadoutRequest.uplayName}}</span>
        <div class="loadout">
          <div class="category">
            <span class="catname">Weapons</span>
            <span
              class="weapons-list"
              v-for="(weapon, idx) in loadoutRequest.loadout.Weapon"
              v-bind:key="idx + 'weapons'"
            >{{slot[weapon.slotIndex]}}: {{weapon.elementIndex}}</span>
          </div>
          <div class="category">
            <span class="catname">Gadgets</span>
            <span
              class="gadgets-list"
              v-for="(gadget, idx) in loadoutRequest.loadout.Gadget"
              v-bind:key="idx + 'gadgets'"
            >{{slot[gadget.slotIndex]}}: {{gadget.elementIndex}}</span>
          </div>
          <div class="buttons">
            <button class="mxbtt approve" @click="approve(idx, true)">Approve</button>
            <button class="mxbtt refuse" @click="approve(idx, false)">Refuse</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { Component } from "vue-property-decorator";
import RequestManagerInst from "../services/RequestManager";
import { SLOT } from "../defaults/general";
@Component
export default class Requests extends Vue {
  loadoutRequests: Array<any> = [];
  slot = SLOT;
  created() {
    // console.log('reeeeeee')
    RequestManagerInst.getRequests$().subscribe((value: any) => {
      this.loadoutRequests = value;
    });
  }

  approve(idx: number, isApproved: boolean) {
    const cssclass = isApproved ? 'approved' : 'refused';
    document.getElementsByClassName('request')[idx].classList.add(cssclass);
    // TODO REPLACE ME WITH REQUEST REMOVE FNC
    const _this = this;
    setTimeout(() => {
      _this.loadoutRequests.splice(idx, 1);
    }, 500);
  }
}
</script>
<style lang="scss" scoped>
.requests-main {
  overflow: hidden;
  flex-direction: column;
  display: flex;
  flex-grow: 1;
  flex-shrink: 1;
  flex-basis: auto;
}

.request {
  padding: 16px;
  border-bottom: 1px solid white;
}

.loadout {
  display: flex;
  flex-direction: row;
}

.category {
  display: flex;
  flex: 0 1 25%;
  flex-direction: column;
  .catname {
    margin-top: 8px;
    margin-bottom: 8px;
  }
}

.requests-list {
  overflow: auto;
  overflow-x: hidden;
}

.mxbtt {
  min-width: 107px;
  margin: 8px 0px;
  &.approve {
    background: linear-gradient(to right, #283c86, #45a247);
  }
  &.refuse {
    background: linear-gradient(to right, #ed213a, #93291e);
  }
}

.approved, .refused {
  pointer-events: none;
  transform: translateX(100%);
  transition: all 500ms;
}

.refused {
  
}
</style>