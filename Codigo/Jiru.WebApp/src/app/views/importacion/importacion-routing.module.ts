import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';

import {ImportarBugComponent} from './importar-bug/importar-bug.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Importacion'
    },
    children: [
      {
        path: 'bug',
        data: {
          title: 'Bug'
        },
        component: ImportarBugComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ImportacionRoutingModule {
}
