
(function(d){"function"===typeof define&&define.amd&&define.amd.jQuery?define(["jquery","webpro","museutils"],d):d(jQuery)})(function(d){Muse.Plugins.TabbedPanelsPlugin={defaultOptions:{widgetClassName:"TabbedPanelsWidget",tabClassName:"TabbedPanelsTab",tabHoverClassName:"TabbedPanelsTabHover",tabDownClassName:"TabbedPanelsTabDown",tabActiveClassName:"TabbedPanelsTabSelected",panelClassName:"TabbedPanelsContent",panelActiveClassName:"TabbedPanelsContentVisible",defaultIndex:0,canCloseAll:!1},initialize:function(b,
c){var a=this;d.extend(c,d.extend({},a.defaultOptions,c));WebPro.Widget.Disclosure.DisplayPropertyTransitionPlugin.initialize(b,c);b.bind("attach-behavior",function(){a._attachBehavior(b)})},_attachBehavior:function(b){var c=b.tabs?b.tabs.$element:null;if(c&&(c.first().addClass("TabbedPanelsTabFirst"),c.last().addClass("TabbedPanelsTabLast"),b.options.event!=="click"))c.on(b.options.event,function(){b.tabs.selectTab(this)})}};Muse.Plugins.AccordionPlugin={defaultOptions:{widgetClassName:"AccordionWidget",
tabClassName:"AccordionPanelTab",tabHoverClassName:"AccordionPanelTabHover",tabDownClassName:"AccordionPanelTabDown",tabActiveClassName:"AccordionPanelTabOpen",panelClassName:"AccordionPanelContent",panelActiveClassName:"AccordionPanelContentActive",defaultIndex:0,canCloseAll:!1,transitionDirection:"vertical"},initialize:function(b,c){var a=this;d.extend(c,d.extend({},a.defaultOptions,c));c.toggleStateEnabled=c.canCloseAll;WebPro.Widget.Disclosure.AccordionTransitionPlugin.initialize(b,c);b.bind("transform-markup",
function(){a._transformMarkup(b)});b.bind("attach-behavior",function(){a._attachBehavior(b)})},_transformMarkup:function(b){var c=b.$element[0],a=b.options,g=a.transitionDirection==="vertical";b.$element.data("initialized")||(b.$element.data("initialized",!0),WebPro.scopedFind(c,".AccordionPanelContent",a.widgetClassName,c).each(function(){var a=d(this),b=!g?parseInt(a.css("left")):0;a.removeClass(g?"AccordionPanelContent colelem":"AccordionPanelContent grpelem").wrap(g?'<div class="AccordionPanelContent colelem"><div class="AccordionPanelContentClip"></div></div>':
'<div class="AccordionPanelContent grpelem"><div class="AccordionPanelContentClip"></div></div>').closest(".AccordionPanelContent").css({width:"100%",height:"100%",position:"relative",left:b+"px"});g||a.css({left:"0px",marginRight:"0px"})}))},_attachBehavior:function(b){var c=b.$element[0],a=b.options,g=0,j=a.transitionDirection==="vertical",f=j?"offsetWidth":"offsetHeight",i=j?"width":"height",h=0;WebPro.scopedFind(c,".AccordionPanel",a.widgetClassName,c).each(function(){g=g<this[f]?this[f]:g}).each(function(){g>
this[f]&&(this.style[i]=g+"px");if(!j){var b=d(this);b.css({width:"auto",marginRight:"0px",left:h+"px"});h+=b.children("."+a.tabClassName).outerWidth()}})}};WebPro.Widget.TabbedPanels.prototype.defaultPlugins=[Muse.Plugins.TabbedPanelsPlugin];WebPro.Widget.Accordion.prototype.defaultPlugins=[Muse.Plugins.AccordionPlugin]});
;(function(){if(!("undefined"==typeof Muse||"undefined"==typeof Muse.assets)){var a=function(a,b){for(var c=0,d=a.length;c<d;c++)if(a[c]==b)return c;return-1}(Muse.assets.required,"musewpdisclosure.js");if(-1!=a){Muse.assets.required.splice(a,1);for(var a=document.getElementsByTagName("meta"),b=0,c=a.length;b<c;b++){var d=a[b];if("generator"==d.getAttribute("name")){"2015.2.1.352"!=d.getAttribute("content")&&Muse.assets.outOfDate.push("musewpdisclosure.js");break}}}}})();
