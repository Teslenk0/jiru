!function(){"use strict";function r(r,o){for(var e=0;e<o.length;e++){var i=o[e];i.enumerable=i.enumerable||!1,i.configurable=!0,"value"in i&&(i.writable=!0),Object.defineProperty(r,i.key,i)}}function o(o,e,i){return e&&r(o.prototype,e),i&&r(o,i),o}function e(r,o){if(!(r instanceof o))throw new TypeError("Cannot call a class as a function")}(self.webpackChunkjiru_webapp=self.webpackChunkjiru_webapp||[]).push([[584],{50584:function(r,i,n){n.r(i),n.d(i,{UsuarioModule:function(){return N}});var t,a=n(12057),s=n(23738),c=n(37911),l=n(23419),u=n(6282),d=n(95859),f=n(88002),m=n(92340),p=n(60793),v=n(58497),g=((t=function r(o){var i=this;e(this,r),this._httpClient=o,this.crearUsuario=function(r){return i._httpClient.post("".concat(i.apiUrl,"/usuario/").concat(r.rol.toLowerCase()),r).pipe((0,f.U)(function(r){return r}))},this.obtenerBugsResueltosPorUsuario=function(r){return i._httpClient.get("".concat(i.apiUrl,"/bug/desarrollador/").concat(r,"/resuelto")).pipe((0,f.U)(function(r){return r}))},this.apiUrl=m.N.apiUrl}).\u0275fac=function(r){return new(r||t)(p.LFG(v.eN))},t.\u0275prov=p.Yz7({token:t,factory:t.\u0275fac,providedIn:"root"}),t);function Z(r,o){if(1&r&&(p.TgZ(0,"div",31),p.TgZ(1,"alert",32),p._UZ(2,"i",33),p._uU(3),p.qZA(),p.qZA()),2&r){var e=o.$implicit;p.xp6(1),p.Q6J("type",e.tipo)("dismissible",e.cerrable),p.xp6(2),p.Oqu(e.mensaje)}}function U(r,o){1&r&&(p.TgZ(0,"div",34),p._uU(1," Ingresa un nombre v\xe1lido "),p.qZA())}function b(r,o){1&r&&(p.TgZ(0,"div",34),p._uU(1," Ingresa un apellido v\xe1lido "),p.qZA())}function h(r,o){1&r&&(p.TgZ(0,"div",34),p._uU(1," Ingresa un nombre de usuario v\xe1lido "),p.qZA())}function A(r,o){1&r&&(p.TgZ(0,"div",34),p._uU(1," Ingresa un rol v\xe1lido "),p.qZA())}function q(r,o){1&r&&(p.TgZ(0,"div",34),p._uU(1," Ingresa un correo electr\xf3nico v\xe1lido "),p.qZA())}function T(r,o){1&r&&(p.TgZ(0,"div",34),p._uU(1," La contrase\xf1a debe tener al menos 6 caracteres "),p.qZA())}function y(r,o){1&r&&(p.TgZ(0,"div",34),p._uU(1," Las contrase\xf1as no coinciden "),p.qZA())}function _(r,o){1&r&&(p.TgZ(0,"div",34),p._uU(1," Ingresa un costo por hora mayor a 0 "),p.qZA())}function I(r,o){if(1&r&&(p.TgZ(0,"div",9),p.TgZ(1,"label",35),p._uU(2,"Costo por hora (*)"),p.qZA(),p._UZ(3,"input",36),p.YNc(4,_,2,0,"div",12),p.qZA()),2&r){var e=p.oxw();p.xp6(3),p.Tol(!e.formUsuario.controls.costoPorHora.valid&&e.formUsuario.controls.costoPorHora.dirty?"form-control is-invalid":"form-control"),p.xp6(1),p.Q6J("ngIf",!e.formUsuario.controls.costoPorHora.valid&&e.formUsuario.controls.costoPorHora.dirty&&"Administrador"!==e.formUsuario.controls.rol.value)}}function x(r,o){if(1&r&&(p.TgZ(0,"div",18),p.TgZ(1,"alert",19),p._UZ(2,"i",20),p._uU(3),p.qZA(),p.qZA()),2&r){var e=o.$implicit;p.xp6(1),p.Q6J("type",e.tipo)("dismissible",e.cerrable),p.xp6(2),p.Oqu(e.mensaje)}}function C(r,o){1&r&&(p.TgZ(0,"div",21),p._uU(1," Ingresa un correo electr\xf3nico v\xe1lido "),p.qZA())}var k=[{path:"",data:{title:"Usuario"},children:[{path:"crear",data:{title:"Crear"},component:function(){var r=function(){function r(o,i,n){var t=this;e(this,r),this.route=o,this.router=i,this._usuarioService=n,this.formUsuario=new s.cw({nombre:new s.NI("",s.kI.required),apellido:new s.NI("",s.kI.required),costoPorHora:new s.NI("0"),correoElectronico:new s.NI("",[s.kI.required,s.kI.email]),nombreUsuario:new s.NI("",s.kI.required),contrasena:new s.NI("",[s.kI.required,s.kI.minLength(6)]),confirmarContrasena:new s.NI("",s.kI.required),rol:new s.NI("Administrador",[s.kI.required])}),this.subscripcionForm=this.formUsuario.valueChanges.subscribe(function(r){t.formUsuario.controls.confirmarContrasena.setErrors(r.contrasena!==r.confirmarContrasena?{mismatch:!0}:null),"Administrador"!==r.rol&&t.formUsuario.controls.rol.addValidators([s.kI.required,s.kI.min(1)])}),this.alertas=[]}return o(r,[{key:"ngOnDestroy",value:function(){this.subscripcionCrear&&this.subscripcionCrear.unsubscribe(),this.subscripcionForm&&this.subscripcionForm.unsubscribe()}},{key:"enviarForm",value:function(){var r=this;this.resetAlertas(),this.formUsuario.valid?this.subscripcionCrear=this._usuarioService.crearUsuario(this.formUsuario.value).subscribe(function(o){o.exito&&(201===o.codigoEstado||200===o.codigoEstado)&&(r.resetForm(!1),r.generarAlerta(o.mensaje,"exito"))},function(o){r.generarAlerta(o.error&&o.error.Mensaje?o.error.Mensaje:"Ha ocurrido un error inesperado","error")}):this.generarAlerta("Por favor, revisa los campos ingresados","error")}},{key:"resetForm",value:function(){var r=!(arguments.length>0&&void 0!==arguments[0])||arguments[0];this.formUsuario.reset(),this.formUsuario.controls.rol.setValue("Administrador"),r&&this.resetAlertas()}},{key:"cancelarForm",value:function(){this.router.navigate(["/proyecto/listado"])}},{key:"resetAlertas",value:function(){this.alertas=[]}},{key:"generarAlerta",value:function(r,o){this.alertas.push("error"===o?{mensaje:r,cerrable:!0,tipo:"danger"}:{mensaje:r,cerrable:!0,tipo:"success"})}}]),r}();return r.\u0275fac=function(o){return new(o||r)(p.Y36(d.gz),p.Y36(d.F0),p.Y36(g))},r.\u0275cmp=p.Xpm({type:r,selectors:[["ng-component"]],decls:61,vars:25,consts:[[1,"animated","fadeIn"],[1,"row"],[1,"col-sm-6"],["class","font-weight-bold",4,"ngFor","ngForOf"],[1,"col-sm-6","col-lg-6"],[1,"card"],[1,"card-header"],[1,"card-body"],[3,"formGroup"],[1,"form-group"],["for","nombre",1,"mt-2"],["type","text","formControlName","nombre","id","nombre","placeholder","Ingresa el nombre","required",""],["class","invalid-feedback",4,"ngIf"],["for","apellido",1,"mt-2"],["type","text","formControlName","apellido","id","apellido","placeholder","Ingresa el apellido","required",""],["for","nombreUsuario",1,"mt-2"],["type","text","formControlName","nombreUsuario","id","nombreUsuario","placeholder","Ingresa el nombre de usuario","required",""],["for","rol",1,"mt-2"],["id","rol","formControlName","rol","required",""],["for","correoElectronico",1,"mt-2"],["type","email","formControlName","correoElectronico","id","correoElectronico","placeholder","Ingresa el correo electr\xf3nico","required",""],["for","contrasena",1,"mt-2"],["type","password","formControlName","contrasena","id","contrasena","placeholder","Ingresa la contrase\xf1a","required",""],["for","confirmarContrasena",1,"mt-2"],["type","password","formControlName","confirmarContrasena","id","confirmarContrasena","placeholder","Ingresa la confirmaci\xf3n de la contrase\xf1a","required",""],["class","form-group",4,"ngIf"],[1,"card-footer","d-flex","flex-row","justify-content-end"],["type","submit",1,"btn","btn-md","btn-primary","mr-2",3,"disabled","click"],[1,"fa","fa-save","mr-1"],["type","reset",1,"btn","btn-md","btn-danger",3,"click"],[1,"fa","fa-remove","mr-1"],[1,"font-weight-bold"],[3,"type","dismissible"],[1,"fa","fa-exclamation-circle","mr-1"],[1,"invalid-feedback"],["for","costoPorHora",1,"mt-2"],["type","number","min","1","formControlName","costoPorHora","id","costoPorHora","placeholder","Ingresa el costo por hora","required",""]],template:function(r,o){1&r&&(p.TgZ(0,"div",0),p.TgZ(1,"div",1),p.TgZ(2,"div",2),p.YNc(3,Z,4,3,"div",3),p.qZA(),p.qZA(),p.TgZ(4,"div",1),p.TgZ(5,"div",4),p.TgZ(6,"div",5),p.TgZ(7,"div",6),p.TgZ(8,"h5"),p._uU(9,"Crear Usuario"),p.qZA(),p.qZA(),p.TgZ(10,"div",7),p.TgZ(11,"form",8),p.TgZ(12,"div",9),p.TgZ(13,"label",10),p._uU(14,"Nombre (*)"),p.qZA(),p._UZ(15,"input",11),p.YNc(16,U,2,0,"div",12),p.qZA(),p.TgZ(17,"div",9),p.TgZ(18,"label",13),p._uU(19,"Apellido (*)"),p.qZA(),p._UZ(20,"input",14),p.YNc(21,b,2,0,"div",12),p.qZA(),p.TgZ(22,"div",9),p.TgZ(23,"label",15),p._uU(24,"Nombre de usuario (*)"),p.qZA(),p._UZ(25,"input",16),p.YNc(26,h,2,0,"div",12),p.qZA(),p.TgZ(27,"div",9),p.TgZ(28,"label",17),p._uU(29,"Rol (*)"),p.qZA(),p.TgZ(30,"select",18),p.TgZ(31,"option"),p._uU(32,"Administrador"),p.qZA(),p.TgZ(33,"option"),p._uU(34,"Desarrollador"),p.qZA(),p.TgZ(35,"option"),p._uU(36,"Tester"),p.qZA(),p.qZA(),p.YNc(37,A,2,0,"div",12),p.qZA(),p.TgZ(38,"div",9),p.TgZ(39,"label",19),p._uU(40,"Correo electr\xf3nico (*)"),p.qZA(),p._UZ(41,"input",20),p.YNc(42,q,2,0,"div",12),p.qZA(),p.TgZ(43,"div",9),p.TgZ(44,"label",21),p._uU(45,"Contrase\xf1a (*)"),p.qZA(),p._UZ(46,"input",22),p.YNc(47,T,2,0,"div",12),p.qZA(),p.TgZ(48,"div",9),p.TgZ(49,"label",23),p._uU(50,"Confirmar contrase\xf1a (*)"),p.qZA(),p._UZ(51,"input",24),p.YNc(52,y,2,0,"div",12),p.qZA(),p.YNc(53,I,5,3,"div",25),p.qZA(),p.qZA(),p.TgZ(54,"div",26),p.TgZ(55,"button",27),p.NdJ("click",function(){return o.enviarForm()}),p._UZ(56,"i",28),p._uU(57,"Guardar "),p.qZA(),p.TgZ(58,"button",29),p.NdJ("click",function(){return o.cancelarForm()}),p._UZ(59,"i",30),p._uU(60,"Cancelar "),p.qZA(),p.qZA(),p.qZA(),p.qZA(),p.qZA(),p.qZA()),2&r&&(p.xp6(3),p.Q6J("ngForOf",o.alertas),p.xp6(8),p.Q6J("formGroup",o.formUsuario),p.xp6(4),p.Tol(!o.formUsuario.controls.nombre.valid&&o.formUsuario.controls.nombre.dirty?"form-control is-invalid":"form-control"),p.xp6(1),p.Q6J("ngIf",!o.formUsuario.controls.nombre.valid&&o.formUsuario.controls.nombre.dirty),p.xp6(4),p.Tol(!o.formUsuario.controls.apellido.valid&&o.formUsuario.controls.apellido.dirty?"form-control is-invalid":"form-control"),p.xp6(1),p.Q6J("ngIf",!o.formUsuario.controls.apellido.valid&&o.formUsuario.controls.apellido.dirty),p.xp6(4),p.Tol(!o.formUsuario.controls.nombreUsuario.valid&&o.formUsuario.controls.nombreUsuario.dirty?"form-control is-invalid":"form-control"),p.xp6(1),p.Q6J("ngIf",!o.formUsuario.controls.nombreUsuario.valid&&o.formUsuario.controls.nombreUsuario.dirty),p.xp6(4),p.Tol(o.formUsuario.controls.rol.valid?"form-control":"form-control is-invalid"),p.xp6(7),p.Q6J("ngIf",!o.formUsuario.controls.rol.valid&&o.formUsuario.controls.rol.dirty),p.xp6(4),p.Tol(!o.formUsuario.controls.correoElectronico.valid&&o.formUsuario.controls.correoElectronico.dirty?"form-control is-invalid":"form-control"),p.xp6(1),p.Q6J("ngIf",!o.formUsuario.controls.correoElectronico.valid&&o.formUsuario.controls.correoElectronico.dirty),p.xp6(4),p.Tol(!o.formUsuario.controls.contrasena.valid&&o.formUsuario.controls.contrasena.dirty?"form-control is-invalid":"form-control"),p.xp6(1),p.Q6J("ngIf",!o.formUsuario.controls.contrasena.valid&&o.formUsuario.controls.contrasena.dirty),p.xp6(4),p.Tol(!o.formUsuario.controls.confirmarContrasena.valid&&o.formUsuario.controls.confirmarContrasena.dirty?"form-control is-invalid":"form-control"),p.xp6(1),p.Q6J("ngIf",!o.formUsuario.controls.confirmarContrasena.valid&&o.formUsuario.controls.confirmarContrasena.dirty),p.xp6(1),p.Q6J("ngIf","Administrador"!==o.formUsuario.controls.rol.value),p.xp6(2),p.Q6J("disabled",o.formUsuario.invalid))},directives:[a.sg,s._Y,s.JL,s.sg,s.Fj,s.JJ,s.u,s.Q7,a.O5,s.EJ,s.YN,s.Kr,l.wx,s.qQ,s.wV],encapsulation:2}),r}()},{path:"consulta",data:{title:"Consulta"},component:function(){var r=function(){function r(o,i,n){e(this,r),this.route=o,this.router=i,this._usuarioService=n,this.formUsuario=new s.cw({correoElectronico:new s.NI("",[s.kI.required,s.kI.email])}),this.alertas=[]}return o(r,[{key:"ngOnDestroy",value:function(){this.subscripcionCrear&&this.subscripcionCrear.unsubscribe()}},{key:"enviarForm",value:function(){var r=this;if(this.resetAlertas(),this.formUsuario.valid){var o=this.formUsuario.value.correoElectronico;this.subscripcionCrear=this._usuarioService.obtenerBugsResueltosPorUsuario(o).subscribe(function(e){e.exito&&(201===e.codigoEstado||200===e.codigoEstado)&&(r.resetForm(!1),r.generarAlerta("El usuario ".concat(o," resolvi\xf3 ").concat(e.resultado," bugs"),"exito"))},function(o){r.generarAlerta(o.error&&o.error.Mensaje?o.error.Mensaje:"Ha ocurrido un error inesperado","error")})}else this.generarAlerta("Por favor, revisa los campos ingresados","error")}},{key:"resetForm",value:function(){var r=!(arguments.length>0&&void 0!==arguments[0])||arguments[0];this.formUsuario.reset(),r&&this.resetAlertas()}},{key:"cancelarForm",value:function(){this.router.navigate(["/proyecto/listado"])}},{key:"resetAlertas",value:function(){this.alertas=[]}},{key:"generarAlerta",value:function(r,o){this.alertas.push("error"===o?{mensaje:r,cerrable:!0,tipo:"danger"}:"success"===o?{mensaje:r,cerrable:!0,tipo:"success"}:{mensaje:r,cerrable:!0,tipo:"info"})}}]),r}();return r.\u0275fac=function(o){return new(o||r)(p.Y36(d.gz),p.Y36(d.F0),p.Y36(g))},r.\u0275cmp=p.Xpm({type:r,selectors:[["ng-component"]],decls:24,vars:6,consts:[[1,"animated","fadeIn"],[1,"row"],[1,"col-sm-6"],["class","font-weight-bold",4,"ngFor","ngForOf"],[1,"col-sm-6","col-lg-6"],[1,"card"],[1,"card-header"],[1,"card-body"],[3,"formGroup"],[1,"form-group"],["for","correoElectronico",1,"mt-2"],["type","email","formControlName","correoElectronico","id","correoElectronico","placeholder","Ingresa el correo electr\xf3nico","required",""],["class","invalid-feedback",4,"ngIf"],[1,"card-footer","d-flex","flex-row","justify-content-end"],["type","submit",1,"btn","btn-md","btn-primary","mr-2",3,"disabled","click"],[1,"fa","fa-save","mr-1"],["type","reset",1,"btn","btn-md","btn-danger",3,"click"],[1,"fa","fa-remove","mr-1"],[1,"font-weight-bold"],[3,"type","dismissible"],[1,"fa","fa-exclamation-circle","mr-1"],[1,"invalid-feedback"]],template:function(r,o){1&r&&(p.TgZ(0,"div",0),p.TgZ(1,"div",1),p.TgZ(2,"div",2),p.YNc(3,x,4,3,"div",3),p.qZA(),p.qZA(),p.TgZ(4,"div",1),p.TgZ(5,"div",4),p.TgZ(6,"div",5),p.TgZ(7,"div",6),p.TgZ(8,"h5"),p._uU(9,"Cantidad de Bugs por Usuario"),p.qZA(),p.qZA(),p.TgZ(10,"div",7),p.TgZ(11,"form",8),p.TgZ(12,"div",9),p.TgZ(13,"label",10),p._uU(14,"Correo electr\xf3nico (*)"),p.qZA(),p._UZ(15,"input",11),p.YNc(16,C,2,0,"div",12),p.qZA(),p.qZA(),p.qZA(),p.TgZ(17,"div",13),p.TgZ(18,"button",14),p.NdJ("click",function(){return o.enviarForm()}),p._UZ(19,"i",15),p._uU(20,"Guardar "),p.qZA(),p.TgZ(21,"button",16),p.NdJ("click",function(){return o.cancelarForm()}),p._UZ(22,"i",17),p._uU(23,"Cancelar "),p.qZA(),p.qZA(),p.qZA(),p.qZA(),p.qZA(),p.qZA()),2&r&&(p.xp6(3),p.Q6J("ngForOf",o.alertas),p.xp6(8),p.Q6J("formGroup",o.formUsuario),p.xp6(4),p.Tol(!o.formUsuario.controls.correoElectronico.valid&&o.formUsuario.controls.correoElectronico.dirty?"form-control is-invalid":"form-control"),p.xp6(1),p.Q6J("ngIf",!o.formUsuario.controls.correoElectronico.valid&&o.formUsuario.controls.correoElectronico.dirty),p.xp6(2),p.Q6J("disabled",o.formUsuario.invalid))},directives:[a.sg,s._Y,s.JL,s.sg,s.Fj,s.JJ,s.u,s.Q7,a.O5,l.wx],encapsulation:2}),r}()}]}],w=function(){var r=function r(){e(this,r)};return r.\u0275fac=function(o){return new(o||r)},r.\u0275mod=p.oAB({type:r}),r.\u0275inj=p.cJS({imports:[[d.Bz.forChild(k)],d.Bz]}),r}(),N=function(){var r=function r(){e(this,r)};return r.\u0275fac=function(o){return new(o||r)},r.\u0275mod=p.oAB({type:r}),r.\u0275inj=p.cJS({imports:[[a.ez,s.UX,s.u5,w,c.Fq.forRoot(),l.nM.forRoot(),u.zk.forRoot(),s.UX]]}),r}()}}])}();