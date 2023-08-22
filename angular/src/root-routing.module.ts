import { NgModule } from "@angular/core";
import { Routes, RouterModule, ExtraOptions } from "@angular/router";

const routes: Routes = [
  { path: "", redirectTo: "/app/trangchu", pathMatch: "full" },
  {
    path: "account",
    loadChildren: () =>
      import("account/account.module").then((m) => m.AccountModule), // Lazy load account module
    data: { preload: true },
  },
  {
    path: "app",
    loadChildren: () => import("app/app.module").then((m) => m.AppModule), // Lazy load account module
    data: { preload: true },
  },
];
const routerOptions: ExtraOptions = {
  scrollPositionRestoration: "enabled",
  anchorScrolling: "enabled",
};
@NgModule({
  imports: [RouterModule.forRoot(routes, routerOptions)],
  exports: [RouterModule],
  providers: [],
})
export class RootRoutingModule {}
