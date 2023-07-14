export class RespuestaAPI {
  public codigoEstado: number;
  public resultado: any;
  public exito: boolean;
  public mensaje: string;

  constructor(
    CodigoEstado: number,
    Resultado: any,
    Exito: boolean,
    Mensaje: string
  ) {
    this.codigoEstado = CodigoEstado;
    this.exito = Exito;
    this.mensaje = Mensaje;
    this.resultado = Resultado;
  }
}
