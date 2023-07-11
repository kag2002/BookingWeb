import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { DiaDiemServiceProxy } from "@shared/service-proxies/service-proxies";

@Component({
  selector: "app-sliderloaichonghi",
  templateUrl: "./sliderloaichonghi.component.html",
  styleUrls: ["./sliderloaichonghi.component.css"],
})
export class SliderloaichonghiComponent implements OnInit, OnDestroy {
  @Input() slidesloaichonghi = [];
  currentIndex: number = 0;
  timeoutId?: number;
  constructor(
    private router: Router,
    private _diadiemService: DiaDiemServiceProxy
  ) {}
  ngOnInit(): void {
    this.resetTimer();
    this._diadiemService.getAllLocations().subscribe((result) => {
      this.slidesloaichonghi = result.map((item) => {
        return {
          tenFileAnhDD: item.tenFileAnhDD,
          tenDiaDiem: item.tenDiaDiem,
          thongTinViTri: item.thongTinViTri,
        };
      });
    });
  }

  ngOnDestroy() {
    this.clearTimer();
  }

  resetTimer() {
    this.clearTimer();
    this.timeoutId = window.setTimeout(() => this.goToNext(), 3000);
  }

  clearTimer() {
    if (this.timeoutId) {
      window.clearTimeout(this.timeoutId);
      this.timeoutId = undefined;
    }
  }

  goToPrevious(): void {
    const newIndex =
      this.currentIndex === 0
        ? this.slidesloaichonghi.length - 1
        : this.currentIndex - 1;

    this.currentIndex = newIndex;
    this.resetTimer();
  }

  goToNext(): void {
    const newIndex =
      this.currentIndex === this.slidesloaichonghi.length - 1
        ? 0
        : this.currentIndex + 1;

    this.currentIndex = newIndex;
    this.resetTimer();
  }

  getCurrentSlideUrl(index: number): string {
    return `url('/assets/img/img-diadanh/${this.slidesloaichonghi[index].tenFileAnhDD}')`;
  }

  onSlideClick(index: number): void {
    // this.router.navigate(["/other", index]);
  }
}
