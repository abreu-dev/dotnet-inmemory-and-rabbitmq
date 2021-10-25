import { Component } from "@angular/core";

import { BsModalRef } from "ngx-bootstrap/modal";

import { VeiculoModelo } from "../../shared/models/veiculo-modelo";

@Component({
  selector: "app-view-veiculo-modelo",
  templateUrl: "./view-veiculo-modelo.component.html",
})
export class ViewVeiculoModeloComponent {
  public veiculoModelo!: VeiculoModelo;

  constructor(private bsModalRef: BsModalRef) {}

  public close(): void {
    this.bsModalRef.hide();
  }
}
