import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';

import {CrearBugComponent} from './crear-modificar-bug/crear-modificar-bug.component';
import {ListarBugsComponent} from './listar-bug/listar-bugs.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Bug'
    },
    children: [
      {
        path: 'crear',
        data: {
          title: 'Crear'
        },
        component: CrearBugComponent
      },
      {
        path: 'listado',
        data: {
          title: 'Listado'
        },
        component: ListarBugsComponent
      },
      {
        path: ':id',
        data: {
          title: 'Modificar'
        },
        component: CrearBugComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BugRoutingModule {
}
