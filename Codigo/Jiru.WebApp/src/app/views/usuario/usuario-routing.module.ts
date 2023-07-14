import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';

import {CrearUsuarioComponent} from './crear-usuario/crear-usuario.component';
import {ConsultasUsuarioComponent} from './consultas-usuario/consultas-usuario.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Usuario'
    },
    children: [
      {
        path: 'crear',
        data: {
          title: 'Crear'
        },
        component: CrearUsuarioComponent
      },
      {
        path: 'consulta',
        data: {
          title: 'Consulta'
        },
        component: ConsultasUsuarioComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsuarioRoutingModule {
}
