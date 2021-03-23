import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeFormComponent } from './components/employee-form/employee-form.component';
import { EmployeeComponent } from './components/employee/employee.component';
import { HomeComponent } from './home/home.component';



const routes: Routes = [
  { path: "", redirectTo: "employee", pathMatch: 'full' },
  { path: "employee", component: EmployeeComponent },
  { path: "employee/new", component: EmployeeFormComponent },
  { path: "employee/edit/:id", component: EmployeeFormComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
