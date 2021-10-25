import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

import { Observable } from "rxjs";

import { environment } from "src/environments/environment";

import { Veiculo } from "../models/veiculo";

@Injectable()
export class VeiculoService {
  constructor(private http: HttpClient) {}

  public getAll(): Observable<Veiculo[]> {
    return this.http.get<Veiculo[]>(`${environment.apiUrl}/veiculo`);
  }

  public getById(id: string): Observable<Veiculo> {
    return this.http.get<Veiculo>(`${environment.apiUrl}/veiculo/${id}`);
  }

  public add(veiculo: Veiculo): Observable<any> {
    return this.http.post(`${environment.apiUrl}/veiculo`, veiculo);
  }

  public update(veiculo: Veiculo): Observable<any> {
    return this.http.put(`${environment.apiUrl}/veiculo`, veiculo);
  }

  public remove(id: string): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/veiculo/${id}`);
  }
}
