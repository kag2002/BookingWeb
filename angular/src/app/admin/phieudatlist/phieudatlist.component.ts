import { ChangeDetectorRef, Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import {
  ChiTietDatPhongDto,
  ChiTietDatPhongServiceProxy,
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
  showTongKet = false;

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
  }

  reload() {
    // this.ketquaservice.getAllKetQua().subscribe((result) => {
    //   this.listketqua = result;
    //   this.cd.detectChanges();
    // });
  }
  filterid() {
    // this.ketquaservice
    //   .getKetQuaStudent(this.formLocId.value.id)
    //   .subscribe((result) => {
    //     this.listketqua = result;
    //     console.log("finnish filter student id");
    //     this.cd.detectChanges();
    //   });
    // this.tongketservice
    //   .getTongKetByStudentId(this.formLocId.value.id)
    //   .subscribe((result) => {
    //     this.tongketselected = result;
    //     this.cd.detectChanges();
    //   });
    this.showTongKet = true;
  }
}
