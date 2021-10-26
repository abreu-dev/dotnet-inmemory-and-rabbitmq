import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

import { BsModalRef } from "ngx-bootstrap/modal";
import { ToastrService } from "ngx-toastr";

import { VeiculoModelo } from "../../shared/models/veiculo-modelo";
import { VeiculoModeloService } from "../../shared/services/veiculo-modelo.service";

@Component({
  selector: "app-update-veiculo-modelo",
  templateUrl: "./update-veiculo-modelo.component.html",
})
export class UpdateVeiculoModeloComponent implements OnInit {
  public form!: FormGroup;
  public veiculoModelo!: VeiculoModelo;
  public errors: string[] = [];
  public submitting: boolean = false;

  constructor(private veiculoModeloService: VeiculoModeloService, private formBuilder: FormBuilder, private bsModalRef: BsModalRef, private toastrService: ToastrService) {}

  public ngOnInit(): void {
    this.form = this.formBuilder.group({
      id: [this.veiculoModelo.id],
      nome: [this.veiculoModelo.nome, Validators.required],
      veiculoMarca: [this.veiculoModelo.veiculoMarca, Validators.required],
    });
  }

  public get nome() {
    return this.form.get("nome");
  }

  public get veiculoMarca() {
    return this.form.get("veiculoMarca");
  }

  public confirm(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.submitting = true;
    this.veiculoModelo = Object.assign({}, this.veiculoModelo, this.form.value);
    this.veiculoModelo.veiculoMarcaId = this.veiculoModelo.veiculoMarca.id;
    this.veiculoModeloService.update(this.veiculoModelo).subscribe(
      () => {
        this.toastrService.info("Modelo de Veículo atualizado com sucesso, aguarde alguns segundos e ele será atualizado.");
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
