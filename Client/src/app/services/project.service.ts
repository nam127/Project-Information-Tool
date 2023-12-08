import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Project } from '../models/project';

export const PROJECT_BASE_URL = 'https://localhost:7099/api/v1';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private httpClient: HttpClient) { }

  getProjects(pageSize: number, pageNumber: number): Observable<Array<Project>> {
    return this.httpClient.get<Array<Project>>(`${PROJECT_BASE_URL}/Project?` + `pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  getProjectsFiltered(pageSize: number, pageNumber: number, searchTerm: string | null, statusValue: string | null) {
    if (statusValue === '') {
      return this.httpClient.get<Array<Project>>(`${PROJECT_BASE_URL}/Project/search?` + `pageNumber=${pageNumber}&pageSize=${pageSize}&searchTerm=${searchTerm}`);
    } else {
      return this.httpClient.get<Array<Project>>(`${PROJECT_BASE_URL}/Project/search?` + `pageNumber=${pageNumber}&pageSize=${pageSize}&searchTerm=${searchTerm}&status=${statusValue}`);
    }
  }

  deleteSingleProject(id: number) {
    return this.httpClient.delete<any>(`${PROJECT_BASE_URL}/Project/delete/${id}`);
  }

  deleteMultipleProjects(projectIds: Array<number>) {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    return this.httpClient.delete(`${PROJECT_BASE_URL}/Project/delete`, {
      ...options,
      body: projectIds
    });
  }

  private searchState: { searchTerm: string | null, statusValue: string | null, resultList: Project[] }
    = { searchTerm: '', statusValue: '', resultList: [] };

  getSearchState(): {
    searchTerm: string | null, statusValue: string | null, resultList: Array<Project>
  } {
    return this.searchState;
  }

  setSearchState(state: {
    searchTerm: string | null,
    statusValue: string | null,
    resultList: Array<Project>
  }) {
    this.searchState = state;
  }

  createProject(project: Project){
    return this.httpClient.post(`${PROJECT_BASE_URL}/Project`, project);
  }

  clearSearchState() 
  {
    const state: {
      searchTerm: string;
      statusValue: string;
      resultList: Array<Project>;
    } = { searchTerm: '', statusValue: '', resultList: [] };

    this.setSearchState(state);
  }

}
