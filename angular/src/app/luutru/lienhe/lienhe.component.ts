import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-lienhe",
  templateUrl: "./lienhe.component.html",
  styleUrls: ["./lienhe.component.css"],
})
export class LienheComponent implements OnInit {
  ngOnInit(): void {
    const faqBoxes = document.querySelectorAll(".contact .row .faq .box h3");

    faqBoxes.forEach((faqBox: HTMLElement) => {
      faqBox.addEventListener("click", () => {
        faqBox.parentElement?.classList.toggle("active");
      });
    });
  }
}
