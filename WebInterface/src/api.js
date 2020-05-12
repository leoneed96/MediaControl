import axios from "axios";
export default {
  baseUrl: "http://192.168.1.121:130/media/",
  getPath(relativeUrl) {
    return this.baseUrl + relativeUrl;
  },
  async getList() {
    return (await axios.get(this.getPath("list"))).data;
  },
  async next(connectionId) {
    return (await axios.post(this.getPath(`next/${connectionId}`))).data;
  },
  async prev(connectionId) {
    return (await axios.post(this.getPath(`prev/${connectionId}`))).data;
  },
  async playPause(connectionId) {
    return (await axios.post(this.getPath(`playPause/${connectionId}`))).data;
  },
  async setVolume(connectionId, value) {
    return (await axios.post(this.getPath(`setVolume/${connectionId}/${value}`))).data;
  },
};
