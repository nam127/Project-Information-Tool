import { Component, OnInit } from '@angular/core';
import { Project } from 'src/app/models/project';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent implements OnInit {
  pageSize: number = 5;
  pageNumber: number = 1;
  count: number = 0;
  dataSource: Array<Project> = [];
  columnsDisplayed: Array<string> = [
    'number',
    'name',
    'status',
    'customer',
    'startDate',
    'action'
  ]
  constructor(private projectService: ProjectService) { }
  ngOnInit(): void {
    this.getProjects();
  }

  getProjects(): void {
    this.projectService.getProjects(this.pageSize, this.pageNumber).subscribe(
      (_projects) => {
        this.dataSource = _projects;
        console.log('Before count'+this.count);
        this.count = _projects.length;
        console.log('Last count'+this.count);
      }
    )
  }

  onTableDataChange(event: number) {
    this.pageNumber = event;
    this.getProjects();
  }
}
