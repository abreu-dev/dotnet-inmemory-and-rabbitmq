import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { AddVeiculoModeloComponent } from "./components/add-veiculo-modelo/add-veiculo-modelo.component";
import { UpdateVeiculoModeloComponent } from "./components/update-veiculo-modelo/update-veiculo-modelo.component";
import { ViewVeiculoModeloComponent } from "./components/view-veiculo-modelo/view-veiculo-modelo.component";
import { RemoveVeiculoModeloComponent } from "./components/remove-veiculo-modelo/remove-veiculo-modelo.component";
import { VeiculoModeloService } from "./shared/services/veiculo-modelo.service";
import { VeiculoModeloListComponent } from "./veiculo-modelo-list.component";
import { VeiculoModeloRoutingModule } from "./veiculo-modelo-routing.module";
import { VeiculoMarcaModule } from "../veiculo-marca/veiculo-marca.module";

@NgModule({
  declarations: [VeiculoModeloListComponent, AddVeiculoModeloComponent, UpdateVeiculoModeloComponent, ViewVeiculoModeloComponent, RemoveVeiculoModeloComponent],
  imports: [CommonModule, RouterModule, VeiculoModeloRoutingModule, FormsModule, ReactiveFormsModule, VeiculoMarcaModule],
  providers: [VeiculoModeloService],
})
export class VeiculoModeloModule {}
