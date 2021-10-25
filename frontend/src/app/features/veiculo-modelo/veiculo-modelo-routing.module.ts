import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { VeiculoModeloListComponent } from "./veiculo-modelo-list.component";

const routes: Routes = [
  {
    path: "",
    component: VeiculoModeloListComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VeiculoModeloRoutingModule {}
