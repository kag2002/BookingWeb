import { Component } from "@angular/core";
import { FormBuilder } from "@angular/forms";
import { ActivatedRoute, Params, Router } from "@angular/router";
import {
  DonViKinhDoanhServiceProxy,
  HinhAnhServiceProxy,
  PhongServiceProxy,
} from "@shared/service-proxies/service-proxies";

@Component({
  selector: "app-khachsan-detail",
  templateUrl: "./khachsan-detail.component.html",
  styleUrls: ["./khachsan-detail.component.css"],
})
export class KhachsanDetailComponent {
  listkhachsan: any;
  listhinhanh = [];
  listphongtrong = [];
  listdichvuphongtrong = [];

  currentIndex = 0;
  id: number;
  value: string;
  constructor(
    private route: ActivatedRoute,

    private _phongService: PhongServiceProxy,
    private _hinhanhService: HinhAnhServiceProxy // private _donvikinhdoanhService: DonViKinhDoanhServiceProxy
  ) {}
  ngOnInit() {
    //Gán id trên router cho biến id
    this.route.params.subscribe((params: Params) => {
      this.id = +params["id"];
    });
    // this._donvikinhdoanhService
    //   .getUnitByLocationId(this.id)
    //   .subscribe((result) => {
    //     this.listdiachichitiet = result.map((item) => ({
    //       diaChiChiTiet: item?.diaChiChiTiet,
    //     }));
    //   });
    this._phongService.getRoomById(this.id).subscribe((result) => {
      if (result) {
        this.listkhachsan = {
          tenFileAnhDaiDien: result?.tenFileAnhDaiDien,
          tenDonVi: result?.tenDonVi,
          hinhThucPhong: result?.hinhThucPhong,
          danhGiaSaoTb: result?.danhGiaSaoTb,
          diaChiChiTiet: result?.diaChiChiTiet,
          diemDanhGiaTB: result?.diemDanhGiaTB,
          listLoaiPhong: result?.listLoaiPhong,
          dichVuChung: result?.dichVuChung,
          luoDatPhong: result?.luoDatPhong,
          giaPhongTheoDem: result?.listLoaiPhong
            ? result.listLoaiPhong[0].giaPhongTheoDem
            : null,
        };

        // Once the data is fetched and available, extract listLoaiPhong

        if (this.listkhachsan.listLoaiPhong) {
          this.listphongtrong = this.listkhachsan.listLoaiPhong;

          // if (this.listphongtrong) {
          //   for (var i of this.listphongtrong) {
          //     this.listdichvuphongtrong.push(i);
          //   }
          // }
        }
        if (this.listkhachsan.dichVuChung) {
          this.listdichvuphongtrong = this.listkhachsan.dichVuChung;

          // if (this.listphongtrong) {
          //   for (var i of this.listphongtrong) {
          //     this.listdichvuphongtrong.push(i);
          //   }
          // }
        }
      } else {
        // Handle the case when the result is null or undefined
        console.error("Invalid result:", result);
      }
    });

    this._hinhanhService.getImageByRoom(this.id).subscribe((result) => {
      this.listhinhanh = result.map((item) => ({
        tenFileAnh: item?.tenFileAnh,
      }));
    });
  }
  splitChiTietIntoArray(chiTiet: string): string[] {
    return chiTiet.split("\n");
  }
  getCurrentSlideUrl(): string {
    return `url('/assets/img/DonViKinhDoanh/${this.listkhachsan?.tenFileAnhDaiDien}')`;
  }
  getCurrentSubSlideUrl(index: number): string {
    return `url('/assets/img/HinhAnh/${this.listhinhanh[index]?.tenFileAnh}')`;
  }

  onSlideClick(index: number): void {
    // this.router.navigate(["/other", index]);
  }
}
