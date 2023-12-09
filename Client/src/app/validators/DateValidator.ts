import { AbstractControl, ValidatorFn } from "@angular/forms";

export function dateRangeValidator(): ValidatorFn {
  return (control: AbstractControl): {[key: string]: any} | null => {
    const startDate = control.get('startDate')?.value;
    const endDate = control.get('endDate')?.value;
    const invalid = startDate > endDate;
    return invalid ? { 'dateRangeInvalid': {value: control.value} } : null;
  };
}