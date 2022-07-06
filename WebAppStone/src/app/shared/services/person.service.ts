import { Injectable } from "@angular/core";
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from "rxjs";
import { Person } from "src/app/models/Person.model";
import { ResultPerson } from "src/app/models/ResultPerson.model";
import { environment } from "src/environments/environment";


@Injectable({
  providedIn: 'root'
})
export class PersonService {
  

  constructor(private http: HttpClient) { }

  getAll({page = 0, name = ""}): Observable<ResultPerson> {
    let params = new URLSearchParams();
    params.append('page', page.toString());

    if(name != ""){
      params.append('name', name);
    }
    return this.http.get<ResultPerson>(`${environment.api_base_url}/Person/GetAll?${params.toString()}`)
  }

  get(id: string): Observable<Person> {
    return this.http.get<Person>(`${environment.api_base_url}/Person/${id}`)
  }

  create(data: any): Observable<any>{
    return this.http.post<Person>(`${environment.api_base_url}/Person`, data)
  }

  delete(id: string): Observable<any>{
    return this.http.delete<Person>(`${environment.api_base_url}/Person/${id}`)
  }

  update(data: Person): Observable<any>{
    return this.http.put<Person>(`${environment.api_base_url}/Person/${data.id}`,data)
  }
}