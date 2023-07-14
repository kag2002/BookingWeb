import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import {
  HinhThucPhongServiceProxy,
  PhongServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { Subscription } from "rxjs";

@Component({
  selector: "app-sliderloaichonghi",
  templateUrl: "./sliderloaichonghi.component.html",
  styleUrls: ["./sliderloaichonghi.component.css"],
})
export class SliderloaichonghiComponent implements OnInit, OnDestroy {
  @Input() slidesloaichonghi: any[] = [];

  currentIndex = 0;
  timeoutId?: number;
  private hinhThucPhongSubscription: Subscription;
  private phongSubscription: Subscription;

  constructor(
    private router: Router,
    private _hinhthucphongService: HinhThucPhongServiceProxy
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
      .getAllForms()
      .subscribe((result) => {
        this.slidesloaichonghi = result.map((item) => ({
          tenHinhThuc: item?.tenHinhThuc,

          anhDaiDien: item?.anhDaiDien,
        }));
      });
  }

  resetTimer(): void {
    this.clearTimer();
    this.timeoutId = window.setTimeout(() => this.goToNext(), 12000);
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
    return `url('/assets/img/img-loaichonghi/${this.slidesloaichonghi[index]?.anhDaiDien}')`;
  }

  onSlideClick(index: number): void {
    // this.router.navigate(["/other", index]);
  }
}
