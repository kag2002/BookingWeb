import { Component, Input } from "@angular/core";
import { Router } from "@angular/router";
import { PhongServiceProxy } from "@shared/service-proxies/service-proxies";
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

  private phongSubscription: Subscription;

  constructor(
    private router: Router,
    private _phongService: PhongServiceProxy
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  ngOnDestroy(): void {
    this.unsubscribeSubscriptions();
  }

  loadData(): void {
    this.phongSubscription = this._phongService
      .getRoomsByDiaDiemId(this.donViId)
      .subscribe((result) => {
        this.slideschonghinoibat = result.map((item) => ({
          tenFileAnhDaiDien: item?.tenFileAnhDaiDien,
          hinhThucPhong: item?.hinhThucPhong,
          tenDonVi: item?.tenDonVi,
          diaDiem: item?.diaDiem,
        }));
      });
  }

  unsubscribeSubscriptions(): void {
    this.phongSubscription.unsubscribe();
  }

  getCurrentSlideUrl(index: number): string {
    return `url('/assets/img/img-chonghinoibat/${this.slideschonghinoibat[index]?.tenFileAnhDaiDien}')`;
  }

  onSlideClick(index: number): void {
    // this.router.navigate(["/other", index]);
  }
}
