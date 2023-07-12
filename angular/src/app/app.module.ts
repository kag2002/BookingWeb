import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientJsonpModule } from "@angular/common/http";
import { HttpClientModule } from "@angular/common/http";
import { ModalModule } from "ngx-bootstrap/modal";
import { BsDropdownModule } from "ngx-bootstrap/dropdown";
import { CollapseModule } from "ngx-bootstrap/collapse";
import { TabsModule } from "ngx-bootstrap/tabs";
import { NgxPaginationModule } from "ngx-pagination";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { ServiceProxyModule } from "@shared/service-proxies/service-proxy.module";
import { SharedModule } from "@shared/shared.module";
import { HomeComponent } from "@app/home/home.component";
import { ButtonModule } from "primeng/button";
import { TabViewModule } from "primeng/tabview";
import { AutoCompleteModule } from "primeng/autocomplete";
import { CalendarModule } from "primeng/calendar";
import { DropdownModule } from "primeng/dropdown";
import { CheckboxModule } from "primeng/checkbox";
import { AnimateModule } from "primeng/animate";
import { ToastModule } from "primeng/toast";

// tenants
import { TenantsComponent } from "@app/tenants/tenants.component";
import { CreateTenantDialogComponent } from "./tenants/create-tenant/create-tenant-dialog.component";
import { EditTenantDialogComponent } from "./tenants/edit-tenant/edit-tenant-dialog.component";
// roles
import { RolesComponent } from "@app/roles/roles.component";
import { CreateRoleDialogComponent } from "./roles/create-role/create-role-dialog.component";
import { EditRoleDialogComponent } from "./roles/edit-role/edit-role-dialog.component";
// users
import { UsersComponent } from "@app/users/users.component";
import { CreateUserDialogComponent } from "@app/users/create-user/create-user-dialog.component";
import { EditUserDialogComponent } from "@app/users/edit-user/edit-user-dialog.component";
import { ChangePasswordComponent } from "./users/change-password/change-password.component";
import { ResetPasswordDialogComponent } from "./users/reset-password/reset-password.component";
// layout
import { HeaderComponent } from "./layout/header.component";
import { HeaderLeftNavbarComponent } from "./layout/header-left-navbar.component";
import { HeaderLanguageMenuComponent } from "./layout/header-language-menu.component";
import { HeaderUserMenuComponent } from "./layout/header-user-menu.component";
import { FooterComponent } from "./layout/footer.component";
import { TrangchuComponent } from "./trangchu/trangchu.component";
import { KhachsanListComponent } from "./khachsan-list/khachsan-list.component";
import { DiadiemComponent } from "./diadiem/diadiem.component";
import { CaidatComponent } from "./caidat/caidat.component";
import { LuutruComponent } from "./luutru/luutru.component";
import { SliderloaichonghiComponent } from "./slider/sliderloaichonghi/sliderloaichonghi.component";
import { SliderdiadiemComponent } from "./slider/sliderdiadiem/sliderdiadiem.component";
import { ChonghinoibatComponent } from "./luutru/chonghinoibat/chonghinoibat.component";
import { SliderchonghinoibatComponent } from './slider/sliderchonghinoibat/sliderchonghinoibat.component';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    TrangchuComponent,
    // tenants
    TenantsComponent,
    CreateTenantDialogComponent,
    EditTenantDialogComponent,
    // roles
    RolesComponent,
    CreateRoleDialogComponent,
    EditRoleDialogComponent,
    // users
    UsersComponent,
    CreateUserDialogComponent,
    EditUserDialogComponent,
    ChangePasswordComponent,
    ResetPasswordDialogComponent,
    // layout
    HeaderComponent,
    HeaderLeftNavbarComponent,
    HeaderLanguageMenuComponent,
    HeaderUserMenuComponent,
    FooterComponent,
    TrangchuComponent,
    KhachsanListComponent,
    DiadiemComponent,
    CaidatComponent,
    LuutruComponent,
    SliderloaichonghiComponent,
    SliderdiadiemComponent,
    ChonghinoibatComponent,
    SliderchonghinoibatComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    HttpClientJsonpModule,
    ModalModule.forChild(),
    BsDropdownModule,
    CollapseModule,
    TabsModule,
    AppRoutingModule,
    ServiceProxyModule,
    SharedModule,
    NgxPaginationModule,
    ButtonModule,
    TabViewModule,
    AutoCompleteModule,
    CalendarModule,
    DropdownModule,
    CheckboxModule,
    AnimateModule,
    ToastModule,
  ],
  providers: [],
})
export class AppModule {}
