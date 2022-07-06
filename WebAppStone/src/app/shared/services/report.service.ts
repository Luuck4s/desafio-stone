import { Injectable } from "@angular/core";
import {HttpClient} from '@angular/common/http';
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { FamilyTree } from "src/app/models/FamilyTree.model";


@Injectable({
  providedIn: 'root'
})
export class ReportService {
  

  constructor(private http: HttpClient) { }

  getReport(id: string, level: number): Observable<FamilyTree> {
    let params = new URLSearchParams();
    params.append('level', level.toString());

    return this.http.get<FamilyTree>(`${environment.api_base_url}/Report/${id}?${params.toString()}`)
  }
}