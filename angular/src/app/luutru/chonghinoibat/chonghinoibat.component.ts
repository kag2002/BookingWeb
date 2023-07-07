import { Component } from "@angular/core";
import {
  SlideDiaDiemInterface,
  SlideLoaiChoNghiInterface,
} from "@app/slider/types/slide.interface";

@Component({
  selector: "app-chonghinoibat",
  templateUrl: "./chonghinoibat.component.html",
  styleUrls: ["./chonghinoibat.component.css"],
})
export class ChonghinoibatComponent {
  slides: SlideDiaDiemInterface[] = [
    {
      url: "/assets/img/img-diadanh/image-diadanh-1.jpg",
      title: "HaLong",
      number: 12343,
    },
    {
      url: "/assets/img/img-diadanh/image-diadanh-2.jpg",
      title: "HaNoi",
      number: 123412,
    },
    {
      url: "/assets/img/img-diadanh/image-diadanh-3.jpg",
      title: "HoChiMinh",
      number: 1234123,
    },
    {
      url: "/assets/img/img-diadanh/image-diadanh-4.jpg",
      title: "SaPa",
      number: 13124,
    },
    {
      url: "/assets/img/img-diadanh/image-diadanh-5.jpg",
      title: "Hue",
      number: 123214,
    },
    {
      url: "/assets/img/img-diadanh/image-diadanh-6.jpg",
      title: "DaNang",
      number: 2342,
    },
    {
      url: "/assets/img/img-diadanh/image-diadanh-7.jpg",
      title: "DaLat",
      number: 123412,
    },
    {
      url: "/assets/img/img-diadanh/image-diadanh-8.jpg",
      title: "NhaTrang",
      number: 1234234,
    },
    {
      url: "/assets/img/img-diadanh/image-diadanh-9.jpg",
      title: "HaiPhong",
      number: 1341,
    },
    {
      url: "/assets/img/img-diadanh/image-diadanh-10.jpg",
      title: "PhuQuoc",
      number: 23423,
    },
    {
      url: "/assets/img/img-diadanh/image-diadanh-11.jpg",
      title: "BinhThuan",
      number: 232342,
    },
    {
      url: "/assets/img/img-diadanh/image-diadanh-12.jpg",
      title: "HaGiang",
      number: 1232342,
    },
    {
      url: "/assets/img/img-diadanh/image-diadanh-13.jpg",
      title: "HoiAn",
      number: 2341232,
    },
  ];
  slidesloaichonghi: SlideLoaiChoNghiInterface[] = [
    {
      url: "/assets/img/img-loaichonghi/image-loaichonghi-1.jpg",
      title: "Khách sạn",
      ChoO: 12343,
    },
    {
      url: "/assets/img/img-loaichonghi/image-loaichonghi-2.jpg",
      title: "Căn hộ",
      ChoO: 12343,
    },
    {
      url: "/assets/img/img-loaichonghi/image-loaichonghi-3.jpg",
      title: "Resort",
      ChoO: 12343,
    },
    {
      url: "/assets/img/img-loaichonghi/image-loaichonghi-4.jpg",
      title: "BIệt thự",
      ChoO: 12343,
    },
    {
      url: "/assets/img/img-loaichonghi/image-loaichonghi-5.jpg",
      title: "Nhà gỗ",
      ChoO: 12343,
    },
    {
      url: "/assets/img/img-loaichonghi/image-loaichonghi-6.jpg",
      title: "Phòng trọ",
      ChoO: 12343,
    },
  ];

  TimPhong = {
    selectedDiadiem: "",
    rangeDate: "",
    adult: 0,
    children: 0,
    room: 0,
  };
}
