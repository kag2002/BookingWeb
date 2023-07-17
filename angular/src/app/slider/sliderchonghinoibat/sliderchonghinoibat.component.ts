import { Component, Input } from "@angular/core";
import { Router } from "@angular/router";
import {
  LoaiPhongServiceProxy,
  SearchingFilterServiceProxy,
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

  private searchingfilterSubscription: Subscription;
  private loaisearchingfilterSubscription: Subscription;
  private nhanxetdanhgiaSubscription: Subscription;

  constructor(
    private router: Router,
    private _searchingfilterService: SearchingFilterServiceProxy,
    private _loaiphongService: LoaiPhongServiceProxy
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  ngOnDestroy(): void {
    this.unsubscribeSubscriptions();
  }

  loadData(): void {
    this.searchingfilterSubscription = this._searchingfilterService
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
    // this.loaisearchingfilterSubscription = this._loaiphongService
    //   .getAllKindOfRoom()
    //   .subscribe((result) => {
    //     this.slideschonghinoibat1 = result.map((item) => ({
    //       giaPhongTheoDem: item?.giaPhongTheoDem[1],
    //     }));
    //   });
  }

  unsubscribeSubscriptions(): void {
    this.searchingfilterSubscription.unsubscribe();
    this.loaisearchingfilterSubscription.unsubscribe();
    this.nhanxetdanhgiaSubscription.unsubscribe();
  }

  getCurrentSlideUrl(index: number): string {
    return `url('/assets/img/DonViKinhDoanh/${this.slideschonghinoibat[index]?.tenFileAnhDaiDien}')`;
  }

  onSlideClick(index: number): void {
    // this.router.navigate(["/other", index]);
  }
}
