import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ButtonsModule} from 'ngx-bootstrap/buttons';
import {AlertModule} from 'ngx-bootstrap/alert';
import {ModalModule} from 'ngx-bootstrap/modal';

import {CrearBugComponent} from './crear-modificar-bug/crear-modificar-bug.component';
import {ListarBugsComponent} from './listar-bug/listar-bugs.component';
import {BugRoutingModule} from './bug-routing.module';


@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        FormsModule,
        BugRoutingModule,
        ButtonsModule.forRoot(),
        AlertModule.forRoot(),
        ModalModule.forRoot(),
    ],
  declarations: [CrearBugComponent, ListarBugsComponent]
})
export class BugModule {
}
