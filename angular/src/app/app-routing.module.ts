import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { AppComponent } from "./app.component";
import { AppRouteGuard } from "@shared/auth/auth-route-guard";
import { HomeComponent } from "./home/home.component";
import { UsersComponent } from "./users/users.component";
import { TenantsComponent } from "./tenants/tenants.component";
import { RolesComponent } from "app/roles/roles.component";
import { ChangePasswordComponent } from "./users/change-password/change-password.component";
import { TrangchuComponent } from "./trangchu/trangchu.component";
import { KhachsanListComponent } from "./khachsan/khachsan-list/khachsan-list.component";
import { KhachsanDetailComponent } from "./khachsan/khachsan-detail/khachsan-detail.component";
import { KhachsanStartComponent } from "./khachsan/khachsan-start/khachsan-start.component";
import { ThongtinlienheComponent } from "./formthongtinlienhe/thongtinlienhe/thongtinlienhe.component";
import { XacnhandatComponent } from "./formthongtinlienhe/xacnhandat/xacnhandat.component";
import { FormtaokhachsanComponent } from "./admin/formtaokhachsan/formtaokhachsan.component";
import { PhieudatlistComponent } from "./admin/phieudatlist/phieudatlist.component";
import { PhieudatstartComponent } from "./admin/phieudatlist/phieudatstart/phieudatstart.component";
import { QuanlyphieudatComponent } from "./admin/phieudatlist/quanlyphieudat/quanlyphieudat.component";
import { HosodatComponent } from "./admin/hosodat/hosodat.component";
import { HosodatstartComponent } from "./admin/hosodat/hosodatstart/hosodatstart.component";
import { QuanlyhosodatComponent } from "./admin/hosodat/quanlyhosodat/quanlyhosodat.component";
import { PhanhoiComponent } from "./phanhoi/phanhoi.component";

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: "",
        component: AppComponent,
        // canActivate: [AppRouteGuard],
        children: [
          {
            path: "home",
            component: HomeComponent,
            canActivate: [AppRouteGuard],
          },
          {
            path: "users",
            component: UsersComponent,
            data: { permission: "Pages.Users" },
            // canActivate: [AppRouteGuard],
          },
          {
            path: "roles",
            component: RolesComponent,
            data: { permission: "Pages.Roles" },
            // canActivate: [AppRouteGuard],
          },
          {
            path: "tenants",
            component: TenantsComponent,
            data: { permission: "Pages.Tenants" },
            // canActivate: [AppRouteGuard],
          },

          {
            path: "trangchu",
            component: TrangchuComponent,
            // canActivate: [AppRouteGuard],
            children: [
              {
                path: ":idloailoc/:iddeloc",
                component: KhachsanListComponent,
                // canActivate: [AppRouteGuard],
              },
            ],
          },

          {
            path: "TaoKhachsan",
            component: FormtaokhachsanComponent,
            // canActivate: [AppRouteGuard],
          },
          {
            path: "Phieudatlist",
            component: PhieudatlistComponent,
            // canActivate: [AppRouteGuard],
          },
          {
            path: "updatephieudat",
            component: PhieudatstartComponent,
            // canActivate: [AppRouteGuard],
            children: [
              {
                path: ":idphieudat",
                component: QuanlyphieudatComponent,
                // canActivate: [AppRouteGuard],
              },
            ],
          },
          {
            path: "Hosodat",
            component: HosodatComponent,
            // canActivate: [AppRouteGuard],
          },
          {
            path: "updatehosodat",
            component: HosodatstartComponent,
            // canActivate: [AppRouteGuard],
            children: [
              {
                path: ":idhosodat",
                component: QuanlyhosodatComponent,
                // canActivate: [AppRouteGuard],
              },
            ],
          },
          {
            path: "khachsanlist",
            component: KhachsanListComponent,
            // canActivate: [AppRouteGuard],
          },
          {
            path: "khachsanstart",
            component: KhachsanStartComponent,
            // canActivate: [AppRouteGuard],
            children: [
              {
                path: ":id",
                component: KhachsanDetailComponent,
                // canActivate: [AppRouteGuard],
              },
            ],
          },
          {
            path: "thongtinlienhestart",
            component: KhachsanStartComponent,
            // canActivate: [AppRouteGuard],
            children: [
              {
                path: ":id/:idloaiphong",
                component: ThongtinlienheComponent,
                // canActivate: [AppRouteGuard],
              },
              {
                path: ":id/:idloaiphong/:idphongxacnhan",
                component: XacnhandatComponent,
                // canActivate: [AppRouteGuard],
              },
            ],
          },
          {
            path: "Phanhoi",
            component: PhanhoiComponent,
            // canActivate: [AppRouteGuard],
          },

          {
            path: "update-password",
            component: ChangePasswordComponent,
            // canActivate: [AppRouteGuard],
          },
        ],
      },
    ]),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
