import { VeiculoModelo } from "../../../veiculo-modelo/shared/models/veiculo-modelo";

export interface Veiculo {
  id: string;
  codigo: number;
  placa: string;
  veiculoModelo: VeiculoModelo;
  veiculoModeloId: string;
}
