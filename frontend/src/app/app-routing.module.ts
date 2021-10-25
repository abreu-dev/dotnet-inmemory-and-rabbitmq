import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

const routes: Routes = [
  {
    path: "",
    redirectTo: "/home",
    pathMatch: "full",
  },
  {
    path: "home",
    loadChildren: () => import("./features/home/home.module").then((m) => m.HomeModule),
  },
  {
    path: "veiculos",
    loadChildren: () => import("./features/veiculo/veiculo.module").then((m) => m.VeiculoModule),
  },
  {
    path: "marcas-de-veiculo",
    loadChildren: () => import("./features/veiculo-marca/veiculo-marca.module").then((m) => m.VeiculoMarcaModule),
  },
  {
    path: "modelos-de-veiculo",
    loadChildren: () => import("./features/veiculo-modelo/veiculo-modelo.module").then((m) => m.VeiculoModeloModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
