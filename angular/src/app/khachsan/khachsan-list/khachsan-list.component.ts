import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, FormBuilder } from "@angular/forms";
import { ActivatedRoute, Params } from "@angular/router";
import { BookingInfoService } from "@app/service/booking-info-service.service";
import {
  InfoBookingDto,
  PhongDto,
  PhongSearchinhFilterDto,
  HinhThucPhongServiceProxy,
  SearchingFilterRoomInputDto,
  SearchingFilterServiceProxy,
} from "@shared/service-proxies/service-proxies";

interface LocItem {
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
  idloailoc: number;
  iddeloc: number;
  rangeValues: number[] = [0, 20000];

  lstLoaiPhong: [];

  sapxeps: any[] = [
    { name: "Giá cao nhất", key: 1 },
    { name: "Giá thấp nhất", key: 2 },
    { name: "Điểm đánh giá", key: 3 },
    { name: "Độ phổ biến", key: 4 },
  ];

  loaiHinhCuTruOptions: LocItem[] = [
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
  saoOptions: LocItem[] = [
    { label: "KS1sao", value: 1 },
    { label: "KS2sao", value: 2 },
    { label: "KS3sao", value: 3 },
    { label: "KS4sao", value: 4 },
    { label: "KS5sao", value: 5 },
  ];

  maxPrice: number = 20000;
  listkhachsan: PhongSearchinhFilterDto[];

  searchingFilterRoomInputDto = new SearchingFilterRoomInputDto();

  listLocKhachSanLuuTru: PhongSearchinhFilterDto[];
  hinhthucphongservice: HinhThucPhongServiceProxy;
  // Tim theo sliderdiadiem
  inforBookingDtoSliderDiaDiem: InfoBookingDto = new InfoBookingDto();

  selectedStars: number[] = [];
  selectedLoaiHinhCuTru: number[] = [];

  constructor(
    private fb: FormBuilder,

    private _searchingFilterService: SearchingFilterServiceProxy,
    private bookingInfoService: BookingInfoService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.idloailoc = +params["idloailoc"];
    });
    this.route.params.subscribe((params: Params) => {
      this.iddeloc = +params["iddeloc"];
    });

    if (this.idloailoc == 0) {
      this.bookingInfoService.getBookingInfo().subscribe(
        (result) => {
          this.listkhachsan = result;
          this.listLocKhachSanLuuTru = result;
        },
        (error) => {
          console.log("Error:", error);
        }
      );
    } else if (this.idloailoc == 1) {
      this.inforBookingDtoSliderDiaDiem.diaDiemid = this.iddeloc;
      this.inforBookingDtoSliderDiaDiem.ngayDat = undefined;
      this.inforBookingDtoSliderDiaDiem.ngayTra = undefined;
      this.inforBookingDtoSliderDiaDiem.slNguoiLon = undefined;
      this.inforBookingDtoSliderDiaDiem.slPhong = undefined;
      this.inforBookingDtoSliderDiaDiem.slTreEm = undefined;
      this._searchingFilterService
        .searchingRoom(this.inforBookingDtoSliderDiaDiem)
        .subscribe(
          (result) => {
            this.listkhachsan = result;
            this.listLocKhachSanLuuTru = result;
          },
          (error) => {
            console.log("Error:", error);
          }
        );
    } else if (this.idloailoc == 2) {
      // console.log(this.iddeloc);
      // this.hinhthucphongservice.getRoomByForm(this.iddeloc).subscribe(
      //   (result) => {
      //     this.listkhachsan = result;
      //     this.listLocKhachSanLuuTru = result;
      //   },
      //   (error) => {
      //     console.log("Error:", error);
      //   }
      // );
    }

    this.formSapXep = this.fb.group({
      selectedCategory: this.sapxeps[3],
    });

    this.formLoc = this.fb.group({
      mienphihuyphong: false,
      inputminprice: [this.rangeValues[0]],
      inputmaxprice: [this.rangeValues[1]],
      inputprice: [this.rangeValues],
      LocSaoData: this.fb.group({
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
    const locSao = this.formLoc.get("LocSaoData") as FormGroup;
    this.saoOptions.forEach((option) => {
      locSao.addControl(option.value.toString(), new FormControl(false));
    });
  }

  getCurrentSlideUrl(index: number): string {
    return `url('/assets/img/DonViKinhDoanh/${this.listkhachsan[index]?.tenFileAnhDaiDien}')`;
  }
  onCheckboxLoaiHinhChange(value: number) {
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
  onCheckboxSaoChange(value: number) {
    const locSao = this.formLoc.get("LocSaoData") as FormGroup;
    const control = locSao.get(value.toString());

    if (control) {
      if (control.value) {
        this.selectedStars.push(value);
      } else {
        const index = this.selectedStars.indexOf(value);
        if (index !== -1) {
          this.selectedStars.splice(index, 1);
        }
      }
    }
  }

  onSubmit() {
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


          // this.selectedStars = [];

          console.log("oke :", this.listkhachsan);

          console.log("Loc Success");
        },
        (error) => {
          console.log("loi 2:", error);
        },
        () => {}
      );
  }
  // resetLoc() {
  //   this.formLoc.reset();
  // }
}
