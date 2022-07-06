import { Injectable } from "@angular/core";
import {HttpClient} from '@angular/common/http';
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { FamilyTree } from "src/app/models/FamilyTree.model";
import { ResultStatistics } from "src/app/models/ResultStatistics.model";


@Injectable({
  providedIn: 'root'
})
export class ReportService {
  

  constructor(private http: HttpClient) { }

  getReport(id: string, level: number): Observable<FamilyTree> {
    let params = new URLSearchParams();
    params.append('level', level.toString());

    return this.http.get<FamilyTree>(`${environment.api_base_url}/api/Report/${id}?${params.toString()}`)
  }

  getReportStatistics = ({
    name = '',
    skincolor = '',
    education = '',
    sex = '',
  }) => {
    let params = new URLSearchParams();

    if(name != ""){
      params.append('name', name);
    }
    if(skincolor != ""){
      params.append('skincolor', skincolor);
    }
    if(education != ""){
      params.append('education', education);
    }
    if(sex != ""){
      params.append('sex', sex);
    }
    

    return this.http.get<ResultStatistics>(`${environment.api_base_url}/api/Report?${params.toString()}`)
  };
}