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
import { DiadiemComponent } from "./diadiem/diadiem.component";
import { KhachsanListComponent } from "./khachsan/khachsan-list/khachsan-list.component";
import { KhachsanDetailComponent } from "./khachsan/khachsan-detail/khachsan-detail.component";
import { KhachsanStartComponent } from "./khachsan/khachsan-start/khachsan-start.component";
import { ThongtinlienheComponent } from "./formthongtinlienhe/thongtinlienhe/thongtinlienhe.component";
import { XacnhandatComponent } from "./xacnhandat/xacnhandat/xacnhandat.component";
import { XacnhandatStartComponent } from "./xacnhandat/xacnhandat-start/xacnhandat-start.component";

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: "",
        component: AppComponent,
        canActivate: [AppRouteGuard],
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
            canActivate: [AppRouteGuard],
          },
          {
            path: "roles",
            component: RolesComponent,
            data: { permission: "Pages.Roles" },
            canActivate: [AppRouteGuard],
          },
          {
            path: "tenants",
            component: TenantsComponent,
            data: { permission: "Pages.Tenants" },
            canActivate: [AppRouteGuard],
          },

          {
            path: "trangchu",
            component: TrangchuComponent,
            canActivate: [AppRouteGuard],
          },
          {
            path: "khachsanlist",
            component: KhachsanListComponent,
            canActivate: [AppRouteGuard],
          },
          {
            path: "khachsanstart",
            component: KhachsanStartComponent,
            canActivate: [AppRouteGuard],
            children: [
              {
                path: ":id",
                component: KhachsanDetailComponent,
                canActivate: [AppRouteGuard],
              },
            ],
          },
          {
            path: "thongtinlienhestart",
            component: KhachsanStartComponent,
            canActivate: [AppRouteGuard],
            children: [
              {
                path: ":id/:idloaiphong",
                component: ThongtinlienheComponent,
                canActivate: [AppRouteGuard],
              },
            ],
          },
          {
            path: "xacnhandat",
            component: XacnhandatStartComponent,
            canActivate: [AppRouteGuard],
            children: [
              {
                path: ":id",
                component: XacnhandatComponent,
                canActivate: [AppRouteGuard],
              },
            ],
          },

          {
            path: "update-password",
            component: ChangePasswordComponent,
            canActivate: [AppRouteGuard],
          },

          // { path: "other/:index", component: DiadiemComponent },
        ],
      },
    ]),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
