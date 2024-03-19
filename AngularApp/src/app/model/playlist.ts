import { Music } from "./music";

export interface Playlist {
  id?: string;
  name: string;
  backdrop?: string;
  musics: Music[];
}
