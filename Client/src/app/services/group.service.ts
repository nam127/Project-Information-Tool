import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PROJECT_BASE_URL } from './project.service';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

constructor(private httpClient: HttpClient) { }

getGroups(){
  return this.httpClient.get(`${PROJECT_BASE_URL}/Group`);
}
}
