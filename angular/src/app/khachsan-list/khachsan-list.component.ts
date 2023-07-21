import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, FormBuilder } from "@angular/forms";

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
    { name: "Giá cao nhất", key: "MaxPrice" },
    { name: "Giá thấp nhất", key: "MinPrice" },
    { name: "Điểm đánh giá", key: "RatePoint" },
    { name: "Độ phổ biến", key: "Popular" },
  ];
  stars: number[] = [1, 2, 3, 4, 5];
  maxPrice: number = 4000000;

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.formSapXep = this.fb.group({
      selectedCategory: new FormControl(),
    });

    this.formLoc = this.fb.group({
      mienphihuyphong: [""],
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
}
