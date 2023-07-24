import { Component, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { FormControl } from "@angular/forms";
import { MessageService } from "primeng/api";
import {
  DiaDiemDto,
  DiaDiemFullDto,
  DiaDiemServiceProxy,
} from "@shared/service-proxies/service-proxies";

@Component({
  selector: "app-luutru",
  templateUrl: "./luutru.component.html",
  styleUrls: ["./luutru.component.css"],
  providers: [MessageService],
})
export class LuutruComponent {
  formData: FormGroup;

  diadiemDto: DiaDiemDto = new DiaDiemDto();
  suggestionsDiaDiem: DiaDiemFullDto[];
  selectedDiadiem: any;
  filteredDiadiems: any[];
  rangeDates: Date[];
  showForm = false;

  adults = 0;
  children = 0;
  rooms = 0;

  submitted = false;

  TimPhong = {
    selectedDiadiem: "",
    rangeDate: "",
    adult: 0,
    children: 0,
    room: 0,
  };

  constructor(
    private _diadiemService: DiaDiemServiceProxy,
    private messageService: MessageService,
    private formBuilder: FormBuilder
  ) {}

  overlayVisible: boolean = false;

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.formData = this.formBuilder.group({
      timkiemData: this.formBuilder.group({
        locations: [null, Validators.required],
        rangeDates: [null, Validators.required],
        adults: [1, Validators.min(1)],
        children: [0, Validators.min(0)],
        rooms: [1, Validators.min(1)],
      }),
      group1: this.formBuilder.control([]), // Use this.formBuilder.control instead of array notation
    });
  }

  searchDiaDiem(event) {
    const query = event.query;
    this._diadiemService.getAllLocations().subscribe((result) => {
      this.suggestionsDiaDiem = this.filterDiadiem(query, result);
    });
  }

  filterDiadiem(query, diaDiem: DiaDiemFullDto[]): any[] {
    const filter: any[] = [];
    for (const i of diaDiem) {
      if (i.tenDiaDiem.toLowerCase().indexOf(query.toLowerCase()) === 0) {
        filter.push(i);
      }
    }
    return filter;
  }

  incrementDecrement(field: string, value: number) {
    if (this.formData.get(`timkiemData.${field}`).value + value >= 0) {
      this.formData
        .get(`timkiemData.${field}`)
        .patchValue(this.formData.get(`timkiemData.${field}`).value + value);
    }
  }

  onSubmit() {
    this.submitted = true;

    if (this.formData.invalid) {
      return;
    }

    this.TimPhong.selectedDiadiem =
      this.formData.get("timkiemData.locations").value?.name || "";
    this.TimPhong.adult = this.formData.get("timkiemData.adults").value;
    this.TimPhong.children = this.formData.get("timkiemData.children").value;
    this.TimPhong.room = this.formData.get("timkiemData.rooms").value;

    if (this.rangeDates && this.rangeDates.length === 2) {
      const start = this.rangeDates[0];
      const end = this.rangeDates[1];

      const millisecondsPerDay = 24 * 60 * 60 * 1000; // Number of milliseconds in a day
      const numberOfDays =
        Math.floor((end.getTime() - start.getTime()) / millisecondsPerDay) + 1;

      this.TimPhong.rangeDate = numberOfDays.toString(); // Store the number of days as a string
    }
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
    const formData = {
      locations: this.formData.get("timkiemData.locations").value,
      rangeDates: this.formData.get("timkiemData.rangeDates").value,
      adults: this.formData.get("timkiemData.adults").value,
      children: this.formData.get("timkiemData.children").value,
      rooms: this.formData.get("timkiemData.rooms").value,
    };

    console.log(formData);
  }
}
