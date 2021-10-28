import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";

import { BsModalRef } from "ngx-bootstrap/modal";
import { ToastrService } from "ngx-toastr";

import { masks } from "src/app/shared/utils/masks";
import { Veiculo } from "../../shared/models/veiculo";
import { VeiculoService } from "../../shared/services/veiculo.service";

@Component({
  selector: "app-add-veiculo",
  templateUrl: "./add-veiculo.component.html",
})
export class AddVeiculoComponent implements OnInit {
  public form!: FormGroup;
  public veiculo!: Veiculo;
  public errors: string[] = [];
  public submitting: boolean = false;
  public plateMask: string = masks.placa;

  constructor(private veiculoService: VeiculoService, private formBuilder: FormBuilder, private bsModalRef: BsModalRef, private toastrService: ToastrService) {}

  public ngOnInit(): void {
    this.form = this.formBuilder.group({
      placa: ["", Validators.required],
      dataAquisicao: ["", Validators.required],
      valorAquisicao: ["", Validators.required],
      veiculoModelo: [null, Validators.required],
    });
  }

  public get placa() {
    return this.form.get("placa");
  }

  public get dataAquisicao() {
    return this.form.get("dataAquisicao");
  }

  public get valorAquisicao() {
    return this.form.get("valorAquisicao");
  }

  public get veiculoModelo() {
    return this.form.get("veiculoModelo") as FormControl;
  }

  public confirm(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.submitting = true;
    this.veiculo = Object.assign({}, this.veiculo, this.form.value);
    this.veiculo.veiculoModeloId = this.veiculo.veiculoModelo.id;
    this.veiculoService.add(this.veiculo).subscribe(
      () => {
        this.toastrService.info("Veiculo adicionado com sucesso, aguarde alguns segundos e ele estará na listagem.");
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
