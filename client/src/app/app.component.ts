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

  constructor(private http: HttpClient) {
    this.http.get('https://localhost:5001/api/products').subscribe((res: IPagination) => {
      this.products = res.data;
    }, error => console.log(error))
  }

  ngOnInit(): void {

  }

}
