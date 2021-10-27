import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { FormControl, Validators } from "@angular/forms";
import { NgbTypeaheadSelectItemEvent } from "@ng-bootstrap/ng-bootstrap";

import { Observable } from "rxjs";
import { debounceTime, distinctUntilChanged, map } from "rxjs/operators";
import { VeiculoModelo } from "../../models/veiculo-modelo";
import { VeiculoModeloService } from "../../services/veiculo-modelo.service";

@Component({
  selector: "app-typeahead-veiculo-modelo",
  templateUrl: "./typeahead-veiculo-modelo.component.html",
})
export class TypeaheadVeiculoModeloComponent implements OnInit {
  @Input()
  public control!: FormControl;
  public model: any;
  public veiculoModelos: VeiculoModelo[] = [];
  public isRequired: boolean = false;

  constructor(private veiculoModeloService: VeiculoModeloService) {}

  public ngOnInit(): void {
    this.veiculoModeloService.getAll().subscribe((veiculoModelos) => {
      this.veiculoModelos = veiculoModelos;
    });
    this.isRequired = this.control.hasValidator(Validators.required);
  }

  public search = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      map((term) => (term === "" ? this.veiculoModelos : this.veiculoModelos.filter((v) => v.nome.toLowerCase().indexOf(term.toLowerCase()) > -1)).slice(0, 10))
    );

  public formatter = (x: { nome: string }) => x.nome;

  public setModel(e: NgbTypeaheadSelectItemEvent) {
    this.control?.setValue(e.item);
  }
}
