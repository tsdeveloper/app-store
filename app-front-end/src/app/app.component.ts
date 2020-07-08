import { environment } from './../environments/environment';
import { IProduct } from './models/product';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IPagination } from './models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'client';
  products: IProduct[];
  urlAPI = environment.apiUrl;

  constructor() {

  }

  ngOnInit(): void {

  }

}
