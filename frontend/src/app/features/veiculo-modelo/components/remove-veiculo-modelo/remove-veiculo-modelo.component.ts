import { Component } from "@angular/core";

import { BsModalRef } from "ngx-bootstrap/modal";
import { ToastrService } from "ngx-toastr";

import { VeiculoModelo } from "../../shared/models/veiculo-modelo";
import { VeiculoModeloService } from "../../shared/services/veiculo-modelo.service";

@Component({
  selector: "app-remove-veiculo-modelo",
  templateUrl: "./remove-veiculo-modelo.component.html",
})
export class RemoveVeiculoModeloComponent {
  public veiculoModelo!: VeiculoModelo;
  public errors: string[] = [];
  public submitting: boolean = false;

  constructor(private veiculoModeloService: VeiculoModeloService, private bsModalRef: BsModalRef, private toastrService: ToastrService) {}

  public confirm(): void {
    this.submitting = true;
    this.veiculoModeloService.remove(this.veiculoModelo.id).subscribe(
      () => {
        this.toastrService.info("Modelo de Veículo removido com sucesso, aguarde alguns segundos e ele será removido.");
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
