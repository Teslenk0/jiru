import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';

import {CrearTareaComponent} from './crear-tarea/crear-tarea.component';
import {ListarTareasComponent} from './listar-tareas/listar-tareas.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Tarea'
    },
    children: [
      {
        path: 'crear',
        data: {
          title: 'Crear'
        },
        component: CrearTareaComponent
      },
      {
        path: 'listado',
        data: {
          title: 'Listado'
        },
        component: ListarTareasComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TareaRoutingModule {
}
