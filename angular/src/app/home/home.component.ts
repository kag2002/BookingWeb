import {
  Component,
  Injector,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
} from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { ThongKeServiceProxy } from "@shared/service-proxies/service-proxies";

@Component({
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
  animations: [appModuleAnimation()],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomeComponent extends AppComponentBase {
  listdoanhthu12thang: any[];
  data: any;
  options: any;
  data2: any;
  options2: any;

  constructor(
    injector: Injector,
    private thongkeservice: ThongKeServiceProxy,
    private cdr: ChangeDetectorRef // Inject ChangeDetectorRef
  ) {
    super(injector);
  }

  ngOnInit() {
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue("--text-color");
    const textColorSecondary = documentStyle.getPropertyValue(
      "--text-color-secondary"
    );
    const surfaceBorder = documentStyle.getPropertyValue("--surface-border");

    this.thongkeservice.getDoanhThu12Thang().subscribe((result) => {
      this.listdoanhthu12thang = result;

      this.data = {
        labels: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"],
        datasets: [
          {
            label: "Doanh thu tháng ",
            backgroundColor: documentStyle.getPropertyValue("--blue-500"),
            borderColor: documentStyle.getPropertyValue("--blue-500"),
            data: this.listdoanhthu12thang,
          },
          // Add other datasets if needed
        ],
      };

      this.data2 = {
        labels: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"],
        datasets: [
          {
            label: "Doanh thu tháng",
            backgroundColor: documentStyle.getPropertyValue("--blue-500"),
            borderColor: documentStyle.getPropertyValue("--blue-500"),
            data: this.listdoanhthu12thang,
          },
          // Add other datasets if needed
        ],
      };

      this.options = {
        maintainAspectRatio: false,
        aspectRatio: 0.8,
        plugins: {
          legend: {
            labels: {
              color: textColor,
            },
          },
        },
        scales: {
          x: {
            ticks: {
              color: textColorSecondary,
              font: {
                weight: 500,
              },
            },
            grid: {
              color: surfaceBorder,
              drawBorder: false,
            },
          },
          y: {
            ticks: {
              color: textColorSecondary,
            },
            grid: {
              color: surfaceBorder,
              drawBorder: false,
            },
          },
        },
      };

      this.options2 = {
        indexAxis: "y",
        maintainAspectRatio: false,
        aspectRatio: 0.8,
        plugins: {
          legend: {
            labels: {
              color: textColor,
            },
          },
        },
        scales: {
          x: {
            ticks: {
              color: textColorSecondary,
              font: {
                weight: 500,
              },
            },
            grid: {
              color: surfaceBorder,
              drawBorder: false,
            },
          },
          y: {
            ticks: {
              color: textColorSecondary,
            },
            grid: {
              color: surfaceBorder,
              drawBorder: false,
            },
          },
        },
      };

      // Manually trigger change detection
      this.cdr.detectChanges();
    });
  }
}
