import { Component } from "@angular/core";
import { FormBuilder } from "@angular/forms";
import { ActivatedRoute, Params } from "@angular/router";
import { PhongServiceProxy } from "@shared/service-proxies/service-proxies";

@Component({
  selector: "app-khachsan-detail",
  templateUrl: "./khachsan-detail.component.html",
  styleUrls: ["./khachsan-detail.component.css"],
})
export class KhachsanDetailComponent {
  listkhachsan = [];
  listkhachsanfiltered = [];
  currentIndex = 0;
  id: number;
  constructor(
    private route: ActivatedRoute,
    private _phongService: PhongServiceProxy
  ) {}
  ngOnInit() {
    this._phongService.getAllRoom().subscribe((result) => {
      this.listkhachsan = result.map((item) => ({
        tenFileAnhDaiDien: item?.tenFileAnhDaiDien,
        tenDonVi: item?.tenDonVi,
        hinhThucPhong: item?.hinhThucPhong,
        danhGiaSaoTb: item?.danhGiaSaoTb,
        diaDiem: item?.diaDiem,
        diemDanhGiaTB: item?.diemDanhGiaTB,
        ListLoaiPhong: item?.listLoaiPhong,
        giaPhongTheoDem: item?.listLoaiPhong[0].giaPhongTheoDem,
      }));

      //Gán id trên router cho biến id
      this.route.params.subscribe((params: Params) => {
        this.id = +params["id"];
      });
    });
  }

  getCurrentSlideUrl(index: number): string {
    return `url('/assets/img/DonViKinhDoanh/${this.listkhachsan[index]?.tenFileAnhDaiDien}')`;
  }
  getCurrentSubSlideUrl(index: number): string {
    return `url('/assets/img/DonViKinhDoanh/${this.listkhachsan[index]?.tenFileAnhDaiDien}')`;
  }
  onSlideClick(index: number): void {
    // this.router.navigate(["/other", index]);
  }
}
