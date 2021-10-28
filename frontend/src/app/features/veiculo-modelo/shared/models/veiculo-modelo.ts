import { VeiculoMarca } from "../../../veiculo-marca/shared/models/veiculo-marca";

export interface VeiculoModelo {
  id: string;
  codigo: number;
  nome: string;
  veiculoMarca: VeiculoMarca;
  veiculoMarcaId: string;
}
