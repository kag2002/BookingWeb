import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { SlideDiaDiemInterface } from "../types/slide.interface";
import { Router } from "@angular/router";

@Component({
  selector: "app-sliderdiadiem",
  templateUrl: "./sliderdiadiem.component.html",
  styleUrls: ["./sliderdiadiem.component.css"],
})
export class SliderdiadiemComponent implements OnInit, OnDestroy {
  @Input() slides: SlideDiaDiemInterface[] = [];
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
      this.currentIndex === 0 ? this.slides.length - 1 : this.currentIndex - 1;

    this.currentIndex = newIndex;
    this.resetTimer();
  }

  goToNext(): void {
    const newIndex =
      this.currentIndex === this.slides.length - 1 ? 0 : this.currentIndex + 1;

    this.currentIndex = newIndex;
    this.resetTimer();
  }

  getCurrentSlideUrl(index: number): string {
    return `url('${this.slides[index].url}')`;
  }

  onSlideClick(index: number): void {
    // this.router.navigate(["/other", index]);
  }
}
