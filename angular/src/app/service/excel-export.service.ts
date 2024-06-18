import * as XLSX from "xlsx";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class ExcelExportService {
  constructor() {}

  exportAsExcelFile(data: any[], excelFileName: string): void {
    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(data);
    const workbook: XLSX.WorkBook = {
      Sheets: { data: worksheet },
      SheetNames: ["data"],
    };
    const excelBuffer: any = XLSX.write(workbook, {
      bookType: "xlsx",
      type: "array",
    });
    this.saveAsExcelFile(excelBuffer, excelFileName);
  }

  private saveAsExcelFile(buffer: any, fileName: string): void {
    const data: Blob = new Blob([buffer], { type: EXCEL_TYPE });
    const file = new File([data], fileName + EXCEL_EXTENSION, {
      type: EXCEL_TYPE,
    });
    const link = document.createElement("a");
    const url = URL.createObjectURL(file);
    link.href = url;
    link.download = fileName + EXCEL_EXTENSION;
    link.click();
    URL.revokeObjectURL(url);
  }
}

const EXCEL_TYPE =
  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8";
const EXCEL_EXTENSION = ".xlsx";
