import {Component} from '@angular/core';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-eliminar-modal',
  templateUrl: 'eliminar-modal.component.html'
})
export class EliminarModalComponent {
  public titulo?: string;
  public botonCerrar?: string;
  public botonEnviar?: string;
  public mensaje: string;

  constructor(public bsModalRef: BsModalRef, private _bsModalService: BsModalService) {
  }

  confirmar(): void {
    this._bsModalService.setDismissReason('operacion-confirmada');
    this.bsModalRef.hide();
  }
}
