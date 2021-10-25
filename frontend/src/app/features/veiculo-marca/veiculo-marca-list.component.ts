import { Component, OnDestroy, OnInit } from "@angular/core";
import { BsModalService } from "ngx-bootstrap/modal";

import { Subscription, timer } from "rxjs";
import { map } from "rxjs/operators";

import { VeiculoMarca } from "./shared/models/veiculo-marca";
import { VeiculoMarcaService } from "./shared/services/veiculo-marca.service";
import { AddVeiculoMarcaComponent } from "./components/add-veiculo-marca/add-veiculo-marca.component";
import { UpdateVeiculoMarcaComponent } from "./components/update-veiculo-marca/update-veiculo-marca.component";
import { ViewVeiculoMarcaComponent } from "./components/view-veiculo-marca/view-veiculo-marca.component";
import { RemoveVeiculoMarcaComponent } from "./components/remove-veiculo-marca/remove-veiculo-marca.component";

@Component({
  selector: "app-veiculo-marca-list",
  templateUrl: "./veiculo-marca-list.component.html",
  styleUrls: ["./veiculo-marca-list.component.css"],
})
export class VeiculoMarcaListComponent implements OnInit, OnDestroy {
  public veiculoMarcas: VeiculoMarca[] = [];
  public timerSubscription!: Subscription;

  constructor(private veiculoMarcaService: VeiculoMarcaService, private modalService: BsModalService) {}

  public ngOnInit(): void {
    this.timerSubscription = timer(0, 5000)
      .pipe(
        map(() => {
          this.loadVeiculoMarcas();
        })
      )
      .subscribe();
  }

  public showAddVeiculoMarcaModal(): void {
    this.modalService.show(AddVeiculoMarcaComponent);
  }

  public showUpdateVeiculoMarcaModal(veiculoMarca: VeiculoMarca): void {
    this.modalService.show(UpdateVeiculoMarcaComponent, {
      initialState: {
        veiculoMarca: veiculoMarca,
      },
    });
  }

  public showViewVeiculoMarcaModal(veiculoMarca: VeiculoMarca): void {
    this.modalService.show(ViewVeiculoMarcaComponent, {
      initialState: {
        veiculoMarca: veiculoMarca,
      },
    });
  }

  public showRemoveVeiculoMarcaModal(veiculoMarca: VeiculoMarca): void {
    this.modalService.show(RemoveVeiculoMarcaComponent, {
      initialState: {
        veiculoMarca: veiculoMarca,
      },
    });
  }

  public loadVeiculoMarcas(): void {
    this.veiculoMarcaService.getAll().subscribe((veiculoMarcas) => {
      this.veiculoMarcas = veiculoMarcas;
    });
  }

  public ngOnDestroy(): void {
    this.timerSubscription.unsubscribe();
  }
}
