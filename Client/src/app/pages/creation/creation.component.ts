import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Message } from 'primeng/api';
import { Observable, catchError, debounceTime, map, of, switchMap, timer } from 'rxjs';
import { Employee } from 'src/app/models/employee';
import { Group } from 'src/app/models/group';
import { EmployeeService } from 'src/app/services/employee.service';
import { ErrorHandlerService } from 'src/app/services/error-handler.service';
import { GroupService } from 'src/app/services/group.service';
import { ProjectService } from 'src/app/services/project.service';
import { dateRangeValidator } from 'src/app/validators/DateValidator';
import { validateProjectNumber } from 'src/app/validators/ProjectNumberValidator';

interface AutoCompleteCompleteEvent {
  originalEvent: Event;
  query: string;
}

@Component({
  selector: 'app-creation',
  templateUrl: './creation.component.html',
  styleUrls: ['./creation.component.css']
})
export class CreationComponent implements OnInit {

  projectForm: FormGroup;
  groups: Group[] = [];
  visas: Employee[] = [];
  errorMessage: string = '';
  alertMessage: Message[] = [];

  constructor(private projectService: ProjectService,
    private groupService: GroupService,
    private employeeService: EmployeeService,
    private fb: FormBuilder,
    private router: Router,
    private errHandler: ErrorHandlerService) {
    this.projectForm = this.fb.group(
      {
        projectNumber: ['', Validators.compose([
          Validators.required,
          Validators.min(1000),
          Validators.max(9999),
        ]),
        validateProjectNumber(projectService)
      ],
        customer: ['', Validators.compose([
          Validators.required,
          Validators.maxLength(50)
        ])],
        status: ['NEW', Validators.required],
        name: ['', Validators.compose([
          Validators.required,
          Validators.maxLength(50)
        ])],
        startDate: ['', Validators.compose([
          Validators.required,
          dateRangeValidator()
        ])],
        endDate: [''],
        groupId: ['', Validators.required],
        visas: ['', Validators.required]
      }
    );
  }
  ngOnInit(): void {
    this.alertMessage = [{ severity: 'error', detail: 'Please enter all mandatory fields (*)' }];
    this.getGroupIds();
  }

  onSubmit() {
    this.addProject();
  }

  private addProject() {
    const visasSelected = this.projectForm.value.visas as Employee[];
    const employeesVisas: any[] = [];

    for (const value of visasSelected) {
      employeesVisas.push(value.visa);
    }

    this.projectForm.get('visas')?.setValue(employeesVisas);

    return this.projectService.createProject(this.projectForm.value).subscribe(
      {
        next: () => {
          this.router.navigate([``]);
        },
        error: (err) => {
          this.errHandler.handleError(err);
          this.errorMessage = err.error.error;
          console.log(this.projectForm.value);
        }
      }
    );
  }

  getGroupIds() {
    this.groupService.getGroups().subscribe(
      (_groups: any) => {
        this.groups = _groups.groups;
      }
    )
  }

  getEmployeesByVisa(event: AutoCompleteCompleteEvent) {
    const visa = event.query;
    console.log(visa);
    this.employeeService.getEmployeesByVisa(visa).subscribe(
      (_employees: Employee[] | any) => {
        this.visas = _employees.employees;
        console.log(this.visas);
      }
    );
  }

  onCancel() {
    this.router.navigate(['']);
  }

}
