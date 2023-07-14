import {INavData} from '@coreui/angular';
import {ROLES} from './constantes/constantes';

export const navItems: INavData[] = [
  {
    title: true,
    name: 'Proyectos',
    attributes: {
      roles: [ROLES.ADMINISTRADOR, ROLES.DESARROLLADOR, ROLES.TESTER]
    }
  },
  {
    name: 'Crear Proyecto',
    url: '/proyecto/crear',
    icon: 'icon-plus',
    attributes: {
      roles: [ROLES.ADMINISTRADOR]
    }
  },
  {
    name: 'Listado de Proyectos',
    url: '/proyecto/listado',
    icon: 'icon-list',
    attributes: {
      roles: [ROLES.ADMINISTRADOR, ROLES.DESARROLLADOR, ROLES.TESTER]
    }
  },
  {
    title: true,
    name: 'Bugs',
    attributes: {
      roles: [ROLES.ADMINISTRADOR, ROLES.DESARROLLADOR, ROLES.TESTER]
    }
  },
  {
    name: 'Crear Bug',
    url: '/bug/crear',
    icon: 'icon-plus',
    attributes: {
      roles: [ROLES.ADMINISTRADOR, ROLES.TESTER]
    }
  },
  {
    name: 'Listado de Bugs',
    url: '/bug/listado',
    icon: 'icon-list',
    attributes: {
      roles: [ROLES.ADMINISTRADOR, ROLES.DESARROLLADOR, ROLES.TESTER]
    }
  },
  {
    title: true,
    name: 'Tareas',
    attributes: {
      roles: [ROLES.ADMINISTRADOR, ROLES.DESARROLLADOR, ROLES.TESTER]
    }
  },
  {
    name: 'Crear Tarea',
    url: '/tarea/crear',
    icon: 'icon-plus',
    attributes: {
      roles: [ROLES.ADMINISTRADOR]
    }
  },
  {
    name: 'Listado de Tareas',
    url: '/tarea/listado',
    icon: 'icon-list',
    attributes: {
      roles: [ROLES.ADMINISTRADOR, ROLES.DESARROLLADOR, ROLES.TESTER]
    }
  },
  {
    title: true,
    name: 'Usuarios',
    attributes: {
      roles: [ROLES.ADMINISTRADOR]
    }
  },
  {
    name: 'Crear Usuario',
    url: '/usuario/crear',
    icon: 'icon-plus',
    attributes: {
      roles: [ROLES.ADMINISTRADOR]
    }
  },
  {
    name: 'Consulta Usuario',
    url: '/usuario/consulta',
    icon: 'icon-plus',
    attributes: {
      roles: [ROLES.ADMINISTRADOR]
    }
  },
  {
    divider: true
  },
  {
    title: true,
    name: 'Extras',
    attributes: {
      roles: [ROLES.ADMINISTRADOR]
    }
  },
  {
    name: 'Importar Bugs',
    url: '/importacion/bug',
    icon: 'cil-data-transfer-up\n',
    attributes: {
      roles: [ROLES.ADMINISTRADOR]
    }
  },
];
