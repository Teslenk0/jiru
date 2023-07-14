import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {ButtonsModule} from 'ngx-bootstrap/buttons';
import {ReactiveFormsModule} from '@angular/forms';

import {CrearModificarProyectoComponent} from './crear-modificar-proyecto/crear-modificar-proyecto.component';
import {ListarProyectosComponent} from './listar-proyectos/listar-proyectos.component';
import {ProyectoRoutingModule} from './proyecto-routing.module';
import {AlertModule} from 'ngx-bootstrap/alert';
import {ModalModule} from 'ngx-bootstrap/modal';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    ProyectoRoutingModule,
    ButtonsModule.forRoot(),
    AlertModule.forRoot(),
    ModalModule.forRoot(),
  ],
  declarations: [CrearModificarProyectoComponent, ListarProyectosComponent]
})
export class ProyectoModule {
}
