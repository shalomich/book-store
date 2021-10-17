import { AbstractControl, AsyncValidatorFn, ValidationErrors, ValidatorFn } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { map } from 'rxjs/operators';

import {
  API_FORM_ENTITY_URI, BOOK_FORMAT_ERROR,
  BOOK_FORMAT_REGULAR_EXPRESSION, BOOK_IMAGE_SIZE_ERROR, BOOK_IMAGES_AMOUNT_ERROR, ISBN_EXISTS_ERROR, ISBN_FORMAT_ERROR,
  ISBN_REGULAR_EXPRESSION, MAX_IMAGES_COUNT,
  MIN_IMAGE_HEIGHT,
  MIN_IMAGE_WIDTH, MIN_IMAGES_COUNT, TITLE_IMAGE_NAME_NOT_EXIST_ERROR
} from '../utils/values';
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
      if (!control.value.match(ISBN_REGULAR_EXPRESSION)) {
        return of({ isbnValidator: ISBN_FORMAT_ERROR });
      }

      const httpClient = InjectorInstance.get<HttpClient>(HttpClient);

      return httpClient.get(`${API_FORM_ENTITY_URI}book/isbn-existed?isbn=${control.value}`)
        .pipe(
          map(response => {
            if (response) {
              return { isbnValidator: ISBN_EXISTS_ERROR };
            }
            return null;
          }),
        );
    };
  }

  public static isBookFormatValid(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control.value.match(BOOK_FORMAT_REGULAR_EXPRESSION) && control.value) {
        return { bookFormatValidator: BOOK_FORMAT_ERROR };
      }

      return null;
    };
  }

  public static imagesValid(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value as Album;

      if (value.images.length <= MIN_IMAGES_COUNT || value.images.length > MAX_IMAGES_COUNT) {
        return { imagesValidator: BOOK_IMAGES_AMOUNT_ERROR };
      }

      if (value.images.find(image => (image.height < MIN_IMAGE_HEIGHT) || (image.width < MIN_IMAGE_WIDTH))) {
        return { imagesValidator: BOOK_IMAGE_SIZE_ERROR };
      }

      return null;
    };
  }

  public static isTitleImageNameValid(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value as Album;
      if (!value.images.find(image => image.name === value.titleImageName)) {
        return { titleImageNameValidator: TITLE_IMAGE_NAME_NOT_EXIST_ERROR };
      }

      return null;
    };
  }
}
