!function(){"use strict";function e(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function t(e,t){for(var n=0;n<t.length;n++){var i=t[n];i.enumerable=i.enumerable||!1,i.configurable=!0,"value"in i&&(i.writable=!0),Object.defineProperty(e,i.key,i)}}function n(e,n,i){return n&&t(e.prototype,n),i&&t(e,i),e}(self.webpackChunkjiru_webapp=self.webpackChunkjiru_webapp||[]).push([[911],{37911:function(t,i,u){u.d(i,{Fq:function(){return v}});var o,s=u(60793),r=u(23738),a={provide:r.JU,useExisting:(0,s.Gpc)(function(){return c}),multi:!0},c=((o=function(){function t(){e(this,t),this.btnCheckboxTrue=!0,this.btnCheckboxFalse=!1,this.state=!1,this.onChange=Function.prototype,this.onTouched=Function.prototype}return n(t,[{key:"onClick",value:function(){this.isDisabled||(this.toggle(!this.state),this.onChange(this.value))}},{key:"ngOnInit",value:function(){this.toggle(this.trueValue===this.value)}},{key:"trueValue",get:function(){return void 0===this.btnCheckboxTrue||this.btnCheckboxTrue}},{key:"falseValue",get:function(){return void 0!==this.btnCheckboxFalse&&this.btnCheckboxFalse}},{key:"toggle",value:function(e){this.state=e,this.value=this.state?this.trueValue:this.falseValue}},{key:"writeValue",value:function(e){this.state=this.trueValue===e,this.value=e?this.trueValue:this.falseValue}},{key:"setDisabledState",value:function(e){this.isDisabled=e}},{key:"registerOnChange",value:function(e){this.onChange=e}},{key:"registerOnTouched",value:function(e){this.onTouched=e}}]),t}()).\u0275fac=function(e){return new(e||o)},o.\u0275dir=s.lG2({type:o,selectors:[["","btnCheckbox",""]],hostVars:3,hostBindings:function(e,t){1&e&&s.NdJ("click",function(){return t.onClick()}),2&e&&(s.uIk("aria-pressed",t.state),s.ekj("active",t.state))},inputs:{btnCheckboxTrue:"btnCheckboxTrue",btnCheckboxFalse:"btnCheckboxFalse"},features:[s._Bn([a])]}),o),l={provide:r.JU,useExisting:(0,s.Gpc)(function(){return h}),multi:!0},h=function(){var t=function(){function t(n,i,u,o){e(this,t),this.el=n,this.cdr=i,this.renderer=u,this.group=o,this.onChange=Function.prototype,this.onTouched=Function.prototype,this.role="radio",this._hasFocus=!1}return n(t,[{key:"value",get:function(){return this.group?this.group.value:this._value},set:function(e){this.group?this.group.value=e:this._value=e}},{key:"disabled",get:function(){return this._disabled},set:function(e){this.setDisabledState(e)}},{key:"controlOrGroupDisabled",get:function(){return!!(this.disabled||this.group&&this.group.disabled)||void 0}},{key:"hasDisabledClass",get:function(){return this.controlOrGroupDisabled&&!this.isActive}},{key:"isActive",get:function(){return this.btnRadio===this.value}},{key:"tabindex",get:function(){if(!this.controlOrGroupDisabled)return this.isActive||null==this.group?0:-1}},{key:"hasFocus",get:function(){return this._hasFocus}},{key:"toggleIfAllowed",value:function(){!this.canToggle()||(this.value=this.uncheckable&&this.btnRadio===this.value?void 0:this.btnRadio,this._onChange(this.value))}},{key:"onSpacePressed",value:function(e){this.toggleIfAllowed(),e.preventDefault()}},{key:"focus",value:function(){this.el.nativeElement.focus()}},{key:"onFocus",value:function(){this._hasFocus=!0}},{key:"onBlur",value:function(){this._hasFocus=!1,this.onTouched()}},{key:"canToggle",value:function(){return!this.controlOrGroupDisabled&&(this.uncheckable||this.btnRadio!==this.value)}},{key:"ngOnInit",value:function(){this.uncheckable=void 0!==this.uncheckable}},{key:"_onChange",value:function(e){this.group?this.group.value=e:(this.onTouched(),this.onChange(e))}},{key:"writeValue",value:function(e){this.value=e,this.cdr.markForCheck()}},{key:"registerOnChange",value:function(e){this.onChange=e}},{key:"registerOnTouched",value:function(e){this.onTouched=e}},{key:"setDisabledState",value:function(e){this._disabled=e,e?this.renderer.setAttribute(this.el.nativeElement,"disabled","disabled"):this.renderer.removeAttribute(this.el.nativeElement,"disabled")}}]),t}();return t.\u0275fac=function(e){return new(e||t)(s.Y36(s.SBq),s.Y36(s.sBO),s.Y36(s.Qsj),s.Y36((0,s.Gpc)(function(){return f}),8))},t.\u0275dir=s.lG2({type:t,selectors:[["","btnRadio",""]],hostVars:8,hostBindings:function(e,t){1&e&&s.NdJ("click",function(){return t.toggleIfAllowed()})("keydown.space",function(e){return t.onSpacePressed(e)})("focus",function(){return t.onFocus()})("blur",function(){return t.onBlur()}),2&e&&(s.uIk("role",t.role)("aria-disabled",t.controlOrGroupDisabled)("aria-checked",t.isActive)("tabindex",t.tabindex),s.ekj("disabled",t.hasDisabledClass)("active",t.isActive))},inputs:{value:"value",disabled:"disabled",uncheckable:"uncheckable",btnRadio:"btnRadio"},features:[s._Bn([l])]}),t}(),d={provide:r.JU,useExisting:(0,s.Gpc)(function(){return f}),multi:!0},f=function(){var t=function(){function t(n){e(this,t),this.cdr=n,this.onChange=Function.prototype,this.onTouched=Function.prototype,this.role="radiogroup"}return n(t,[{key:"value",get:function(){return this._value},set:function(e){this._value=e,this.onChange(e)}},{key:"tabindex",get:function(){return this._disabled?null:0}},{key:"writeValue",value:function(e){this._value=e,this.cdr.markForCheck()}},{key:"registerOnChange",value:function(e){this.onChange=e}},{key:"registerOnTouched",value:function(e){this.onTouched=e}},{key:"setDisabledState",value:function(e){this.radioButtons&&(this._disabled=e,this.radioButtons.forEach(function(t){t.setDisabledState(e)}),this.cdr.markForCheck())}},{key:"onFocus",value:function(){if(!this._disabled){var e=this.getActiveOrFocusedRadio();if(e)e.focus();else{var t=this.radioButtons.find(function(e){return!e.disabled});t&&t.focus()}}}},{key:"onBlur",value:function(){this.onTouched&&this.onTouched()}},{key:"selectNext",value:function(e){this.selectInDirection("next"),e.preventDefault()}},{key:"selectPrevious",value:function(e){this.selectInDirection("previous"),e.preventDefault()}},{key:"disabled",get:function(){return this._disabled}},{key:"selectInDirection",value:function(e){if(!this._disabled){var t=this.getActiveOrFocusedRadio();if(t)for(var n=this.radioButtons.toArray(),i=n.indexOf(t),u=o(i,n);u!==i;u=o(u,n))if(n[u].canToggle()){n[u].toggleIfAllowed(),n[u].focus();break}}function o(t,n){var i=(t+("next"===e?1:-1))%n.length;return i<0&&(i=n.length-1),i}}},{key:"getActiveOrFocusedRadio",value:function(){return this.radioButtons.find(function(e){return e.isActive})||this.radioButtons.find(function(e){return e.hasFocus})}}]),t}();return t.\u0275fac=function(e){return new(e||t)(s.Y36(s.sBO))},t.\u0275dir=s.lG2({type:t,selectors:[["","btnRadioGroup",""]],contentQueries:function(e,t,n){var i;(1&e&&s.Suo(n,h,4),2&e)&&(s.iGM(i=s.CRH())&&(t.radioButtons=i))},hostVars:2,hostBindings:function(e,t){1&e&&s.NdJ("focus",function(){return t.onFocus()})("blur",function(){return t.onBlur()})("keydown.ArrowRight",function(e){return t.selectNext(e)})("keydown.ArrowDown",function(e){return t.selectNext(e)})("keydown.ArrowLeft",function(e){return t.selectPrevious(e)})("keydown.ArrowUp",function(e){return t.selectPrevious(e)}),2&e&&s.uIk("role",t.role)("tabindex",t.tabindex)},features:[s._Bn([d])]}),t}(),v=function(){var t=function(){function t(){e(this,t)}return n(t,null,[{key:"forRoot",value:function(){return{ngModule:t,providers:[]}}}]),t}();return t.\u0275fac=function(e){return new(e||t)},t.\u0275mod=s.oAB({type:t}),t.\u0275inj=s.cJS({}),t}()}}])}();