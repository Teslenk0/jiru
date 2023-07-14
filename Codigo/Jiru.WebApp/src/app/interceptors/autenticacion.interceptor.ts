import {Injectable} from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {Router} from '@angular/router';
import {AutenticacionService} from '../autenticacion.service';

@Injectable()
export class AutenticacionInterceptor implements HttpInterceptor {

  constructor(private router: Router, private _autenticacionService: AutenticacionService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    let nuevaReq = req.clone();

    if (!this.router.url.includes('iniciar-sesion')
      && this._autenticacionService.accessToken && this._autenticacionService.usuarioLogeado) {
      nuevaReq = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + this._autenticacionService.accessToken)
      });
    }

    // Response
    return next.handle(nuevaReq).pipe(
      catchError((error) => {
        if (!this.router.url.includes('iniciar-sesion') && error instanceof HttpErrorResponse
          && (error.status === 401 || error.status === 403)) {

          this._autenticacionService.cerrarSesion();

          this.router.navigate(['/iniciar-sesion']);
        }

        return throwError(error);
      })
    );
  }
}
