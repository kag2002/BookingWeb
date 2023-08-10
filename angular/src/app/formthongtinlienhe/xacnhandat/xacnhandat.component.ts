import { Component } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Params } from "@angular/router";
import { BookingInfoService } from "@app/service/booking-info-service.service";
import {
  ChinhSachChungOutoutDto,
  ChinhSachChungServiceProxy,
  ClientBookRoomOutputDto,
  ConfirmDto,
  GetInfoRoomToBookOutputDto,
  InfoBookingDto,
  PhongServiceProxy,
} from "@shared/service-proxies/service-proxies";
@Component({
  selector: "app-thongtinlienhe",
  templateUrl: "./xacnhandat.component.html",
  styleUrls: ["./xacnhandat.component.css"],
})
export class XacnhandatComponent {
  FormXacNhan: FormGroup;
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

  chinhsachchung: ChinhSachChungOutoutDto = new ChinhSachChungOutoutDto();

  infoRoom: GetInfoRoomToBookOutputDto = new GetInfoRoomToBookOutputDto();
  infoClient: ClientBookRoomOutputDto = new ClientBookRoomOutputDto();
  infoBooking: InfoBookingDto = new InfoBookingDto();

  confirmBook: ConfirmDto = new ConfirmDto();

  constructor(
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private _phongService: PhongServiceProxy,
    private bookingInfo: BookingInfoService,
    private _chinhSachChung: ChinhSachChungServiceProxy
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

    this._phongService.getRoomById(this.id).subscribe(
      (result) => {
        this.selectedphong = result;
        this._chinhSachChung
          .getPolicyByDVKDId(this.selectedloaiphong.donViKinhDoanhId)
          .subscribe(
            (result) => {
              this.chinhsachchung = result;
            },
            (error) => {
              console.log(error);
            }
          );
      },
      (error) => {
        console.log(error);
      }
    );
    this._phongService
      .getInfoRoomToBook(this.idloaiphong)
      .subscribe((result) => {
        this.selectedloaiphong = result;
        this.infoRoom = result;
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
    this.FormXacNhan = this.formBuilder.group({
      name: ["", Validators.required],
      cccd: [
        "",
        Validators.required,
        [Validators.required, Validators.pattern(/^\d{12}$/)],
      ], // Remove Validators.required
      phone: ["", [Validators.required, Validators.pattern(/^\d{10}$/)]],
      email: [
        "",
        [Validators.required, Validators.maxLength(50), Validators.email],
      ],
      datho: [null, Validators.required],
    });
    this.FormYeuCauDacBiet = this.formBuilder.group({
      selectedCategory: ["", Validators.required],
    });
  }

  // calculateNumberOfNights(start: moment.Moment, end: moment.Moment): number {
  //   const duration = moment.duration(end.diff(start));
  //   return duration.asDays();
  // }

  calculateNumberOfNights(start: Date | null, end: Date | null): number {
    if (!start || !end) {
      return 0; // Trả về 0 nếu một trong hai ngày không có giá trị
    }

    const oneDay = 24 * 60 * 60 * 1000; // Number of milliseconds in a day
    const startTime = start.getTime();
    const endTime = end.getTime();
    const diffTime = Math.abs(endTime - startTime);
    const numberOfNights = Math.ceil(diffTime / oneDay);
    return numberOfNights;
  }

  addClientBookRoom() {
    debugger;
    this.confirmBook.hoTen = this.infoClient.hoTen;
    this.confirmBook.cccd = this.infoClient.cccd;
    this.confirmBook.sdt = this.infoClient.sdt;
    this.confirmBook.email = this.infoClient.email;
    this.confirmBook.datHo = this.infoClient.datHo;
    this.confirmBook.yeuCauDacBiet = this.infoClient.yeuCauDacBiet;
    this.confirmBook.ngayDat = this.infoBooking.ngayDat;
    this.confirmBook.ngayTra = this.infoBooking.ngayTra;
    this.confirmBook.slNguoiLon = this.infoBooking.slNguoiLon;
    this.confirmBook.slTreEm = this.infoBooking.slTreEm;
    this.confirmBook.slNguoiLon = this.infoBooking.slNguoiLon;
    this.confirmBook.slPhong = this.infoBooking.slPhong;
    this.confirmBook.tongTien =
      (this.infoRoom.giaPhongTheoDem + this.infoRoom.giaDichVuThem) *
      (1 - this.infoRoom.giamGia - this.infoRoom.uuDaiDacBiet);
    this.confirmBook.phongId = this.infoRoom.phongId;

    this._phongService.confirmBookRoom(this.confirmBook).subscribe(
      (result) => {
        console.log(result);
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
