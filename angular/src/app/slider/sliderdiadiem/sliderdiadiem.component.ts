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
  @Input() slides2 = [];
  constructor(
    private router: Router,
    private _diadiemService: DiaDiemServiceProxy
  ) {}
  ngOnInit(): void {
    this.resetTimer();
    this._diadiemService.getAllLocations().subscribe((result) => {
      this.slides2 = result.map((item) => {
        return {
          tenFileAnhDD: item.tenFileAnhDD,
          tenDiaDiem: item.tenDiaDiem,
          thongTinViTri: item.thongTinViTri,
        }; // Map the result to an array of objects with TenFileAnhDD property
        // Map the result to an array of objects with TenFileAnhDD property
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
      this.currentIndex === 0 ? this.slides2.length - 1 : this.currentIndex - 1;

    this.currentIndex = newIndex;
    this.resetTimer();
  }

  goToNext(): void {
    const newIndex =
      this.currentIndex === this.slides2.length - 1 ? 0 : this.currentIndex + 1;

    this.currentIndex = newIndex;
    this.resetTimer();
  }

  getCurrentSlideUrl(index: number): string {
    return `url('/assets/img/img-diadanh/${this.slides2[index].tenFileAnhDD}')`;
  }

  onSlideClick(index: number): void {
    // this.router.navigate(["/other", index]);
  }
  GetDiaDiem() {}
}
