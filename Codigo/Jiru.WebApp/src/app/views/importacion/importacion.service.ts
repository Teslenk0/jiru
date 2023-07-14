import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {map} from 'rxjs/operators';
import {Importador, RespuestaAPI} from '../../types';
import {environment} from '../../../environments/environment';


@Injectable({
  providedIn: 'root',
})
export class ImportacionService {

  private apiUrl: string;

  constructor(private _httpClient: HttpClient) {
    this.apiUrl = environment.apiUrl;
  }

  obtenerImportadoresDisponibles = () =>
    this._httpClient
      .get<RespuestaAPI>(`${this.apiUrl}/importacion/disponible`)
      .pipe(map((res: RespuestaAPI) => res));

  importarBugs = (importador: Importador) =>
    this._httpClient
      .post<RespuestaAPI>(`${this.apiUrl}/importacion`, importador)
      .pipe(map((res: RespuestaAPI) => res));
}
