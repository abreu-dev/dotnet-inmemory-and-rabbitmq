import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { VeiculoListComponent } from "./veiculo-list.component";

const routes: Routes = [
  {
    path: "",
    component: VeiculoListComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VeiculoRoutingModule {}
