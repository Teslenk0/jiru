import {HttpErrorResponse} from '@angular/common/http';
import {Component, OnDestroy} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Alerta, RespuestaAPI, Usuario} from '../../../types';
import {UsuarioService} from '../usuario.service';
import {ActivatedRoute} from '@angular/router';
import {Subscription} from 'rxjs';
import { Router } from '@angular/router';

@Component({
  templateUrl: 'crear-usuario.component.html',
})
export class CrearUsuarioComponent implements OnDestroy {
  public formUsuario: FormGroup;
  public alertas: Alerta[];
  public usuario: Usuario;

  private subscripcionForm: Subscription;
  private subscripcionCrear: Subscription;

  constructor(
    private route: ActivatedRoute,
    public router: Router,
    private _usuarioService: UsuarioService) {
    this.formUsuario = new FormGroup({
      nombre: new FormControl('', Validators.required),
      apellido: new FormControl('', Validators.required),
      costoPorHora: new FormControl('0'),
      correoElectronico: new FormControl('', [Validators.required, Validators.email]),
      nombreUsuario: new FormControl('', Validators.required),
      contrasena: new FormControl('', [Validators.required, Validators.minLength(6)]),
      confirmarContrasena: new FormControl('', Validators.required),
      rol: new FormControl('Administrador', [Validators.required]),
    });

    this.subscripcionForm = this.formUsuario.valueChanges.subscribe(campo => {
      if (campo.contrasena !== campo.confirmarContrasena) {
        this.formUsuario.controls['confirmarContrasena'].setErrors({mismatch: true});
      } else {
        this.formUsuario.controls['confirmarContrasena'].setErrors(null);
      }

      if (campo.rol !== 'Administrador') {
        this.formUsuario.controls['rol'].addValidators([Validators.required, Validators.min(1)]);
      }
    });

    this.alertas = [];
  }

  ngOnDestroy(): void {
    if (this.subscripcionCrear) {
      this.subscripcionCrear.unsubscribe();
    }

    if (this.subscripcionForm) {
      this.subscripcionForm.unsubscribe();
    }
  }

  public enviarForm() {
    this.resetAlertas();

    const esValido = this.formUsuario.valid;

    if (esValido) {
      const datos = this.formUsuario.value;

      this.subscripcionCrear = this._usuarioService.crearUsuario(datos).subscribe(
        (obs: RespuestaAPI) => {
          if (obs.exito && (obs.codigoEstado === 201 || obs.codigoEstado === 200)) {
            this.resetForm(false);
            this.generarAlerta(obs.mensaje, 'exito');
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
    this.formUsuario.controls['rol'].setValue('Administrador');
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
    } else {
      this.alertas.push({
        mensaje: mensaje,
        cerrable: true,
        tipo: 'success',
      });
    }
  }
}
