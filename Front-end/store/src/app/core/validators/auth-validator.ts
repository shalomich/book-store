import { AbstractControl, AsyncValidatorFn, FormControl, ValidationErrors, ValidatorFn } from '@angular/forms';


import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { InjectorInstance } from '../../app.module';

import {
  EMAIL_EXISTENCE_ERROR,
  EMAIL_FORMAT_ERROR,
  PASSWORD_FORMAT_ERROR,
  PASSWORD_MATCH_ERROR,
  PHONE_FORMAT_ERROR,
} from '../utils/validation-errors';

export class AuthValidator {

  public static matchPassword(password: AbstractControl): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (control.value !== password.value) {
        return { error: PASSWORD_MATCH_ERROR };
      }

      return null;
    };
  }

  /**
   * Validating entered email (returns error if user with such email already exists).
   */
  public static doesEmailExist(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      const httpClient = InjectorInstance.get<HttpClient>(HttpClient);

      return httpClient.get(`https://comic-store-server.herokuapp.com/store/Account/email-existence/${control.value}`)
        .pipe(
          map(response => {
            if (response) {
              return { error: EMAIL_EXISTENCE_ERROR };
            }
            return null;
          }),
        );
    };
  }

  public static emailFormat(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const isFormatValid = String(control.value)
        .toLowerCase()
        .match(
          /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/,
        );

      if (!isFormatValid && control.value) {
        return { error: EMAIL_FORMAT_ERROR };
      }

      return null;
    };
  }

  public static passwordFormat(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const isFormatValid = (control.value.search(/\d/) !== -1) && (control.value.search(/[a-zA-Z]/) !== -1);

      if ((control.value.length < 8 || !isFormatValid) && control.value) {
        return { error: PASSWORD_FORMAT_ERROR };
      }

      return null;
    };
  }

  public static phoneNumberFormat(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const isFormatValid = String(control.value)
        .match(
          /(^8|7|\+7)((\d{10})|(\s\(\d{3}\)\s\d{3}\s\d{2}\s\d{2}))$/,
        );

      if (!isFormatValid && control.value) {
        return { error: PHONE_FORMAT_ERROR };
      }

      return null;
    };
  }
}
