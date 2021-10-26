import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

import { AddVeiculoMarcaComponent } from "./components/add-veiculo-marca/add-veiculo-marca.component";
import { UpdateVeiculoMarcaComponent } from "./components/update-veiculo-marca/update-veiculo-marca.component";
import { ViewVeiculoMarcaComponent } from "./components/view-veiculo-marca/view-veiculo-marca.component";
import { RemoveVeiculoMarcaComponent } from "./components/remove-veiculo-marca/remove-veiculo-marca.component";
import { VeiculoMarcaService } from "./shared/services/veiculo-marca.service";
import { VeiculoMarcaListComponent } from "./veiculo-marca-list.component";
import { VeiculoMarcaRoutingModule } from "./veiculo-marca-routing.module";
import { TypeaheadVeiculoMarcaComponent } from "./shared/components/typeahead/typeahead-veiculo-marca.component";

@NgModule({
  declarations: [VeiculoMarcaListComponent, AddVeiculoMarcaComponent, UpdateVeiculoMarcaComponent, ViewVeiculoMarcaComponent, RemoveVeiculoMarcaComponent, TypeaheadVeiculoMarcaComponent],
  imports: [CommonModule, RouterModule, VeiculoMarcaRoutingModule, FormsModule, ReactiveFormsModule, NgbModule],
  providers: [VeiculoMarcaService],
  exports: [TypeaheadVeiculoMarcaComponent],
})
export class VeiculoMarcaModule {}
