import {Component, OnDestroy, OnInit} from '@angular/core';
import {TareaService} from '../tarea.service';
import {Alerta, RespuestaAPI, Tarea} from '../../../types';
import {BsModalService} from 'ngx-bootstrap/modal';
import {Subscription} from 'rxjs';

@Component({
  templateUrl: 'listar-tareas.component.html',
})
export class ListarTareasComponent implements OnInit, OnDestroy {
  public tareas: Tarea[];
  public alertas: Alerta[];
  public subscripcionObtener: Subscription;

  constructor(private _tareaService: TareaService, private _modalService: BsModalService) {
  }

  ngOnInit() {
    this.obtenerTareas();
  }

  ngOnDestroy() {
    if (this.subscripcionObtener) {
      this.subscripcionObtener.unsubscribe();
    }
  }

  obtenerTareas(): void {
    this.subscripcionObtener = this._tareaService.obtenerTareas().pipe()
      .subscribe((res: RespuestaAPI) => {
        this.tareas = res.resultado;
      });
  }
}
