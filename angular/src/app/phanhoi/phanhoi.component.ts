import { Component } from "@angular/core";
import {
  DanhSachOutputDto,
  LienHeServiceProxy,
} from "@shared/service-proxies/service-proxies";

@Component({
  selector: "app-phanhoi",
  templateUrl: "./phanhoi.component.html",
  styleUrls: ["./phanhoi.component.css"],
})
export class PhanhoiComponent {
  comments: DanhSachOutputDto[];
  constructor(private _lienheService: LienHeServiceProxy) {}
  ngOnInit() {
    this._lienheService.getDanhSachLienHe().subscribe((result) => {
      this.comments = result;
    });
  }
}
