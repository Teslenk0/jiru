export class Usuario {
  constructor(public id: number,
              public nombre: string,
              public apellido: string,
              public nombreUsuario: string,
              public contrasena: string,
              public correoElectronico: string,
              public rol: string,
              public costoPorHora: number) {
  }
}
