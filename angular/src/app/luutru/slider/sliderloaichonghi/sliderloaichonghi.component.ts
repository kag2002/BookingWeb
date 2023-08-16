import { Component, Input, OnInit } from "@angular/core";
import { HinhThucPhongServiceProxy } from "@shared/service-proxies/service-proxies";

@Component({
  selector: "app-sliderloaichonghi",
  templateUrl: "./sliderloaichonghi.component.html",
  styleUrls: ["./sliderloaichonghi.component.css"],
})
export class SliderloaichonghiComponent implements OnInit {
  @Input() slidesloaichonghi: any[] = [];

  currentIndex = 0;
  timeoutId?: number;

  constructor(private _hinhthucphongService: HinhThucPhongServiceProxy) {}

  ngOnInit(): void {
    this.loadData();
    this.resetTimer();
  }

  loadData(): void {
    this._hinhthucphongService.getAllForms().subscribe((result) => {
      this.slidesloaichonghi = result;
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
    const slideIndex = this.adjustIndex(index);
    return `url('/assets/img/HinhThucPhong/${this.slidesloaichonghi[slideIndex]?.anhDaiDien}')`;
  }

  adjustIndex(index: number): number {
    if (index < 0) {
      return this.slidesloaichonghi.length - 1;
    } else if (index >= this.slidesloaichonghi.length) {
      return 0;
    }
    return index;
  }
}
