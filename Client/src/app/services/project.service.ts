import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Project } from '../models/project';

const PROJECT_BASE_URL = 'https://localhost:7099/api/v1';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {


  constructor(private httpClient: HttpClient) { }

  getProjects(pageSize: number, pageNumber: number): Observable<Array<Project>> {
    return this.httpClient.get<Array<Project>>(`${PROJECT_BASE_URL}/Project?` + `pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }
}
