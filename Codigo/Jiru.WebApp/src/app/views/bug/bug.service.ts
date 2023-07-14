import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Bug} from '../../types/bug.type';
import {map} from 'rxjs/operators';
import {Filtro, RespuestaAPI} from '../../types';
import {environment} from '../../../environments/environment';


@Injectable({
  providedIn: 'root',
})
export class BugService {

  private apiUrl: string;

  constructor(private _httpClient: HttpClient) {
    this.apiUrl = environment.apiUrl;
  }

  crearBug = (bug: Bug) =>
    this._httpClient
      .post<RespuestaAPI>(`${this.apiUrl}/bug`, bug)
      .pipe(map((res: RespuestaAPI) => res));

  editarBug = (idBug: string, bug: Bug) =>
    this._httpClient
      .put<RespuestaAPI>(`${this.apiUrl}/bug/${idBug}`, bug)
      .pipe(map((res: RespuestaAPI) => res));

  obtenerBugs = (filtros?: Filtro) =>
    this._httpClient
      .get<RespuestaAPI>(`${this.apiUrl}/bug`, {
        params: {
          ...filtros
        }
      })
      .pipe(map((res: RespuestaAPI) => res));

  eliminarBug = (idBug: number) =>
    this._httpClient
      .delete<RespuestaAPI>(`${this.apiUrl}/bug/${idBug}`)
      .pipe(map((res: RespuestaAPI) => res));

  obtenerBugPorId = (idBug: string) =>
    this._httpClient
      .get<RespuestaAPI>(`${this.apiUrl}/bug/${idBug}`)
      .pipe(map((res: RespuestaAPI) => res));
}
