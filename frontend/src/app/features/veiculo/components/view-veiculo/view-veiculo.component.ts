import { Component } from "@angular/core";

import { BsModalRef } from "ngx-bootstrap/modal";

import { masks } from "src/app/shared/utils/masks";
import { Veiculo } from "../../shared/models/veiculo";

@Component({
  selector: "app-view-veiculo",
  templateUrl: "./view-veiculo.component.html",
})
export class ViewVeiculoComponent {
  public veiculo!: Veiculo;
  public plateMask: string = masks.placa;

  constructor(private bsModalRef: BsModalRef) {}

  public close(): void {
    this.bsModalRef.hide();
  }
}
