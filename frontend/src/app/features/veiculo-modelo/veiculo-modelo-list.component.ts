import { Component, OnDestroy, OnInit } from "@angular/core";
import { BsModalService } from "ngx-bootstrap/modal";

import { Subscription, timer } from "rxjs";
import { map } from "rxjs/operators";

import { VeiculoModelo } from "./shared/models/veiculo-modelo";
import { VeiculoModeloService } from "./shared/services/veiculo-modelo.service";
import { AddVeiculoModeloComponent } from "./components/add-veiculo-modelo/add-veiculo-modelo.component";
import { UpdateVeiculoModeloComponent } from "./components/update-veiculo-modelo/update-veiculo-modelo.component";
import { ViewVeiculoModeloComponent } from "./components/view-veiculo-modelo/view-veiculo-modelo.component";
import { RemoveVeiculoModeloComponent } from "./components/remove-veiculo-modelo/remove-veiculo-modelo.component";

@Component({
  selector: "app-veiculo-modelo-list",
  templateUrl: "./veiculo-modelo-list.component.html",
  styleUrls: ["./veiculo-modelo-list.component.css"],
})
export class VeiculoModeloListComponent implements OnInit, OnDestroy {
  public veiculoModelos: VeiculoModelo[] = [];
  public timerSubscription!: Subscription;

  constructor(private veiculoModeloService: VeiculoModeloService, private modalService: BsModalService) {}

  public ngOnInit(): void {
    this.timerSubscription = timer(0, 5000)
      .pipe(
        map(() => {
          this.loadVeiculoModelos();
        })
      )
      .subscribe();
  }

  public showAddVeiculoModeloModal(): void {
    this.modalService.show(AddVeiculoModeloComponent);
  }

  public showUpdateVeiculoModeloModal(veiculoModelo: VeiculoModelo): void {
    this.modalService.show(UpdateVeiculoModeloComponent, {
      initialState: {
        veiculoModelo: veiculoModelo,
      },
    });
  }

  public showViewVeiculoModeloModal(veiculoModelo: VeiculoModelo): void {
    this.modalService.show(ViewVeiculoModeloComponent, {
      initialState: {
        veiculoModelo: veiculoModelo,
      },
    });
  }

  public showRemoveVeiculoModeloModal(veiculoModelo: VeiculoModelo): void {
    this.modalService.show(RemoveVeiculoModeloComponent, {
      initialState: {
        veiculoModelo: veiculoModelo,
      },
    });
  }

  public loadVeiculoModelos(): void {
    this.veiculoModeloService.getAll().subscribe((veiculoModelos) => {
      this.veiculoModelos = veiculoModelos;
    });
  }

  public ngOnDestroy(): void {
    this.timerSubscription.unsubscribe();
  }
}
