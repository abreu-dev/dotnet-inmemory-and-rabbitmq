import { VeiculoMarca } from "../../../veiculo-marca/shared/models/veiculo-marca";

export interface VeiculoModelo {
  id: string;
  nome: string;
  veiculoMarca: VeiculoMarca;
  veiculoMarcaId: string;
}
