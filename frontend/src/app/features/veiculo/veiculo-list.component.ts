import { Component, OnDestroy, OnInit } from "@angular/core";
import { BsModalService } from "ngx-bootstrap/modal";

import { Subscription, timer } from "rxjs";
import { map } from "rxjs/operators";

import { masks } from "src/app/shared/utils/masks";
import { Veiculo } from "./shared/models/veiculo";
import { VeiculoService } from "./shared/services/veiculo.service";
import { AddVeiculoComponent } from "./components/add-veiculo/add-veiculo.component";
import { UpdateVeiculoComponent } from "./components/update-veiculo/update-veiculo.component";
import { ViewVeiculoComponent } from "./components/view-veiculo/view-veiculo.component";
import { RemoveVeiculoComponent } from "./components/remove-veiculo/remove-veiculo.component";

@Component({
  selector: "app-veiculo-list",
  templateUrl: "./veiculo-list.component.html",
  styleUrls: ["./veiculo-list.component.css"],
})
export class VeiculoListComponent implements OnInit, OnDestroy {
  public veiculos: Veiculo[] = [];
  public timerSubscription!: Subscription;
  public plateMask: string = masks.placa;

  constructor(private veiculoService: VeiculoService, private modalService: BsModalService) {}

  public ngOnInit(): void {
    this.timerSubscription = timer(0, 5000)
      .pipe(
        map(() => {
          this.loadVeiculos();
        })
      )
      .subscribe();
  }

  public showAddVeiculoModal(): void {
    this.modalService.show(AddVeiculoComponent);
  }

  public showUpdateVeiculoModal(veiculo: Veiculo): void {
    this.modalService.show(UpdateVeiculoComponent, {
      initialState: {
        veiculo: veiculo,
      },
    });
  }

  public showViewVeiculoModal(veiculo: Veiculo): void {
    this.modalService.show(ViewVeiculoComponent, {
      initialState: {
        veiculo: veiculo,
      },
    });
  }

  public showRemoveVeiculoModal(veiculo: Veiculo): void {
    this.modalService.show(RemoveVeiculoComponent, {
      initialState: {
        veiculo: veiculo,
      },
    });
  }

  public loadVeiculos(): void {
    this.veiculoService.getAll().subscribe((veiculos) => {
      this.veiculos = veiculos;
    });
  }

  public ngOnDestroy(): void {
    this.timerSubscription.unsubscribe();
  }
}
