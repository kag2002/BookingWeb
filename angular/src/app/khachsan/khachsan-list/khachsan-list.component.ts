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
import { result } from "lodash-es";

@Component({
  selector: "app-khachsan-list",
  templateUrl: "./khachsan-list.component.html",
  styleUrls: ["./khachsan-list.component.css"],
})
export class KhachsanListComponent implements OnInit {
  formSapXep: FormGroup;
  formLoc: FormGroup;

  rangeValues: number[] = [1000, 3000];

  lstLoaiPhong: [];

  sapxeps: any[] = [
    { name: "Giá cao nhất", key: 1 },
    { name: "Giá thấp nhất", key: 2 },
    { name: "Điểm đánh giá", key: 3 },
    { name: "Độ phổ biến", key: 4 },
  ];
  stars: number[] = [1, 2, 3, 4, 5];

  maxPrice: number = 20000;
  listkhachsan: PhongSearchinhFilterDto[];

  searchingFilterRoomInputDto = new SearchingFilterRoomInputDto();

  listLocKhachSanLuuTru: PhongSearchinhFilterDto[];

  selectedStars: number[] = [];
  selectedLoaiHinhCuTru: number[] = [];
  constructor(
    private fb: FormBuilder,
    private _phongService: PhongServiceProxy,
    private _searchingFilterService: SearchingFilterServiceProxy,
    private bookingInfoService: BookingInfoService
  ) {}
  ngOnInit() {
    // this._phongService.getAllRoom().subscribe((result) => {
    //   this.listkhachsan = result;
    // });
    console.log("Observable:", this.bookingInfoService.getBookingInfo());

    this.bookingInfoService.getBookingInfo().subscribe(
      (result) => {
        console.log("Received result:", result);
        this.listkhachsan = result;
        this.listLocKhachSanLuuTru = result;
      },
      (error) => {
        console.log("Error:", error);
      }
    );

    this.formSapXep = this.fb.group({
      selectedCategory: new FormControl(this.sapxeps[3]),
    });

    this.formLoc = this.fb.group({
      mienphihuyphong: false,
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
    // this.listLocKhachSanLuuTru = this.bookingInfoService.getBookingInfo();
    debugger;
    this.searchingFilterRoomInputDto.lst = this.listLocKhachSanLuuTru;
    this.searchingFilterRoomInputDto.danhGiaSao = [1, 2];
    this.searchingFilterRoomInputDto.giaPhongNhoNhat =
      this.formLoc.value.inputminprice;
    this.searchingFilterRoomInputDto.giaPhongLonNhat =
      this.formLoc.value.inputmaxprice;
    this.searchingFilterRoomInputDto.giaPhongNhoNhat =
      this.formLoc.value.inputminprice;
    this.searchingFilterRoomInputDto.hinhThucPhongId = [1, 2, 3, 4, 5];
    // this.selectedLoaiHinhCuTru;
    this.searchingFilterRoomInputDto.mienPhiHuyPhong =
      this.formLoc.value.mienphihuyphong;
    this.searchingFilterRoomInputDto.sortCondition = 4;

    this._searchingFilterService
      .getRoomsByLocationAndFilter(this.searchingFilterRoomInputDto)
      .subscribe(
        (result) => {
          console.log("ket qua", result);
          this.listkhachsan = result;
        },
        (error) => {
          console.log("loi 2:", error);
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
