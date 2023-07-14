import {Component, OnDestroy} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Alerta, RespuestaAPI} from '../../types';
import {Subscription} from 'rxjs';
import {AutenticacionService} from '../../autenticacion.service';
import {HttpErrorResponse} from '@angular/common/http';
import {Router} from '@angular/router';

@Component({
  selector: 'app-inicio',
  templateUrl: 'iniciar-sesion.component.html'
})
export class IniciarSesionComponent implements OnDestroy {

  private subscripcionForm: Subscription;
  private subscripcionAcceder: Subscription;
  public formAcceder: FormGroup;
  public alertas: Alerta[];

  constructor(public _autenticacionService: AutenticacionService, private router: Router) {
    this.formAcceder = new FormGroup({
      correoElectronico: new FormControl('', [Validators.required, Validators.email]),
      contrasena: new FormControl('', [Validators.required, Validators.minLength(6)])
    });

    this.alertas = [];
  }

  ngOnDestroy(): void {
    if (this.subscripcionForm) {
      this.subscripcionForm.unsubscribe();
    }

    if (this.subscripcionAcceder) {
      this.subscripcionAcceder.unsubscribe();
    }
  }

  public enviarForm() {
    this.resetAlertas();

    const esValido = this.formAcceder.valid;

    if (esValido) {
      const datos = this.formAcceder.value;

      this.subscripcionAcceder = this._autenticacionService.iniciarSesion(datos.correoElectronico, datos.contrasena).subscribe(
        (obs: RespuestaAPI) => {
          if (obs.exito && (obs.codigoEstado === 201 || obs.codigoEstado === 200)) {
            this.router.navigate([`/proyecto/listado`]);
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
    } else {
      this.alertas.push({
        mensaje: mensaje,
        cerrable: true,
        tipo: 'success',
      });
    }
  }
}
