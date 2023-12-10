import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  isEn: boolean = true;

  constructor(private translateService: TranslateService) {}
  setLanguage(lang: string): void{
    this.translateService.use(lang);
    console.log('languge trigger');
    this.isEn = lang == "en";
  }
}

