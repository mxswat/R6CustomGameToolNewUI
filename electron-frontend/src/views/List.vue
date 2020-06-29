<template>
  <div class="list">
    <div class="list-container">
      <h3 class="list-title">Weapons</h3>
      <input class="search" type="text" name id placeholder="Search..." />
      <div class="list-inner">
        <div class="category" v-for="category in list" v-bind:key="category.name">
          <input class="css-dropdown" :id="'check'+ category.name" type="checkbox" name="menu" />
          <label class="css-dropdown" :for="'check'+ category.name">
            {{category.name}}
            <div class="arrow-down"></div>
          </label>
          <div class="nodes" v-for="element in category.children" v-bind:key="element.index">
            <span class="node-name">{{element.name}}</span>
            <div class="select-buttons">
              <input
                class="radio primary"
                type="radio"
                :id="'primary-' + element.index"
                name="primary"
                :value="element.index"
              />
              <label
                class="label primary"
                :for="'primary-' + element.index"
                data-title="Set as Primary weapon"
                @click="changeWeapon(0, element.index, selectedPlayer)"
              >P</label>
              <input
                class="radio secondary"
                type="radio"
                :id="'secondary-' + element.index"
                name="secondary"
                :value="element.index"
              />
              <label
                class="label secondary"
                :for="'secondary-' + element.index"
                data-title="Set as Secondary weapon"
              >S</label>
              <input
                class="radio everyone"
                type="radio"
                :id="'everyone-' + element.index"
                name="everyone"
                :value="element.index"
                @click="changeWeapon(0, element.index, selectedPlayer)"
              />
              <label class="label everyone" :for="'everyone-' + element.index">E</label>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { Component, Prop } from "vue-property-decorator";
import { PlayerUpdated$, changeWeapon } from "../services/ipcfront";

@Component
export default class List extends Vue {
  @Prop({ default: [] })
  list: Array<any>;

  @Prop({ default: "none" })
  selectedPlayer: Array<any>;

  changeWeapon(slotIndex, elementIndex, selectedPlayerIndex) {
    changeWeapon(selectedPlayerIndex, slotIndex, elementIndex);
  }
}
</script>

<style lang="scss">
// LIST COMMON CODE
.list-title {
  text-transform: uppercase;
  margin-left: 8px;
}
.search {
  background: #202225;
  border-radius: 10px;
  padding: 8px;
  border: 0px;
  font-size: 16px;
  color: white;
  margin: 8px;
}

.nodes {
  display: none;
  padding: 4px;
  padding-left: 16px;
  max-width: 100%;
  align-items: center;
  .node-name {
    font-size: 18px;
  }
  input.radio {
    display: none;
    &:checked + label {
      font-weight: 600;
      opacity: 1;
      &.primary {
        background: linear-gradient(141.54deg, #64e8de 0%, #8a64eb 100%);
      }
      &.secondary {
        background: linear-gradient(141.54deg, #ff9482 0%, #7d77ff 100%);
      }
      &.everyone {
        background: linear-gradient(141.54deg, #7bf2e9 0%, #b65eba 100%);
      }
    }
  }
  label.label {
    // padding: 8px;
    // cursor: pointer;
    // font-weight: 400;
    // width: 19px;
    // text-align: center;
    // border-radius: 10px;
    // margin-right: 8px;
    // opacity: 0.5;
    // border: 1px solid gray;
    padding: 4px;
    cursor: pointer;
    font-weight: 400;
    width: 24px;
    text-align: center;
    border-radius: 10px;
    margin-right: 8px;
    opacity: 0.5;
    border: 1px solid gray;
    font-size: 20px;
  }
  .select-buttons {
    display: flex;
    margin-left: auto;
    &:hover {
      label.label {
        opacity: 1;
      }
    }
  }
  &:hover {
    .select-buttons {
      display: flex;
    }
  }
}

input.css-dropdown {
  display: none;
  &:checked {
    ~ div.nodes {
      display: flex;
    }
    ~ label .arrow-down {
      transform: rotate(180deg);
    }
  }
}

label.css-dropdown {
  padding: 8px;
  display: flex;
  cursor: pointer;
}

.arrow-down {
  height: 14px;
  width: 14px;
  background: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='14' fill='white' height='10' role='presentation' class='vs__open-indicator'%3E%3Cpath d='M9.211364 7.59931l4.48338-4.867229c.407008-.441854.407008-1.158247 0-1.60046l-.73712-.80023c-.407008-.441854-1.066904-.441854-1.474243 0L7 5.198617 2.51662.33139c-.407008-.441853-1.066904-.441853-1.474243 0l-.737121.80023c-.407008.441854-.407008 1.158248 0 1.600461l4.48338 4.867228L7 10l2.211364-2.40069z'%3E%3C/path%3E%3C/svg%3E");
  background-repeat: no-repeat;
  margin-left: auto;
  background-position: bottom;
  transition: transform 300ms;
}

.list-inner {
  overflow-y: scroll;
  overflow-x: hidden;
  height: 100%;
  width: 100%;
}

.list {
  display: flex;
  flex-grow: 1;
  flex-shrink: 1;
  flex-basis: auto;
  overflow: hidden;
}

.list-container {
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  flex-shrink: 1;
  flex-basis: auto;
}
</style>