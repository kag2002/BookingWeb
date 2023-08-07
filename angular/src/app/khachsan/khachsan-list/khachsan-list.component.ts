import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, FormBuilder } from "@angular/forms";
import { BookingInfoService } from "@app/service/booking-info-service.service";
import {
  PhongSearchinhFilterDto,
  PhongServiceProxy,
  SearchingFilterRoomInputDto,
  SearchingFilterServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { error } from "console";

@Component({
  selector: "app-khachsan-list",
  templateUrl: "./khachsan-list.component.html",
  styleUrls: ["./khachsan-list.component.css"],
})
export class KhachsanListComponent implements OnInit {
  formSapXep: FormGroup;
  formLoc: FormGroup;

  rangeValues: number[] = [1000000, 3000000];

  sapxeps: any[] = [
    { name: "Giá cao nhất", key: 1 },
    { name: "Giá thấp nhất", key: 2 },
    { name: "Điểm đánh giá", key: 3 },
    { name: "Độ phổ biến", key: 4 },
  ];
  stars: number[] = [1, 2, 3, 4, 5];
  maxPrice: number = 4000000;
  listkhachsan = [];


  searchingFilterRoomInputDto = new SearchingFilterRoomInputDto();

  listLocKhachSanLuuTru: PhongSearchinhFilterDto[];

  constructor(
    private fb: FormBuilder,
    private _phongService: PhongServiceProxy,
    private _searchingFilterService: SearchingFilterServiceProxy,
    private bookingInfoService: BookingInfoService
  ) {}
  ngOnInit() {
    this._phongService.getAllRoom().subscribe((result) => {
      this.listkhachsan = result;
    });

    this.formSapXep = this.fb.group({
      selectedCategory: new FormControl(this.sapxeps[3]),
    });

    this.formLoc = this.fb.group({
      mienphihuyphong: null,
      inputminprice: [this.rangeValues[0]],
      inputmaxprice: [this.rangeValues[1]],
      inputprice: [this.rangeValues],
      LocSaoData: this.fb.group({
        value1: [],
        value2: [],
        value3: [],
        value4: [],
        value5: [],
        numberStar1: [1],
        numberStar2: [2],
        numberStar3: [3],
        numberStar4: [4],
        numberStar5: [5],
      }),
      LocLoaiHinhCuTru: this.fb.group({
        khachsan: [],
        khachsancaocap: [],
        homestay: [],
        nhanghi: [],
        resort: [],
        canho: [],
        chonghi: [],
        nhadan: [],
        nhatro: [],
        bietthu: [],
        studio: [],
      }),
    });
  }

  getCurrentSlideUrl(index: number): string {
    return `url('/assets/img/DonViKinhDoanh/${this.listkhachsan[index]?.tenFileAnhDaiDien}')`;
  }


  onSubmit() {
    this.listLocKhachSanLuuTru = this.bookingInfoService.getBookingInfo();
    console.log("Đã nhận được:", this.listLocKhachSanLuuTru);

    this.searchingFilterRoomInputDto.lst = this.listLocKhachSanLuuTru;
    this.searchingFilterRoomInputDto.danhGiaSao = [1, 2];
    this.searchingFilterRoomInputDto.giaPhongLonNhat = 100000;
    this.searchingFilterRoomInputDto.giaPhongNhoNhat = 0;
    this.searchingFilterRoomInputDto.hinhThucPhongId = [
      1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
    ];
    this.searchingFilterRoomInputDto.mienPhiHuyPhong = false;
    this.searchingFilterRoomInputDto.sortCondition = 4;

    this._searchingFilterService
      .getRoomsByLocationAndFilter(this.searchingFilterRoomInputDto)
      .subscribe(
        (result) => {
          console.log("ket qua", result);
        },
        (error) => {
          console.log("loi:", error);
        },
        () => {
          console.log("da loc thanh cong");
        }
      );
  }
  // resetLoc() {
  //   this.formLoc.reset();
  // }
}
