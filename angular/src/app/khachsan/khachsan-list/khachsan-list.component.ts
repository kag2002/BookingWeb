import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, FormBuilder } from "@angular/forms";
import { BookingInfoService } from "@app/service/booking-info-service.service";
import {
  PhongDto,
  PhongSearchinhFilterDto,
  PhongServiceProxy,
  SearchingFilterRoomInputDto,
  SearchingFilterServiceProxy,
} from "@shared/service-proxies/service-proxies";

interface LoaiHinhCuTruItem {
  label: string;
  value: number;
}
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

  loaiHinhCuTruOptions: LoaiHinhCuTruItem[] = [
    { label: "Khách Sạn", value: 1 },
    { label: "Khách Sạn Cao Cấp", value: 2 },
    { label: "HomeStay", value: 3 },
    { label: "Nhà Nghỉ", value: 4 },
    { label: "Resort", value: 5 },
    { label: "Căn Hộ", value: 6 },
    { label: "Chỗ nghỉ", value: 7 },
    { label: "Nhà dân", value: 8 },
    { label: "Nhà Trọ", value: 9 },
    { label: "Biệt thự", value: 10 },
    { label: "Studio", value: 11 },
  ];
  stars: number[] = [1, 2, 3, 4, 5];

  maxPrice: number = 20000;
  listkhachsan: PhongSearchinhFilterDto[];
  listkhachsandiadiem: PhongServiceProxy;

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
    // this._phongService.getRoomsByDiaDiemId(id).subscribe((result) => {
    //   this.listkhachsandiadiem = result;
    // });

    this.bookingInfoService.getBookingInfo().subscribe(
      (result) => {
        this.listkhachsan = result;
        this.listLocKhachSanLuuTru = result;
      },
      (error) => {
        console.log("Error:", error);
      }
    );

    this.formSapXep = this.fb.group({
      selectedCategory: this.sapxeps[3],
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
      LocLoaiHinhCuTru: this.fb.group({}),
    });
    const locLoaiHinhCuTru = this.formLoc.get("LocLoaiHinhCuTru") as FormGroup;
    this.loaiHinhCuTruOptions.forEach((option) => {
      locLoaiHinhCuTru.addControl(
        option.value.toString(),
        new FormControl(false)
      );
    });
  }

  getCurrentSlideUrl(index: number): string {
    return `url('/assets/img/DonViKinhDoanh/${this.listkhachsan[index]?.tenFileAnhDaiDien}')`;
  }
  onCheckboxChange(value: number) {
    const locLoaiHinhCuTru = this.formLoc.get("LocLoaiHinhCuTru") as FormGroup;
    const control = locLoaiHinhCuTru.get(value.toString());

    if (control) {
      if (control.value) {
        this.selectedLoaiHinhCuTru.push(value);
      } else {
        const index = this.selectedLoaiHinhCuTru.indexOf(value);
        if (index !== -1) {
          this.selectedLoaiHinhCuTru.splice(index, 1);
        }
      }
    }
  }
  onSubmit() {
    // Get the selected stars from LocSaoData
    const locSaoData = this.formLoc.get("LocSaoData") as FormGroup;
    for (let star of this.stars) {
      if (locSaoData.get(`value${star}`).value) {
        this.selectedStars.push(star);
      }
    }

    // this.listLocKhachSanLuuTru = this.bookingInfoService.getBookingInfo();
    this.searchingFilterRoomInputDto.lst = this.listLocKhachSanLuuTru;
    this.searchingFilterRoomInputDto.danhGiaSao = this.selectedStars;
    this.searchingFilterRoomInputDto.giaPhongNhoNhat =
      this.formLoc.value.inputminprice;
    this.searchingFilterRoomInputDto.giaPhongLonNhat =
      this.formLoc.value.inputmaxprice;
    this.searchingFilterRoomInputDto.giaPhongNhoNhat =
      this.formLoc.value.inputminprice;
    this.searchingFilterRoomInputDto.hinhThucPhongId =
      this.selectedLoaiHinhCuTru;
    this.searchingFilterRoomInputDto.mienPhiHuyPhong =
      this.formLoc.value.mienphihuyphong;
    this.searchingFilterRoomInputDto.sortCondition =
      this.formSapXep.value.selectedCategory.key;

    this._searchingFilterService
      .getRoomsByLocationAndFilter(this.searchingFilterRoomInputDto)
      .subscribe(
        (result) => {
          this.listkhachsan = result;
          this.selectedStars = [];
        },
        (error) => {
          console.log("loi 2:", error);
        },
        () => {
          console.log("Loc Success");
        }
      );
  }
  // resetLoc() {
  //   this.formLoc.reset();
  // }
}
