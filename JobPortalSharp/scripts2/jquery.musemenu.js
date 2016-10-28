
(function(d){"function"===typeof define&&define.amd&&define.amd.jQuery?define(["jquery","museutils"],d):d(jQuery)})(function(d){d.fn.museMenu=function(){return this.each(function(){var b=this.id,c=d(this),a=c.closest(".breakpoint"),g="absolute",j,f,i,h,l,k;if(!c.data("initialized")){c.data("initialized",!0);var m=function(){if(c.css("position")=="fixed"){g="fixed";k=c;var a=Muse.Utils.getStyleSheetRulesById(Muse.Utils.getPageStyleSheets(),b);j=a?Muse.Utils.getRuleProperty(a,"top"):c.css("top");f=
a?Muse.Utils.getRuleProperty(a,"left"):c.css("left");i=a?Muse.Utils.getRuleProperty(a,"right"):c.css("right");h=a?Muse.Utils.getRuleProperty(a,"bottom"):c.css("bottom");l=parseInt(c.css("margin-left"))}else for(a=c.parent();!a.is(document)&&a.length>0&&a.attr("id")!="page";){if(a.css("position")=="fixed"){g="fixed";k=a;var d=a.offset(),m=c.offset(),n=Muse.Utils.getStyleSheetRulesById(Muse.Utils.getPageStyleSheets(),a.attr("id")),o=n?Muse.Utils.getRuleProperty(n,"top"):a.css("top"),p=n?Muse.Utils.getRuleProperty(n,
"left"):a.css("left"),q=n?Muse.Utils.getRuleProperty(n,"right"):a.css("right"),n=n?Muse.Utils.getRuleProperty(n,"bottom"):a.css("bottom");j=o&&o!="auto"?parseInt(o)+(m.top-d.top):o;f=p&&p!="auto"&&p.indexOf("%")==-1?parseInt(p)+(m.left-d.left):p;i=q&&q!="auto"&&q.indexOf("%")==-1?parseInt(q)+(d.left+a.width())-(m.left+c.width()):q;h=n&&n!="auto"?parseInt(n)+(d.top+a.height())-(m.top+c.height()):n;l=parseInt(a.css("margin-left"))+(p&&p.indexOf("%")!=-1?m.left-d.left:0);break}a=a.parent()}},n=function(b,
c){a.is(b)&&r.each(function(){var a=d(this).data("offsetContainerRaw");a&&(c.swapPlaceholderNodesRecursively(a),c.activateIDs(a))})};d("body").on("muse_bp_activate",function(a,b,c,d){n(c,d);m()});m();var p=d(),o=!1,q=c.find(".MenuItemContainer"),r=c.find(".MenuItem"),t=c.find(".SubMenu").add(r),v;t.on("mouseover",function(){o=!0});t.on("mouseleave",function(){o=!1;setTimeout(function(){o===!1&&(q.each(function(){d(this).data("hideSubmenu")()}),p=d())},300)});q.on("mouseleave",function(a){var b=d(a.target),
c=b.closest(".SubMenu");v&&clearTimeout(v);c.length>0&&(v=setTimeout(function(){c.find(".MenuItemContainer").each(function(){d(this).data("hideSubmenu")()});p=b.closest(".MenuItemContainer").data("$parentMenuItemContainer")},300))});q.on("mouseenter",function(){clearTimeout(v)});r.each(function(){var a=d(this),b=a.siblings(".SubMenu"),m=a.closest(".MenuItemContainer"),n=m.parentsUntil(".MenuBar").filter(".MenuItemContainer").length===0,o;if(n&&b.length>0)a.data("offsetContainerRaw",d("<div style='position:"+
g+"' class='MenuBar popup_element'></div>").hide().appendTo("body")),b.show(),o=b.position().top,b.hide();m.data("$parentMenuItemContainer",m.parent().closest(".MenuItemContainer")).data("showSubmenuOnly",function(){if(n&&b.length>0){var d=a.data("offsetContainer"),d=d||a.data("offsetContainerRaw");if(g!="fixed"){var p=m.offset();d.css({left:p.left,top:p.top,width:a.width()})}else{var p=m.position(),q=0,r=0;i&&i!="auto"&&(q=c.outerWidth()-p.left-a.width());h&&h!="auto"&&(r=o);l=parseInt(k.css("margin-left"));
if(k!=c){var t=Muse.Utils.getStyleSheetRulesById(Muse.Utils.getPageStyleSheets(),k.attr("id"));(t=t?Muse.Utils.getRuleProperty(t,"left"):k.css("left"))&&t.indexOf("%")!=-1&&(l+=c.offset().left-k.offset().left)}d.css({left:f,top:j,right:i,bottom:h,marginLeft:l+p.left,marginRight:q,marginTop:p.top,marginBottom:r,width:a.width()})}d.append(b).show();a.data("offsetContainer",d);k&&d&&k.hasClass("scroll_effect")===!0&&d.cloneScrollEffectsFrom(k)}b.show();b.find(".SubMenu").hide()}).data("hideSubmenu",
function(){var c=a.data("offsetContainer");c&&c.hasClass("scroll_effect")===!0&&c.clearScrollEffects();b.hide()}).data("isDescendentOf",function(a){for(var b=m.data("$parentMenuItemContainer");b.length>0;){if(a.index(b)>=0)return!0;b=b.data("$parentMenuItemContainer")}return!1});var q=function(){var a=p;a.length==0?m.data("showSubmenuOnly")():m.data("$parentMenuItemContainer").index(a)>=0?m.data("showSubmenuOnly")():m.siblings().index(a)>=0?(a.data("hideSubmenu")(),m.data("showSubmenuOnly")()):a.data("isDescendentOf")(m)?
m.data("showSubmenuOnly")():a.data("isDescendentOf")(m.siblings(".MenuItemContainer"))?(m.siblings(".MenuItemContainer").each(function(){d(this).data("hideSubmenu")()}),m.data("showSubmenuOnly")()):a.get(0)==m.get(0)&&m.data("showSubmenuOnly")();p=m},r=null;a.on("mouseenter",function(){a.data("mouseEntered",!0);r=setTimeout(function(){q()},200);a.one("mouseleave",function(){clearTimeout(r);a.data("mouseEntered",!1)})});b.length&&(a.attr("aria-haspopup",!0),Muse.Browser.Features.Touch&&(a.click(function(){return b.is(":visible")}),
d(document.documentElement).on(Muse.Browser.Features.Touch.End,Muse.Browser.Features.Touch.Listener(function(c){!b.is(":visible")&&d(c.target).closest(m).length>0?(c.stopPropagation(),Muse.Utils.redirectCancelled=!0,setTimeout(function(){Muse.Utils.redirectCancelled=!1},16),a.data("mouseEntered")&&setTimeout(function(){m.data("showSubmenuOnly")()},200)):b.is(":visible")&&d(c.target).closest(b).length==0&&d(c.target).closest(m).length==0&&m.data("hideSubmenu")()}))))});r.filter(".MuseMenuActive").each(function(){for(var a=
d(this).closest(".MenuItemContainer").data("$parentMenuItemContainer");a&&a.length>0;)a.children(".MenuItem").addClass("MuseMenuActive"),a=a.data("$parentMenuItemContainer")})}})}});
;(function(){if(!("undefined"==typeof Muse||"undefined"==typeof Muse.assets)){var a=function(a,b){for(var c=0,d=a.length;c<d;c++)if(a[c]==b)return c;return-1}(Muse.assets.required,"jquery.musemenu.js");if(-1!=a){Muse.assets.required.splice(a,1);for(var a=document.getElementsByTagName("meta"),b=0,c=a.length;b<c;b++){var d=a[b];if("generator"==d.getAttribute("name")){"2015.2.1.352"!=d.getAttribute("content")&&Muse.assets.outOfDate.push("jquery.musemenu.js");break}}}}})();