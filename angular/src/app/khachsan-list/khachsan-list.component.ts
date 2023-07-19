import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, FormBuilder } from "@angular/forms";

@Component({
  selector: "app-khachsan-list",
  templateUrl: "./khachsan-list.component.html",
  styleUrls: ["./khachsan-list.component.css"],
})
export class KhachsanListComponent {
  formSapXep: FormGroup;
  formLoc: FormGroup;
  sapxeps: any[] = [
    { name: "Giá cao nhất", key: "MaxPrice" },
    { name: "Giá thấp nhất", key: "MinPrice" },
    { name: "Điểm đánh giá", key: "RatePoint" },
    { name: "Độ phổ biến", key: "Popular" },
  ];
  huyphongs: any[] = [{ name: "Miễn phí hủy phòng", key: "FreeCancel" }];
  saos: any[] = [];
  saveValue: any;
  rangeValues: number[] = [1000000, 3000000];
  saoCheckedList: string[] = [];
  constructor(private fb: FormBuilder) {}
  ngOnInit() {
    this.formSapXep = new FormGroup({
      selectedCategory: new FormControl(),
    });

    this.formLoc = this.fb.group({
      mienphihuyphong: [""],
      locsao: [""],
      inputminprice: [""],
      inputmaxprice: [""],
      inputprice: [""],

      LocSaoData: this.fb.group({
        saos: [],
        value1: [""],
        value2: [""],
        value3: [""],
        value4: [""],
        value5: [""],
      }),
    });
    this.formLoc.patchValue({
      inputminprice: this.rangeValues[0],
      inputmaxprice: this.rangeValues[1],
      inputprice: this.rangeValues,
    });
    // this.getValueForSave();
  }
  onSubmit() {
    // TODO: Use EventEmitter with form value
    // console.warn(this.formLoc.value);
    this.getValueForSave();
  }
  getValueForSave() {
    this.saoCheckedList = Object.keys(
      this.formLoc.get("LocSaoData")?.value || {}
    )
      .filter(
        (key) =>
          key.startsWith("value") &&
          this.formLoc.get(`LocSaoData.${key}`)?.value
      )
      .map((key) => this.formLoc.get(`LocSaoData.${key}`)?.value);

    console.log(this.saoCheckedList);
  }
}
