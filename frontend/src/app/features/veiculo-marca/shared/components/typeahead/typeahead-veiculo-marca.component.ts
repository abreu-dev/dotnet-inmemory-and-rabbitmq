import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { AbstractControl, FormControl } from "@angular/forms";
import { NgbTypeaheadSelectItemEvent } from "@ng-bootstrap/ng-bootstrap";

import { Observable, Subscription, timer } from "rxjs";
import { debounceTime, distinctUntilChanged, map } from "rxjs/operators";
import { VeiculoMarca } from "../../models/veiculo-marca";
import { VeiculoMarcaService } from "../../services/veiculo-marca.service";

@Component({
  selector: "app-typeahead-veiculo-marca",
  templateUrl: "./typeahead-veiculo-marca.component.html",
})
export class TypeaheadVeiculoMarcaComponent implements OnInit {
  @Input()
  public control!: FormControl;
  public model: any;
  public veiculoMarcas: VeiculoMarca[] = [];

  constructor(private veiculoMarcaService: VeiculoMarcaService) {}

  public ngOnInit(): void {
    this.veiculoMarcaService.getAll().subscribe((veiculoMarcas) => {
      this.veiculoMarcas = veiculoMarcas;
    });
  }

  public search = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      map((term) => (term === "" ? this.veiculoMarcas : this.veiculoMarcas.filter((v) => v.nome.toLowerCase().indexOf(term.toLowerCase()) > -1)).slice(0, 10))
    );

  public formatter = (x: { nome: string }) => x.nome;

  public setModel(e: NgbTypeaheadSelectItemEvent) {
    this.control?.setValue(e.item);
  }
}
