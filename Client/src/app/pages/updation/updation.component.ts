import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { debounceTime } from 'rxjs';
import { Employee } from 'src/app/models/employee';
import { Group } from 'src/app/models/group';
import { EmployeeService } from 'src/app/services/employee.service';
import { ErrorHandlerService } from 'src/app/services/error-handler.service';
import { GroupService } from 'src/app/services/group.service';
import { ProjectService } from 'src/app/services/project.service';

interface AutoCompleteCompleteEvent {
  originalEvent: Event;
  query: string;
}

@Component({
  selector: 'app-updation',
  templateUrl: './updation.component.html',
  styleUrls: ['./updation.component.css']
})
export class UpdationComponent implements OnInit {

  projectForm: FormGroup;
  groups: Group[] = [];
  visas: Employee[] = [];
  errorMessage: string = '';
  projectId!: string | null;
  project: any;
  visasList: string[] = [];
  visible: boolean = false;

  constructor(private projectService: ProjectService,
    private groupService: GroupService,
    private employeeService: EmployeeService,
    private fb: FormBuilder,
    private activateRouter: ActivatedRoute,
    private router: Router,
    private errHandler: ErrorHandlerService,
    private datePipe: DatePipe) {
    this.projectForm = this.fb.group(
      {
        projectNumber: ['', Validators.required],
        customer: ['', Validators.required],
        status: ['NEW'],
        name: ['', Validators.required],
        startDate: ['', Validators.required],
        endDate: ['', Validators.required],
        groupId: ['', Validators.required],
        visas: ['', Validators.required]
      }
    );
  }

  ngOnInit(): void {
    this.projectId = this.activateRouter.snapshot.paramMap.get('id');
    this.projectForm.get('projectNumber')?.disable();
    this.getGroupIds();
    console.log(this.projectId);
    this.getProjectForm(this.projectId);
    // this.getVisasByProject(this.projectId);
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

  getGroupIds() {
    this.groupService.getGroups().subscribe(
      (_groups: any) => {
        this.groups = _groups.groups;
      }
    )
  }

  getProjectForm(projectId: any) {
    this.projectService.getProjectById(projectId).subscribe({
      next: (_projects) => {
        this.project = _projects;
        console.log(this.project);

        let formatStartDate = this.datePipe.transform(this.project.startDate,
          'yyyy-MM-dd');

        let formatEndDate = this.datePipe.transform(this.project.endDate,
          'yyyy-MM-dd');

        this.projectForm.patchValue({
          projectNumber: this.project.projectNumber,
          name: this.project.name,
          customer: this.project.customer,
          groupId: this.project.groupId,
          status: this.project.status,
          startDate: formatStartDate,
          endDate: formatEndDate,
          visas: this.getVisasByProject(projectId)
        })

        // this.getVisasByProject(projectId);
      }
    })
  }

  getVisasByProject(projectId: any) {
    this.employeeService.getVisasByProjectId(projectId).subscribe({
      next: (_employees: any) => {
        // this.visasList = _visas;
        this.projectForm.get('visas')?.setValue(_employees.employees);
      }
    })
  }

  onSubmit() {
    this.updateProject();
  }

  updateProject() {
    this.projectId = this.activateRouter.snapshot.paramMap.get('id');
    project: this.getProjectForm(this.projectId);
    const visasSelected = this.projectForm.value.visas as Employee[];
    const employeesVisas: any[] = [];

    for (const value of visasSelected) {
      employeesVisas.push(value.visa);
      console.log('visa to push' + value.visa);
    }
    console.log('visa', employeesVisas);
    this.projectForm.get('visas')?.patchValue(employeesVisas);
    console.log('Visas update: ', this.projectForm.get('visas')?.value);

    this.projectService.updateProject(this.projectId, this.projectForm.value)
    .pipe(debounceTime(3000))
    .subscribe({
      next: () => {
        console.log('successfull');
        this.router.navigate(['']);
      },
      error: (err) => {
        this.errHandler.handleError(err);
        this.errorMessage = err.error.error;
        console.log(this.projectForm.value);
      }
    });
  }
}
