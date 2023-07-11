import { Component, ViewChild } from "@angular/core";
import { LuutruService } from "../luutru/luutru.service";
import { NgForm } from "@angular/forms";

import { MessageService } from "primeng/api";
import {
  DiaDiemDto,
  DiaDiemServiceProxy,
} from "@shared/service-proxies/service-proxies";

interface AutoCompleteCompleteEvent {
  originalEvent: Event;
  query: string;
}

@Component({
  selector: "app-luutru",
  templateUrl: "./luutru.component.html",
  styleUrls: ["./luutru.component.css"],
  providers: [MessageService],
})
export class LuutruComponent {
  @ViewChild("f") signupForm: NgForm;

  diadiemDto: DiaDiemDto = new DiaDiemDto();
  diadiems: any[];
  selectedDiadiem: any;
  filteredDiadiems: any[];
  rangeDates: Date[];
  showForm = false;
  adults = 0;
  children = 0;
  rooms = 0;
  selectedCities: string[] = [];
  submitted = false;
  slidesdiadiem = [];

  slidesloaichonghi: [] = [];

  TimPhong = {
    selectedDiadiem: "",
    rangeDate: "",
    adult: 0,
    children: 0,
    room: 0,
  };

  constructor(
    private luutruService: LuutruService,
    private messageService: MessageService,
    private _diadiemService: DiaDiemServiceProxy
  ) {}
  overlayVisible: boolean = false;

  ngOnInit() {
    this.luutruService.getDiadiems().then((diadiems) => {
      this.diadiems = diadiems;
    });
  }

  filterDiadiem(event: AutoCompleteCompleteEvent) {
    const query = event.query.toLowerCase();
    this.filteredDiadiems = this.diadiems.filter((diadiem) =>
      diadiem.name.toLowerCase().startsWith(query)
    );
  }

  incrementDecrement(field: string, value: number) {
    if (this[field] + value >= 0) {
      this[field] += value;
    }
  }
  onSubmit() {
    this.submitted = true;
    this.TimPhong.selectedDiadiem = this.selectedDiadiem?.name || "";
    this.TimPhong.adult = this.adults;
    this.TimPhong.children = this.children;
    this.TimPhong.room = this.rooms;

    if (this.rangeDates && this.rangeDates.length === 2) {
      const start = this.rangeDates[0];
      const end = this.rangeDates[1];

      const millisecondsPerDay = 24 * 60 * 60 * 1000; // Number of milliseconds in a day
      const numberOfDays =
        Math.floor((end.getTime() - start.getTime()) / millisecondsPerDay) + 1;

      this.TimPhong.rangeDate = numberOfDays.toString(); // Store the number of days as a string
    }

    const formData = { ...this.TimPhong }; // Copy form data to a separate variable

    console.log(formData);
  }

  toggleForm() {
    this.overlayVisible = !this.overlayVisible;
    this.showForm = !this.showForm;
  }

  show() {
    this.messageService.add({
      severity: "success",
      summary: "Success",
      detail: "Tìm thành công",
    });
  }

  // AddDiaDiem() {
  //   this.diadiemDto.tenDiaDiem = "dia diem 3";
  //   this.diadiemDto.thongTinViTri = "hanoi 3";
  //   this.diadiemDto.tenFileAnhDD =
  //     "/assets/img/img-diadanh/image-diadanh-1.jpg";
  //   this._diadiemService
  //     .addNewLocation(this.diadiemDto)
  //     .subscribe((result) => {});
  // }
  // DeleteDiaDiem(id: number) {
  //   this._diadiemService.deleteLocation(id).subscribe((result) => {});
  // }

  // GetDiaDiem() {
  //   this._diadiemService.getAllLocations().subscribe((result) => {
  //     this.slidesdiadiem = result.map((item) => {
  //       return { tenFileAnhDD: item.tenFileAnhDD }; // Map the result to an array of objects with TenFileAnhDD property
  //     });
  //   });
  // }
}
