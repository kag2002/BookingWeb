import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: "app-lienhe",
  templateUrl: "./lienhe.component.html",
  styleUrls: ["./lienhe.component.css"],
})
export class LienheComponent implements OnInit {
  contactForm: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.initForm();
    const faqBoxes = document.querySelectorAll(".contact .row .faq .box h3");

    faqBoxes.forEach((faqBox: HTMLElement) => {
      faqBox.addEventListener("click", () => {
        faqBox.parentElement?.classList.toggle("active");
      });
    });
  }

  private initForm(): void {
    this.contactForm = this.fb.group({
      name: ["", [Validators.required, Validators.maxLength(50)]],
      email: [
        "",
        [Validators.required, Validators.maxLength(50), Validators.email],
      ],
      number: ["", [Validators.required, Validators.pattern(/^\d{10}$/)]],
      message: ["", [Validators.required, Validators.maxLength(1000)]],
    });
  }
  onSubmit(): void {
    if (this.contactForm.valid) {
      const formData = this.contactForm.value;
      console.log(formData);
    }
  }
}
