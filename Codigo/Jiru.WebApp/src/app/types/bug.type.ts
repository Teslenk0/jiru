export class Bug {
  constructor(public id: number,
              public nombre: string,
              public proyectoId: number,
              public descripcion: string,
              public version: string,
              public estado: string,
              public resueltoPorId: number,
              public reportadoPorId: number,
              public idExterno: number,
              public duracionHoras: number) {
  }
}
