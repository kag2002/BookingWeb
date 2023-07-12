import { Component, Input } from "@angular/core";
import { Router } from "@angular/router";
import { SearchingFilterServiceProxy } from "@shared/service-proxies/service-proxies";
import { Subscription } from "rxjs";

@Component({
  selector: "app-sliderchonghinoibat",
  templateUrl: "./sliderchonghinoibat.component.html",
  styleUrls: ["./sliderchonghinoibat.component.css"],
})
export class SliderchonghinoibatComponent {
  @Input() slideschonghinoibat: any[] = [];
  @Input() donViId: number;
  currentIndex = 0;
  timeoutId?: number;

  private searchingfilterSubscription: Subscription;

  constructor(
    private router: Router,

    private _searchingfilterService: SearchingFilterServiceProxy
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
    this.searchingfilterSubscription = this._searchingfilterService
      .getRoomsByLocation(this.donViId)
      .subscribe((result) => {
        this.slideschonghinoibat = result.map((item) => ({
          tenFileAnhDaiDien: item?.tenFileAnhDaiDien,
          tenDonVi: item?.tenDonVi,
        }));
      });
  }

  resetTimer(): void {
    this.clearTimer();
    this.timeoutId = window.setTimeout(() => this.goToNext(), 3000);
  }

  clearTimer(): void {
    if (this.timeoutId) {
      window.clearTimeout(this.timeoutId);
      this.timeoutId = undefined;
    }
  }

  unsubscribeSubscriptions(): void {
    this.searchingfilterSubscription.unsubscribe();
  }

  goToPrevious(): void {
    this.currentIndex =
      this.currentIndex === 0
        ? this.slideschonghinoibat.length - 1
        : this.currentIndex - 1;
    this.resetTimer();
  }

  goToNext(): void {
    this.currentIndex =
      this.currentIndex === this.slideschonghinoibat.length - 1
        ? 0
        : this.currentIndex + 1;
    this.resetTimer();
  }

  getCurrentSlideUrl(index: number): string {
    console.log(this.slideschonghinoibat[index]?.tenFileAnhDaiDien);
    return `url('/assets/img/img-chonghinoibat/danang/${this.slideschonghinoibat[index]?.tenFileAnhDaiDien}')`;
  }
  // getCurrentSlideUrlHaNoi(index: number): string {
  //   return `url('/assets/img/img-chonghinoibat/hanoi/${this.slideschonghinoibatimage[index]?.tenFileAnhDaiDien}')`;
  // }
  // getCurrentSlideUrlHoChiMinh(index: number): string {
  //   return `url('/assets/img/img-chonghinoibat/hochiminh/${this.slideschonghinoibatimage[index]?.tenFileAnhDaiDien}')`;
  // }
  // getCurrentSlideUrlNhaTrang(index: number): string {
  //   return `url('/assets/img/img-chonghinoibat/nhatrang/${this.slideschonghinoibatimage[index]?.tenFileAnhDaiDien}')`;
  // }
  // getCurrentSlideUrlHaiPhong(index: number): string {
  //   return `url('/assets/img/img-chonghinoibat/haiphong/${this.slideschonghinoibatimage[index]?.tenFileAnhDaiDien}')`;
  // }

  onSlideClick(index: number): void {
    // this.router.navigate(["/other", index]);
  }
}
