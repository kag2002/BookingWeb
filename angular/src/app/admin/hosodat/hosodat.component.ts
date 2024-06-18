import { ChangeDetectorRef, Component } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import {
  ChiTietDatPhongServiceProxy,
  PhieuDaDuyetDto,
} from "@shared/service-proxies/service-proxies";
import { MessageService } from "primeng/api";
import { ExcelExportService } from "../../../app/service/excel-export.service";
@Component({
  selector: "app-hosodat",
  templateUrl: "./hosodat.component.html",
  styleUrls: ["./hosodat.component.css"],
})
export class HosodatComponent {
  listPhieuDaDuyet: PhieuDaDuyetDto[];
  formLocId: FormGroup;
  formLoccccd: FormGroup;
  showTongKet = false;

  constructor(
    private _ChiTietDatPhongService: ChiTietDatPhongServiceProxy,
    private cd: ChangeDetectorRef,
    private fb: FormBuilder,
    private messageService: MessageService,
    private excelExportService: ExcelExportService
  ) {}

  ngOnInit(): void {
    this._ChiTietDatPhongService.getAllPhieuDaDuyet().subscribe((result) => {
      this.listPhieuDaDuyet = result;
    });
    this.formLocId = this.fb.group({
      id: ["", Validators.required],
    });
    this.formLoccccd = this.fb.group({
      cccd: ["", Validators.required],
    });
  }

  reload() {
    this._ChiTietDatPhongService.getAllPhieuDaDuyet().subscribe((result) => {
      this.listPhieuDaDuyet = result;
      this.cd.detectChanges();
    });
  }

  filtercccd() {
    this._ChiTietDatPhongService
      .getPhieuDaDuyetByCccd(this.formLoccccd.value.cccd)
      .subscribe((result) => {
        this.listPhieuDaDuyet = result;

        console.log("loc thanh cong cccd ");
        this.cd.detectChanges();
      });
  }

  huyphong(phieuDatPhongId: number) {
    const confirmDelete = confirm("Bạn có chắc muốn hủy phiếu này?");

    if (confirmDelete) {
      this._ChiTietDatPhongService
        .deleteBookingFromPhieuDaDuyet(phieuDatPhongId)
        .subscribe(
          (result) => {
            this.reload(); // Reload the data after deletion
            this.messageService.add({
              severity: "success",
              summary: "Success",
              detail: "Hủy thành công",
            });
          },
          (error) => {
            this.messageService.add({
              severity: "error",
              summary: "Error",
              detail: "Hủy không thành công vui lòng kiểm tra lại",
            });
          }
        );
    }
  }

  dathanhtoan(phieuDatPhongId: number) {
    this._ChiTietDatPhongService.finishBooking(phieuDatPhongId).subscribe(
      (result) => {
        this.reload();
        this.messageService.add({
          severity: "success",
          summary: "Success",
          detail: "Thanh toán thành công",
        });
      },
      (error) => {
        this.messageService.add({
          severity: "error",
          summary: "Error",
          detail: "Thanh toán không thành công vui lòng kiểm tra lại",
        });
      }
    );
  }
  exportToExcel() {
    this.excelExportService.exportAsExcelFile(
      this.listPhieuDaDuyet,
      "PhieuDaDuyet"
    );
  }
}
