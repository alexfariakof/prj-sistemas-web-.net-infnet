import { Component } from '@angular/core';
import ToolBarSecondaryModule from '../../components/tool-bar-secondary/tool-bar-secondary.module';

@Component({
  selector: 'app-page-access-denied',
  standalone: true,
  imports: [ToolBarSecondaryModule],
  templateUrl: './page-access-denied.component.html'
})
export class PageAccessDeniedComponent {

}
