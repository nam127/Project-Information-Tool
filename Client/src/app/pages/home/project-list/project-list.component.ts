import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ConfirmEventType, ConfirmationService, MessageService } from 'primeng/api';
import { Project } from 'src/app/models/project';
import { ErrorHandlerService } from 'src/app/services/error-handler.service';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css'],
})
export class ProjectListComponent implements OnInit {
  pageSize: number = 5;
  pageNumber: number = 1;
  count: number = 0;
  dataSource: Array<Project> = [];
  searchForm = this.fb.group({
    searchTerm: '',
    statusValue: ''
  });
  searchState: boolean = false;
  selectedItems: Array<number> = [];
  errorMessage: string = '';
  visible: boolean = false;
  isLoading: boolean = true;

  columnsDisplayed: Array<string> = [
    'actions',
    'number',
    'name',
    'status',
    'customer',
    'startDate',
    'action'
  ]
  constructor(
    private projectService: ProjectService,
    private fb: FormBuilder,
    private confirmService: ConfirmationService,
    private messageService: MessageService,
    private errHandler: ErrorHandlerService,) { }

  ngOnInit(): void {
    const searchStateRemain = this.projectService.getSearchState();
    console.log(searchStateRemain);
    if (searchStateRemain.searchTerm || searchStateRemain.statusValue) {
      this.searchState = true;
      this.searchForm.setValue({
        searchTerm: searchStateRemain.searchTerm,
        statusValue: searchStateRemain.statusValue
      });

      if (searchStateRemain.resultList.length > 0) {
        this.dataSource = searchStateRemain.resultList;
        this.count = searchStateRemain.resultList.length;
        console.log('datasource remain: ' + this.dataSource);
      }
      else {
        this.onSubmit();
        console.log(this.dataSource);
      }
    } 
    else {
      this.getProjects();
    }
  }

  showDialog() {
    this.visible = true;
  }

  getProjects(): void {
    this.projectService.getProjects(this.pageSize, this.pageNumber).subscribe(
      (_projects: any) => {
        this.count = _projects.totalPages;
        this.dataSource = _projects.projects;
        console.log('total items: ' + _projects.totalPages);
      }
    )
  }

  onTableDataChange(event: number) {
    if (this.searchState == false) {
      this.pageNumber = event;
      this.getProjects();
    } else {
      this.onSubmit();
    }
  }

  onSubmit(): void {
    const searchTerm = this.searchForm.value.searchTerm!;
    const statusValue = this.searchForm.value.statusValue!;
    if (searchTerm?.length === 0 && statusValue === null) {
      this.getProjects();
    }
    else {
      this.projectService.getProjectsFiltered(this.pageSize, this.pageNumber, searchTerm, statusValue).subscribe(
        (_projects: any) => {
          this.count = _projects.totalPages;
          this.dataSource = _projects.projects;
          this.searchState = true;

          this.projectService.setSearchState({
            searchTerm,
            statusValue,
            resultList: _projects.projects
          })
          if (_projects.totalPages === 0) {
            console.log("Not Found");
          }
        }
      );
    }
  }

  onResetSearch(): void {
    this.searchState = false;
    this.searchForm.value.searchTerm = '';
    this.searchForm.setValue({ searchTerm: '', statusValue: '' });
    this.projectService.clearSearchState();
    console.log("reset search");
    this.getProjects();
  }

  onDeleteProject(id: number) {
    this.projectService.deleteSingleProject(id).subscribe({
      next: (_projects: any) => {
        this.messageService.add({ severity: 'info', summary: 'Confirmed', detail: 'Delete Successfully' });
        this.getProjects();
      },
      error: (err: HttpErrorResponse) => {
        this.errHandler.handleError(err);
        this.errorMessage = err.error.error;
        this.showDialog();
      }
    }
    );
  }

  onProjectIdChange($event: any, projectId: number) {
    const index = this.selectedItems.indexOf(projectId);
    console.log(projectId);
    console.log(index);
    if (index === -1) {
      this.selectedItems.push(projectId);
    }
    else {
      this.selectedItems.splice(index, 1);
    }
    console.log(this.selectedItems);
  }

  onDeleteProjects(selectedItems: Array<number>) {
    this.projectService.deleteMultipleProjects(selectedItems).subscribe({
      next: (_projects: any) => {
        this.getProjects();
      },
      error: (err) => {
        this.errHandler.handleError(err);
        this.errorMessage = err.error.error;
        this.showDialog();
      }
    });
  }

  openDeleteDialog(projectId: number) {
    console.log(projectId);
    this.confirmService.confirm(
      {
        message: 'Are you sure to delete this project ?',
        header: 'PIM Tool',
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
          this.onDeleteProject(projectId);
        },
        reject: (type: any) => {
          switch (type) {
            case ConfirmEventType.REJECT:
              this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'You have rejected' });
              break;
            case ConfirmEventType.CANCEL:
              this.messageService.add({ severity: 'warn', summary: 'Cancelled', detail: 'You have cancelled' });
              break;
          }
        }
      }
    )
  };

  openDeleteProjectsDialog() {
    this.confirmService.confirm(
      {
        message: 'Are you sure to delete these projects ?',
        header: 'PIM Tool',
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
          this.onDeleteProjects(this.selectedItems);
        },
        reject: (type: any) => {
          switch (type) {
            case ConfirmEventType.REJECT:
              this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'You have rejected' });
              break;
            case ConfirmEventType.CANCEL:
              this.messageService.add({ severity: 'warn', summary: 'Cancelled', detail: 'You have cancelled' });
              break;
          }
        }
      }
    )
  };

}
