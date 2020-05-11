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
    </v-app-bar>

    <v-content>
      <HelloWorld/>
    </v-content>
  </v-app>
</template>

<script>
import HelloWorld from './components/HelloWorld';

export default {
  name: 'App',

  components: {
    HelloWorld,
  },

  data: () => ({
    //
  }),
};
</script>
