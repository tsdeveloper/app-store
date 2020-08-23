import { Injectable } from '@angular/core';
import {NgxSpinnerService} from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
buryResquestCount = 0;

constructor(private spinnerService: NgxSpinnerService) { }

busy() {
  this.buryResquestCount++;
  this.spinnerService.show(undefined, {
    type: 'timer',
    bdColor: 'rgba(255,255,255, 0.7)',
    color: '#333'
  });
}

idle() {
  this.buryResquestCount--;
  if (this.buryResquestCount <= 0) {
    this.buryResquestCount = 0;
    this.spinnerService.hide();
  }
}
}
