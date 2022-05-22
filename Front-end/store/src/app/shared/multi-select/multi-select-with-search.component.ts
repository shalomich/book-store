import {
  ChangeDetectionStrategy,
  Component, ContentChild,
  forwardRef,
  Input, OnChanges,
  SimpleChanges, TemplateRef,
  ViewEncapsulation,
} from '@angular/core';
import { ControlValueAccessor, FormBuilder, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { MatOptionSelectionChange } from '@angular/material/core';
import { combineLatest, Observable, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';

import { listenControlChanges } from '../../../core';
import { DestroyableBase } from '../../helpers/destroyable.mixin';

import { Option } from './option';

/**
 * Multi select with search.
 */
@Component({
  selector: 'crm-multi-select',
  templateUrl: './multi-select-with-search.component.html',
  styleUrls: ['./multi-select-with-search.component.scss'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => MultiSelectWithSearchComponent),
      multi: true,
    },
  ],
})
export class MultiSelectWithSearchComponent extends DestroyableBase implements ControlValueAccessor, OnChanges {
  /**
   * Option template.
   */
  @ContentChild('optionTemplate', { static: true })
  public optionTemplateRef: TemplateRef<any>;
  
  /**
   * Set required property.
   */
  @Input()
  public readonly required: boolean;

  /**
   * Check if touched from parent component.
   */
  @Input()
  public readonly isTouched: boolean;

  /**
   * Set placeholder select.
   */
  @Input()
  public placeholder: string;

  /**
   * All options.
   */
  @Input()
  public options: any[];

  /**
   * Function to compare values in selections.CompareWith in mat-select.
   */
  @Input()
  public compareOption = (valueFirst: any, valueSecond: any) => valueFirst === valueSecond

  /**
   * Function to compare option and search term.
   */
  @Input()
  public compareOptionWithSearchTerm = (option: any, term: string) => option.name.toString().toLowerCase().includes(term.toLowerCase())

  /**
   * Create option search form.
   * @param formBuilder Helped create reactive form.
   */
  public constructor(private formBuilder: FormBuilder) {
    super();
  }
  /**
   * All option.
   */
  private allOption$ = new ReplaySubject<any[]>(1);

  /**
   * Search control.
   */
  public searchControl = new FormControl('');

  /**
   * Select form control.
   */
  public selectFormControl = new FormControl([]);

  /**
   * Filtered options by option name term.
   */
  public filteredOptions$ = this.createFilteredOptionStream();

  /**
   * Form control changed.
   */
  public formControlChanged: (value: any) => void = () => {};

  /**
   * Creates a sorted filtered stream with options.
   */
  private createFilteredOptionStream(): Observable<Option[]> {
    return combineLatest([
      this.allOption$,
      listenControlChanges<string>(this.searchControl)]).pipe(
      map(([allOptions, searchValue]) => allOptions ?
        allOptions.filter(option => this.compareOptionWithSearchTerm(option, searchValue)) :
        []),
    );
  }

  /**
   * Triggers when a options is selected.
   * @param changedOptionsWithStatus option that has changed. Remove or add it to the selected.
   */
  public onSelectedOptionChange(changedOptionsWithStatus: MatOptionSelectionChange): void {
    if (changedOptionsWithStatus.isUserInput) {
      if (changedOptionsWithStatus.source.selected) {
        this.formControlChanged(this.selectFormControl.value.concat(changedOptionsWithStatus.source.value));
      } else {
        this.formControlChanged(this.selectFormControl.value.filter(
          option => !this.compareOption(option, changedOptionsWithStatus.source.value)),
        );
      }
    }
  }

  /**
   * Now select all options makes all options unchecked and loads all data.
   */
  public onSelectAllOptions(): void {
    /**
     * Emits empty array to make unchecked.
     */
    this.selectFormControl.setValue([]);
  }

  /**
   * On menu closed.
   */
  public onSelectOpenedChange(): void {
    this.searchControl.reset('');
  }

  /**
   * Register on change.Return value in HH:mm format.
   */
  public registerOnChange(fn: (value: any) => void): void {
    this.formControlChanged = fn;
  }

  /**
   * Register on touched.
   */
  public registerOnTouched(fn: () => {}): void {
  }

  /**
   * @inheritDoc
   */
  public writeValue(value: any[]): void {
    if (value) {
      this.selectFormControl.setValue(value);
    }
  }

  /**
   * @inheritDoc
   */
  public ngOnChanges(changes: SimpleChanges): void {
    if ('options' in changes && changes.options.currentValue) {
      this.allOption$.next(this.options);
    }
  }
}
