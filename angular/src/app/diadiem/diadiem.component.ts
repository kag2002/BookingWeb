import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-diadiem",
  templateUrl: "./diadiem.component.html",
  styleUrls: ["./diadiem.component.css"],
})
export class DiadiemComponent {
  index: number;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.index = +params.get("index");
    });
  }
}
