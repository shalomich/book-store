import { AbstractControl, AsyncValidatorFn, FormControl, ValidationErrors, ValidatorFn } from '@angular/forms';
export class AuthValidator {

  public static matchPassword(password: AbstractControl): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (control.value !== password.value) {
        return { error: 'Пароли не совпадают' };
      }

      return null;
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
        return { error: 'Неверный формат email' };
      }

      return null;
    };
  }

  public static passwordFormat(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const isFormatValid = (control.value.search(/\d/) !== -1) && (control.value.search(/[a-zA-Z]/) !== -1);

      if ((control.value.length < 8 || !isFormatValid) && control.value) {
        return { error: 'Пароль должен быть не меньше 8 символов, и содержать буквы и цифры' };
      }

      return null;
    };
  }
}
