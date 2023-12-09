import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {
public errorMessage: string = '';

constructor(private router: Router) { }

public handleError = (error: HttpErrorResponse) => {
  if(error.status === 500){
    this.handle500Error(error);
  }
  else if (error.status === 404){
    this.handle404Error(error);
  }
  else if(error.status === 400){
    this.handle400Error(error);
  }
  else if(error.status === 409){
    this.handle409Error(error);
  }
}

private handle500Error = (error: HttpErrorResponse) => {
  this.createErrorMessage(error);
  this.router.navigate(['/error/500']);
}

private handle404Error = (error: HttpErrorResponse) => {
  this.createErrorMessage(error);
  this.router.navigate(['/error/400']);
}

private handle400Error = (error: HttpErrorResponse) => {
  this.createErrorMessage(error);
}

private handle409Error = (error: HttpErrorResponse) => {
  this.createErrorMessage(error);
}

private createErrorMessage = (error: HttpErrorResponse) => {
  this.errorMessage = error.error ? error.error : error.statusText;
}

}
