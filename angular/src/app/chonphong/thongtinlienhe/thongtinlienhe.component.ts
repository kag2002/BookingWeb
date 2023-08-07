import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Params } from "@angular/router";
import {
  ClientBookRoomInputDto,
  ClientBookRoomOutputDto,
  PhongDto,
  PhongServiceProxy,
} from "@shared/service-proxies/service-proxies";

@Component({
  selector: "app-thongtinlienhe",
  templateUrl: "./thongtinlienhe.component.html",
  styleUrls: ["./thongtinlienhe.component.css"],
})
export class ThongtinlienheComponent {
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

  constructor(
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private _phongService: PhongServiceProxy
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
  }

  private initForm() {
    this.FormThongTinLienHe = this.formBuilder.group({
      name: ["", Validators.required],
      phone: ["", Validators.required],
      email: ["", Validators.required],
      datho: ["", Validators.required],
    });
    this.FormYeuCauDacBiet = this.formBuilder.group({
      selectedCategory: ["", Validators.required],
    });
  }

  onSubmit() {
    console.log(this.selectedloaiphong.value);
  }

  addClientBookRoom() {
    this.clientBookRoomInputDto.phongId = this.id;
    this.clientBookRoomInputDto.loaiPhongId = this.idloaiphong;
    this.clientBookRoomInputDto.datHo = this?.FormThongTinLienHe.value.datho;
    this.clientBookRoomInputDto.cccd = "76545678965678";
    this.clientBookRoomInputDto.hoTen = this?.FormThongTinLienHe.value.name;
    this.clientBookRoomInputDto.sdt = this?.FormThongTinLienHe.value.phone;
    this.clientBookRoomInputDto.email = this?.FormThongTinLienHe.value.email;
    this.clientBookRoomInputDto.yeuCauDacBiet = this.FormYeuCauDacBiet.value;

    this.clientBookRoomInputDto.tongTien =
      this.selectedloaiphong.giaPhongTheoDem;
    // this._phongService
    //   .clientBookRoom(this.clientBookRoomInputDto)
    //   .subscribe((result) => {});
    console.log(this.clientBookRoomInputDto);
  }
}
