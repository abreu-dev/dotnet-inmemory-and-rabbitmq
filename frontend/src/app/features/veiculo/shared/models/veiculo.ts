import { VeiculoModelo } from "../../../veiculo-modelo/shared/models/veiculo-modelo";

export interface Veiculo {
  id: string;
  codigo: number;
  placa: string;
  dataAquisicao: Date;
  valorAquisicao: number;
  veiculoModelo: VeiculoModelo;
  veiculoModeloId: string;
}
