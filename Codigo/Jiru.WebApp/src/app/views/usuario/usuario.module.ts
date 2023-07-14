import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ButtonsModule} from 'ngx-bootstrap/buttons';
import {AlertModule} from 'ngx-bootstrap/alert';
import {ModalModule} from 'ngx-bootstrap/modal';

import {CrearUsuarioComponent} from './crear-usuario/crear-usuario.component';
import {UsuarioRoutingModule} from './usuario-routing.module';
import {ConsultasUsuarioComponent} from './consultas-usuario/consultas-usuario.component';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    UsuarioRoutingModule,
    ButtonsModule.forRoot(),
    AlertModule.forRoot(),
    ModalModule.forRoot(),
    ReactiveFormsModule,
  ],
  declarations: [CrearUsuarioComponent, ConsultasUsuarioComponent]
})
export class UsuarioModule {
}
