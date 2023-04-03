import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class BoardService {
  public toggleLikeAction: Subject<void> = new Subject<void>();

  setToggleLikeAction() {
    this.toggleLikeAction.next();
  }

  getToggleLikeAction() {
    return this.toggleLikeAction.asObservable();
  }
}