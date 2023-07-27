import { Component, Input } from "@angular/core";
import { Router } from "@angular/router";
import { PhongServiceProxy } from "@shared/service-proxies/service-proxies";

@Component({
  selector: "app-sliderchonghinoibat",
  templateUrl: "./sliderchonghinoibat.component.html",
  styleUrls: ["./sliderchonghinoibat.component.css"],
})
export class SliderchonghinoibatComponent {
  @Input() donViId: number;
  slideschonghinoibat: any[] = [];

  value: number = 4;

  currentIndex = 0;

  constructor(private _phongService: PhongServiceProxy) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this._phongService.getRoomsByDiaDiemId(this.donViId).subscribe((result) => {
      this.slideschonghinoibat = result.map((item) => ({
        tenFileAnhDaiDien: item?.tenFileAnhDaiDien,
        hinhThucPhong: item?.hinhThucPhong,
        tenDonVi: item?.tenDonVi,
        tenDiaDiem: item?.tenDiaDiem,
        danhGiaSaoTb: item?.danhGiaSaoTb,
        giaPhongTheoDem: item?.listLoaiPhong[0].giaPhongTheoDem,
      }));
    });
  }

  getCurrentSlideUrl(index: number): string {
    return `url('/assets/img/DonViKinhDoanh/${this.slideschonghinoibat[index]?.tenFileAnhDaiDien}')`;
  }
}
