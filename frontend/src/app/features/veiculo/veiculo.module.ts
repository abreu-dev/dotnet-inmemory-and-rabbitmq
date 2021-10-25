import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { AddVeiculoComponent } from "./components/add-veiculo/add-veiculo.component";
import { UpdateVeiculoComponent } from "./components/update-veiculo/update-veiculo.component";
import { ViewVeiculoComponent } from "./components/view-veiculo/view-veiculo.component";
import { RemoveVeiculoComponent } from "./components/remove-veiculo/remove-veiculo.component";
import { VeiculoService } from "./shared/services/veiculo.service";
import { VeiculoListComponent } from "./veiculo-list.component";
import { VeiculoRoutingModule } from "./veiculo-routing.module";
import { NgxMaskModule } from "ngx-mask";

@NgModule({
  declarations: [VeiculoListComponent, AddVeiculoComponent, UpdateVeiculoComponent, ViewVeiculoComponent, RemoveVeiculoComponent],
  imports: [CommonModule, RouterModule, VeiculoRoutingModule, FormsModule, ReactiveFormsModule, NgxMaskModule.forChild()],
  providers: [VeiculoService],
})
export class VeiculoModule {}
