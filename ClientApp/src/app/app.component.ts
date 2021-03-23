import { Component, OnInit } from '@angular/core';
import { EmployeeService } from './core/data-service/employee.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'ClientApp';
  constructor() {

  }
  ngOnInit() {
  }
}
