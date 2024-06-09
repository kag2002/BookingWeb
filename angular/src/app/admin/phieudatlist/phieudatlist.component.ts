import { ChangeDetectorRef, Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import {
  DatPhongServiceProxy,
  PhieuDatPhongOutputDto,
} from "@shared/service-proxies/service-proxies";

@Component({
  selector: "app-phieudatlist",
  templateUrl: "./phieudatlist.component.html",
  styleUrls: ["./phieudatlist.component.css"],
})
export class PhieudatlistComponent implements OnInit {
  listChiTietPhieuDat: PhieuDatPhongOutputDto[];
  formLocId: FormGroup;
  formLoccccd: FormGroup;

  constructor(
    private _ChiTietDatPhongService: DatPhongServiceProxy,
    private cd: ChangeDetectorRef,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this._ChiTietDatPhongService.getAllList().subscribe((result) => {
      this.listChiTietPhieuDat = result;
    });
    this.formLocId = this.fb.group({
      id: ["", Validators.required],
    });
    this.formLoccccd = this.fb.group({
      cccd: ["", Validators.required],
    });
  }

  reload() {
    this._ChiTietDatPhongService.getAllList().subscribe((result) => {
      this.listChiTietPhieuDat = result;
      this.cd.detectChanges();
    });
  }
  filterid() {
    this._ChiTietDatPhongService
      .getPhieuById(this.formLocId.value.id)
      .subscribe((result) => {
        this.listChiTietPhieuDat = result;
        console.log("loc thanh cong phieu id");
        this.cd.detectChanges();
      });
  }
  filtercccd() {
    this._ChiTietDatPhongService
      .getPhieuByCccd(this.formLoccccd.value.cccd)
      .subscribe((result) => {
        this.listChiTietPhieuDat = result;

        console.log("loc thanh cong cccd ");
        this.cd.detectChanges();
      });
  }
}
