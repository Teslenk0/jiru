import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {map} from 'rxjs/operators';
import {RespuestaAPI, Tarea} from '../../types';
import {environment} from '../../../environments/environment';


@Injectable({
  providedIn: 'root',
})
export class TareaService {

  private apiUrl: string;

  constructor(private _httpClient: HttpClient) {
    this.apiUrl = environment.apiUrl;
  }

  crearTarea = (tarea: Tarea) =>
    this._httpClient
      .post<RespuestaAPI>(`${this.apiUrl}/tarea`, tarea)
      .pipe(map((res: RespuestaAPI) => res));

  obtenerTareas = () =>
    this._httpClient
      .get<RespuestaAPI>(`${this.apiUrl}/tarea`)
      .pipe(map((res: RespuestaAPI) => res));
}
