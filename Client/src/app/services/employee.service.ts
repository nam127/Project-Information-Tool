import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PROJECT_BASE_URL } from './project.service';
import { Employee } from '../models/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private httpClient: HttpClient) { }

  getEmployeesByVisa(visa: string) {
    return this.httpClient.get<Array<Employee>>(`${PROJECT_BASE_URL}/Employee?visa=${visa}`);
  }

  getVisasByProjectId(projectId: number) {
    return this.httpClient.get(`${PROJECT_BASE_URL}/Employee/find-visas?projectId=${projectId}`);
  }

}