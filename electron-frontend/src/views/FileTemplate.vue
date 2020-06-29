<template>
  <div>
    <p>Long-form v-model example</p>
    <input :value="myDataProperty" @input="updateMyProperty($event)"/>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { Component } from 'vue-property-decorator'

@Component
export default class List extends Vue {

}
</script>