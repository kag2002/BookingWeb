import { Component, Input } from "@angular/core";
import { Router } from "@angular/router";
import {
  LoaiPhongServiceProxy,
  NhanXetDanhGiaServiceProxy,
  PhongServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { Subscription } from "rxjs";

@Component({
  selector: "app-sliderchonghinoibat",
  templateUrl: "./sliderchonghinoibat.component.html",
  styleUrls: ["./sliderchonghinoibat.component.css"],
})
export class SliderchonghinoibatComponent {
  @Input() donViId: number;
  slideschonghinoibat: any[] = [];
  slideschonghinoibat1: any[] = [];
  saoslideschonghinoibat: any[] = [];

  value: number = 4;
  currentIndex = 0;

  private phongSubscription: Subscription;
  private loaiphongSubscription: Subscription;
  private nhanxetdanhgiaSubscription: Subscription;

  constructor(
    private router: Router,
    private _phongService: PhongServiceProxy,
    private _loaiphongService: LoaiPhongServiceProxy
  ) {}

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
        }));
      });
    this.loaiphongSubscription = this._loaiphongService
      .getAllKindOfRoom()
      .subscribe((result) => {
        this.slideschonghinoibat1 = result.map((item) => ({
          giaPhongTheoDem: item?.giaPhongTheoDem,
        }));
      });
  }

  unsubscribeSubscriptions(): void {
    this.phongSubscription.unsubscribe();
    this.loaiphongSubscription.unsubscribe();
    this.nhanxetdanhgiaSubscription.unsubscribe();
  }

  getCurrentSlideUrl(index: number): string {
    return `url('/assets/img/img-chonghinoibat/${this.slideschonghinoibat[index]?.tenFileAnhDaiDien}')`;
  }

  onSlideClick(index: number): void {
    // this.router.navigate(["/other", index]);
  }
}
