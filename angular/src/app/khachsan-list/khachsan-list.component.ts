import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";

@Component({
  selector: "app-khachsan-list",
  templateUrl: "./khachsan-list.component.html",
  styleUrls: ["./khachsan-list.component.css"],
})
export class KhachsanListComponent {
  formSapXep!: FormGroup;
  formLoc!: FormGroup;
  sapxeps: any[] = [
    { name: "Giá cao nhất", key: "MaxPrice" },
    { name: "Giá thấp nhất", key: "MinPrice" },
    { name: "Điểm đánh giá", key: "RatePoint" },
    { name: "Độ phổ biến", key: "Popular" },
  ];
  huyphongs: any[] = [{ name: "Miễn phí hủy phòng", key: "FreeCancel" }];
  rangeValues: number[] = [1000000, 3000000];

  ngOnInit() {
    this.formSapXep = new FormGroup({
      selectedCategory: new FormControl(),
    });
    this.formLoc = new FormGroup({
      mienphihuyphong: new FormControl<string | null>(null),
    });
  }
}
