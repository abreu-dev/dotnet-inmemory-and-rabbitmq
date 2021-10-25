import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { AddVeiculoMarcaComponent } from "./components/add-veiculo-marca/add-veiculo-marca.component";
import { UpdateVeiculoMarcaComponent } from "./components/update-veiculo-marca/update-veiculo-marca.component";
import { ViewVeiculoMarcaComponent } from "./components/view-veiculo-marca/view-veiculo-marca.component";
import { RemoveVeiculoMarcaComponent } from "./components/remove-veiculo-marca/remove-veiculo-marca.component";
import { VeiculoMarcaService } from "./shared/services/veiculo-marca.service";
import { VeiculoMarcaListComponent } from "./veiculo-marca-list.component";
import { VeiculoMarcaRoutingModule } from "./veiculo-marca-routing.module";

@NgModule({
  declarations: [VeiculoMarcaListComponent, AddVeiculoMarcaComponent, UpdateVeiculoMarcaComponent, ViewVeiculoMarcaComponent, RemoveVeiculoMarcaComponent],
  imports: [CommonModule, RouterModule, VeiculoMarcaRoutingModule, FormsModule, ReactiveFormsModule],
  providers: [VeiculoMarcaService],
})
export class VeiculoMarcaModule {}
