import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { SlideDiaDiemInterface } from "../types/slide.interface";
import { Router } from "@angular/router";
import { DiaDiemServiceProxy } from "@shared/service-proxies/service-proxies";

@Component({
  selector: "app-sliderdiadiem",
  templateUrl: "./sliderdiadiem.component.html",
  styleUrls: ["./sliderdiadiem.component.css"],
})
export class SliderdiadiemComponent implements OnInit, OnDestroy {
  @Input() slides: SlideDiaDiemInterface[] = [];
  currentIndex: number = 0;
  timeoutId?: number;
  @Input() slidesdiadiem = [];
  constructor(
    private router: Router,
    private _diadiemService: DiaDiemServiceProxy
  ) {}
  ngOnInit(): void {
    this.resetTimer();
    this._diadiemService.getAllLocations().subscribe((result) => {
      this.slidesdiadiem = result.map((item) => {
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
    this.timeoutId = window.setTimeout(() => this.goToNext(), 5000);
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
        ? this.slidesdiadiem.length - 1
        : this.currentIndex - 1;

    this.currentIndex = newIndex;
    this.resetTimer();
  }

  goToNext(): void {
    const newIndex =
      this.currentIndex === this.slidesdiadiem.length - 1
        ? 0
        : this.currentIndex + 1;

    this.currentIndex = newIndex;
    this.resetTimer();
  }

  getCurrentSlideUrl(index: number): string {
    return `url('/assets/img/img-diadanh/${this.slidesdiadiem[index].tenFileAnhDD}')`;
  }

  onSlideClick(index: number): void {
    // this.router.navigate(["/other", index]);
  }
}
