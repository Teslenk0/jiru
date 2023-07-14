import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {map} from 'rxjs/operators';
import {RespuestaAPI, Proyecto} from '../../types';
import {environment} from '../../../environments/environment';


@Injectable({
  providedIn: 'root',
})
export class ProyectoService {

  private apiUrl: string;

  constructor(private _httpClient: HttpClient) {
    this.apiUrl = environment.apiUrl;
  }

  crearProyecto = (proyecto: Proyecto) =>
    this._httpClient
      .post<RespuestaAPI>(`${this.apiUrl}/proyecto`, proyecto)
      .pipe(map((res: RespuestaAPI) => res));

  editarProyecto = (idProyecto: string, proyecto: Proyecto) =>
    this._httpClient
      .put<RespuestaAPI>(`${this.apiUrl}/proyecto/${idProyecto}`, proyecto)
      .pipe(map((res: RespuestaAPI) => res));

  asignarUsuario = (idProyecto: string, correoElectronico: string, rol: string) =>
    this._httpClient
      .put<RespuestaAPI>(`${this.apiUrl}/proyecto/${idProyecto}/${rol}/${correoElectronico}`, {})
      .pipe(map((res: RespuestaAPI) => res));

  desasignarUsuario = (idProyecto: string, correoElectronico: string, rol: string) =>
    this._httpClient
      .delete<RespuestaAPI>(`${this.apiUrl}/proyecto/${idProyecto}/${rol}/${correoElectronico}`)
      .pipe(map((res: RespuestaAPI) => res));

  obtenerProyectos = () =>
    this._httpClient
      .get<RespuestaAPI>(`${this.apiUrl}/proyecto`)
      .pipe(map((res: RespuestaAPI) => res));

  eliminarProyecto = (idProyecto: number) =>
    this._httpClient
      .delete<RespuestaAPI>(`${this.apiUrl}/proyecto/${idProyecto}`)
      .pipe(map((res: RespuestaAPI) => res));

  obtenerProyectoPorId = (idProyecto: string) =>
    this._httpClient
      .get<RespuestaAPI>(`${this.apiUrl}/proyecto/${idProyecto}`)
      .pipe(map((res: RespuestaAPI) => res));
}
