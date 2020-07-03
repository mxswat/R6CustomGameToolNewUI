<template>
  <div class="switch">
    <div>
      <input type="checkbox" :id="id" :value="value" @change="onChange($event.target.value)" />
      <label :for="id">
        <span class="switch-label">{{label}}</span>
        <span class="toggle"></span>
      </label>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { Component, Prop } from "vue-property-decorator";

@Component
export default class MxSwitch extends Vue {
  @Prop({ default: false })
  value!: boolean;

  @Prop({ default: 'label' })
  label!: string;

  @Prop({ default: 'id' })
  id!: string;

  onChange(value: boolean) {
    this.$emit("changed", value);
  }
}
</script>

<style lang="scss" scoped>
$white: #e8e9ed;
$gray: #ff4b77;
$blue: #18172c;
$green: #00d084;

[type="checkbox"] {
  position: absolute;
  left: -9999px;
}

.switch-label {
  font-size: 18px;
  font-weight: 400;
  margin-left: 8px;
}

.switch label {
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.switch span.toggle {
  position: relative;
  width: 50px;
  height: 26px;
  border-radius: 15px;
  box-shadow: inset 0 0 5px rgba(0, 0, 0, 0.4);
  background: $gray;
  transition: all 0.3s;
  margin-left: 8px;
}

.switch span.toggle::before,
.switch span.toggle::after {
  content: "";
  position: absolute;
}

.switch span.toggle::before {
  left: 1px;
  top: 1px;
  width: 24px;
  height: 24px;
  background: $white;
  border-radius: 50%;
  z-index: 1;
  transition: transform 0.3s;
}

.switch span.toggle::after {
  top: 50%;
  right: 8px;
  width: 12px;
  height: 12px;
  transform: translateY(-50%);
  background: url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/162656/uncheck-switcher.svg);
  background-size: 12px 12px;
}

.switch [type="checkbox"]:checked + label span.toggle {
  background: $green;
}

.switch [type="checkbox"]:checked + label span.toggle::before {
  transform: translateX(24px);
}

.switch [type="checkbox"]:checked + label span.toggle::after {
  width: 14px;
  height: 14px;
  /*right: auto;*/
  left: 8px;
  background-image: url(https://s3-us-west-2.amazonaws.com/s.cdpn.io/162656/checkmark-switcher.svg);
  background-size: 14px 14px;
}
</style>