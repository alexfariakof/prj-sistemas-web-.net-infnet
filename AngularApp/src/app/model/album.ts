import { Music } from "./music";

export interface Album {
  id: string;
  name: string;
  backdrop: string;
  flatId?: string;
  bandId: string;
  musics?: Music[];
}
