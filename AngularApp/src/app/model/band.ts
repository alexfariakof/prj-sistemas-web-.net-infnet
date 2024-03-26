import { Album } from "./album";

export interface Band {
  id: string;
  name: string;
  description: string;
  backdrop: string;
  albums: Album[];
}
