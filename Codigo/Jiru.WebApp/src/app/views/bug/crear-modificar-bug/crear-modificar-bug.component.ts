import {HttpErrorResponse} from '@angular/common/http';
import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Alerta, RespuestaAPI, Proyecto, Usuario} from '../../../types';
import {BugService} from '../bug.service';
import {ProyectoService} from '../../proyecto/proyecto.service';
import {ActivatedRoute} from '@angular/router';
import {Bug} from '../../../types/bug.type';
import {AutenticacionService} from '../../../autenticacion.service';
import {Subscription} from 'rxjs';
import {ROLES} from '../../../constantes/constantes';
import {Router} from '@angular/router';

@Component({
  templateUrl: 'crear-modificar-bug.component.html',
})
export class CrearBugComponent implements OnInit, OnDestroy {
  public formBug: FormGroup;
  public esEdicion: boolean = false;
  public alertas: Alerta[];
  public proyectos: Proyecto[];
  public bug: Bug;
  public subscipcionCrearMod: Subscription;
  public subscipcionObtener: Subscription;
  public subscipcionObtenerProyectos: Subscription;
  public subscripcionForm: Subscription;
  public usuarioLogeado: Usuario;

  constructor(
    private route: ActivatedRoute,
    public router: Router,
    private _proyectoService: ProyectoService,
    private _autenticacionService: AutenticacionService,
    private _bugService: BugService) {
    this.formBug = new FormGroup({
      nombre: new FormControl('', Validators.required),
      proyectoId: new FormControl('', Validators.required),
      descripcion: new FormControl('', Validators.required),
      version: new FormControl('', Validators.required),
      estado: new FormControl('', Validators.required),
      duracionHoras: new FormControl(0),
    });

    this.subscripcionForm = this.formBug.valueChanges.subscribe(campo => {
      if (campo.estado === 'Resuelto'
        && !this.formBug.controls['duracionHoras'].hasValidator(Validators.required)
        && !this.formBug.controls['duracionHoras'].hasValidator(Validators.min(1))) {
        this.formBug.controls['duracionHoras'].addValidators([Validators.required, Validators.min(1)]);
        this.formBug.controls['duracionHoras'].setValue(1);
      } else if (campo.estado === 'Activo' && this.formBug.controls['duracionHoras'].value !== 0) {
        this.formBug.controls['duracionHoras'].clearValidators();
        this.formBug.controls['duracionHoras'].setValue(0);
      }
    });

    this.alertas = [];
    this.usuarioLogeado = _autenticacionService.usuarioLogeado;
  }

  ngOnDestroy() {
    if (this.subscipcionCrearMod) {
      this.subscipcionCrearMod.unsubscribe();
    }

    if (this.subscipcionObtener) {
      this.subscipcionObtener.unsubscribe();
    }

    if (this.subscipcionObtenerProyectos) {
      this.subscipcionObtenerProyectos.unsubscribe();
    }

    if (this.subscripcionForm) {
      this.subscripcionForm.unsubscribe();
    }

  }

  ngOnInit() {
    const idBug = this.route.snapshot.paramMap.get('id');
    if (idBug) {
      this.esEdicion = true;
      this.obtenerBug(idBug);
    }
    this.obtenerProyectos();
  }

  obtenerProyectos(): void {
    this.subscipcionObtenerProyectos = this._proyectoService.obtenerProyectos().pipe()
      .subscribe((res: RespuestaAPI) => {
        this.proyectos = res.resultado;
      });
  }

  obtenerBug(idBug: string): void {
    this.subscipcionObtener = this._bugService.obtenerBugPorId(idBug).pipe()
      .subscribe((res: RespuestaAPI) => {
        this.bug = res.resultado;
        this.formBug.setValue({
          nombre: this.bug.nombre,
          proyectoId: this.bug.proyectoId,
          descripcion: this.bug.descripcion,
          version: this.bug.version,
          estado: this.bug.estado,
          duracionHoras: this.bug.duracionHoras
        });
        if (this.usuarioLogeado.rol.toUpperCase() === ROLES.DESARROLLADOR) {
          this.formBug.controls['nombre'].disable();
          this.formBug.controls['proyectoId'].disable();
          this.formBug.controls['descripcion'].disable();
          this.formBug.controls['version'].disable();
        }

      });
  }

  public enviarForm() {
    this.resetAlertas();

    const esValido = this.formBug.valid;

    if (esValido) {
      const datos = this.formBug.getRawValue();

      let operacion;

      if (this._autenticacionService.usuarioLogeado.rol.toUpperCase() === ROLES.ADMINISTRADOR && datos.estado === 'Resuelto') {
        return this.generarAlerta('Los usuarios administradores no pueden crear o marcar bugs como resueltos.', 'error');
      }

      if (this._autenticacionService.usuarioLogeado.rol.toUpperCase() !== ROLES.ADMINISTRADOR && datos.estado === 'Resuelto') {
        datos.resueltoPorId = this._autenticacionService.usuarioLogeado.id;
      }

      if (this.esEdicion) {
        operacion = this._bugService.editarBug(this.bug.id.toString(), {...datos, reportadoPorId: this.bug.reportadoPorId});
      } else {
        operacion = this._bugService.crearBug({...datos, reportadoPorId: this._autenticacionService.usuarioLogeado.id});
      }
      this.subscipcionCrearMod = operacion.subscribe(
        (obs: RespuestaAPI) => {
          if (obs.exito && (obs.codigoEstado === 201 || obs.codigoEstado === 200)) {
            if (!this.esEdicion) {
              this.formBug.reset();
            }
            this.setearValoresDefecto();
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

  private setearValoresDefecto() {
    this.formBug.controls['estado'].setValue('');
    this.formBug.controls['proyectoId'].setValue('');
  }

  public cancelarForm() {
    this.router.navigate(['/bug/listado']);
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
