import { Component } from "@angular/core";

import { BsModalRef } from "ngx-bootstrap/modal";
import { ToastrService } from "ngx-toastr";

import { VeiculoMarca } from "../../shared/models/veiculo-marca";
import { VeiculoMarcaService } from "../../shared/services/veiculo-marca.service";

@Component({
  selector: "app-remove-veiculo-marca",
  templateUrl: "./remove-veiculo-marca.component.html",
})
export class RemoveVeiculoMarcaComponent {
  public veiculoMarca!: VeiculoMarca;
  public errors: string[] = [];
  public submitting: boolean = false;

  constructor(private veiculoMarcaService: VeiculoMarcaService, private bsModalRef: BsModalRef, private toastrService: ToastrService) {}

  public confirm(): void {
    this.submitting = true;
    this.veiculoMarcaService.remove(this.veiculoMarca.id).subscribe(
      () => {
        this.toastrService.info("Marca de Veículo removida com sucesso, aguarde alguns segundos e ela será removida.");
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
