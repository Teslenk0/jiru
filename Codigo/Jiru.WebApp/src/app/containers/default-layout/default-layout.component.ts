import {Component, OnInit} from '@angular/core';
import {navItems} from '../../_nav';
import {AutenticacionService} from '../../autenticacion.service';
import {Usuario} from '../../types';

@Component({
  selector: 'app-inicio',
  templateUrl: './default-layout.component.html'
})
export class DefaultLayoutComponent implements OnInit {
  public sidebarMinimized = false;
  public navItems = [];
  public usuarioLogeado: Usuario;

  constructor(public _autenticacionService: AutenticacionService) {
    this.navItems = navItems.filter(navItem => {
      if (navItem.attributes) {
        return navItem.attributes.roles.includes(_autenticacionService.usuarioLogeado.rol.toUpperCase());
      }
      return false;
    });
  }

  toggleMinimize(e) {
    this.sidebarMinimized = e;
  }

  ngOnInit() {
    this.usuarioLogeado = this._autenticacionService.usuarioLogeado;
  }

  cerrarSesion(): void {
    this._autenticacionService.cerrarSesion();

  }
}
