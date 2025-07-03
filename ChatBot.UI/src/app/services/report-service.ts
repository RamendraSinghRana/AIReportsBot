import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(private http: HttpClient) { }

  generateExcel(data: any): Observable<Blob> {
    return this.http.post('http://localhost:5000/api/report/excel', data, { responseType: 'blob' });
  }
  generatePDF(data: any): Observable<Blob> {
    return this.http.post('http://localhost:5000/api/report/pdf', data, { responseType: 'blob' });
  }
  generateChart(data: any): Observable<Blob> {
    return this.http.post('http://localhost:5000/api/report/chart', data, { responseType: 'blob' });
  }
}
