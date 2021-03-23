import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

const ApiUrl = environment.apiUrl;
@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient) { }

  getCountries() {
    return this.http.get(ApiUrl + "/countries");
  }
  getEmployee() {
    return this.http.get(ApiUrl + "/employees");
  }
  getEmployeeById(id: number) {
    return this.http.get(ApiUrl + "/employees/" + id);
  }
  deleteEmployee(id: number) {
    return this.http.delete(ApiUrl + "/employees/" + id);
  }
  updateEmployee(id: number, employee: any) {
    return this.http.put(ApiUrl + "/employees/" + id, employee);
  }

  createEmployee(employee: any) {
    return this.http.post(ApiUrl + "/employees/", employee);
  }
  getSkills() {
    return this.http.get(ApiUrl + "/skills");
  }
}
