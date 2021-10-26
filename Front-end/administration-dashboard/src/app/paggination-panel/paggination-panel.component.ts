import {Component, EventEmitter, Input, OnInit, Output} from "@angular/core";
import {PaggingPanelState} from "../core/enums/pagging-panel-state";

@Component({
  selector: 'paggination-panel',
  templateUrl: './paggination-panel.component.html'
})
export class PagginationPanelComponent implements OnInit {

  private readonly maxButtonCount = 9;
  private readonly maxButtonTogetherCount = this.maxButtonCount - 2;

  public readonly pageNumber = 1;

  public state: PaggingPanelState | undefined;
  public pageNumbers: Array<number> | undefined;

  @Input() pageCount = 1;

  @Output() pageTurnedOver = new EventEmitter<number>();

  private setState() {
    const createRange = (first: number, last: number) => {
      const range: Array<number> = [];

      for (let i = first; i <= last; i++)
        range.push(i);

      return range;
    };

    if (this.pageCount <= this.maxButtonCount) {
      this.state = PaggingPanelState.FULL;
      this.pageNumbers = createRange(1, this.pageCount);
      return;
    }

    const leftOrientationExtremeButtonNumber = this.maxButtonTogetherCount;
    const rightOrientationExtremeButtonNumber = this.pageCount - this.maxButtonTogetherCount + 1;

    switch (true) {
      case (this.pageNumber < leftOrientationExtremeButtonNumber):
        this.state = PaggingPanelState.LEFT;
        this.pageNumbers = createRange(1, leftOrientationExtremeButtonNumber);
        break;
      case (this.pageNumber >= leftOrientationExtremeButtonNumber && this.pageNumber <= rightOrientationExtremeButtonNumber):
        this.state = PaggingPanelState.CENTRE;
        const borderDistance = Math.trunc((this.maxButtonTogetherCount - 2) / 2);
        this.pageNumbers = createRange(this.pageNumber - borderDistance, this.pageNumber + borderDistance);
        break;
      case (this.pageNumber > rightOrientationExtremeButtonNumber):
        this.state = PaggingPanelState.RIGHT;
        this.pageNumbers = createRange(rightOrientationExtremeButtonNumber, this.pageCount)
        break;
      default: throw 'error'
    }
  }

  public onPageButtonClicked(event: MouseEvent) {
    const nextPageNumber = parseInt((event.target as HTMLButtonElement).value);
    this.pageTurnedOver.emit(nextPageNumber);
  }

  public ngOnInit(): void {
    this.setState();
  }
}
