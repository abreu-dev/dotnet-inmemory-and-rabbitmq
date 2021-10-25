import { VeiculoModelo } from "../../../veiculo-modelo/shared/models/veiculo-modelo";

export interface Veiculo {
  id: string;
  placa: string;
  veiculoModelo: VeiculoModelo;
}
