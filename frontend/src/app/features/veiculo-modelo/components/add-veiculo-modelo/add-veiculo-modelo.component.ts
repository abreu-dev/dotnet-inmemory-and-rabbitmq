import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

import { BsModalRef } from "ngx-bootstrap/modal";
import { ToastrService } from "ngx-toastr";

import { VeiculoModelo } from "../../shared/models/veiculo-modelo";
import { VeiculoModeloService } from "../../shared/services/veiculo-modelo.service";

@Component({
  selector: "app-add-veiculo-modelo",
  templateUrl: "./add-veiculo-modelo.component.html",
})
export class AddVeiculoModeloComponent implements OnInit {
  public form!: FormGroup;
  public veiculoModelo!: VeiculoModelo;
  public errors: string[] = [];
  public submitting: boolean = false;

  constructor(private veiculoModeloService: VeiculoModeloService, private formBuilder: FormBuilder, private bsModalRef: BsModalRef, private toastrService: ToastrService) {}

  public ngOnInit(): void {
    this.form = this.formBuilder.group({
      nome: ["", Validators.required],
      veiculoMarca: [null, Validators.required],
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
    this.veiculoModeloService.add(this.veiculoModelo).subscribe(
      () => {
        this.toastrService.info("Modelo de Veículo adicionado com sucesso, aguarde alguns segundos e ele estará na listagem.");
        this.close();
      },
      (failure) => {
        this.submitting = false;
        this.errors = failure.errors;
      }
    );
  }

  public close(): void {
    this.bsModalRef.hide();
  }
}
