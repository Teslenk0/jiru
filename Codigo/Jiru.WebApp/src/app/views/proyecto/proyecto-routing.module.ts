import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';

import {CrearModificarProyectoComponent} from './crear-modificar-proyecto/crear-modificar-proyecto.component';
import {ListarProyectosComponent} from './listar-proyectos/listar-proyectos.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Proyecto'
    },
    children: [
      {
        path: 'crear',
        data: {
          title: 'Crear'
        },
        component: CrearModificarProyectoComponent
      },
      {
        path: 'listado',
        data: {
          title: 'Listado'
        },
        component: ListarProyectosComponent
      },
      {
        path: ':id',
        data: {
          title: 'Modificar'
        },
        component: CrearModificarProyectoComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProyectoRoutingModule {

}
