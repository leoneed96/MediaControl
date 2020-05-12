<template>
  <v-container>
    <v-row class="text-center" justify="space-around">
      <v-col cols="12" v-for="(item, index) in clients" :key="index">
        <v-card>
          <v-card-title primary-title>
            {{ item.deviceName }}
          </v-card-title>
          <v-card-subtitle>
            Last heartbeat: {{ getDateString(item.lastHeartbeat) }}
          </v-card-subtitle>
          <v-card-text>
            <v-row class="text-center" justify="space-around">
              <v-col cols="12">
                <v-icon @click="onPrev(item.connectionId)"
                  >ma-1 mdi-48px mdi mdi-arrow-left-drop-circle-outline</v-icon
                >
                <v-icon @click="onPlayPause(item.connectionId)"
                  >ma-1 mdi-48px mdi mdi-play-pause</v-icon
                >
                <v-icon @click="onNext(item.connectionId)"
                  >ma-1 mdi-48px mdi mdi-arrow-right-drop-circle-outline</v-icon
                >
              </v-col>
              <v-col xs="12" md="6">
                <v-slider
                  thumb-label="always"
                  @change="setVolume(item.connectionId, $event)"
                  max="100"
                  v-model="item.volume"
                  step="0"
                >
                  <template v-slot:thumb-label="{ value }">
                    {{
                      Math.floor(value)
                    }}
                  </template>
                </v-slider>
              </v-col>
            </v-row>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import api from "../api.js";
export default {
  name: "mediaControl",

  data: () => ({
    clients: [],
  }),
  methods: {
    getDateString(str) {
      try {
        let a = new Date(str);
        return a.toLocaleString();
      } catch {
        return "data unavailable";
      }
    },
    async refreshClients() {
      this.clients = await api.getList();
    },
    async onNext(connectionId) {
      await api.next(connectionId);
    },
    async onPlayPause(connectionId) {
      await api.playPause(connectionId);
    },
    async onPrev(connectionId) {
      await api.prev(connectionId);
    },
    async setVolume(connectionId, value) {
      await api.setVolume(connectionId, value);
    },
  },
  async mounted() {
    await this.refreshClients();
  },
};
</script>
