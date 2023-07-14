import {Usuario as UsuarioType} from './usuario.type';

export class AutenticacionType {
  public token: string;
  public usuario: UsuarioType;

  constructor(public Token: string, public Usuario: UsuarioType) {
    this.token = Token;
    this.usuario = Usuario;
  }
}
