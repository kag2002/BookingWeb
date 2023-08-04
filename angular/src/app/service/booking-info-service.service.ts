import { Injectable } from "@angular/core";
import { PhongSearchinhFilterDto } from "../../shared/service-proxies/service-proxies";

@Injectable({
  providedIn: "root",
})
export class BookingInfoService {
  private inforBookingDto: PhongSearchinhFilterDto[];

  setBookingInfo(info: PhongSearchinhFilterDto[]): void {
    this.inforBookingDto = info;
  }

  getBookingInfo() {
    return this.inforBookingDto;
  }
}
