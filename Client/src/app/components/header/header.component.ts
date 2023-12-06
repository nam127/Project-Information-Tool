import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  isEn: boolean = true;

  setLanguage(lang: string): void{
    this.isEn = lang == "en";
  }
}

