import { AbstractControl, AsyncValidatorFn, ValidationErrors, ValidatorFn } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { map } from 'rxjs/operators';

import { API_FORM_ENTITY_URI } from '../utils/values';
import { InjectorInstance } from '../../app.module';

export class BookFormValidation {

  /**
   * Validating entered isbn (returns error if book with such isbn already exists, or if isbn's format is incorrect).
   */
  public static isISBNValid(currentISBN: string | null): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      if (currentISBN) {
        if (currentISBN === control.value) {
          return of(null);
        }
      }
      if (!control.value.match('^978-5-\\d{6}-\\d{2}-\\d{1}$')) {
        return of({ isbnValidator: 'Incorrect ISBN format!' });
      }

      const httpClient = InjectorInstance.get<HttpClient>(HttpClient);

      return httpClient.get(`${API_FORM_ENTITY_URI}/book/isbn-existed?isbn=${control.value}`)
        .pipe(
          map(response => {
            if (response) {
              return of({ isbnValidator: 'Book with such ISBN already exists!' });
            }
            return null;
          }),
        );
    };
  }

  public static isBookFormatValid(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control.value.match('^[1-9]\\d{1}x[1-9]\\d{1}x[1-9]$') && control.value) {
        return { bookFormatValidator: 'Incorrect book format!' };
      }

      return null;
    };
  }
}
