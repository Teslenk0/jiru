import {HttpErrorResponse} from '@angular/common/http';
import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Alerta, RespuestaAPI, Proyecto} from '../../../types';
import {TareaService} from '../tarea.service';
import {ProyectoService} from '../../proyecto/proyecto.service';
import {ActivatedRoute} from '@angular/router';
import {Subscription} from 'rxjs';
import { Router } from '@angular/router';

@Component({
  templateUrl: 'crear-tarea.component.html',
})
export class CrearTareaComponent implements OnInit, OnDestroy {
  public formTarea: FormGroup;
  public alertas: Alerta[];
  public proyectos: Proyecto[];
  public subscripcionCrear: Subscription;
  public subscripcionObtenerProyectos: Subscription;

  constructor(
    private route: ActivatedRoute,
    public router: Router,
    private _proyectoService: ProyectoService,
    private _tareaService: TareaService) {
    this.formTarea = new FormGroup({
      nombre: new FormControl('', Validators.required),
      proyectoId: new FormControl('', Validators.required),
      costoPorHora: new FormControl('', [Validators.required, Validators.min(1)]),
      duracionHoras: new FormControl('', [Validators.required, Validators.min(1)]),
    });

    this.alertas = [];
  }

  ngOnInit() {
    this.obtenerProyectos();
  }

  ngOnDestroy() {
    if (this.subscripcionCrear) {
      this.subscripcionCrear.unsubscribe();
    }

    if (this.subscripcionObtenerProyectos) {
      this.subscripcionObtenerProyectos.unsubscribe();
    }
  }

  obtenerProyectos(): void {
    this.subscripcionObtenerProyectos = this._proyectoService.obtenerProyectos().pipe()
      .subscribe((res: RespuestaAPI) => {
        this.proyectos = res.resultado;
      });
  }

  public enviarForm() {
    this.resetAlertas();

    const esValido = this.formTarea.valid;

    if (esValido) {
      const datos = this.formTarea.value;

      this.subscripcionCrear = this._tareaService.crearTarea(datos).subscribe(
        (obs: RespuestaAPI) => {
          if (obs.exito && (obs.codigoEstado === 201 || obs.codigoEstado === 200)) {
            this.formTarea.reset();
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
    this.formTarea.reset();
    this.resetAlertas();
  }

  public cancelarForm(){
    this.router.navigate(['/tarea/listado']);
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
