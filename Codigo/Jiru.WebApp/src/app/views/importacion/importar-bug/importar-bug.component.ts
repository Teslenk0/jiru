import {HttpErrorResponse} from '@angular/common/http';
import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Alerta, RespuestaAPI} from '../../../types';
import {ImportacionService} from '../importacion.service';
import {ActivatedRoute} from '@angular/router';
import {Importador} from '../../../types';
import {Subscription} from 'rxjs';
import {Router} from '@angular/router';

@Component({
  templateUrl: 'importar-bug.component.html',
})
export class ImportarBugComponent implements OnDestroy, OnInit {
  public formImportacion: FormGroup;
  public alertas: Alerta[];
  public importadores: Importador[];

  private subscripcionObtenerImportadores: Subscription;
  private subscripcionForm: Subscription;
  private subscripcionImportar: Subscription;

  constructor(
    private formBuilder: FormBuilder,
    public router: Router,
    private route: ActivatedRoute,
    private _importacionService: ImportacionService) {


    this.formImportacion = this.formBuilder.group({
      proveedor: ['default', [Validators.required]],
      correoUsuarioResuelve: ['', [Validators.required, Validators.email]],
      parametros: this.formBuilder.array([
        this.formBuilder.group({
          clave: ['', []],
          valor: ['', []]
        })
      ])
    });

    this.alertas = [];
  }

  ngOnInit() {
    this.obtenerImportadores();
  }

  ngOnDestroy(): void {
    if (this.subscripcionImportar) {
      this.subscripcionImportar.unsubscribe();
    }

    if (this.subscripcionForm) {
      this.subscripcionForm.unsubscribe();
    }

    if (this.subscripcionObtenerImportadores) {
      this.subscripcionObtenerImportadores.unsubscribe();
    }
  }


  get parametros() {
    return this.formImportacion.get('parametros') as FormArray;
  }

  obtenerImportadores(): void {
    this.subscripcionObtenerImportadores = this._importacionService
      .obtenerImportadoresDisponibles()
      .pipe()
      .subscribe((res: RespuestaAPI) => {
        this.importadores = res.resultado;
      });
  }

  public agregarParametro($event): void {
    $event.preventDefault();
    this.parametros.push(
      this.formBuilder.group({
        clave: ['', []],
        valor: ['', []]
      })
    );
  }

  public quitarParametro($event, indice: number): void {
    $event.preventDefault();

    this.parametros.removeAt(indice);
  }

  public enviarForm() {
    this.resetAlertas();

    const esValido = this.formImportacion.valid;

    if (esValido) {
      const datos = this.formImportacion.value;

      const params = {};

      datos.parametros.forEach(param => params[param.clave.replace(' ', '_')] = param.valor);

      datos.parametros = {...params, correoUsuarioResuelve: datos.correoUsuarioResuelve};

      this.subscripcionImportar = this._importacionService.importarBugs(datos)
        .subscribe(
          (obs: RespuestaAPI) => {
            if (obs.exito && (obs.codigoEstado === 201 || obs.codigoEstado === 200)) {
              this.resetForm(false);
              this.generarAlerta(obs.mensaje, 'exito');
            }
          },
          (err: HttpErrorResponse) => {
            if (err.error && err.error.Mensaje) {
              this.generarAlerta(err.error.Mensaje, 'error');
            } else {
              this.generarAlerta('Ha ocurrido un error inesperado', 'error');
            }
          }
        );
    } else {
      this.generarAlerta('Por favor, revisa los campos ingresados', 'error');
    }
  }

  public resetForm(resetAlertas: boolean = true) {
    this.formImportacion.reset();
    this.formImportacion.controls['proveedor'].setValue('default');
    if (resetAlertas) {
      this.resetAlertas();
    }

  }
  public cancelarForm(){
    this.router.navigate(['/bug/listado']);
  }

  private resetAlertas() {
    this.alertas = [];
  }

  private generarAlerta(mensaje: string, tipo: string) {
    if (tipo === 'error') {
      this.alertas.push({
        mensaje: mensaje,
        cerrable: true,
        tipo: 'danger',
      });
    } else {
      this.alertas.push({
        mensaje: mensaje,
        cerrable: true,
        tipo: 'success',
      });
    }
  }
}
