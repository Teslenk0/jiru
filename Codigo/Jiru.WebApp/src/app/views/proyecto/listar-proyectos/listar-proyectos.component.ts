import {Component, OnDestroy, OnInit} from '@angular/core';
import {take} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {Router} from '@angular/router';
import {Subscription} from 'rxjs';

import {ProyectoService} from '../proyecto.service';
import {Alerta, RespuestaAPI, Proyecto, Usuario} from '../../../types';
import {BsModalService} from 'ngx-bootstrap/modal';
import {EliminarModalComponent} from '../../../components/eliminar-modal/eliminar-modal.component';
import {AsignarDesasignarModalComponent} from '../../../components/asignar-desasignar-modal/asignar-desasignar-modal.component';
import {AutenticacionService} from '../../../autenticacion.service';
import { ROLES } from '../../../constantes/constantes';


@Component({
  templateUrl: 'listar-proyectos.component.html',
})
export class ListarProyectosComponent implements OnInit, OnDestroy {
  public proyectos: Proyecto[];
  public alertas: Alerta[];
  private subscripcionEliminar: Subscription;
  private subscripcionAsignacion: Subscription;
  private subscripcionObtenerDatos: Subscription;
  private subscripcionModal: Subscription;
  public usuarioLogeado: Usuario;
  public readonly ROLES = ROLES;

  constructor(private _proyectoService: ProyectoService,
              private _modalService: BsModalService,
              private router: Router,
              private _autenticacionService: AutenticacionService) {
    this.alertas = [];
    this.usuarioLogeado = _autenticacionService.usuarioLogeado;
  }

  ngOnInit() {
    this.obtenerProyectos();
  }

  obtenerProyectos(): void {
    this.subscripcionObtenerDatos = this._proyectoService.obtenerProyectos().pipe()
      .subscribe((res: RespuestaAPI) => {
        this.proyectos = res.resultado;
      });
  }

  public editarProyecto(proyecto: Proyecto): void {
    this.router.navigate([`/proyecto/${proyecto.id}`]);
  }

  public eliminarProyecto(proyecto: Proyecto): void {

    const modal = this._modalService.show(EliminarModalComponent, {
      initialState: {
        titulo: `Eliminar proyecto ${proyecto.id}`,
        mensaje: `Estás seguro que deseas eliminar el proyecto ${proyecto.nombre}? Esta operación es irreversible.`,
        botonCerrar: 'Cancelar',
        botonEnviar: 'Eliminar'
      }
    });

    this.subscripcionModal = modal.onHide
      .pipe(take(1))
      .subscribe((data) => {
        if (data && data === 'operacion-confirmada') {
          this.confirmarEliminacion(proyecto);
        }
      });
  }


  public abrirModalAsignacion(operacion: string, proyecto: Proyecto): void {

    const parametros = {
      titulo: '',
      botonCerrar: 'Cancelar',
      botonEnviar: operacion === 'asignar' ? 'Asignar' : 'Desasignar'
    };

    if (operacion === 'asignar') {
      parametros.titulo = `Asignar usuario a ${proyecto.nombre}`;
    } else {
      parametros.titulo = `Desasignar usuario de ${proyecto.nombre}`;
    }

    const modal = this._modalService.show(AsignarDesasignarModalComponent, {
      initialState: parametros
    });

    this.subscripcionModal = modal.onHide
      .pipe(take(1))
      .subscribe((res) => {
        const data = JSON.parse(res);
        if (data && data.estado === 'operacion-confirmada') {
          let func = null;
          if (operacion === 'asignar') {
            func = this._proyectoService.asignarUsuario(proyecto.id.toString(), data.correoElectronico, data.rol);
          } else {
            func = this._proyectoService.desasignarUsuario(proyecto.id.toString(), data.correoElectronico, data.rol);
          }

          this.subscripcionAsignacion = func.subscribe(
            (obs: RespuestaAPI) => {
              if (obs.exito && obs.codigoEstado === 200) {
                this.generarAlerta(obs.mensaje, 'exito');
              }
            },
            (err: HttpErrorResponse) => {
              console.log(err);
              if (err.error && err.error.Mensaje) {
                this.generarAlerta(err.error.Mensaje, 'error');
              } else {
                this.generarAlerta('Ha ocurrido un error inesperado', 'error');
              }
            }
          );
        }
      });
  }

  public confirmarEliminacion(proyecto: Proyecto): void {
    this.resetAlertas();

    this._proyectoService.eliminarProyecto(proyecto.id).subscribe(
      (obs: RespuestaAPI) => {
        if (obs.exito && obs.codigoEstado === 200) {
          this.obtenerProyectos();
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

  ngOnDestroy(): void {
    if (this.subscripcionEliminar) {
      this.subscripcionEliminar.unsubscribe();
    }

    if (this.subscripcionObtenerDatos) {
      this.subscripcionObtenerDatos.unsubscribe();
    }

    if (this.subscripcionAsignacion) {
      this.subscripcionAsignacion.unsubscribe();
    }

    if (this.subscripcionModal) {
      this.subscripcionModal.unsubscribe();
    }
  }
}
