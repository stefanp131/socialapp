import { AppUserLike } from "./AppUserLike";

export interface AppUser{
  id: number;
  username: string;
  description: string;
  profilePicture: string;
  likedByUsers: AppUserLike[];
}

