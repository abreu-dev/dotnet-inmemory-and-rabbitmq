import { Component } from "@angular/core";

import { BsModalRef } from "ngx-bootstrap/modal";

import { VeiculoMarca } from "../../shared/models/veiculo-marca";

@Component({
  selector: "app-view-veiculo-marca",
  templateUrl: "./view-veiculo-marca.component.html",
})
export class ViewVeiculoMarcaComponent {
  public veiculoMarca!: VeiculoMarca;

  constructor(private bsModalRef: BsModalRef) {}

  public close(): void {
    this.bsModalRef.hide();
  }
}
