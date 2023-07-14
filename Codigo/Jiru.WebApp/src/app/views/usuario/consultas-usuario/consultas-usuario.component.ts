import {HttpErrorResponse} from '@angular/common/http';
import {Component, OnDestroy} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Alerta, RespuestaAPI, Usuario} from '../../../types';
import {UsuarioService} from '../usuario.service';
import {ActivatedRoute} from '@angular/router';
import {Subscription} from 'rxjs';
import { Router } from '@angular/router';

@Component({
  templateUrl: 'consultas-usuario.component.html',
})
export class ConsultasUsuarioComponent implements OnDestroy {
  public formUsuario: FormGroup;
  public alertas: Alerta[];
  public usuario: Usuario;
  private subscripcionCrear: Subscription;

  constructor(
    private route: ActivatedRoute,
    public router: Router,
    private _usuarioService: UsuarioService) {
    this.formUsuario = new FormGroup({
      correoElectronico: new FormControl('', [Validators.required, Validators.email]),
    });

    this.alertas = [];
  }

  ngOnDestroy(): void {
    if (this.subscripcionCrear) {
      this.subscripcionCrear.unsubscribe();
    }
  }

  public enviarForm() {
    this.resetAlertas();

    const esValido = this.formUsuario.valid;

    if (esValido) {
      const datos = this.formUsuario.value.correoElectronico;

      this.subscripcionCrear = this._usuarioService
        .obtenerBugsResueltosPorUsuario(datos)
        .subscribe(
          (obs: RespuestaAPI) => {
            if (obs.exito && (obs.codigoEstado === 201 || obs.codigoEstado === 200)) {
              this.resetForm(false);
              this.generarAlerta(`El usuario ${datos} resolvió ${obs.resultado} bugs`, 'exito');
            }
          },
          (err: HttpErrorResponse) => {
            if (err.error && err.error.Mensaje) {
              this.generarAlerta(err.error.Mensaje, 'error');
            } else {
              this.generarAlerta('Ha ocurrido un error inesperado', 'error');
            }
          }
        );
    } else {
      this.generarAlerta('Por favor, revisa los campos ingresados', 'error');
    }
  }

  public resetForm(resetAlertas: boolean = true) {
    this.formUsuario.reset();
    if (resetAlertas) {
      this.resetAlertas();
    }

  }

  public cancelarForm(){
    this.router.navigate(['/proyecto/listado']);
  }

  private resetAlertas() {
    this.alertas = [];
  }

  private generarAlerta(mensaje: string, tipo: string) {
    if (tipo === 'error') {
      this.alertas.push({
        mensaje: mensaje,
        cerrable: true,
        tipo: 'danger',
      });
    } else if (tipo === 'success') {
      this.alertas.push({
        mensaje: mensaje,
        cerrable: true,
        tipo: 'success',
      });
    } else {
      this.alertas.push({
        mensaje: mensaje,
        cerrable: true,
        tipo: 'info',
      });
    }
  }

}
