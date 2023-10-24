import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { AccountComponent } from "./account.component";
import { DieukhoanComponent } from "@app/dieukhoanchinhsach/dieukhoan/dieukhoan.component";

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: "",
        component: AccountComponent,
        children: [
          { path: "login", component: LoginComponent },
          {
            path: "chinhsachdieukhoan",
            component: DieukhoanComponent,
          },
          { path: "register", component: RegisterComponent },
        ],
      },
    ]),
  ],
  exports: [RouterModule],
})
export class AccountRoutingModule {}
