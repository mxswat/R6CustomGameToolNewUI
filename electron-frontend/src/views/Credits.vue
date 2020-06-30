<template>
  <div class="credits-container">
    <div class="History">
      <router-link to="/" class="link-btt underline-from-left">Home</router-link>
    </div>
    <div class="credits">
      <h2>Credits</h2>
      <div class="the-team">
        <div class="teammember" v-for="op in team" v-bind:key="op.name">
          <span class="name">{{op.name}}</span>
          <span>{{op.task}}</span>
          <div class="op-links">
            <a
              :href="link"
              v-for="(link, name) in op.links"
              target="_blank"
              class="twitter"
              v-bind:key="link"
            >{{name}}</a>
          </div>
        </div>
      </div>
      <h2>Instruction / TODO UPDATE THIS</h2>
      <ul>
        <li>
          Disable BattlEye by going to Steam and/or Uplay launch options,
          and type in /belaunch -be (slash included).
        </li>
        <li>Start the game as you normally would.</li>
        <li>
          Once you're in a local custom game lobby op select screen or
          Solo terrohunt operator select screen, pick your operator,
          make sure you have the attachments you want for the guest weapon,
          confirm, then pause timer. (guest weapon is the target weapon you're trying to switch to.)
        </li>
        <li>pick your name, pick your guns, then unpause timer.</li>
      </ul>

      <h2>FAQ</h2>
      <p class="question">Can I get banned for this?</p>
      <p
        class="answer"
      >Simple answer no because you cannot use this online and you need to disable BattlEye</p>
      <p class="question">What version do I need to use the Custom Game Tool?</p>
      <p class="answer">At the moment it only works on the latest version of Rainbow Six Siege</p>
      <p class="question">Can I use this on Uplay?</p>
      <p
        class="answer"
      >Yes you can use the custom game tool on Uplay and steam as long as BattlEye is disabled</p>
      <p class="question">Is this a virus and why is my antivirus blocking it?</p>
      <p
        class="answer"
      >No this is not a virus you just need to disable your antivirus for it to work</p>
      <p class="question">How do I disable BattlEye?</p>
      <p class="answer">
        If you're using Steam go to Rainbow Six Siege > properties (by right-clicking on it) and go to General and then Set launch options and put /belaunch
        If you're using Uplay go to Rainbow Six Siege > View game details > Properties > Add command-line arguments and put /belaunch
      </p>
      <p class="question">How do I put an ACOG on guns?</p>
      <p class="answer">
        Select an operator that has an ACOG and then select the gun you want to use in the custom game tool (please note this only works on some guns)
        for more info go to
        <a
          target="_blank"
          href="https://docs.google.com/spreadsheets/d/1BZQ-dJX_ZLk8vVvJV6T7nCnpSIZCNlW8tdKJzzU5GN4/edit?usp=sharing"
        >https://docs.google.com/spreadsheets/d/1BZQ-dJX_ZLk8vVvJV6T7nCnpSIZCNlW8tdKJzzU5GN4/edit?usp=sharing</a>
      </p>
      <p
        class="question"
      >Do my friends need to download the Custom Game Tool for this to work for them?</p>
      <p class="answer">No only you need to have it installed</p>
      <p class="question">Does the Custom Game Tool work with Vulkan?</p>
      <p class="answer">Yes it works on both Directx and Vulkan</p>
      <p class="question">My browser is blocking the download what do I do?</p>
      <p class="answer">Disable your antivirus and download it again</p>
      <p class="question">How can I confirm BattlEye is deactivated correctly?</p>
      <p
        class="answer"
      >Queue up for a quick match. After 30secs, you should receive an error message. You can also check if the service is still running in services.msc</p>
      <p class="question">Can I use this online?</p>
      <p
        class="answer"
      >No. If you try to use this tool in a public match or without deactivating BattlEye, you will absolutely get banned.</p>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { Component } from "vue-property-decorator";
import TEAM from "../defaults/team";

@Component
export default class Credits extends Vue {
  team: any = [];
  created() {
    this.team = TEAM;
  }
}
</script>

<style lang="scss">
.credits {
  padding: 16px;
  font-size: 18px;
}

.the-team {
  display: flex;
}
.teammember {
  margin-left: 16px;
  margin-bottom: 16px;
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
  .name {
    font-size: 24px;
    font-weight: 400;
  }
  .op-links {
    a {
      margin-right: 8px;
      color: white;
    }
  }
}

.answer,
.question {
  margin: 0;
}
.answer {
  margin-bottom: 16px;
}
</style>