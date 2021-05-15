import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {

  baseUrl = environment.apiUrl;
  ErrorValidation: any;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  get404Error(): void
  {
    this.http.get(this.baseUrl + 'products/sdf').subscribe(response => {
      console.log(response);
    },
    error => {
      console.log(error);
      this.ErrorValidation = error.errors;
    });
  }
}
