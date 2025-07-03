import { Component } from '@angular/core';
import * as XLSX from 'xlsx';
import { ReportService } from '../../services/report-service';

@Component({
  selector: 'app-chat',
  imports: [],
  templateUrl: './chat.html',
  styleUrl: './chat.css'
})
export class Chat {

  chartSrc: string | null = null;
  data: any[] = [];

  constructor(private reportService: ReportService) {}

  onFileChange(event: any): void {
    const target: DataTransfer = <DataTransfer>(event.target);
    const reader: FileReader = new FileReader();
    reader.onload = (e: any) => {
      const bstr: string = e.target.result;
      const workbook: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' });
      const firstSheetName: string = workbook.SheetNames[0];
      const worksheet: XLSX.WorkSheet = workbook.Sheets[firstSheetName];
      const jsonData: any[] = XLSX.utils.sheet_to_json(worksheet);
      this.data = jsonData.map((row:any) => ({
        Category: row['Category'],
        Value: row['Value']
      }));
      console.log(jsonData);
    };
    reader.readAsBinaryString(target.files[0]);
  }

  generate(type: 'excel' | 'pdf' | 'chart'): void {
    const methodMap: { 
      excel: (data: any) => import('rxjs').Observable<Blob>, 
      pdf: (data: any) => import('rxjs').Observable<Blob>, 
      chart: (data: any) => import('rxjs').Observable<Blob> 
    } = {
      excel: this.reportService.generateExcel,
      pdf: this.reportService.generatePDF,
      chart: this.reportService.generateChart
    };
    
    const method = methodMap[type];
    if (!method) {
      console.error(`Invalid report type: ${type}`);
      alert('Invalid report type selected.');
      return;
    }
    

    method.bind(this.reportService)(this.data).subscribe((blob: Blob) => {
      if(type === 'chart') {
        this.chartSrc = URL.createObjectURL(blob as Blob);
      } else {
        const a: HTMLAnchorElement = document.createElement('a');
        a.href = URL.createObjectURL(blob as Blob);
        a.download = `report.${type === 'excel' ? 'xlsx' : 'pdf'}`;
        a.click();
      }
    }, (error: any) => {
      console.error('Error generating report:', error);
      alert('Failed to generate report. Please try again.'); 
  });
}

}
