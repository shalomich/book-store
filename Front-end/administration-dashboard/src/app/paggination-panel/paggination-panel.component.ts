import {Component, EventEmitter, Input, OnInit, Output} from "@angular/core";

@Component({
  selector: 'paggination-panel',
  templateUrl: './paggination-panel.component.html'
})
export class PagginationPanelComponent implements OnInit {

  private readonly maxButtonCount = 9;
  private readonly maxButtonTogetherCount = 7;

  private pageNumber = 1;

  @Input() pageCount = 1;

  @Output() pageTurnedOver = new EventEmitter<number>();

  private createPageButton(number: number) {
    let button = document.createElement('button');
    button.value = number.toString();
    button.textContent = number.toString();
    button.onclick = (event: MouseEvent) => {
      const nextPageNumber = parseInt((event.target as HTMLButtonElement).value);
      this.pageTurnedOver.emit(nextPageNumber);
      this.pageNumber = nextPageNumber;
    };

    if (number == this.pageNumber)
      button.disabled = true;

    return button;
  }

  private createPassButton()  {
    let button = document.createElement('button');
    button.disabled = true;
    button.textContent = '...';

    return button;
  }

  private buildPagginationPanel(): void {
    const buttonsContainer = document.getElementById('buttons-container')! as HTMLDivElement;

    const addPageButton = (number: number) => buttonsContainer.appendChild(this.createPageButton(number));
    const addPassButton = () => buttonsContainer.appendChild(this.createPassButton());

    if (this.pageCount <= this.maxButtonCount) {
      for (let i = 1; i <= this.pageCount; i++)
        addPageButton(i);

      return;
    }

    if (this.pageNumber < this.maxButtonTogetherCount) {
      for (let i = 1; i <= this.maxButtonTogetherCount; i++)
        addPageButton(i);

      addPassButton();
      addPageButton(this.pageCount);
    }
    else if (this.pageNumber > this.pageCount - this.maxButtonTogetherCount + 1){
      addPageButton(1);
      addPassButton();

      for (let i = this.pageCount - this.maxButtonTogetherCount + 1; i <= this.pageCount; i++)
        addPageButton(i);
    }
    else {
      addPageButton(1);
      addPassButton();

      const borderDistance = Math.trunc((this.maxButtonTogetherCount - 2) / 2);
      for (let i = this.pageNumber - borderDistance; i <= this.pageNumber + borderDistance; i++)
        addPageButton(i);

      addPassButton();
      addPageButton(this.pageCount);
    }
  }

  public ngOnInit(): void {
    this.buildPagginationPanel();
  }

}
