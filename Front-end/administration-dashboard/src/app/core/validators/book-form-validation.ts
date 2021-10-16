import { AbstractControl, AsyncValidatorFn, ValidationErrors, ValidatorFn } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { map } from 'rxjs/operators';

import { API_FORM_ENTITY_URI, MIN_IMAGE_HEIGHT, MIN_IMAGE_WIDTH } from '../utils/values';
import { InjectorInstance } from '../../app.module';
import { Album } from '../interfaces/album';

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

  public static imagesValid(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value as Album;

      if (value.images.length <= 0 || value.images.length > 5) {
        return { imagesValidator: 'Images amount should be from 1 to 5!' };
      }

      if (value.images.find(image => (image.height < MIN_IMAGE_HEIGHT) || (image.width < MIN_IMAGE_WIDTH))) {
        return { imagesValidator: 'Minimum image size should be 600x800!' };
      }

      return null;
    };
  }

  public static isTitleImageNameValid(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value as Album;
      if (!value.images.find(image => image.name === value.titleImageName)) {
        return { titleImageNameValidator: 'Image with such name was not selected!' };
      }

      return null;
    };
  }
}
