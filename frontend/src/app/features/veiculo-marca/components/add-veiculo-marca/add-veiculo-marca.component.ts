import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

import { BsModalRef } from "ngx-bootstrap/modal";
import { ToastrService } from "ngx-toastr";

import { VeiculoMarca } from "../../shared/models/veiculo-marca";
import { VeiculoMarcaService } from "../../shared/services/veiculo-marca.service";

@Component({
  selector: "app-add-veiculo-marca",
  templateUrl: "./add-veiculo-marca.component.html",
})
export class AddVeiculoMarcaComponent implements OnInit {
  public form!: FormGroup;
  public veiculoMarca!: VeiculoMarca;
  public errors: string[] = [];
  public submitting: boolean = false;

  constructor(private veiculoMarcaService: VeiculoMarcaService, private formBuilder: FormBuilder, private bsModalRef: BsModalRef, private toastrService: ToastrService) {}

  public ngOnInit(): void {
    this.form = this.formBuilder.group({
      nome: ["", Validators.required],
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
    this.veiculoMarcaService.add(this.veiculoMarca).subscribe(
      () => {
        this.toastrService.info("Marca de Veículo adicionada com sucesso, aguarde alguns segundos e ela estará na listagem.");
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
