import * as _ from "underscore";
import { SaveEmployee, Employee } from '../../core/models/employee';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { EmployeeService } from 'src/app/core/data-service/employee.service';

import { ToastyService } from 'ng2-toasty';
import { forkJoin } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbDateStruct } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html',
  styleUrls: ['./employee-form.component.css']
})
export class EmployeeFormComponent implements OnInit {
  model: NgbDateStruct;
  isDisabled: true;
  @ViewChild('fileInput') fileInput: ElementRef;
  filename: any;
  countries: any = []
  cities: any = []
  skillsObject: any = []
  employee: SaveEmployee = {
    id: 0,
    name: null,
    cityId: 0,
    countryId: 0,
    skills: [],
    dateOfBirth: null,
    resume: null
  }
  constructor(private employeeService: EmployeeService, private toastyService: ToastyService, private router: Router, private route: ActivatedRoute) {
    route.params.subscribe(p => {
      this.employee.id = +p['id'] || 0;
    });
  }

  ngOnInit(): void {
    this.model = this.employee.dateOfBirth;
    let sources = [
      this.employeeService.getCountries(),
      this.employeeService.getSkills(),
    ]
    if (this.employee.id) {
      sources.push(this.employeeService.getEmployeeById(this.employee.id))
    }
    forkJoin(sources).subscribe(res => {
      this.countries = res[0];
      this.skillsObject = res[1];
      if (this.employee.id) {
        this.setEmployee(res[2]);
        this.populateModels();
      }
    })
  }
  private setEmployee(e: any) {
    this.employee.id = e.id;
    this.employee.name = e.name;
    this.employee.cityId = e.city.id;
    this.employee.countryId = e.country.id;
    this.employee.dateOfBirth = e.dateOfBirth;
    this.employee.resume = e.resume;
    this.employee.skills = _.pluck(e.skills, 'id');
  }
  onCountryChange() {
    this.populateModels();

    delete this.employee.cityId;
  }
  private populateModels() {
    let selectedCountry = this.countries.find(con => con.id == this.employee.countryId);
    this.cities = selectedCountry ? selectedCountry.cities : [];
  }


  onSkillToggle(id: any, $event) {
    if ($event.target.checked)
      this.employee.skills.push(id);
    else {
      var index = this.employee.skills.indexOf(id);
      this.employee.skills.splice(index, 1);
    }
    console.log("checkbox:", $event, id, this.employee.skills)
  }
  submit() {
    var result$ = (!this.employee.id) ? this.employeeService.createEmployee(this.employee) : this.employeeService.updateEmployee(this.employee.id, this.employee);
    result$.subscribe(res => {

      console.log(res)
      this.toastyService.success({
        title: 'Success',
        msg: 'Data was sucessfully saved.',
        theme: 'bootstrap',
        showClose: true,
        timeout: 5000
      });
      this.router.navigate(["/employee"]);
    }
      , error => {
        this.toastyService.error({
          title: 'Error',
          msg: 'An unexpected error occoured.',
          theme: 'bootstrap',
          showClose: true,
          timeout: 5000
        });
      })
  }

  async uploadPhoto() {
    let nativeElement: HTMLInputElement = this.fileInput.nativeElement;
    let file = nativeElement.files[0];
    console.log("file size:", file.size)
    let baseFile = await this.toBase64(file);
    this.filename = this.dataUrlBase64ToFile(baseFile, "limon's.cv")
    console.log(baseFile)
  }
  toBase64(file) {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result);
      reader.onerror = error => reject(error);
    });
  }
  dataUrlBase64ToFile(data, fileName) {
    var arr = data.split(','),
      mime = arr[0].match(/:(.*?);/)[1],
      bstr = atob(arr[1]),
      n = bstr.length,
      u8arr = new Uint8Array(n);

    while (n--) {
      u8arr[n] = bstr.charCodeAt(n);
    }

    return new File([u8arr], fileName, { type: mime });
  }
}
