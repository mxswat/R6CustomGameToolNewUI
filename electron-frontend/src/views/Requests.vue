<template>
  <div class="requests-main">
    <!-- <h3 class="list-title">Requests</h3> -->
    <div class="toolbar">
      <button class="mxbtt approve" @click="approve(idx, true)">Approve All</button>
      <button class="mxbtt refuse" @click="approve(idx, false)">Refuse All</button>
    </div>
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
import { SLOT, PlayerData } from "../defaults/general";
import {
  BehaviorSubjects,
  changeWeapon,
  changeGadget
} from "../services/ipcfront";
@Component
export default class Requests extends Vue {
  loadoutRequests: Array<any> = [];
  slot = SLOT;
  playerList: Array<PlayerData> = [];
  created() {
    // console.log('reeeeeee')
    RequestManagerInst.getRequests$().subscribe((value: any) => {
      this.loadoutRequests = value;
    });
    BehaviorSubjects.PlayerList$.subscribe((playerList: Array<PlayerData>) => {
      this.playerList = playerList;
    });
  }

  approve(idx: number, isApproved: boolean) {
    const cssclass = isApproved ? "approved" : "refused";
    document.getElementsByClassName("request")[idx].classList.add(cssclass);
    const _this = this;
    const request = this.loadoutRequests[idx];
    if (!request.debug && isApproved) {
      // Bad code but should work
      const idx = this.playerList.findIndex(element => {
        return element.name == this.loadoutRequests[idx].uplayName;
      });
      for (let i = 0; i < request.Weapon.length; i++) {
        const weapon = request.Weapon[i];
        changeWeapon(idx.toString(), weapon.slotIndex, weapon.elementIndex);
      }
      for (let i = 0; i < request.Gadget.length; i++) {
        const gadget = request.Gadget[i];
        changeGadget(idx.toString(), gadget.slotIndex, gadget.elementIndex);
      }
    }
    setTimeout(() => {
      RequestManagerInst.removeLoadoutRequests(idx);
    }, 1000);
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

.toolbar {
  display: flex;
  flex-direction: row;
  padding: 0px 8px;
  button {
    margin-right: 16px;
  }
}

.approved,
.refused {
  background: linear-gradient(
    90deg,
    rgba(2, 0, 36, 0) 0%,
    rgba(107, 229, 133, 1) 100%
  );
  .username,
  .loadout {
    pointer-events: none;
    transform: translateX(100%);
    opacity: 0;
    transition: all 1000ms;
    display: flex;
  }
}

.refused {
  background: linear-gradient(
    90deg,
    rgba(2, 0, 36, 0) 0%,
    rgba(147, 41, 30, 1) 100%
  );
}

.buttons {
  .mxbtt:first-child {
    margin-top: 0px;
  }
}
</style>