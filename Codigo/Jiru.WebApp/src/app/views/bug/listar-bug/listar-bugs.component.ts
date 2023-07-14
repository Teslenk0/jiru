import {Component, OnDestroy, OnInit} from '@angular/core';
import {BugService} from '../bug.service';
import {Alerta, Proyecto, RespuestaAPI, Bug, Filtro, Usuario} from '../../../types';
import {BsModalService} from 'ngx-bootstrap/modal';
import {EliminarModalComponent} from '../../../components/eliminar-modal/eliminar-modal.component';
import {take} from 'rxjs/operators';
import {AutenticacionService} from '../../../autenticacion.service';
import {HttpErrorResponse} from '@angular/common/http';
import {Router} from '@angular/router';
import {FormBuilder, FormGroup} from '@angular/forms';
import {ProyectoService} from '../../proyecto/proyecto.service';
import {ROLES} from '../../../constantes/constantes';
import {Subscription} from 'rxjs';

@Component({
  templateUrl: 'listar-bugs.component.html',
})
export class ListarBugsComponent implements OnInit, OnDestroy {
  public bugs: Bug[];
  public proyectos: Proyecto[];
  public alertas: Alerta[];
  public formFiltros: FormGroup;
  public subscripcionBugs: Subscription;
  public subscripcionProyectos: Subscription;
  public subscripcionEliminar: Subscription;
  public subscripcionFormFiltros: Subscription;
  public usuarioLogeado: Usuario;
  public readonly ROLES = ROLES;

  constructor(
    private _bugService: BugService,
    private _proyectoService: ProyectoService,
    private _modalService: BsModalService,
    private _autenticacionService: AutenticacionService,
    private router: Router,
    private _formBuilder: FormBuilder) {
    this.formFiltros = _formBuilder.group({
      proyectoId: _formBuilder.control(''),
      id: _formBuilder.control(''),
      nombre: _formBuilder.control(''),
      estado: _formBuilder.control('')
    });

    this.subscripcionFormFiltros = this.formFiltros.valueChanges.subscribe((data: Filtro) => {
      const filtros = Object.fromEntries(Object.entries(data).filter(value => value[1]));
      this.obtenerBugs(filtros);
    });

    this.usuarioLogeado = _autenticacionService.usuarioLogeado;
  }

  ngOnInit() {
    this.obtenerBugs({});
    this.obtenerProyectos();
  }

  ngOnDestroy() {
    if (this.subscripcionBugs) {
      this.subscripcionBugs.unsubscribe();
    }

    if (this.subscripcionProyectos) {
      this.subscripcionProyectos.unsubscribe();
    }

    if (this.subscripcionEliminar) {
      this.subscripcionEliminar.unsubscribe();
    }

    if (this.subscripcionFormFiltros) {
      this.subscripcionFormFiltros.unsubscribe();
    }
  }

  obtenerProyectos(): void {
    this.subscripcionProyectos = this._proyectoService.obtenerProyectos().pipe()
      .subscribe((res: RespuestaAPI) => {
        this.proyectos = res.resultado;
      });
  }

  obtenerBugs(filtros: Filtro): void {
    this.subscripcionBugs = this._bugService.obtenerBugs(filtros).pipe()
      .subscribe((res: RespuestaAPI) => {
        this.bugs = res.resultado;
      });
  }

  public editarBug(bug: Bug): void {
    this.router.navigate([`/bug/${bug.id}`]);
  }

  public eliminarBug(bug: Bug): void {

    const modal = this._modalService.show(EliminarModalComponent, {
      initialState: {
        titulo: `Eliminar bug ${bug.id}`,
        mensaje: `Estás seguro que deseas eliminar el bug ${bug.nombre}? Esta operación es irreversible.`,
        botonCerrar: 'Cancelar',
        botonEnviar: 'Eliminar'
      }
    });

    modal.onHide
      .pipe(take(1))
      .subscribe((data) => {
        if (data && data === 'operacion-confirmada') {
          this.confirmarEliminacion(bug);
        }
      });
  }

  public confirmarEliminacion(bug: Bug): void {
    this.resetAlertas();

    this.subscripcionEliminar = this._bugService.eliminarBug(bug.id).subscribe(
      (obs: RespuestaAPI) => {
        if (obs.exito && obs.codigoEstado === 200) {
          this.obtenerBugs({});
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
}
