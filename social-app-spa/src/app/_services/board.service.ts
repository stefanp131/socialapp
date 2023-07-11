import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class BoardService {
  public toggleLikeAction: Subject<number> = new Subject<number>();

  setToggleLikeAction(id: number) {
    this.toggleLikeAction.next(id);
  }

  getToggleLikeAction() {
    return this.toggleLikeAction.asObservable();
  }
}