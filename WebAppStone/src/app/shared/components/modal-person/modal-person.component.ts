import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Person } from 'src/app/models/Person.model';
import { PersonService } from '../../services/person.service';
import { FormBuilder, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { firstValueFrom, lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-modal-person',
  templateUrl: './modal-person.component.html',
})
export class ModalPersonComponent implements OnInit {
  public personFatherFilteredOption: Person[] = [];
  public personMotherFilteredOption: Person[] = [];
  public sexOptions: string[] = ['F', 'M', 'Não Informada'];
  public skinColorOptions: string[] = [
    'Branco(a)',
    'Pardo(a)',
    'Amarelo(a)',
    'Indígena',
    'Não Informada',
  ];
  public educationOptions: string[] = [
    'Analfabeto(a)',
    'Alfabetizado(a)',
    'Ensino Fundamental',
    'Ensino Médio',
    'Ensino Superior',
    'Pós-Graduação',
    'Não Informada',
  ];

  public selectedFather:any;;
  public selectedMother:any;

  public formPerson = this.fb.group({
    id: [''],
    name: ['', Validators.required],
    lastName: ['', Validators.required],
    sex: ['', Validators.required],
    skinColor: ['', Validators.required],
    education: ['', Validators.required],
    fatherID: [''],
    motherID: [''],
  });

  constructor(
    private personService: PersonService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<ModalPersonComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Person
  ) {}

  ngOnInit(): void {
    this.setValues();
  }

  async setValues() {
    if (this.data != null) {
      if(this.data.motherID){
        this.selectedMother = await firstValueFrom(this.personService.get(this.data.motherID));
      }

      if(this.data.fatherID){
        this.selectedFather = await firstValueFrom(this.personService.get(this.data.fatherID));
      }

      this.formPerson.setValue({
        id: this.data.id,
        name: this.data.name,
        lastName: this.data.lastName,
        sex: this.data.sex,
        skinColor: this.data.skinColor,
        education: this.data.education,
        fatherID: this.data.fatherID || '',
        motherID: this.data.motherID || '',
      });
    }
  }

  getAllPersonByName(name: string) {
    return this.personService.getAll({name});
  }


  async getPersonFather(event: any){
    const inputValue = event.target.value;
    this.getAllPersonByName(inputValue).subscribe({
      next: (value) => {
        this.personFatherFilteredOption = value.items;
      }
    });
  }

  async getPersonMother(event: any){
    const inputValue = event.target.value;
    this.getAllPersonByName(inputValue).subscribe({
      next: (value) => {
        this.personMotherFilteredOption = value.items;
      }
    });
  }

  displayFn(person: Person){
    return person && person.name ? person.name : '';
  }

  handleSave() {
    if (this.formPerson.invalid) {
      this.snackBar.open('Preencha os campos obrigatórios.');
    } else {
      let dataToSend = this.formPerson.value;
      if(this.selectedFather != null){
        dataToSend.fatherID = this.selectedFather.id;
      }

      if(this.selectedMother != null){
        dataToSend.motherID = this.selectedMother.id;
      }
      
      return this.dialogRef.close(dataToSend);
    }
  }
}
