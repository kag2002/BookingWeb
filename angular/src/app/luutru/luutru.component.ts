import { Component, ViewChild } from "@angular/core";
import { LuutruService } from "../luutru/luutru.service";
import { NgForm } from "@angular/forms";
import { SlideInterface } from "@app/sliderloaichonghi/types/slide.interface";

interface AutoCompleteCompleteEvent {
  originalEvent: Event;
  query: string;
}

@Component({
  selector: "app-luutru",
  templateUrl: "./luutru.component.html",
  styleUrls: ["./luutru.component.css"],
})
export class LuutruComponent {
  diadiems: any[];
  selectedDiadiem: any;
  filteredDiadiems: any[];
  rangeDates: Date[];
  showForm = false;
  adults = 0;
  children = 0;
  rooms = 0;
  submitted = false;
  slides: SlideInterface[] = [
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

  TimPhong = {
    selectedDiadiem: "",
    rangeDate: "",
    adult: 0,
    children: 0,
    room: 0,
  };

  @ViewChild("f") signupForm: NgForm;

  constructor(private luutruService: LuutruService) {}

  ngOnInit() {
    this.luutruService.getDiadiems().then((diadiems) => {
      this.diadiems = diadiems;
    });
  }

  filterDiadiem(event: AutoCompleteCompleteEvent) {
    const query = event.query.toLowerCase();
    this.filteredDiadiems = this.diadiems.filter((diadiem) =>
      diadiem.name.toLowerCase().startsWith(query)
    );
  }

  toggleForm() {
    this.showForm = !this.showForm;
  }

  increment(field: string) {
    this[field]++;
  }

  decrement(field: string) {
    if (this[field] > 0) {
      this[field]--;
    }
  }

  onSubmit() {
    this.submitted = true;
    this.TimPhong.selectedDiadiem = this.selectedDiadiem?.name || "";
    this.TimPhong.adult = this.adults;
    this.TimPhong.children = this.children;
    this.TimPhong.room = this.rooms;

    if (this.rangeDates && this.rangeDates.length === 2) {
      const start = this.rangeDates[0];
      const end = this.rangeDates[1];

      const millisecondsPerDay = 24 * 60 * 60 * 1000; // Number of milliseconds in a day
      const numberOfDays =
        Math.floor((end.getTime() - start.getTime()) / millisecondsPerDay) + 1;

      this.TimPhong.rangeDate = numberOfDays.toString(); // Store the number of days as a string
    }

    const formData = { ...this.TimPhong }; // Copy form data to a separate variable

    // Perform desired action with the form data (e.g., display in the template)
    console.log(formData);
  }
}
