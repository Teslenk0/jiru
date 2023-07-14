import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Import Containers
import { DefaultLayoutComponent } from './containers';

import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { IniciarSesionComponent } from './views/iniciar-sesion/iniciar-sesion.component';
import {AutenticacionGuard} from './guards/autenticacion.guard';
import {RolesGuard} from './guards/roles.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'proyecto/listado',
    pathMatch: 'full',
  },
  {
    path: '404',
    component: P404Component,
    data: {
      title: 'Error 404'
    }
  },
  {
    path: '500',
    component: P500Component,
    data: {
      title: 'Error 500'
    }
  },
  {
    path: 'iniciar-sesion',
    component: IniciarSesionComponent,
    data: {
      title: 'Iniciar Sesión'
    }
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    canActivate: [AutenticacionGuard, RolesGuard],
    canActivateChild: [AutenticacionGuard, RolesGuard],
    data: {
      title: 'Inicio'
    },
    children: [
      {
        path: 'proyecto',
        loadChildren: () => import('./views/proyecto/proyecto.module').then(m => m.ProyectoModule)
      },
      {
        path: 'usuario',
        loadChildren: () => import('./views/usuario/usuario.module').then(m => m.UsuarioModule)
      },
      {
        path: 'bug',
        loadChildren: () => import('./views/bug/bug.module').then(m => m.BugModule)
      },
      {
        path: 'tarea',
        loadChildren: () => import('./views/tarea/tarea.module').then(m => m.TareaModule)
      },
      {
        path: 'importacion',
        loadChildren: () => import('./views/importacion/importacion.module').then(m => m.ImportacionModule)
      },
    ]
  },
  { path: '**', component: P404Component }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' }) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
