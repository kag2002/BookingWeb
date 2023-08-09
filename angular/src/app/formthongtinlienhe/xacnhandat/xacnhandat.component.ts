import { DatePipe } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Params } from "@angular/router";
import { BookingInfoService } from "@app/service/booking-info-service.service";
import {
  ClientBookRoomInputDto,
  ClientBookRoomOutputDto,
  InfoBookingDto,
  PhongDto,
  PhongSearchinhFilterDto,
  PhongServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { error } from "console";
import { result } from "lodash-es";

@Component({
  selector: "app-thongtinlienhe",
  templateUrl: "./xacnhandat.component.html",
  styleUrls: ["./xacnhandat.component.css"],
})
export class XacnhandatComponent {
  FormThongTinLienHe: FormGroup;
  FormYeuCauDacBiet: FormGroup;

  selectedloaiphong: any;
  selectedphong: any;
  id: number;
  idloaiphong: number;
  yeucaus: any[] = [
    { name: "Phòng không hút thuốc", key: "NoSmoke" },
    { name: "Phòng liên thông", key: "Connect" },
    { name: "Tầng lầu", key: "TopFloor" },
    { name: "Khác", key: "More" },
  ];
  dathos: any[] = [
    { name: "Tôi là khách lưu trú", key: 1 },
    { name: "Tôi đặt cho người khác", key: 2 },
  ];

  clientBookRoomInputDto: ClientBookRoomInputDto = new ClientBookRoomInputDto();
  infoClient: ClientBookRoomOutputDto = new ClientBookRoomOutputDto();
  infoBooking: InfoBookingDto = new InfoBookingDto();
  constructor(
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private _phongService: PhongServiceProxy,
    private bookingInfo: BookingInfoService,
    private datePipe: DatePipe
  ) {}

  ngOnInit() {
    this.initAPI();
    this.initForm();
  }

  private initAPI() {
    this.route.params.subscribe((params: Params) => {
      this.id = +params["id"];
    });
    this.route.params.subscribe((params: Params) => {
      this.idloaiphong = +params["idloaiphong"];
    });

    this._phongService.getRoomById(this.id).subscribe((result) => {
      this.selectedphong = result;
    });
    this._phongService
      .getInfoRoomToBook(this.idloaiphong)
      .subscribe((result) => {
        this.selectedloaiphong = result;
      });
    this.bookingInfo.getClientInfo().subscribe(
      (result) => {
        this.infoClient = result;
      },
      (error) => {
        console.log(error);
      }
    );
    this.bookingInfo.getSearchBookingInfo().subscribe(
      (result) => {
        this.infoBooking = result;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  private initForm() {
    this.FormThongTinLienHe = this.formBuilder.group({
      name: ["", Validators.required],
      cccd: ["", Validators.required],
      phone: [, Validators.required],
      email: ["", Validators.required],
      datho: [, Validators.required],
    });
    this.FormYeuCauDacBiet = this.formBuilder.group({
      selectedCategory: ["", Validators.required],
    });
  }

  onSubmit() {
    console.log(this.selectedloaiphong.value);
  }

  addClientBookRoom() {}
}
