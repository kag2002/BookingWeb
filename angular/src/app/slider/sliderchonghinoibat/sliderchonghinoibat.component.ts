import { Component, Input } from "@angular/core";
import { Router } from "@angular/router";
import {
  HinhThucPhongServiceProxy,
  PhongServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { Subscription } from "rxjs";

@Component({
  selector: "app-sliderchonghinoibat",
  templateUrl: "./sliderchonghinoibat.component.html",
  styleUrls: ["./sliderchonghinoibat.component.css"],
})
export class SliderchonghinoibatComponent {
  @Input() slidesloaichonghi: any[] = [];
  @Input() slidesloaichonghiimage: any[] = [];
  currentIndex = 0;
  timeoutId?: number;
  private hinhThucPhongSubscription: Subscription;
  private phongSubscription: Subscription;

  constructor(
    private router: Router,
    private _hinhthucphongService: HinhThucPhongServiceProxy,
    private _phongService: PhongServiceProxy
  ) {}

  ngOnInit(): void {
    this.loadData();
    this.resetTimer();
  }

  ngOnDestroy(): void {
    this.clearTimer();
    this.unsubscribeSubscriptions();
  }

  loadData(): void {
    this.hinhThucPhongSubscription = this._hinhthucphongService
      .getAllList()
      .subscribe((result) => {
        this.slidesloaichonghi = result.map((item) => ({
          tenHinhThuc: item?.tenHinhThuc,
          tenDonVi: item?.tenDonVi,
        }));
      });

    this.phongSubscription = this._phongService
      .getAllRoom()
      .subscribe((result) => {
        this.slidesloaichonghiimage = result.map((item) => ({
          tenFileAnhDaiDien: item?.tenFileAnhDaiDien,
        }));
      });
  }

  resetTimer(): void {
    this.clearTimer();
    this.timeoutId = window.setTimeout(() => this.goToNext(), 3000);
  }

  clearTimer(): void {
    if (this.timeoutId) {
      window.clearTimeout(this.timeoutId);
      this.timeoutId = undefined;
    }
  }

  unsubscribeSubscriptions(): void {
    this.hinhThucPhongSubscription.unsubscribe();
    this.phongSubscription.unsubscribe();
  }

  goToPrevious(): void {
    this.currentIndex =
      this.currentIndex === 0
        ? this.slidesloaichonghi.length - 1
        : this.currentIndex - 1;
    this.resetTimer();
  }

  goToNext(): void {
    this.currentIndex =
      this.currentIndex === this.slidesloaichonghi.length - 1
        ? 0
        : this.currentIndex + 1;
    this.resetTimer();
  }

  getCurrentSlideUrl(index: number): string {
    return `url('/assets/img/img-chonghinoibat/${this.slidesloaichonghiimage[index]?.tenFileAnhDaiDien}')`;
  }

  onSlideClick(index: number): void {
    // this.router.navigate(["/other", index]);
  }
}
