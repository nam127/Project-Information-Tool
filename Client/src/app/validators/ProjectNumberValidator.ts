import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import { ProjectService } from "../services/project.service";
import { Observable, catchError, debounceTime, map, of, switchMap, tap } from "rxjs";

// export const validateProjectNumber = (projectService: ProjectService) => {
//   return (control: AbstractControl): Observable<ValidationErrors | null> => {
//     return control.valueChanges.pipe(
//       debounceTime(300),
//       switchMap(value => {
//         return projectService.checkProjectNumber(control.value).pipe(
//           catchError(error => {
//             console.log('error response' + error);
//             return of({ projectNumberExists: true })
//           }),
//           map((isValid) => {
//             return isValid ? null : { projectNumberExists: true };
//           }),
//           tap(response => console.log('get the response of api project number' + response)),
//         );
//       })
//     );
//   };
// }

export const validateProjectNumber = (projectService: ProjectService) => {
  return (control: AbstractControl): Observable<ValidationErrors | null> => {
    return projectService.checkProjectNumber(control.value).pipe(
      map((isValid) => {
        return isValid ? null : { projectNumberExists: true };
      }),
      catchError(() => of({ projectNumberExists: true })) // Handle API errors
    );
  };
};