import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
})
export class NavBarComponent  {
  

  public open: boolean = false;

  toggleOpenMenu(force = false){
    if(force){
      this.open = false;
    }else {
      this.open = !this.open;
    }
    
  }
}
