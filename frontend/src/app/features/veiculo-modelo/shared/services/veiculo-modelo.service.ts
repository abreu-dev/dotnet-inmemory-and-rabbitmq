import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

import { Observable } from "rxjs";

import { environment } from "src/environments/environment";

import { VeiculoModelo } from "../models/veiculo-modelo";

@Injectable()
export class VeiculoModeloService {
  constructor(private http: HttpClient) {}

  public getAll(): Observable<VeiculoModelo[]> {
    return this.http.get<VeiculoModelo[]>(`${environment.apiUrl}/veiculo-modelo`);
  }

  public getById(id: string): Observable<VeiculoModelo> {
    return this.http.get<VeiculoModelo>(`${environment.apiUrl}/veiculo-modelo/${id}`);
  }

  public add(veiculo: VeiculoModelo): Observable<any> {
    return this.http.post(`${environment.apiUrl}/veiculo-modelo`, veiculo);
  }

  public update(veiculo: VeiculoModelo): Observable<any> {
    return this.http.put(`${environment.apiUrl}/veiculo-modelo`, veiculo);
  }

  public remove(id: string): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/veiculo-modelo/${id}`);
  }
}
