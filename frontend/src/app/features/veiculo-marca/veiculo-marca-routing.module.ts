import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { VeiculoMarcaListComponent } from "./veiculo-marca-list.component";

const routes: Routes = [
  {
    path: "",
    component: VeiculoMarcaListComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VeiculoMarcaRoutingModule {}
