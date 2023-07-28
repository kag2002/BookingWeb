import { Component, Input, OnInit } from "@angular/core";
import { Router } from "@angular/router";
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

  constructor(
    private router: Router,
    private _hinhthucphongService: HinhThucPhongServiceProxy
  ) {}

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
    return `url('/assets/img/HinhThucPhong/${this.slidesloaichonghi[index]?.anhDaiDien}')`;
  }

  onSlideClick(index: number): void {
    // this.router.navigate(["/other", index]);
  }
}
