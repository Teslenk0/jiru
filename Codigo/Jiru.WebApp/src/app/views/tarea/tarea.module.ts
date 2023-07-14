import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {ButtonsModule} from 'ngx-bootstrap/buttons';
import {ReactiveFormsModule} from '@angular/forms';

import {CrearTareaComponent} from './crear-tarea/crear-tarea.component';
import {ListarTareasComponent} from './listar-tareas/listar-tareas.component';
import {TareaRoutingModule} from './tarea-routing.module';
import {AlertModule} from 'ngx-bootstrap/alert';
import {ModalModule} from 'ngx-bootstrap/modal';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    TareaRoutingModule,
    ButtonsModule.forRoot(),
    AlertModule.forRoot(),
    ModalModule.forRoot(),
  ],
  declarations: [CrearTareaComponent, ListarTareasComponent]
})
export class TareaModule {
}
