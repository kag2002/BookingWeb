import { ChangeDetectorRef, Component } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ActivatedRoute, Params, Router } from "@angular/router";
import {
  ChiTietDatPhongDto,
  ChiTietDatPhongServiceProxy,
  InfoBookingDto,
  PhieuDatPhongOutputDto,
} from "@shared/service-proxies/service-proxies";
import * as moment from "moment";
import { MessageService } from "primeng/api";
@Component({
  selector: "app-quanlyphieudat",
  templateUrl: "./quanlyphieudat.component.html",
  styleUrls: ["./quanlyphieudat.component.css"],
})
export class QuanlyphieudatComponent {
  idphieudat: number;
  SoDemThue;
  listChiTietPhieuDat: PhieuDatPhongOutputDto[];

  phieuselected: ChiTietDatPhongDto;

  phieudatdataupdate: PhieuDatPhongOutputDto = new PhieuDatPhongOutputDto();
  infoBooking: InfoBookingDto | undefined;

  formSuaPhieuDat: FormGroup;

  constructor(
    // private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private _ChiTietDatPhongService: ChiTietDatPhongServiceProxy,
    private messageService: MessageService,

    private cd: ChangeDetectorRef
  ) {}
  calculateNumberOfNights(start: moment.Moment, end: moment.Moment): number {
    if (start && end) {
      const duration = moment.duration(end.diff(start));
      return duration.asDays();
    }
    return 0; // Hoặc giá trị mặc định tương ứng
  }
  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.idphieudat = +params["idphieudat"];
    });
    this._ChiTietDatPhongService
      .getChiTietDatPhongByPhieuDatPhongId(this.idphieudat)
      .subscribe((result) => {
        this.phieuselected = result;
        this.SoDemThue = this.calculateNumberOfNights(
          result.ngayBatDau,
          result.ngayHenTra
        );
        this.cd.detectChanges();
      });

    // this.formSuaPhieuDat = this.fb.group({
    //   ngaydat: ["", Validators.required],
    //   ngaytra: ["", Validators.required],
    // });
  }

  update() {
    this.phieudatdataupdate.id = this.idphieudat;
    // this.phieudatdataupdate.hoTen = this.phieuselected[0].hoTen;
    // this.phieudatdataupdate.cccd = this.phieuselected[0].cccd;
    // this.phieudatdataupdate.email = this.phieuselected[0].email;
    // this.phieudatdataupdate.datHo = this.phieuselected[0].datHo;
    // this.phieudatdataupdate.sdt = this.phieuselected[0].sdt;

    this.phieudatdataupdate.ngayBatDau = this.infoBooking.ngayDat;
    this.phieudatdataupdate.ngayHenTra = this.infoBooking.ngayTra;
    // this.phieudatdataupdate.diemQuaTrinh = this.formSuaPhieuDat.value.diemquatrinh;

    // this._ChiTietDatPhongService
    //   .updateTicket(this.phieudatdataupdate)
    //   .subscribe((result) => {});
  }
  deny() {
    const confirmDelete = confirm("Bạn có chắc muốn hủy phiếu này?");

    if (confirmDelete) {
      this._ChiTietDatPhongService
        .denyBooking(this.idphieudat)
        .subscribe((result) => {
          if (result) {
            console.log("Booking denied");
          } else {
            console.log("Failed to deny booking");
          }
        });
    }
  }
  accept() {
    this._ChiTietDatPhongService
      .acceptBooking(this.idphieudat)
      .subscribe((result) => {
        if (result) {
          this.messageService.add({
            severity: "success",
            summary: "Success",
            detail: "Đặt thành công",
          });
          console.log("Booking accepted and added to PhieuDaDuyet");
        } else {
          console.log("Failed to accept booking");
          this.messageService.add({
            severity: "error",
            summary: "Error",
            detail: "Đặt không thành công vui lòng kiểm tra lại",
          });
        }
      });
    this.router.navigate(["/app/Phieudatlist"]).then(() => {
      // Reload the browser after navigation
      window.location.reload();
    });
  }
  // delete() {
  //   this._ChiTietDatPhongService(this.idphieudat).subscribe((result) => {});
  // }
}
