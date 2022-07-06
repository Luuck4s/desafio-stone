import { Component, OnInit } from '@angular/core';
import { ResultStatistics } from 'src/app/models/ResultStatistics.model';
import { ReportService } from 'src/app/shared/services/report.service';
import { StatisticsService } from 'src/app/shared/services/statistics.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
  public name: string = '';
  public sex: string = '';
  public education: string = '';
  public skinColor: string = '';

  public result: ResultStatistics = new ResultStatistics();
  public view: [number, number] = [350, 350];
  public personByEducation: any[] = [];
  public personBySex: any[] = [];
  public personBySkinColor: any[] = [];

  public sexOptions: string[] = ['Todos','F', 'M', 'Não Informada'];
  public skinColorOptions: string[] = [
    'Todos',
    'Branco(a)',
    'Pardo(a)',
    'Amarelo(a)',
    'Indígena',
    'Não Informada',
  ];
  public educationOptions: string[] = [
    'Todos',
    'Analfabeto(a)',
    'Alfabetizado(a)',
    'Ensino Fundamental',
    'Ensino Médio',
    'Ensino Superior',
    'Pós-Graduação',
    'Não Informada',
  ];

  constructor(
    private statisticsService: StatisticsService,
    private reportService: ReportService
  ) {}

  ngOnInit(): void {
    this.getData();
    this.statisticsService.startConnection(() => this.getData());
  }

  async getData() {
    this.reportService
      .getReportStatistics({
        name: this.name,
        sex: this.sex,
        education: this.education,
        skincolor: this.skinColor,
      })
      .subscribe({
        next: (value) => {
          this.result = value;
          this.formatData(value);
        },
      });
  }

  formatData(data: ResultStatistics) {
    this.personByEducation = this.aggregateByColumn(data, "education");
    this.personBySex = this.aggregateByColumn(data, "sex");
    this.personBySkinColor = this.aggregateByColumn(data, "skinColor");
  }

  aggregateByColumn(data: ResultStatistics, column: string){
    let result: any[] = [];

    data.items.map((item: any) => {
      let hasDataIndex = result.findIndex((i: any) => i.name == item[column])

      if(hasDataIndex != -1){
        result[hasDataIndex].value += 1;
      }else {
        result.push({
          name: item[column],
          value: 1,
        })
      }
    });

    return result;
  }

  handleSearchByName(event: any){
    const value = event.target.value.trim();

    if(value.length < 1 && this.name == ""){
      return;
    }

    if(value != ""){
      this.name = value;
    }else {
      this.name = "";
    }

    this.getData();
  }

  handleSearchBySkinColor(selectedValue: any){
    const value = selectedValue.value;

    if(value == "Todos"){
      this.skinColor = "";
    }else {
      this.skinColor = value;
    }

   
    this.getData();
  }

  handleSearchByEscolaridade(selectedValue: any){
    const value = selectedValue.value;

    if(value == "Todos"){
      this.education = "";
    }else {
      this.education = value;
    }

    this.getData();
  }

  handleSearchBySex(selectedValue: any){
    const value = selectedValue.value;

    if(value == "Todos"){
      this.sex = "";
    }else {
      this.sex = value;
    }
    
    this.getData();
  }
}
