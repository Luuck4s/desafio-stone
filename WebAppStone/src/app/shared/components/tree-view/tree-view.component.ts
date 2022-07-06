import {
  Component,
  AfterViewInit,
  ElementRef,
  ViewChild,
  Inject,
} from '@angular/core';

import { Network } from 'vis-network/peer/esm/vis-network';
import { DataSet } from 'vis-data/peer/esm/vis-data';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Person } from 'src/app/models/Person.model';
import { ReportService } from '../../services/report.service';
import { FamilyTree } from 'src/app/models/FamilyTree.model';

@Component({
  selector: 'tree-view-app',
  templateUrl: 'tree-view.component.html',
})
export class TreeComponent implements AfterViewInit {
  @ViewChild('network') el: ElementRef | undefined;
  private networkInstance: any;
  private familyTree: FamilyTree | undefined;

  constructor(
    private reportService: ReportService,
    public dialogRef: MatDialogRef<TreeComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Person
  ) {}

  ngAfterViewInit() {
    this.getTreeUser();
  }

  async getTreeUser(levelTree: number = 1) {
    this.reportService.getReport(this.data.id, levelTree).subscribe({
      next: (value) => {
        this.familyTree = value;
        this.showTree();
      },
    });
  }

  showTree() {
    const container = this.el!.nativeElement;
    const nodes = this.createNodes();

    const edges = this.createEdges();
    const data = { nodes, edges };

    const options = {
      height: '500px',
      layout: {
        hierarchical: {
          enabled: true,
          direction: 'UD',
          sortMethod: 'directed',
        },
      },
      interaction: {
        dragNodes: false,
      },
      physics: {
        enabled: false,
      },
    };

    this.networkInstance = new Network(container, data, options);

    this.networkInstance.options;
  }

  createNodes() {
    let nodes: any = this.familyTree?.people.map((person) => {
      return {
        id: person.id,
        label: person.name,
        shape: 'box',
        size: 450,
        color: person.id == this.data.id ? "#7298ed" : "#d1d5db"
      };
    });

    return new DataSet<any>(nodes);
  }

  createEdges() {
    let relations: any = this.familyTree?.relations.map((relation) => {
      return {
        from: relation.parentId,
        to: relation.childId,
      };
    });

    return new DataSet<any>(relations);
  }

  handleGetTreeLevel(event: any){
    const inputValue = event.target.value;

    this.getTreeUser(inputValue);
  }
}
