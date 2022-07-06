import { Person } from "./Person.model";

export class FamilyTreePerson{
  id!: string
  name!: string
  fatherId?: string
  motherId?: string
}
