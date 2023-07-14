"use strict";(self.webpackChunkjiru_webapp=self.webpackChunkjiru_webapp||[]).push([[356],{61356:function(r,o,e){e.r(o),e.d(o,{TareaModule:function(){return x}});var t=e(12057),a=e(23738),i=e(37911),n=e(95859),s=e(88002),c=e(92340),l=e(60793),d=e(58497);let u=(()=>{class r{constructor(r){this._httpClient=r,this.crearTarea=r=>this._httpClient.post(`${this.apiUrl}/tarea`,r).pipe((0,s.U)(r=>r)),this.obtenerTareas=()=>this._httpClient.get(`${this.apiUrl}/tarea`).pipe((0,s.U)(r=>r)),this.apiUrl=c.N.apiUrl}}return r.\u0275fac=function(o){return new(o||r)(l.LFG(d.eN))},r.\u0275prov=l.Yz7({token:r,factory:r.\u0275fac,providedIn:"root"}),r})();var p=e(77919),Z=e(23419);function f(r,o){if(1&r&&(l.TgZ(0,"div",26),l.TgZ(1,"alert",27),l._UZ(2,"i",28),l._uU(3),l.qZA(),l.qZA()),2&r){const r=o.$implicit;l.xp6(1),l.Q6J("type",r.tipo)("dismissible",r.cerrable),l.xp6(2),l.Oqu(r.mensaje)}}function m(r,o){1&r&&(l.TgZ(0,"div",29),l._uU(1," Ingresa un nombre v\xe1lido "),l.qZA())}function g(r,o){if(1&r&&(l.TgZ(0,"option",15),l._uU(1),l.qZA()),2&r){const r=o.$implicit;l.Q6J("value",r.id),l.xp6(1),l.Oqu(r.nombre)}}function T(r,o){1&r&&(l.TgZ(0,"div",29),l._uU(1," Selecciona un proyecto v\xe1lido "),l.qZA())}function b(r,o){1&r&&(l.TgZ(0,"div",29),l._uU(1," Ingresa un costo por hora v\xe1lido "),l.qZA())}function h(r,o){1&r&&(l.TgZ(0,"div",29),l._uU(1," Ingresa una duraci\xf3n en horas v\xe1lida "),l.qZA())}let v=(()=>{class r{constructor(r,o,e,t){this.route=r,this.router=o,this._proyectoService=e,this._tareaService=t,this.formTarea=new a.cw({nombre:new a.NI("",a.kI.required),proyectoId:new a.NI("",a.kI.required),costoPorHora:new a.NI("",[a.kI.required,a.kI.min(1)]),duracionHoras:new a.NI("",[a.kI.required,a.kI.min(1)])}),this.alertas=[]}ngOnInit(){this.obtenerProyectos()}ngOnDestroy(){this.subscripcionCrear&&this.subscripcionCrear.unsubscribe(),this.subscripcionObtenerProyectos&&this.subscripcionObtenerProyectos.unsubscribe()}obtenerProyectos(){this.subscripcionObtenerProyectos=this._proyectoService.obtenerProyectos().pipe().subscribe(r=>{this.proyectos=r.resultado})}enviarForm(){this.resetAlertas(),this.formTarea.valid?this.subscripcionCrear=this._tareaService.crearTarea(this.formTarea.value).subscribe(r=>{r.exito&&(201===r.codigoEstado||200===r.codigoEstado)&&(this.formTarea.reset(),this.generarAlerta(r.mensaje,"exito"))},r=>{this.generarAlerta(r.error&&r.error.Mensaje?r.error.Mensaje:"Ha ocurrido un error inesperado","error")}):this.generarAlerta("Por favor, revisa los campos ingresados","error")}resetForm(){this.formTarea.reset(),this.resetAlertas()}cancelarForm(){this.router.navigate(["/tarea/listado"])}resetAlertas(){this.alertas=[]}generarAlerta(r,o){this.alertas.push("error"===o?{mensaje:r,cerrable:!0,tipo:"danger"}:{mensaje:r,cerrable:!0,tipo:"success"})}}return r.\u0275fac=function(o){return new(o||r)(l.Y36(n.gz),l.Y36(n.F0),l.Y36(p.X),l.Y36(u))},r.\u0275cmp=l.Xpm({type:r,selectors:[["ng-component"]],decls:42,vars:17,consts:[[1,"animated","fadeIn"],[1,"row"],[1,"col-sm-6"],["class","font-weight-bold",4,"ngFor","ngForOf"],[1,"col-sm-6","col-lg-6"],[1,"card"],[1,"card-header"],[1,"card-body"],[3,"formGroup"],[1,"form-group"],["for","nombre"],["type","text","formControlName","nombre","id","nombre","placeholder","Ingresa el nombre de la tarea","required",""],["class","invalid-feedback",4,"ngIf"],["for","proyectoId",1,"mt-2"],["id","proyectoId","formControlName","proyectoId","required",""],[3,"value"],[3,"value",4,"ngFor","ngForOf"],["for","costoPorHora",1,"mt-2"],["type","number","min","1","formControlName","costoPorHora","id","costoPorHora","placeholder","Ingresa el costo por hora","required",""],["for","duracionHoras",1,"mt-2"],["type","number","min","1","formControlName","duracionHoras","id","duracionHoras","placeholder","Ingresa la duracion","required",""],[1,"card-footer","d-flex","flex-row","justify-content-end"],["type","submit",1,"btn","btn-md","btn-primary","mr-2",3,"disabled","click"],[1,"fa","fa-save","mr-1"],["type","reset",1,"btn","btn-md","btn-danger",3,"click"],[1,"fa","fa-remove","mr-1"],[1,"font-weight-bold"],[3,"type","dismissible"],[1,"fa","fa-exclamation-circle","mr-1"],[1,"invalid-feedback"]],template:function(r,o){1&r&&(l.TgZ(0,"div",0),l.TgZ(1,"div",1),l.TgZ(2,"div",2),l.YNc(3,f,4,3,"div",3),l.qZA(),l.qZA(),l.TgZ(4,"div",1),l.TgZ(5,"div",4),l.TgZ(6,"div",5),l.TgZ(7,"div",6),l.TgZ(8,"h5"),l._uU(9,"Crear Tarea"),l.qZA(),l.qZA(),l.TgZ(10,"div",7),l.TgZ(11,"form",8),l.TgZ(12,"div",9),l.TgZ(13,"label",10),l._uU(14,"Nombre (*)"),l.qZA(),l._UZ(15,"input",11),l.YNc(16,m,2,0,"div",12),l.qZA(),l.TgZ(17,"div",9),l.TgZ(18,"label",13),l._uU(19,"Proyecto (*)"),l.qZA(),l.TgZ(20,"select",14),l.TgZ(21,"option",15),l._uU(22,"Selecciona un proyecto"),l.qZA(),l.YNc(23,g,2,2,"option",16),l.qZA(),l.YNc(24,T,2,0,"div",12),l.qZA(),l.TgZ(25,"div",9),l.TgZ(26,"label",17),l._uU(27,"Costo por hora (*)"),l.qZA(),l._UZ(28,"input",18),l.YNc(29,b,2,0,"div",12),l.qZA(),l.TgZ(30,"div",9),l.TgZ(31,"label",19),l._uU(32,"Duracion (*)"),l.qZA(),l._UZ(33,"input",20),l.YNc(34,h,2,0,"div",12),l.qZA(),l.qZA(),l.qZA(),l.TgZ(35,"div",21),l.TgZ(36,"button",22),l.NdJ("click",function(){return o.enviarForm()}),l._UZ(37,"i",23),l._uU(38,"Guardar "),l.qZA(),l.TgZ(39,"button",24),l.NdJ("click",function(){return o.cancelarForm()}),l._UZ(40,"i",25),l._uU(41,"Cancelar "),l.qZA(),l.qZA(),l.qZA(),l.qZA(),l.qZA(),l.qZA()),2&r&&(l.xp6(3),l.Q6J("ngForOf",o.alertas),l.xp6(8),l.Q6J("formGroup",o.formTarea),l.xp6(4),l.Tol(!o.formTarea.controls.nombre.valid&&o.formTarea.controls.nombre.dirty?"form-control is-invalid":"form-control"),l.xp6(1),l.Q6J("ngIf",!o.formTarea.controls.nombre.valid&&o.formTarea.controls.nombre.dirty),l.xp6(4),l.Tol(!o.formTarea.controls.proyectoId.valid&&o.formTarea.controls.proyectoId.dirty?"form-control is-invalid":"form-control"),l.xp6(1),l.Q6J("value",""),l.xp6(2),l.Q6J("ngForOf",o.proyectos),l.xp6(1),l.Q6J("ngIf",!o.formTarea.controls.proyectoId.valid&&o.formTarea.controls.proyectoId.dirty),l.xp6(4),l.Tol(!o.formTarea.controls.costoPorHora.valid&&o.formTarea.controls.costoPorHora.dirty?"form-control is-invalid":"form-control"),l.xp6(1),l.Q6J("ngIf",!o.formTarea.controls.costoPorHora.valid&&o.formTarea.controls.costoPorHora.dirty),l.xp6(4),l.Tol(!o.formTarea.controls.duracionHoras.valid&&o.formTarea.controls.duracionHoras.dirty?"form-control is-invalid":"form-control"),l.xp6(1),l.Q6J("ngIf",!o.formTarea.controls.duracionHoras.valid&&o.formTarea.controls.duracionHoras.dirty),l.xp6(2),l.Q6J("disabled",o.formTarea.invalid))},directives:[t.sg,a._Y,a.JL,a.sg,a.Fj,a.JJ,a.u,a.Q7,t.O5,a.EJ,a.YN,a.Kr,a.qQ,a.wV,Z.wx],encapsulation:2}),r})();var q=e(6282);function A(r,o){if(1&r&&(l.TgZ(0,"div",15),l.TgZ(1,"alert",16),l._UZ(2,"i",17),l._uU(3),l.qZA(),l.qZA()),2&r){const r=o.$implicit;l.xp6(1),l.Q6J("type",r.tipo)("dismissible",r.cerrable),l.xp6(2),l.Oqu(r.mensaje)}}function y(r,o){if(1&r&&(l.TgZ(0,"tr"),l.TgZ(1,"td",18),l.TgZ(2,"span"),l._uU(3),l.qZA(),l.qZA(),l.TgZ(4,"td",19),l.TgZ(5,"span"),l._uU(6),l.qZA(),l.qZA(),l.TgZ(7,"td",10),l.TgZ(8,"span",20),l._uU(9),l.qZA(),l.qZA(),l.TgZ(10,"td",10),l.TgZ(11,"span",21),l._uU(12),l.qZA(),l.qZA(),l.TgZ(13,"td",22),l.TgZ(14,"span"),l._uU(15),l.qZA(),l.qZA(),l.TgZ(16,"td",18),l.TgZ(17,"span"),l._uU(18),l.qZA(),l.qZA(),l.qZA()),2&r){const r=o.$implicit;l.xp6(3),l.Oqu(r.id),l.xp6(3),l.Oqu(r.nombre),l.xp6(3),l.hij("$",r.costoPorHora,""),l.xp6(3),l.hij("",r.duracionHoras," hs"),l.xp6(3),l.Oqu(r.proyecto.nombre),l.xp6(3),l.Oqu(r.proyecto.id)}}const _=[{path:"",data:{title:"Tarea"},children:[{path:"crear",data:{title:"Crear"},component:v},{path:"listado",data:{title:"Listado"},component:(()=>{class r{constructor(r,o){this._tareaService=r,this._modalService=o}ngOnInit(){this.obtenerTareas()}ngOnDestroy(){this.subscripcionObtener&&this.subscripcionObtener.unsubscribe()}obtenerTareas(){this.subscripcionObtener=this._tareaService.obtenerTareas().pipe().subscribe(r=>{this.tareas=r.resultado})}}return r.\u0275fac=function(o){return new(o||r)(l.Y36(u),l.Y36(q.tT))},r.\u0275cmp=l.Xpm({type:r,selectors:[["ng-component"]],decls:31,vars:2,consts:[[1,"animated","fadeIn"],[1,"row"],[1,"col-sm-6"],["class","font-weight-bold",4,"ngFor","ngForOf"],[1,"col-sm-12","col-lg-12"],[1,"card"],[1,"card-header"],[1,"card-body"],[1,"table","table-responsive-sm","table-hover","table-outline","mb-0"],[1,"thead-light"],[1,"text-center"],[1,"fa","fa-money","mr-1"],[1,"fa","fa-clock-o","mr-1"],[4,"ngFor","ngForOf"],[1,"card-footer"],[1,"font-weight-bold"],[3,"type","dismissible"],[1,"fa","fa-exclamation-circle","mr-1"],[1,"text-center","font-xl","font-weight-bold"],[1,"font-xl"],[1,"badge","badge-dark","font-xl"],[1,"badge","badge-success","font-xl"],[1,"font-xl","text-center"]],template:function(r,o){1&r&&(l.TgZ(0,"div",0),l.TgZ(1,"div",1),l.TgZ(2,"div",2),l.YNc(3,A,4,3,"div",3),l.qZA(),l.qZA(),l.TgZ(4,"div",1),l.TgZ(5,"div",4),l.TgZ(6,"div",5),l.TgZ(7,"div",6),l.TgZ(8,"h5"),l._uU(9,"Listado de Tareas"),l.qZA(),l.qZA(),l.TgZ(10,"div",7),l.TgZ(11,"table",8),l.TgZ(12,"thead",9),l.TgZ(13,"tr"),l.TgZ(14,"th",10),l._uU(15,"ID"),l.qZA(),l.TgZ(16,"th"),l._uU(17,"Nombre"),l.qZA(),l.TgZ(18,"th",10),l._UZ(19,"i",11),l._uU(20,"Costo por hora"),l.qZA(),l.TgZ(21,"th",10),l._UZ(22,"i",12),l._uU(23,"Duraci\xf3n"),l.qZA(),l.TgZ(24,"th",10),l._uU(25,"Proyecto"),l.qZA(),l.TgZ(26,"th",10),l._uU(27,"ID Proyecto"),l.qZA(),l.qZA(),l.qZA(),l.TgZ(28,"tbody"),l.YNc(29,y,19,6,"tr",13),l.qZA(),l.qZA(),l.qZA(),l._UZ(30,"div",14),l.qZA(),l.qZA(),l.qZA(),l.qZA()),2&r&&(l.xp6(3),l.Q6J("ngForOf",o.alertas),l.xp6(26),l.Q6J("ngForOf",o.tareas))},directives:[t.sg,Z.wx],encapsulation:2}),r})()}]}];let U=(()=>{class r{}return r.\u0275fac=function(o){return new(o||r)},r.\u0275mod=l.oAB({type:r}),r.\u0275inj=l.cJS({imports:[[n.Bz.forChild(_)],n.Bz]}),r})(),x=(()=>{class r{}return r.\u0275fac=function(o){return new(o||r)},r.\u0275mod=l.oAB({type:r}),r.\u0275inj=l.cJS({imports:[[t.ez,a.UX,a.u5,U,i.Fq.forRoot(),Z.nM.forRoot(),q.zk.forRoot()]]}),r})()}}]);