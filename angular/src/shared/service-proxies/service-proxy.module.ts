import { NgModule } from "@angular/core";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { AbpHttpInterceptor } from "abp-ng2-module";

import * as ApiServiceProxies from "./service-proxies";

@NgModule({
  providers: [
    ApiServiceProxies.RoleServiceProxy,
    ApiServiceProxies.SessionServiceProxy,
    ApiServiceProxies.TenantServiceProxy,
    ApiServiceProxies.UserServiceProxy,
    ApiServiceProxies.TokenAuthServiceProxy,
    ApiServiceProxies.AccountServiceProxy,
    ApiServiceProxies.ConfigurationServiceProxy,
    ApiServiceProxies.DiaDiemServiceProxy,
    ApiServiceProxies.HinhThucPhongServiceProxy,
    ApiServiceProxies.PhongServiceProxy,
    ApiServiceProxies.SearchingFilterServiceProxy,
    ApiServiceProxies.LoaiPhongServiceProxy,
    ApiServiceProxies.NhanXetDanhGiaServiceProxy,
    ApiServiceProxies.ChiTietDatPhongServiceProxy,
    ApiServiceProxies.DatPhongServiceProxy,
    ApiServiceProxies.DichVuTienIchServiceProxy,
    ApiServiceProxies.DonViKinhDoanhServiceProxy,
    ApiServiceProxies.HinhAnhServiceProxy,
    ApiServiceProxies.KhachHangServiceProxy,
    ApiServiceProxies.LoaiKhachHangServiceProxy,
    ApiServiceProxies.NhanVienServiceProxy,
    ApiServiceProxies.NhanXetDanhGiaServiceProxy,
    ApiServiceProxies.ThongKeServiceProxy,
    { provide: HTTP_INTERCEPTORS, useClass: AbpHttpInterceptor, multi: true },
  ],
})
export class ServiceProxyModule {}
