import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {map} from 'rxjs/operators';
import {RespuestaAPI, Usuario} from './types';
import {environment} from '../environments/environment';
import {Observable, of} from 'rxjs';
import {Router} from '@angular/router';


@Injectable({
  providedIn: 'root',
})
export class AutenticacionService {

  private apiUrl: string;

  constructor(private _httpClient: HttpClient, private router: Router) {
    this.apiUrl = environment.apiUrl;
  }

  set accessToken(token: string) {
    localStorage.setItem('access_token', token);
  }

  get accessToken(): string {
    return localStorage.getItem('access_token');
  }

  set usuarioLogeado(usuario: Usuario) {
    localStorage.setItem('usuario_logeado', JSON.stringify(usuario));
  }

  get usuarioLogeado(): Usuario {
    return JSON.parse(localStorage.getItem('usuario_logeado'));
  }

  cerrarSesion = () => {
    this.usuarioLogeado = null;
    this.accessToken = null;
    this.router.navigate(['iniciar-sesion']);
  };

  iniciarSesion = (correoElectronico: string, contrasena: string) =>
    this._httpClient
      .post<RespuestaAPI>(`${this.apiUrl}/autenticacion/acceder`, {
        correoElectronico,
        contrasena
      })
      .pipe(map((res: RespuestaAPI) => {
        this.accessToken = res.resultado.token;
        this.usuarioLogeado = res.resultado.usuario;
        return res;
      }));


  estaAutenticado(): Observable<boolean> {
    if (this.accessToken && this.usuarioLogeado) {
      return of(true);
    }
    return of(false);
  }

}
