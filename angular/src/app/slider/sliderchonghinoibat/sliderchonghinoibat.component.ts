import { Component, Input } from "@angular/core";
import { Router } from "@angular/router";
import { PhongServiceProxy } from "@shared/service-proxies/service-proxies";
import { Subscription } from "rxjs";

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

  private phongSubscription: Subscription;

  constructor(private _phongService: PhongServiceProxy) {}

  ngOnInit(): void {
    this.loadData();
  }

  ngOnDestroy(): void {
    this.unsubscribeSubscriptions();
  }

  loadData(): void {
    this.phongSubscription = this._phongService
      .getRoomsByDiaDiemId(this.donViId)
      .subscribe((result) => {
        this.slideschonghinoibat = result.map((item) => ({
          tenFileAnhDaiDien: item?.tenFileAnhDaiDien,
          hinhThucPhong: item?.hinhThucPhong,
          tenDonVi: item?.tenDonVi,
          diaDiem: item?.diaDiem,
          danhGiaSaoTb: item?.danhGiaSaoTb,
          giaPhongTheoDem: item?.listLoaiPhong[0].giaPhongTheoDem,
        }));
      });
  }

  unsubscribeSubscriptions(): void {
    this.phongSubscription.unsubscribe();
  }

  getCurrentSlideUrl(index: number): string {
    return `url('/assets/img/DonViKinhDoanh/${this.slideschonghinoibat[index]?.tenFileAnhDaiDien}')`;
  }

  onSlideClick(index: number): void {
    // this.router.navigate(["/other", index]);
  }
}
