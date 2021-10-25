import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

import { Observable } from "rxjs";

import { environment } from "src/environments/environment";

import { VeiculoMarca } from "../models/veiculo-marca";

@Injectable()
export class VeiculoMarcaService {
  constructor(private http: HttpClient) {}

  public getAll(): Observable<VeiculoMarca[]> {
    return this.http.get<VeiculoMarca[]>(`${environment.apiUrl}/veiculo-marca`);
  }

  public getById(id: string): Observable<VeiculoMarca> {
    return this.http.get<VeiculoMarca>(`${environment.apiUrl}/veiculo-marca/${id}`);
  }

  public add(veiculo: VeiculoMarca): Observable<any> {
    return this.http.post(`${environment.apiUrl}/veiculo-marca`, veiculo);
  }

  public update(veiculo: VeiculoMarca): Observable<any> {
    return this.http.put(`${environment.apiUrl}/veiculo-marca`, veiculo);
  }

  public remove(id: string): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/veiculo-marca/${id}`);
  }
}
