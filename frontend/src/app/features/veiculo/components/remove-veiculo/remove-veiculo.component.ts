import { Component } from "@angular/core";

import { BsModalRef } from "ngx-bootstrap/modal";
import { ToastrService } from "ngx-toastr";

import { masks } from "src/app/shared/utils/masks";
import { Veiculo } from "../../shared/models/veiculo";
import { VeiculoService } from "../../shared/services/veiculo.service";

@Component({
  selector: "app-remove-veiculo",
  templateUrl: "./remove-veiculo.component.html",
})
export class RemoveVeiculoComponent {
  public veiculo!: Veiculo;
  public errors: string[] = [];
  public submitting: boolean = false;
  public plateMask: string = masks.placa;

  constructor(private veiculoService: VeiculoService, private bsModalRef: BsModalRef, private toastrService: ToastrService) {}

  public confirm(): void {
    this.submitting = true;
    this.veiculoService.remove(this.veiculo.id).subscribe(
      () => {
        this.toastrService.info("Veiculo removido com sucesso, aguarde alguns segundos e ele será removido.");
        this.close();
      },
      (failure) => {
        this.errors = failure.errors;
        this.submitting = false;
      }
    );
  }

  public close(): void {
    this.bsModalRef.hide();
  }
}
