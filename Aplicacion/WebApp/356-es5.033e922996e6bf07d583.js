!function(){"use strict";function r(r,o){for(var e=0;e<o.length;e++){var t=o[e];t.enumerable=t.enumerable||!1,t.configurable=!0,"value"in t&&(t.writable=!0),Object.defineProperty(r,t.key,t)}}function o(o,e,t){return e&&r(o.prototype,e),t&&r(o,t),o}function e(r,o){if(!(r instanceof o))throw new TypeError("Cannot call a class as a function")}(self.webpackChunkjiru_webapp=self.webpackChunkjiru_webapp||[]).push([[356],{61356:function(r,t,n){n.r(t),n.d(t,{TareaModule:function(){return w}});var a,i=n(12057),c=n(23738),s=n(37911),u=n(95859),l=n(88002),d=n(92340),f=n(60793),p=n(58497),Z=((a=function r(o){var t=this;e(this,r),this._httpClient=o,this.crearTarea=function(r){return t._httpClient.post("".concat(t.apiUrl,"/tarea"),r).pipe((0,l.U)(function(r){return r}))},this.obtenerTareas=function(){return t._httpClient.get("".concat(t.apiUrl,"/tarea")).pipe((0,l.U)(function(r){return r}))},this.apiUrl=d.N.apiUrl}).\u0275fac=function(r){return new(r||a)(f.LFG(p.eN))},a.\u0275prov=f.Yz7({token:a,factory:a.\u0275fac,providedIn:"root"}),a),m=n(77919),g=n(23419);function v(r,o){if(1&r&&(f.TgZ(0,"div",26),f.TgZ(1,"alert",27),f._UZ(2,"i",28),f._uU(3),f.qZA(),f.qZA()),2&r){var e=o.$implicit;f.xp6(1),f.Q6J("type",e.tipo)("dismissible",e.cerrable),f.xp6(2),f.Oqu(e.mensaje)}}function b(r,o){1&r&&(f.TgZ(0,"div",29),f._uU(1," Ingresa un nombre v\xe1lido "),f.qZA())}function T(r,o){if(1&r&&(f.TgZ(0,"option",15),f._uU(1),f.qZA()),2&r){var e=o.$implicit;f.Q6J("value",e.id),f.xp6(1),f.Oqu(e.nombre)}}function h(r,o){1&r&&(f.TgZ(0,"div",29),f._uU(1," Selecciona un proyecto v\xe1lido "),f.qZA())}function q(r,o){1&r&&(f.TgZ(0,"div",29),f._uU(1," Ingresa un costo por hora v\xe1lido "),f.qZA())}function A(r,o){1&r&&(f.TgZ(0,"div",29),f._uU(1," Ingresa una duraci\xf3n en horas v\xe1lida "),f.qZA())}var y=function(){var r=function(){function r(o,t,n,a){e(this,r),this.route=o,this.router=t,this._proyectoService=n,this._tareaService=a,this.formTarea=new c.cw({nombre:new c.NI("",c.kI.required),proyectoId:new c.NI("",c.kI.required),costoPorHora:new c.NI("",[c.kI.required,c.kI.min(1)]),duracionHoras:new c.NI("",[c.kI.required,c.kI.min(1)])}),this.alertas=[]}return o(r,[{key:"ngOnInit",value:function(){this.obtenerProyectos()}},{key:"ngOnDestroy",value:function(){this.subscripcionCrear&&this.subscripcionCrear.unsubscribe(),this.subscripcionObtenerProyectos&&this.subscripcionObtenerProyectos.unsubscribe()}},{key:"obtenerProyectos",value:function(){var r=this;this.subscripcionObtenerProyectos=this._proyectoService.obtenerProyectos().pipe().subscribe(function(o){r.proyectos=o.resultado})}},{key:"enviarForm",value:function(){var r=this;this.resetAlertas(),this.formTarea.valid?this.subscripcionCrear=this._tareaService.crearTarea(this.formTarea.value).subscribe(function(o){o.exito&&(201===o.codigoEstado||200===o.codigoEstado)&&(r.formTarea.reset(),r.generarAlerta(o.mensaje,"exito"))},function(o){r.generarAlerta(o.error&&o.error.Mensaje?o.error.Mensaje:"Ha ocurrido un error inesperado","error")}):this.generarAlerta("Por favor, revisa los campos ingresados","error")}},{key:"resetForm",value:function(){this.formTarea.reset(),this.resetAlertas()}},{key:"cancelarForm",value:function(){this.router.navigate(["/tarea/listado"])}},{key:"resetAlertas",value:function(){this.alertas=[]}},{key:"generarAlerta",value:function(r,o){this.alertas.push("error"===o?{mensaje:r,cerrable:!0,tipo:"danger"}:{mensaje:r,cerrable:!0,tipo:"success"})}}]),r}();return r.\u0275fac=function(o){return new(o||r)(f.Y36(u.gz),f.Y36(u.F0),f.Y36(m.X),f.Y36(Z))},r.\u0275cmp=f.Xpm({type:r,selectors:[["ng-component"]],decls:42,vars:17,consts:[[1,"animated","fadeIn"],[1,"row"],[1,"col-sm-6"],["class","font-weight-bold",4,"ngFor","ngForOf"],[1,"col-sm-6","col-lg-6"],[1,"card"],[1,"card-header"],[1,"card-body"],[3,"formGroup"],[1,"form-group"],["for","nombre"],["type","text","formControlName","nombre","id","nombre","placeholder","Ingresa el nombre de la tarea","required",""],["class","invalid-feedback",4,"ngIf"],["for","proyectoId",1,"mt-2"],["id","proyectoId","formControlName","proyectoId","required",""],[3,"value"],[3,"value",4,"ngFor","ngForOf"],["for","costoPorHora",1,"mt-2"],["type","number","min","1","formControlName","costoPorHora","id","costoPorHora","placeholder","Ingresa el costo por hora","required",""],["for","duracionHoras",1,"mt-2"],["type","number","min","1","formControlName","duracionHoras","id","duracionHoras","placeholder","Ingresa la duracion","required",""],[1,"card-footer","d-flex","flex-row","justify-content-end"],["type","submit",1,"btn","btn-md","btn-primary","mr-2",3,"disabled","click"],[1,"fa","fa-save","mr-1"],["type","reset",1,"btn","btn-md","btn-danger",3,"click"],[1,"fa","fa-remove","mr-1"],[1,"font-weight-bold"],[3,"type","dismissible"],[1,"fa","fa-exclamation-circle","mr-1"],[1,"invalid-feedback"]],template:function(r,o){1&r&&(f.TgZ(0,"div",0),f.TgZ(1,"div",1),f.TgZ(2,"div",2),f.YNc(3,v,4,3,"div",3),f.qZA(),f.qZA(),f.TgZ(4,"div",1),f.TgZ(5,"div",4),f.TgZ(6,"div",5),f.TgZ(7,"div",6),f.TgZ(8,"h5"),f._uU(9,"Crear Tarea"),f.qZA(),f.qZA(),f.TgZ(10,"div",7),f.TgZ(11,"form",8),f.TgZ(12,"div",9),f.TgZ(13,"label",10),f._uU(14,"Nombre (*)"),f.qZA(),f._UZ(15,"input",11),f.YNc(16,b,2,0,"div",12),f.qZA(),f.TgZ(17,"div",9),f.TgZ(18,"label",13),f._uU(19,"Proyecto (*)"),f.qZA(),f.TgZ(20,"select",14),f.TgZ(21,"option",15),f._uU(22,"Selecciona un proyecto"),f.qZA(),f.YNc(23,T,2,2,"option",16),f.qZA(),f.YNc(24,h,2,0,"div",12),f.qZA(),f.TgZ(25,"div",9),f.TgZ(26,"label",17),f._uU(27,"Costo por hora (*)"),f.qZA(),f._UZ(28,"input",18),f.YNc(29,q,2,0,"div",12),f.qZA(),f.TgZ(30,"div",9),f.TgZ(31,"label",19),f._uU(32,"Duracion (*)"),f.qZA(),f._UZ(33,"input",20),f.YNc(34,A,2,0,"div",12),f.qZA(),f.qZA(),f.qZA(),f.TgZ(35,"div",21),f.TgZ(36,"button",22),f.NdJ("click",function(){return o.enviarForm()}),f._UZ(37,"i",23),f._uU(38,"Guardar "),f.qZA(),f.TgZ(39,"button",24),f.NdJ("click",function(){return o.cancelarForm()}),f._UZ(40,"i",25),f._uU(41,"Cancelar "),f.qZA(),f.qZA(),f.qZA(),f.qZA(),f.qZA(),f.qZA()),2&r&&(f.xp6(3),f.Q6J("ngForOf",o.alertas),f.xp6(8),f.Q6J("formGroup",o.formTarea),f.xp6(4),f.Tol(!o.formTarea.controls.nombre.valid&&o.formTarea.controls.nombre.dirty?"form-control is-invalid":"form-control"),f.xp6(1),f.Q6J("ngIf",!o.formTarea.controls.nombre.valid&&o.formTarea.controls.nombre.dirty),f.xp6(4),f.Tol(!o.formTarea.controls.proyectoId.valid&&o.formTarea.controls.proyectoId.dirty?"form-control is-invalid":"form-control"),f.xp6(1),f.Q6J("value",""),f.xp6(2),f.Q6J("ngForOf",o.proyectos),f.xp6(1),f.Q6J("ngIf",!o.formTarea.controls.proyectoId.valid&&o.formTarea.controls.proyectoId.dirty),f.xp6(4),f.Tol(!o.formTarea.controls.costoPorHora.valid&&o.formTarea.controls.costoPorHora.dirty?"form-control is-invalid":"form-control"),f.xp6(1),f.Q6J("ngIf",!o.formTarea.controls.costoPorHora.valid&&o.formTarea.controls.costoPorHora.dirty),f.xp6(4),f.Tol(!o.formTarea.controls.duracionHoras.valid&&o.formTarea.controls.duracionHoras.dirty?"form-control is-invalid":"form-control"),f.xp6(1),f.Q6J("ngIf",!o.formTarea.controls.duracionHoras.valid&&o.formTarea.controls.duracionHoras.dirty),f.xp6(2),f.Q6J("disabled",o.formTarea.invalid))},directives:[i.sg,c._Y,c.JL,c.sg,c.Fj,c.JJ,c.u,c.Q7,i.O5,c.EJ,c.YN,c.Kr,c.qQ,c.wV,g.wx],encapsulation:2}),r}(),_=n(6282);function U(r,o){if(1&r&&(f.TgZ(0,"div",15),f.TgZ(1,"alert",16),f._UZ(2,"i",17),f._uU(3),f.qZA(),f.qZA()),2&r){var e=o.$implicit;f.xp6(1),f.Q6J("type",e.tipo)("dismissible",e.cerrable),f.xp6(2),f.Oqu(e.mensaje)}}function x(r,o){if(1&r&&(f.TgZ(0,"tr"),f.TgZ(1,"td",18),f.TgZ(2,"span"),f._uU(3),f.qZA(),f.qZA(),f.TgZ(4,"td",19),f.TgZ(5,"span"),f._uU(6),f.qZA(),f.qZA(),f.TgZ(7,"td",10),f.TgZ(8,"span",20),f._uU(9),f.qZA(),f.qZA(),f.TgZ(10,"td",10),f.TgZ(11,"span",21),f._uU(12),f.qZA(),f.qZA(),f.TgZ(13,"td",22),f.TgZ(14,"span"),f._uU(15),f.qZA(),f.qZA(),f.TgZ(16,"td",18),f.TgZ(17,"span"),f._uU(18),f.qZA(),f.qZA(),f.qZA()),2&r){var e=o.$implicit;f.xp6(3),f.Oqu(e.id),f.xp6(3),f.Oqu(e.nombre),f.xp6(3),f.hij("$",e.costoPorHora,""),f.xp6(3),f.hij("",e.duracionHoras," hs"),f.xp6(3),f.Oqu(e.proyecto.nombre),f.xp6(3),f.Oqu(e.proyecto.id)}}var I=[{path:"",data:{title:"Tarea"},children:[{path:"crear",data:{title:"Crear"},component:y},{path:"listado",data:{title:"Listado"},component:function(){var r=function(){function r(o,t){e(this,r),this._tareaService=o,this._modalService=t}return o(r,[{key:"ngOnInit",value:function(){this.obtenerTareas()}},{key:"ngOnDestroy",value:function(){this.subscripcionObtener&&this.subscripcionObtener.unsubscribe()}},{key:"obtenerTareas",value:function(){var r=this;this.subscripcionObtener=this._tareaService.obtenerTareas().pipe().subscribe(function(o){r.tareas=o.resultado})}}]),r}();return r.\u0275fac=function(o){return new(o||r)(f.Y36(Z),f.Y36(_.tT))},r.\u0275cmp=f.Xpm({type:r,selectors:[["ng-component"]],decls:31,vars:2,consts:[[1,"animated","fadeIn"],[1,"row"],[1,"col-sm-6"],["class","font-weight-bold",4,"ngFor","ngForOf"],[1,"col-sm-12","col-lg-12"],[1,"card"],[1,"card-header"],[1,"card-body"],[1,"table","table-responsive-sm","table-hover","table-outline","mb-0"],[1,"thead-light"],[1,"text-center"],[1,"fa","fa-money","mr-1"],[1,"fa","fa-clock-o","mr-1"],[4,"ngFor","ngForOf"],[1,"card-footer"],[1,"font-weight-bold"],[3,"type","dismissible"],[1,"fa","fa-exclamation-circle","mr-1"],[1,"text-center","font-xl","font-weight-bold"],[1,"font-xl"],[1,"badge","badge-dark","font-xl"],[1,"badge","badge-success","font-xl"],[1,"font-xl","text-center"]],template:function(r,o){1&r&&(f.TgZ(0,"div",0),f.TgZ(1,"div",1),f.TgZ(2,"div",2),f.YNc(3,U,4,3,"div",3),f.qZA(),f.qZA(),f.TgZ(4,"div",1),f.TgZ(5,"div",4),f.TgZ(6,"div",5),f.TgZ(7,"div",6),f.TgZ(8,"h5"),f._uU(9,"Listado de Tareas"),f.qZA(),f.qZA(),f.TgZ(10,"div",7),f.TgZ(11,"table",8),f.TgZ(12,"thead",9),f.TgZ(13,"tr"),f.TgZ(14,"th",10),f._uU(15,"ID"),f.qZA(),f.TgZ(16,"th"),f._uU(17,"Nombre"),f.qZA(),f.TgZ(18,"th",10),f._UZ(19,"i",11),f._uU(20,"Costo por hora"),f.qZA(),f.TgZ(21,"th",10),f._UZ(22,"i",12),f._uU(23,"Duraci\xf3n"),f.qZA(),f.TgZ(24,"th",10),f._uU(25,"Proyecto"),f.qZA(),f.TgZ(26,"th",10),f._uU(27,"ID Proyecto"),f.qZA(),f.qZA(),f.qZA(),f.TgZ(28,"tbody"),f.YNc(29,x,19,6,"tr",13),f.qZA(),f.qZA(),f.qZA(),f._UZ(30,"div",14),f.qZA(),f.qZA(),f.qZA(),f.qZA()),2&r&&(f.xp6(3),f.Q6J("ngForOf",o.alertas),f.xp6(26),f.Q6J("ngForOf",o.tareas))},directives:[i.sg,g.wx],encapsulation:2}),r}()}]}],k=function(){var r=function r(){e(this,r)};return r.\u0275fac=function(o){return new(o||r)},r.\u0275mod=f.oAB({type:r}),r.\u0275inj=f.cJS({imports:[[u.Bz.forChild(I)],u.Bz]}),r}(),w=function(){var r=function r(){e(this,r)};return r.\u0275fac=function(o){return new(o||r)},r.\u0275mod=f.oAB({type:r}),r.\u0275inj=f.cJS({imports:[[i.ez,c.UX,c.u5,k,s.Fq.forRoot(),g.nM.forRoot(),_.zk.forRoot()]]}),r}()}}])}();