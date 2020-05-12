<template>
  <v-app>
    <v-app-bar
      app
      color="#9e9e9e"
      dark
    >
      <div class="d-flex align-center">
        <v-icon style="font-size: 35px">mdi-play-box-multiple</v-icon>
      </div>
      <v-spacer></v-spacer>
      <v-btn @click="onRefresh" color="info">refresh</v-btn>
    </v-app-bar>

    <v-content>
      <MediaControl ref="media"/>
    </v-content>
  </v-app>
</template>

<script>
import MediaControl from './components/MediaControl';

export default {
  name: 'App',

  components: {
    MediaControl,
  },
  methods: {
    async onRefresh(){
      await this.$refs.media.refreshClients();
    }
  },
  data: () => ({
    //
  }),
};
</script>
