import { Component } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { ActivatedRoute, Params } from "@angular/router";
import { BookingInfoService } from "@app/service/booking-info-service.service";
import {
  ChinhSachChungOutputDto,
  ChinhSachChungServiceProxy,
  ClientBookRoomOutputDto,
  ConfirmBookRoomResultDto,
  ConfirmDto,
  GetInfoRoomToBookOutputDto,
  InfoBookingDto,
  PhongServiceProxy,
} from "@shared/service-proxies/service-proxies";
import * as moment from "moment";
import { MessageService } from "primeng/api";
import pdfMake from "pdfmake/build/pdfmake";
import pdfFonts from "pdfmake/build/vfs_fonts";

pdfMake.vfs = pdfFonts.pdfMake.vfs;

@Component({
  selector: "app-thongtinlienhe",
  templateUrl: "./xacnhandat.component.html",
  styleUrls: ["./xacnhandat.component.css"],
})
export class XacnhandatComponent {
  formLich: FormGroup;
  FormYeuCauDacBiet: FormGroup;

  selectedloaiphong: any;
  selectedphong: any;
  id: number;
  idloaiphong: number;

  confirmBookRoomResultDto: ConfirmBookRoomResultDto;

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

  chinhsachchung: ChinhSachChungOutputDto = new ChinhSachChungOutputDto();

  infoRoom: GetInfoRoomToBookOutputDto | undefined;
  infoClient: ClientBookRoomOutputDto | undefined;
  infoBooking: InfoBookingDto | undefined;

  confirmBook: ConfirmDto = new ConfirmDto();

  invoiceHtml: string = "";
  showInvoice = false;
  showPreviewBtn = false;

  constructor(
    private route: ActivatedRoute,
    private messageService: MessageService,
    private _phongService: PhongServiceProxy,
    private bookingInfo: BookingInfoService,
    private _chinhSachChung: ChinhSachChungServiceProxy
  ) {}

