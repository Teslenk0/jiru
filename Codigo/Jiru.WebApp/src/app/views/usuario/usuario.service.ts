import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {map} from 'rxjs/operators';
import {RespuestaAPI, Usuario} from '../../types';
import {environment} from '../../../environments/environment';


@Injectable({
  providedIn: 'root',
})
export class UsuarioService {

  private apiUrl: string;

  constructor(private _httpClient: HttpClient) {
    this.apiUrl = environment.apiUrl;
  }

  crearUsuario = (usuario: Usuario) =>
    this._httpClient
      .post<RespuestaAPI>(`${this.apiUrl}/usuario/${usuario.rol.toLowerCase()}`, usuario)
      .pipe(map((res: RespuestaAPI) => res));

  obtenerBugsResueltosPorUsuario = (correoElectronico: string) =>
    this._httpClient.get<RespuestaAPI>(`${this.apiUrl}/bug/desarrollador/${correoElectronico}/resuelto`)
      .pipe(map((res: RespuestaAPI) => res));
}
