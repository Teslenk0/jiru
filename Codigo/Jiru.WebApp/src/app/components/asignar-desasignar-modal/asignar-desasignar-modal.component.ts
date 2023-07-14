import {Component} from '@angular/core';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-eliminar-modal',
  templateUrl: 'asignar-desasignar-modal.component.html'
})
export class AsignarDesasignarModalComponent {
  public titulo?: string;
  public botonCerrar?: string;
  public botonEnviar?: string;

  public formUsuario: FormGroup;

  constructor(public bsModalRef: BsModalRef, private _bsModalService: BsModalService) {
    this.formUsuario = new FormGroup({
      correoElectronico: new FormControl('', [Validators.required, Validators.email]),
      rol: new FormControl('Desarrollador', [Validators.required]),
    });
  }

  confirmar(): void {
    if (this.formUsuario.valid) {
      const data = this.formUsuario.value;
      this._bsModalService.setDismissReason(JSON.stringify({
        estado: 'operacion-confirmada',
        correoElectronico: data.correoElectronico,
        rol: data.rol
      }));
      this.bsModalRef.hide();
    }

  }
}
