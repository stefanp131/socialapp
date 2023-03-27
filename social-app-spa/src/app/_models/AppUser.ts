import { AppUserLike } from "./AppUserLike";

export interface AppUser{
  username: string;
  description: string;
  profilePicture: string;
  likedByUsers: AppUserLike[];
}

