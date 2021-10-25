import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

import { BsModalRef } from "ngx-bootstrap/modal";
import { ToastrService } from "ngx-toastr";

import { masks } from "src/app/shared/utils/masks";
import { Veiculo } from "../../shared/models/veiculo";
import { VeiculoService } from "../../shared/services/veiculo.service";

@Component({
  selector: "app-update-veiculo",
  templateUrl: "./update-veiculo.component.html",
})
export class UpdateVeiculoComponent implements OnInit {
  public form!: FormGroup;
  public veiculo!: Veiculo;
  public errors: string[] = [];
  public submitting: boolean = false;
  public plateMask: string = masks.placa;

  constructor(private veiculoService: VeiculoService, private formBuilder: FormBuilder, private bsModalRef: BsModalRef, private toastrService: ToastrService) {}

  public ngOnInit(): void {
    this.form = this.formBuilder.group({
      id: [this.veiculo.id],
      placa: [this.veiculo.placa, Validators.required],
    });
  }

  public get placa() {
    return this.form.get("placa");
  }

  public confirm(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.submitting = true;
    this.veiculo = Object.assign({}, this.veiculo, this.form.value);
    this.veiculoService.update(this.veiculo).subscribe(
      () => {
        this.toastrService.info("Veiculo atualizado com sucesso, aguarde alguns segundos e ele será atualizado.");
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
