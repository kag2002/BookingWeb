import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { SlideLoaiChoNghiInterface } from "../types/slide.interface";
import { Router } from "@angular/router";

@Component({
  selector: "app-sliderloaichonghi",
  templateUrl: "./sliderloaichonghi.component.html",
  styleUrls: ["./sliderloaichonghi.component.css"],
})
export class SliderloaichonghiComponent implements OnInit, OnDestroy {
  @Input() slidesloaichonghi: SlideLoaiChoNghiInterface[] = [];
  currentIndex: number = 0;
  timeoutId?: number;
  constructor(private router: Router) {}
  ngOnInit(): void {
    this.resetTimer();
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
    return `url('${this.slidesloaichonghi[index].url}')`;
  }

  onSlideClick(index: number): void {
    // this.router.navigate(["/other", index]);
  }
}
