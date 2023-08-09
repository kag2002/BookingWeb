import { Injectable } from "@angular/core";
import { PhongSearchinhFilterDto } from "../../shared/service-proxies/service-proxies";
import { BehaviorSubject, Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class BookingInfoService {
  private inforBookingDtoSubject = new BehaviorSubject<
    PhongSearchinhFilterDto[]
  >([]);

  setBookingInfo(info: PhongSearchinhFilterDto[]): void {
    this.inforBookingDtoSubject.next(info);
    // localStorage.setItem("bookingInfo", JSON.stringify(info));
  }

  getBookingInfo(): Observable<PhongSearchinhFilterDto[]> {
    return this.inforBookingDtoSubject.asObservable();
  }
}
