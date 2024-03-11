import { Music } from "./music";

export interface Album {
  id: string;
  name: string;
  flatId?: string;
  musics?: Music[];
}
