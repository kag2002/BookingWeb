import { ChangeDetectorRef, Component } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ActivatedRoute, Params } from "@angular/router";
import {
  DatPhongServiceProxy,
  InfoBookingDto,
  PhieuDatPhongOutputDto,
  PhongInputDto,
} from "@shared/service-proxies/service-proxies";

@Component({
  selector: "app-quanlyphieudat",
  templateUrl: "./quanlyphieudat.component.html",
  styleUrls: ["./quanlyphieudat.component.css"],
})
export class QuanlyphieudatComponent {
  idphieudat: number;
  listChiTietPhieuDat: PhieuDatPhongOutputDto[];

  phieuselected: PhieuDatPhongOutputDto[];

  phieudatdataupdate: PhieuDatPhongOutputDto = new PhieuDatPhongOutputDto();
  infoBooking: InfoBookingDto | undefined;

  formSuaPhieuDat: FormGroup;

  constructor(
    // private fb: FormBuilder,
    private route: ActivatedRoute,
    private _ChiTietDatPhongService: DatPhongServiceProxy,

    private cd: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.idphieudat = +params["idphieudat"];
    });
    this._ChiTietDatPhongService
      .getPhieuById(this.idphieudat)
      .subscribe((result) => {
        this.phieuselected = result;
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
    debugger;
    this.phieudatdataupdate.ngayBatDau = this.infoBooking.ngayDat;
    this.phieudatdataupdate.ngayHenTra = this.infoBooking.ngayTra;
    // this.phieudatdataupdate.diemQuaTrinh = this.formSuaPhieuDat.value.diemquatrinh;

    this._ChiTietDatPhongService
      .updateTicket(this.phieudatdataupdate)
      .subscribe((result) => {});
  }
  // delete() {
  //   this._ChiTietDatPhongService(this.idphieudat).subscribe((result) => {});
  // }
}
