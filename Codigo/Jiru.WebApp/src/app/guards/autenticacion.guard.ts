import {Injectable} from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateChild,
  CanLoad,
  Route,
  Router,
  RouterStateSnapshot,
  UrlSegment,
  UrlTree
} from '@angular/router';
import {Observable, of} from 'rxjs';
import {AutenticacionService} from '../autenticacion.service';
import {switchMap} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AutenticacionGuard implements CanActivate, CanActivateChild, CanLoad {

  constructor(
    private _autenticacionService: AutenticacionService,
    private _router: Router
  ) {
  }

  private _check(): Observable<boolean> {
    return this._autenticacionService.estaAutenticado()
      .pipe(
        switchMap((autenticado) => {
          if (!autenticado) {
            this._router.navigate(['iniciar-sesion']);
            return of(false);
          }

          return of(true);
        })
      );
  }


  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    return this._check();
  }

  canActivateChild(childRoute: ActivatedRouteSnapshot,
                   state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this._check();
  }

  canLoad(route: Route, segments: UrlSegment[]): Observable<boolean> | Promise<boolean> | boolean {
    return this._check();
  }
}
