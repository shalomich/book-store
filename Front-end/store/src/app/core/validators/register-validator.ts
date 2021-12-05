import {AbstractControl, AsyncValidatorFn, FormControl, ValidationErrors, ValidatorFn} from '@angular/forms';
export class RegisterValidator {

  public static matchPassword(password: FormControl): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (control.value !== password.value) {
        return { passwordMatchError: 'Пароли не совпадают!' };
      }

      return null;
    };
  }
}
