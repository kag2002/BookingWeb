import { ChangeDetectorRef, Component } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ActivatedRoute, Params } from "@angular/router";
import {
  ChiTietDatPhongDto,
  ChiTietDatPhongServiceProxy,
  DatPhongServiceProxy,
  InfoBookingDto,
  PhieuDatPhongOutputDto,
  PhongInputDto,
} from "@shared/service-proxies/service-proxies";
import * as moment from "moment";
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
    private _ChiTietDatPhongService: ChiTietDatPhongServiceProxy,

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
    this._ChiTietDatPhongService.denyBooking(this.idphieudat);
  }
  accept() {
    this._ChiTietDatPhongService.acceptBooking(this.idphieudat);
  }
  // delete() {
  //   this._ChiTietDatPhongService(this.idphieudat).subscribe((result) => {});
  // }
}
