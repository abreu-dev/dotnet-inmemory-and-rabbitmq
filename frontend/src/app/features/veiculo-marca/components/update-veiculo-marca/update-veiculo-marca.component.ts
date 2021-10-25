import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

import { BsModalRef } from "ngx-bootstrap/modal";
import { ToastrService } from "ngx-toastr";

import { VeiculoMarca } from "../../shared/models/veiculo-marca";
import { VeiculoMarcaService } from "../../shared/services/veiculo-marca.service";

@Component({
  selector: "app-update-veiculo-marca",
  templateUrl: "./update-veiculo-marca.component.html",
})
export class UpdateVeiculoMarcaComponent implements OnInit {
  public form!: FormGroup;
  public veiculoMarca!: VeiculoMarca;
  public errors: string[] = [];
  public submitting: boolean = false;

  constructor(private veiculoMarcaService: VeiculoMarcaService, private formBuilder: FormBuilder, private bsModalRef: BsModalRef, private toastrService: ToastrService) {}

  public ngOnInit(): void {
    this.form = this.formBuilder.group({
      id: [this.veiculoMarca.id],
      nome: [this.veiculoMarca.nome, Validators.required],
    });
  }

  public get nome() {
    return this.form.get("nome");
  }

  public confirm(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.submitting = true;
    this.veiculoMarca = Object.assign({}, this.veiculoMarca, this.form.value);
    this.veiculoMarcaService.update(this.veiculoMarca).subscribe(
      () => {
        this.toastrService.info("Marca de Veículo atualizada com sucesso, aguarde alguns segundos e ela será atualizada.");
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
