import {Injectable} from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateChild,
  Router,
  RouterStateSnapshot,
  UrlTree
} from '@angular/router';
import {Observable, of} from 'rxjs';
import {AutenticacionService} from '../autenticacion.service';
import {navItems} from '../_nav';
import {ROLES} from '../constantes/constantes';

@Injectable({
  providedIn: 'root'
})
export class RolesGuard implements CanActivate, CanActivateChild {

  private readonly PATHS_REGEXS: object;

  constructor(
    private _autenticacionService: AutenticacionService,
    private _router: Router
  ) {

    this.PATHS_REGEXS = {};

    this.PATHS_REGEXS['\/proyecto\/([0-9]*)$'] = [ROLES.TESTER, ROLES.DESARROLLADOR, ROLES.ADMINISTRADOR];

    this.PATHS_REGEXS['\/bug\/([0-9]*)$'] = [ROLES.TESTER, ROLES.DESARROLLADOR, ROLES.ADMINISTRADOR];
  }

  private _check(ruta: string): Observable<boolean> {
    const usuarioLogeado = this._autenticacionService.usuarioLogeado;

    const navItem = navItems.find(item => item.url === ruta);

    if (navItem) {
      if (navItem.attributes && navItem.attributes.roles.includes(usuarioLogeado.rol.toUpperCase())) {
        return of(true);
      } else {
        return of(false);
      }
    } else {

      let puedeSeguir = false;

      Object.keys(this.PATHS_REGEXS).forEach((regexRuta) => {
        const regExp = new RegExp(regexRuta, 'gm');

        if (regExp.test(ruta)) {
          puedeSeguir = true;
        }
      });

      if (puedeSeguir) {
        return of(true);
      }
      this._router.navigate(['404']);
    }
  }


  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    return this._check(state.url);
  }

  canActivateChild(childRoute: ActivatedRouteSnapshot,
                   state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this._check(state.url);
  }
}
