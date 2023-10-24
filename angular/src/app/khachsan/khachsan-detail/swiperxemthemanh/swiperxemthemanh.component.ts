import { Component } from "@angular/core";
declare var Swiper: any;
@Component({
  selector: "app-swiperxemthemanh",
  templateUrl: "./swiperxemthemanh.component.html",
  styleUrls: ["./swiperxemthemanh.component.css"],
})
export class SwiperxemthemanhComponent {
  ngAfterViewInit() {
    const swiper = new Swiper(".mySwiper", {
      loop: true,
      spaceBetween: 10,
      slidesPerView: 4,
      freeMode: true,
      watchSlidesProgress: true,
    });

    const swiper2 = new Swiper(".mySwiper2", {
      loop: true,
      spaceBetween: 10,
      navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
      },
      thumbs: {
        swiper: swiper,
      },
    });
  }
}
