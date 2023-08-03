import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, FormBuilder } from "@angular/forms";
import {
  PhongSearchinhFilterDto,
  PhongServiceProxy,
  SearchingFilterServiceProxy,
} from "@shared/service-proxies/service-proxies";

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
  listkhachsanfiltered = [];
  currentIndex = 0;

  phongSearchinhFilterDto: PhongSearchinhFilterDto[];

  // searchingFilterRoomInputDto:SearchingFilterRoomInputDto
  constructor(
    private fb: FormBuilder,
    private _phongService: PhongServiceProxy,
    private _searchingFilterService: SearchingFilterServiceProxy
  ) {}

  ngOnInit() {
    this._phongService.getAllRoom().subscribe((result) => {
      this.listkhachsan = result;
    });

    this.formSapXep = this.fb.group({
      selectedCategory: new FormControl(this.sapxeps[3]),
    });

    this.formLoc = this.fb.group({
      mienphihuyphong: [false],
      inputminprice: [this.rangeValues[0]],
      inputmaxprice: [this.rangeValues[1]],
      inputprice: [this.rangeValues],
      LocSaoData: this.fb.group({
        value1: [""],
        value2: [""],
        value3: [""],
        value4: [""],
        value5: [""],
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

  onSubmit() {
    this.getValueForSave();
  }

  getValueForSave() {
    const selectedStars = this.stars.filter(
      (star) => this.formLoc.get(["LocSaoData", "value" + star])?.value
    );
    console.log(selectedStars.map((star) => "Khach San " + star + " sao"));
  }
  getCurrentSlideUrl(index: number): string {
    return `url('/assets/img/DonViKinhDoanh/${this.listkhachsan[index]?.tenFileAnhDaiDien}')`;
  }

  filterKhachSan() {}
  SearchingBookings() {
    this._searchingFilterService
      .getRoomsByLocationAndFilter(
        undefined,
        this.listkhachsan.length,
        undefined,
        undefined,
        undefined,
        undefined,
        undefined,
        undefined
      )
      .subscribe((results) => {
        this.phongSearchinhFilterDto = results.items;
      });
  }
}
