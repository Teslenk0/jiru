!function(){"use strict";function t(t,n){if(!(t instanceof n))throw new TypeError("Cannot call a class as a function")}function n(t,n){for(var e=0;e<n.length;e++){var o=n[e];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(t,o.key,o)}}(self.webpackChunkjiru_webapp=self.webpackChunkjiru_webapp||[]).push([[592],{86308:function(e,o,r){r.d(o,{f:function(){return u}});var i,c=r(60793),a=r(6282),u=((i=function(){function e(n,o){t(this,e),this.bsModalRef=n,this._bsModalService=o}var o,r,i;return o=e,(r=[{key:"confirmar",value:function(){this._bsModalService.setDismissReason("operacion-confirmada"),this.bsModalRef.hide()}}])&&n(o.prototype,r),i&&n(o,i),e}()).\u0275fac=function(t){return new(t||i)(c.Y36(a.UZ),c.Y36(a.tT))},i.\u0275cmp=c.Xpm({type:i,selectors:[["app-eliminar-modal"]],decls:14,vars:4,consts:[[1,"modal-header"],[1,"modal-title","pull-left"],["type","button","aria-label","Close",1,"btn-close","close","pull-right",3,"click"],["aria-hidden","true",1,"visually-hidden"],[1,"modal-body"],[1,"font-lg","font-weight-bold"],[1,"modal-footer"],["type","button",1,"btn","btn-danger",3,"click"],["type","button",1,"btn","btn-success",3,"click"]],template:function(t,n){1&t&&(c.TgZ(0,"div",0),c.TgZ(1,"h4",1),c._uU(2),c.qZA(),c.TgZ(3,"button",2),c.NdJ("click",function(){return n.bsModalRef.hide()}),c.TgZ(4,"span",3),c._uU(5,"\xd7"),c.qZA(),c.qZA(),c.qZA(),c.TgZ(6,"div",4),c.TgZ(7,"p",5),c._uU(8),c.qZA(),c.qZA(),c.TgZ(9,"div",6),c.TgZ(10,"button",7),c.NdJ("click",function(){return n.confirmar()}),c._uU(11),c.qZA(),c.TgZ(12,"button",8),c.NdJ("click",function(){return n.bsModalRef.hide()}),c._uU(13),c.qZA(),c.qZA()),2&t&&(c.xp6(2),c.Oqu(n.titulo),c.xp6(6),c.Oqu(n.mensaje),c.xp6(3),c.Oqu(n.botonEnviar),c.xp6(2),c.Oqu(n.botonCerrar))},encapsulation:2}),i)},77919:function(n,e,o){o.d(e,{X:function(){return p}});var r,i=o(88002),c=o(92340),a=o(60793),u=o(58497),p=((r=function n(e){var o=this;t(this,n),this._httpClient=e,this.crearProyecto=function(t){return o._httpClient.post("".concat(o.apiUrl,"/proyecto"),t).pipe((0,i.U)(function(t){return t}))},this.editarProyecto=function(t,n){return o._httpClient.put("".concat(o.apiUrl,"/proyecto/").concat(t),n).pipe((0,i.U)(function(t){return t}))},this.asignarUsuario=function(t,n,e){return o._httpClient.put("".concat(o.apiUrl,"/proyecto/").concat(t,"/").concat(e,"/").concat(n),{}).pipe((0,i.U)(function(t){return t}))},this.desasignarUsuario=function(t,n,e){return o._httpClient.delete("".concat(o.apiUrl,"/proyecto/").concat(t,"/").concat(e,"/").concat(n)).pipe((0,i.U)(function(t){return t}))},this.obtenerProyectos=function(){return o._httpClient.get("".concat(o.apiUrl,"/proyecto")).pipe((0,i.U)(function(t){return t}))},this.eliminarProyecto=function(t){return o._httpClient.delete("".concat(o.apiUrl,"/proyecto/").concat(t)).pipe((0,i.U)(function(t){return t}))},this.obtenerProyectoPorId=function(t){return o._httpClient.get("".concat(o.apiUrl,"/proyecto/").concat(t)).pipe((0,i.U)(function(t){return t}))},this.apiUrl=c.N.apiUrl}).\u0275fac=function(t){return new(t||r)(a.LFG(u.eN))},r.\u0275prov=a.Yz7({token:r,factory:r.\u0275fac,providedIn:"root"}),r)}}])}();