  ngOnInit() {
    this.infoBooking = new InfoBookingDto();
    this.infoClient = new ClientBookRoomOutputDto();
    this.infoRoom = new GetInfoRoomToBookOutputDto();

    this.initAPI();
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
        this.infoBooking.ngayDat = moment(this.infoBooking.ngayDat); // Chuyển sang đối tượng moment.Moment
        this.infoBooking.ngayTra = moment(this.infoBooking.ngayTra);
        console.log("lay dl2", this.infoBooking);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  calculateNumberOfNights(start: moment.Moment, end: moment.Moment): number {
    if (start && end) {
      const duration = moment.duration(end.diff(start));
      return duration.asDays();
    }
    return 0;
  }

  addClientBookRoom() {
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
      (1 - this.infoRoom.giamGia - this.infoRoom.uuDaiDacBiet) *
      this.calculateNumberOfNights(
        this.infoBooking.ngayDat,
        this.infoBooking.ngayTra
      ) *
      this.infoBooking.slPhong;
    this.confirmBook.phongId = this.infoRoom.phongId;
    this.confirmBook.loaiPhongId = this.idloaiphong;

    this._phongService.confirmBookRoom(this.confirmBook).subscribe(
      (result) => {
        this.confirmBookRoomResultDto = result;
        this.messageService.add({
          severity: "success",
          summary: "Success",
          detail: "Đặt thành công",
        });
        this.generateInvoiceHtml();
        this.showPreviewBtn = true;
      },
      (error) => {
        console.log(error);
        this.messageService.add({
          severity: "error",
          summary: "Error",
          detail: "Đặt không thành công vui lòng kiểm tra lại",
        });
      }
    );
  }

  generateInvoiceHtml() {
    this.invoiceHtml = `
     

     
     
   
    <div style="display: flex; justify-content: center">
      <div id="invoice" style="margin: 50px 20px">
        <img
          style="width: 166px"
          src="../../../assets/img/Logo/LogoWeb/LogoWebsite.png"
          alt=""
        />
        <br />
        <b><h4>Thông tin khách</h4> </b>

        <hr />
        <div class="row">
          <b class="col-lg-4">Tên khách hàng</b>
          <div class="col-lg-8">${this.infoClient.hoTen}</div>
        </div>
        <hr />
        <div class="row">
          <b class="col-lg-4">CCCD</b>
          <div class="col-lg-8">${this.infoClient.cccd}</div>
        </div>
        <hr />
        <div class="row">
          <b class="col-lg-4">Email</b>
          <div class="col-lg-8">${this.infoClient.email}</div>
        </div>
        <hr />
        <div class="row">
          <b class="col-lg-4">SĐT</b>
          <div class="col-lg-8">${this.infoClient.sdt}</div>
        </div>
        <hr />

        <br /><br />
        <b><h4>Thông tin đặt</h4></b>
        <hr />

        <div class="row">
          <b class="col-lg-4">Ngày đặt</b>
          <div class="col-lg-8"> ${this.infoBooking.ngayDat.format(
            "DD-MM-YYYY"
          )}</div>
        </div>
        <hr />
        <div class="row">
          <b class="col-lg-4">Ngày trả</b>
          <div class="col-lg-8">${this.infoBooking.ngayTra.format(
            "DD-MM-YYYY"
          )}</div>
        </div>
        <hr />
        <div class="row">
          <b class="col-lg-4">Người lớn</b>
          <div class="col-lg-8">${this.infoBooking.slNguoiLon}</div>
        </div>
        <hr />
        <div class="row">
          <b class="col-lg-4">Trẻ em</b>
          <div class="col-lg-8">${this.infoBooking.slTreEm}</div>
        </div>
        <hr />
        <div class="row">
          <b class="col-lg-4">Số phòng</b>
          <div class="col-lg-8">${this.infoBooking.slPhong}</div>
        </div>
        
        <hr />
        <div class="row">
          <b class="col-lg-4"><h5>Tổng tiền</h5></b>
          <div class="col-lg-8">${
            (this.infoRoom.giaPhongTheoDem + this.infoRoom.giaDichVuThem) *
            (1 - this.infoRoom.giamGia - this.infoRoom.uuDaiDacBiet) *
            this.calculateNumberOfNights(
              this.infoBooking.ngayDat,
              this.infoBooking.ngayTra
            ) *
            this.infoBooking.slPhong
          }</div>
        </div>
        <hr/>
        <div class="row">
          <b class="col-lg-4">Địa chỉ</b>
          <div class="col-lg-8">${
            this.confirmBookRoomResultDto.diaChiChiTiet
          }</div>
        </div>
        <br>
        <div class="row">
          <h6 style="text-align: center;">Hãy tải thông tin về máy và đến nhận phòng đúng thời hạn </h6><br>
          <h6 style="text-align: center;">Vui lòng quét mã QR để chuyển trước phí đặt cọc ${
            ((this.infoRoom.giaPhongTheoDem + this.infoRoom.giaDichVuThem) *
              (1 - this.infoRoom.giamGia - this.infoRoom.uuDaiDacBiet) *
              this.calculateNumberOfNights(
                this.infoBooking.ngayDat,
                this.infoBooking.ngayTra
              ) *
              this.infoBooking.slPhong) /
            10
          } VNĐ (10% phí đặt) <br> Nội dung chuyển khoản là mã số phiếu của bạn: ${
      this.confirmBookRoomResultDto.idPhieuDatPhong
    } <br><br>Bên khách sạn sẽ gọi điện lại xác nhận khi bạn đã chuyển tiền đặt cọc trong vòng 24h nếu không đặt cọc yêu cầu sẽ bị hủy.</h6>
        </div>
      </div>
    </div>
  
    `;
    this.showInvoice = true;
  }
  Preview() {
    this.generateInvoiceHtml();
    this.showInvoice = true;
  }
  closeInvoice() {
    this.showInvoice = false;
    console.log("Invoice closed");
  }
  downloadInvoice() {
    const documentDefinition = {
      content: [
        { text: "Thông tin đặt", style: "header" },
        { text: "Thông tin khách", style: "subheader" },
        {
          table: {
            widths: ["*", "*"],
            body: [
              ["Tên khách hàng", this.infoClient.hoTen],
              ["CCCD", this.infoClient.cccd],
              ["Email", this.infoClient.email],
              ["SĐT", this.infoClient.sdt],
            ],
          },
          layout: "lightHorizontalLines",
        },
        { text: "Thông tin đặt", style: "subheader", margin: [0, 20, 0, 10] },
        {
          table: {
            widths: ["*", "*"],
            body: [
              ["Mã phiếu", this.confirmBookRoomResultDto.idPhieuDatPhong],
              ["Ngày đặt", this.infoBooking.ngayDat.format("DD-MM-YYYY")],
              ["Ngày trả", this.infoBooking.ngayTra.format("DD-MM-YYYY")],
              ["Người lớn", this.infoBooking.slNguoiLon],
              ["Trẻ em", this.infoBooking.slTreEm],
              ["Số phòng", this.infoBooking.slPhong],
              ["Địa chỉ", this.confirmBookRoomResultDto.diaChiChiTiet],
              [
                "Tổng tiền",
                (this.infoRoom.giaPhongTheoDem + this.infoRoom.giaDichVuThem) *
                  (1 - this.infoRoom.giamGia - this.infoRoom.uuDaiDacBiet) *
                  this.calculateNumberOfNights(
                    this.infoBooking.ngayDat,
                    this.infoBooking.ngayTra
                  ) *
                  this.infoBooking.slPhong,
              ],
            ],
          },
          layout: "lightHorizontalLines",
        },
        {
          text: "Cảm ơn vì đã tin tưởng và lựa chọn chúng tôi. Mong chúng ta sẽ cùng đồng hành trong tương lai tới ^^.",
          style: "note",
          margin: [0, 20, 0, 0],
        },
      ],
      styles: {
        header: {
          fontSize: 18,
          bold: true,
          alignment: "center",
          margin: [0, 0, 0, 10],
        },
        subheader: {
          fontSize: 14,
          bold: true,
          margin: [0, 10, 0, 5],
        },
        note: {
          italics: true,
          fontSize: 12,
          color: "gray",
        },
      },
    };

    pdfMake.createPdf(documentDefinition).download("HoaDonStayEase.pdf");
  }
}
