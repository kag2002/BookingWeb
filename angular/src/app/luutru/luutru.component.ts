import { Component, Input, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MessageService } from "primeng/api";
import {
  DiaDiemDto,
  DiaDiemFullDto,
  DiaDiemServiceProxy,
  InfoBookingDto,
  PhongSearchinhFilterDto,
  SearchingFilterServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { BookingInfoService } from "../service/booking-info-service.service";
import { now } from "moment";
@Component({
  selector: "app-luutru",
  templateUrl: "./luutru.component.html",
  styleUrls: ["./luutru.component.css"],
  providers: [MessageService],
})
export class LuutruComponent {
  @Input() HienTrangChu = true;
  formTimPhong: FormGroup;
  basicData: any;

  basicOptions: any;
  diadiemDto: DiaDiemDto = new DiaDiemDto();
  suggestionsDiaDiem: DiaDiemFullDto[];

  inforBookingDto: InfoBookingDto = new InfoBookingDto();

  selectedDiadiem: any;
  filteredDiadiems: any[];
  rangeDates: Date[];
  showTimKiemGanDay = false;

  adults = 0;
  children = 0;
  rooms = 0;
  iddiadiem = 0;

  phongSearchinhFilterDto: PhongSearchinhFilterDto[];
  constructor(
    private _diadiemService: DiaDiemServiceProxy,
    private messageService: MessageService,
    private formBuilder: FormBuilder,
    private _searchingFilterService: SearchingFilterServiceProxy,
    private bookingInfoService: BookingInfoService
  ) {}

  overlayVisible: boolean = false;

  ngOnInit() {
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue("--text-color");
    const textColorSecondary = documentStyle.getPropertyValue(
      "--text-color-secondary"
    );
    const surfaceBorder = documentStyle.getPropertyValue("--surface-border");

    this.basicData = {
      labels: ["Q1", "Q2", "Q3", "Q4"],
      datasets: [
        {
          label: "Sales",
          data: [540, 325, 702, 620],
          backgroundColor: [
            "rgba(255, 159, 64, 0.2)",
            "rgba(75, 192, 192, 0.2)",
            "rgba(54, 162, 235, 0.2)",
            "rgba(153, 102, 255, 0.2)",
          ],
          borderColor: [
            "rgb(255, 159, 64)",
            "rgb(75, 192, 192)",
            "rgb(54, 162, 235)",
            "rgb(153, 102, 255)",
          ],
          borderWidth: 1,
        },
      ],
    };

    this.basicOptions = {
      plugins: {
        legend: {
          labels: {
            color: textColor,
          },
        },
      },
      scales: {
        y: {
          beginAtZero: true,
          ticks: {
            color: textColorSecondary,
          },
          grid: {
            color: surfaceBorder,
            drawBorder: false,
          },
        },
        x: {
          ticks: {
            color: textColorSecondary,
          },
          grid: {
            color: surfaceBorder,
            drawBorder: false,
          },
        },
      },
    };
    // this.activatedRoute.params.subscribe((params) => {
    //   this.HienTrangChu = true;
    // });
    this.createForm();
  }

  createForm() {
    this.formTimPhong = this.formBuilder.group({
      timkiemData: this.formBuilder.group({
        locations: [null, Validators.required],
        rangeDates: [now, Validators.required],
        adults: [2, Validators.min(1)],
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
    if (this.formTimPhong.get(`timkiemData.${field}`).value + value >= 0) {
      this.formTimPhong
        .get(`timkiemData.${field}`)
        .patchValue(
          this.formTimPhong.get(`timkiemData.${field}`).value + value
        );
    }
  }

  onSubmit() {
    this.inforBookingDto.diaDiemid =
      this.formTimPhong.value.timkiemData.locations?.id;

    this.iddiadiem = this.formTimPhong.value.timkiemData.locations?.id;

    this.inforBookingDto.ngayDat =
      this.formTimPhong.value.timkiemData?.rangeDates[0];

    this.inforBookingDto.ngayTra =
      this.formTimPhong.value.timkiemData?.rangeDates[1];

    this.inforBookingDto.slNguoiLon =
      this.formTimPhong.value.timkiemData?.adults;
    this.inforBookingDto.slTreEm =
      this.formTimPhong.value.timkiemData?.children;
    this.inforBookingDto.slPhong = this.formTimPhong.value.timkiemData?.rooms;

    this._searchingFilterService
      .searchingRoom(this.inforBookingDto)
      .subscribe((result) => {
        this.phongSearchinhFilterDto = result;
        this.bookingInfoService.setBookingInfo(this.phongSearchinhFilterDto);

        console.log("Gui du lieu 1:", this.phongSearchinhFilterDto);
      });

    this.bookingInfoService.setSearchBookingInfo(this.inforBookingDto);
    console.log("Gui du lieu 2:", this.inforBookingDto);

    this.showTimKiemGanDay = true;
  }
  AnTrangChuOnSubmit() {
    this.HienTrangChu = false;
  }

  toggleForm() {
    this.overlayVisible = !this.overlayVisible;
  }

  show() {
    this.messageService.add({
      severity: "success",
      summary: "Success",
      detail: "Tìm thành công",
    });
  }

  searchHotels() {}
}
