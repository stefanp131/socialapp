import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class MessageBoardService {
  public toggleLikeAction: Subject<number> = new Subject<number>();

  setToggleLikeAction(sourceId: number) {
    this.toggleLikeAction.next(sourceId);
  }

  getToggleLikeAction() {
    return this.toggleLikeAction.asObservable();
  }
}