import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import {
  HinhThucPhongServiceProxy,
  PhongServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { Subscription } from "rxjs";

@Component({
  selector: "app-chonghinoibat",
  templateUrl: "./chonghinoibat.component.html",
  styleUrls: ["./chonghinoibat.component.css"],
})
export class ChonghinoibatComponent implements OnInit, OnDestroy {
  slides = [];
  private hinhThucPhongSubscription: Subscription;
  private phongSubscription: Subscription;
  @Input() slidesloaichonghi = [];
  @Input() slidesloaichonghiimage = [];

  currentIndex: number = 0;
  timeoutId?: number;
  constructor(
    private router: Router,
    private _hinhthucphongService: HinhThucPhongServiceProxy,
    private _phongService: PhongServiceProxy
  ) {}
  async ngOnInit(): Promise<void> {
    this.resetTimer();

    try {
      const hinhThucPhongResult = await this._hinhthucphongService
        .getAllList()
        .toPromise();
      this.slidesloaichonghi = hinhThucPhongResult.map((item) => ({
        tenHinhThuc: item?.tenHinhThuc,
        tenDonVi: item?.tenDonVi,
      }));

      const phongResult = await this._phongService.getAllRoom().toPromise();
      this.slidesloaichonghiimage = phongResult.map((item) => ({
        tenFileAnhDaiDien: item?.tenFileAnhDaiDien,
      }));
    } catch (error) {
      // Handle the error
    }
  }

  ngOnDestroy() {
    this.hinhThucPhongSubscription.unsubscribe();
    this.phongSubscription.unsubscribe();
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

  onSlideClick(index: number): void {
    // this.router.navigate(["/other", index]);
  }
}
