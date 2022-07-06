import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { Person } from 'src/app/models/Person.model';
import { ResultPerson } from 'src/app/models/ResultPerson.model';
import { PersonService } from 'src/app/shared/services/person.service';
import { ModalPersonComponent } from 'src/app/shared/components/modal-person/modal-person.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TreeComponent } from 'src/app/shared/components/tree-view/tree-view.component';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-person',
  templateUrl: './person.component.html',
})
export class PersonComponent implements OnInit {
  public deleting: string[] = [];
  public person: Person[] = [];
  public total: number = 0;
  private page = 0;

  constructor(
    private personService: PersonService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.getAllPerson();
  }

  async getAllPerson() {
    this.personService.getAll({page: this.page}).subscribe({
      next: (data: ResultPerson) => {
        console.log(data);
        this.person = data.items;
        this.total = data.total;
      },
    });
  }

  handleEdit(person: Person) {
    let ref = this.dialog.open(ModalPersonComponent, {
      data: person,
    });
    ref.afterClosed().subscribe((value) => {
      if (value != null) {
        this.updatePerson(value)
      }
    });
  }

  handleDelete(person: Person) {
    this.deleting.push(person.id);
    this.personService.delete(person.id).subscribe({
      next: () => {
        this.snackBar.open('Pessoa deletada com sucesso.');
        this.deleting = this.deleting.filter(e => e != person.id); 
        this.getAllPerson();
      },
      error: () => {
        this.snackBar.open('Error ao tentar deletar pessoa.');
      }
    });
  }

  handleCreate() {
    let ref = this.dialog.open(ModalPersonComponent);
    ref.afterClosed().subscribe((value: Person) => {
      if (value != null) {
        this.createPerson(value);
      }
    });
  }

  createPerson(person: Person) {
    this.personService.create(person).subscribe({
      next: () => {
        this.snackBar.open('Pessoa cadastrada com sucesso.');
        this.getAllPerson();
      },
      error: () => {
        this.snackBar.open('Error ao tentar cadastrar pessoa.');
      }
    });
  }

  updatePerson(person: Person) {
    this.personService.update(person).subscribe({
      next: () => {
        this.snackBar.open('Pessoa atualizada com sucesso.');
        this.getAllPerson();
      },
      error: () => {
        this.snackBar.open('Error ao tentar atualizada pessoa.');
      }
    });
  }


  handleShowTree(person: Person){
    let ref = this.dialog.open(TreeComponent, {
      data: person,
    });
  }

  handleGetPerson(event: PageEvent){
    this.page = event.pageIndex + 1;
    this.getAllPerson();
  }
}
