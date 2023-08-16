import { Component, Input, OnDestroy, OnInit, Output } from "@angular/core";
import { Router } from "@angular/router";
import { BookingInfoService } from "@app/service/booking-info-service.service";
import {
  DiaDiemServiceProxy,
  InfoBookingDto,
} from "@shared/service-proxies/service-proxies";

@Component({
  selector: "app-sliderdiadiem",
  templateUrl: "./sliderdiadiem.component.html",
  styleUrls: ["./sliderdiadiem.component.css"],
})
export class SliderdiadiemComponent implements OnInit, OnDestroy {
  currentIndex: number = 0;
  timeoutId?: number;
  @Input() slidesdiadiem = [];
  @Input() AnTrangChu = true;

  infoBooking: InfoBookingDto;

  constructor(private _diadiemService: DiaDiemServiceProxy) {}

  ngOnInit(): void {
    this.resetTimer();
    this.loadData();
  }

  ngOnDestroy() {
    this.clearTimer();
  }

  loadData(): void {
    this._diadiemService.getAllLocations().subscribe((result) => {
      this.slidesdiadiem = result;
    });
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

  adjustIndex(index: number): number {
    if (index < 0) {
      return this.slidesdiadiem.length - 1;
    } else if (index >= this.slidesdiadiem.length) {
      return 0;
    }
    return index;
  }

  goToPrevious(): void {
    const newIndex = this.adjustIndex(this.currentIndex - 1);
    this.currentIndex = newIndex;
    this.resetTimer();
  }

  goToNext(): void {
    const newIndex = this.adjustIndex(this.currentIndex + 1);
    this.currentIndex = newIndex;
    this.resetTimer();
  }

  getCurrentSlideUrl(index: number): string {
    const slideIndex = this.adjustIndex(index);
    return `url('/assets/img/DiaDiem/${this.slidesdiadiem[slideIndex]?.tenFileAnhDD}')`;
  }
}
