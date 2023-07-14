import {HttpErrorResponse} from '@angular/common/http';
import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Alerta, RespuestaAPI, Proyecto} from '../../../types';
import {ProyectoService} from '../proyecto.service';
import {ActivatedRoute} from '@angular/router';
import {Subscription} from 'rxjs';
import {Router} from '@angular/router';

@Component({
  templateUrl: 'crear-modificar-proyecto.component.html',
})
export class CrearModificarProyectoComponent implements OnInit, OnDestroy {
  public formProyecto: FormGroup;
  public esEdicion: boolean = false;
  public alertas: Alerta[];
  public proyecto: Proyecto;
  private subscripcion: Subscription;
  private subscripcionCrear: Subscription;

  constructor(
    private route: ActivatedRoute,
    public router: Router,
    private _proyectoService: ProyectoService) {
    this.formProyecto = new FormGroup({
      nombre: new FormControl('', Validators.required),
    });

    this.alertas = [];
  }

  ngOnDestroy(): void {
    if (this.subscripcion) {
      this.subscripcion.unsubscribe();
    }

    if (this.subscripcionCrear) {
      this.subscripcionCrear.unsubscribe();
    }
  }

  ngOnInit() {
    const idProyecto = this.route.snapshot.paramMap.get('id');
    if (idProyecto) {
      this.esEdicion = true;
      this.obtenerProyecto(idProyecto);
    }
  }

  obtenerProyecto(idProyecto: string): void {
    this.subscripcion = this._proyectoService.obtenerProyectoPorId(idProyecto).pipe()
      .subscribe((res: RespuestaAPI) => {
        this.proyecto = res.resultado;
        this.formProyecto.setValue({
          nombre: this.proyecto.nombre
        });
      });
  }

  public enviarForm() {
    this.resetAlertas();

    const esValido = this.formProyecto.valid;

    if (esValido) {
      const datos = this.formProyecto.value;
      let operacion;
      if (this.esEdicion) {
        operacion = this._proyectoService.editarProyecto(this.proyecto.id.toString(), datos);
      } else {
        operacion = this._proyectoService.crearProyecto(datos);
      }
      this.subscripcionCrear = operacion.subscribe(
        (obs: RespuestaAPI) => {
          if (obs.exito && (obs.codigoEstado === 201 || obs.codigoEstado === 200)) {
            if (!this.esEdicion) {
              this.formProyecto.reset();
            }
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

  public resetForm() {
    if (!this.esEdicion) {
      this.formProyecto.reset();
    }
    this.resetAlertas();

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
