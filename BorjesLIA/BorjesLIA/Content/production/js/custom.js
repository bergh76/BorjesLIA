/**
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

var URL = window.location.href.split('?')[0],
    $BODY = $('body'),
    $MENU_TOGGLE = $('#menu_toggle'),
    $SIDEBAR_MENU = $('#sidebar-menu'),
    $SIDEBAR_FOOTER = $('.sidebar-footer'),
    $LEFT_COL = $('.left_col'),
    $RIGHT_COL = $('.right_col'),
    $NAV_MENU = $('.nav_menu'),
    $FOOTER = $('footer');

// Sidebar
$(document).ready(function() {
    // TODO: This is some kind of easy fix, maybe we can improve this
    var setContentHeight = function () {
        // reset height
        $RIGHT_COL.css('min-height', $(window).height());

        var bodyHeight = $BODY.height(),
            leftColHeight = $LEFT_COL.eq(1).height() + $SIDEBAR_FOOTER.height(),
            contentHeight = bodyHeight < leftColHeight ? leftColHeight : bodyHeight;

        // normalize content
        contentHeight -= $NAV_MENU.height() + $FOOTER.height();

        $RIGHT_COL.css('min-height', contentHeight);
    };

    $SIDEBAR_MENU.find('a').on('click', function(ev) {
        var $li = $(this).parent();

        if ($li.is('.active')) {
            $li.removeClass('active');
            $('ul:first', $li).slideUp(function() {
                setContentHeight();
            });
        } else {
            // prevent closing menu if we are on child menu
            if (!$li.parent().is('.child_menu')) {
                $SIDEBAR_MENU.find('li').removeClass('active');
                $SIDEBAR_MENU.find('li ul').slideUp();
            }
            
            $li.addClass('active');

            $('ul:first', $li).slideDown(function() {
                setContentHeight();
            });
        }
    });

    // toggle small or large menu
    $MENU_TOGGLE.on('click', function() {
        if ($BODY.hasClass('nav-md')) {
            $BODY.removeClass('nav-md').addClass('nav-sm');
            $LEFT_COL.removeClass('scroll-view').removeAttr('style');

            if ($SIDEBAR_MENU.find('li').hasClass('active')) {
                $SIDEBAR_MENU.find('li.active').addClass('active-sm').removeClass('active');
            }
        } else {
            $BODY.removeClass('nav-sm').addClass('nav-md');

            if ($SIDEBAR_MENU.find('li').hasClass('active-sm')) {
                $SIDEBAR_MENU.find('li.active-sm').addClass('active').removeClass('active-sm');
            }
        }

        setContentHeight();
    });

    // check active menu
    $SIDEBAR_MENU.find('a[href="' + URL + '"]').parent('li').addClass('current-page');

    $SIDEBAR_MENU.find('a').filter(function () {
        return this.href == URL;
    }).parent('li').addClass('current-page').parents('ul').slideDown(function() {
        setContentHeight();
    }).parent().addClass('active');

    // recompute content when resizing
    $(window).smartresize(function(){  
        setContentHeight();
    });
});
// /Sidebar

// Panel toolbox
$(document).ready(function() {
    $('.collapse-link').on('click', function() {
        var $BOX_PANEL = $(this).closest('.x_panel'),
            $ICON = $(this).find('i'),
            $BOX_CONTENT = $BOX_PANEL.find('.x_content');
        
        // fix for some div with hardcoded fix class
        if ($BOX_PANEL.attr('style')) {
            $BOX_CONTENT.slideToggle(200, function(){
                $BOX_PANEL.removeAttr('style');
            });
        } else {
            $BOX_CONTENT.slideToggle(200); 
            $BOX_PANEL.css('height', 'auto');  
        }

        $ICON.toggleClass('fa-chevron-up fa-chevron-down');
    });

    $('.close-link').click(function () {
        var $BOX_PANEL = $(this).closest('.x_panel');

        $BOX_PANEL.remove();
    });
});
// /Panel toolbox

// Tooltip
$(document).ready(function() {
    $('[data-toggle="tooltip"]').tooltip();
});
// /Tooltip

// Progressbar
if ($(".progress .progress-bar")[0]) {
    $('.progress .progress-bar').progressbar(); // bootstrap 3
}
// /Progressbar

// Switchery
$(document).ready(function() {
    if ($(".js-switch")[0]) {
        var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));
        elems.forEach(function (html) {
            var switchery = new Switchery(html, {
                color: '#26B99A'
            });
        });
    }
});
// /Switchery

// iCheck
$(document).ready(function() {
    if ($("input.flat")[0]) {
        $(document).ready(function () {
            $('input.flat').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });
        });
    }
});
// /iCheck

// Table
$('table input').on('ifChecked', function () {
    checkState = '';
    $(this).parent().parent().parent().addClass('selected');
    countChecked();
});
$('table input').on('ifUnchecked', function () {
    checkState = '';
    $(this).parent().parent().parent().removeClass('selected');
    countChecked();
});

var checkState = '';

$('.bulk_action input').on('ifChecked', function () {
    checkState = '';
    $(this).parent().parent().parent().addClass('selected');
    countChecked();
});
$('.bulk_action input').on('ifUnchecked', function () {
    checkState = '';
    $(this).parent().parent().parent().removeClass('selected');
    countChecked();
});
$('.bulk_action input#check-all').on('ifChecked', function () {
    checkState = 'all';
    countChecked();
});
$('.bulk_action input#check-all').on('ifUnchecked', function () {
    checkState = 'none';
    countChecked();
});

function countChecked() {
    if (checkState === 'all') {
        $(".bulk_action input[name='table_records']").iCheck('check');
    }
    if (checkState === 'none') {
        $(".bulk_action input[name='table_records']").iCheck('uncheck');
    }

    var checkCount = $(".bulk_action input[name='table_records']:checked").length;

    if (checkCount) {
        $('.column-title').hide();
        $('.bulk-actions').show();
        $('.action-cnt').html(checkCount + ' Records Selected');
    } else {
        $('.column-title').show();
        $('.bulk-actions').hide();
    }
}

// Accordion
$(document).ready(function() {
    $(".expand").on("click", function () {
        $(this).next().slideToggle(200);
        $expand = $(this).find(">:first-child");

        if ($expand.text() == "+") {
            $expand.text("-");
        } else {
            $expand.text("+");
        }
    });
});

// NProgress
if (typeof NProgress != 'undefined') {
    $(document).ready(function () {
        NProgress.start();
    });

    $(window).load(function () {
        NProgress.done();
    });
}

/**
 * Resize function without multiple trigger
 * 
 * Usage:
 * $(window).smartresize(function(){  
 *     // code here
 * });
 */
(function($,sr){
    // debouncing function from John Hann
    // http://unscriptable.com/index.php/2009/03/20/debouncing-javascript-methods/
    var debounce = function (func, threshold, execAsap) {
      var timeout;

        return function debounced () {
            var obj = this, args = arguments;
            function delayed () {
                if (!execAsap)
                    func.apply(obj, args);
                timeout = null; 
            }

            if (timeout)
                clearTimeout(timeout);
            else if (execAsap)
                func.apply(obj, args);

            timeout = setTimeout(delayed, threshold || 100); 
        };
    };

    // smartresize 
    jQuery.fn[sr] = function(fn){  return fn ? this.bind('resize', debounce(fn)) : this.trigger(sr); };

})(jQuery, 'smartresize');


// JQUERY TO STARTPAGE FUNCTIONALITY
jQuery(function ($) {
    //'use strict';

    //// ADD PAGINATIONS TO TABLES
    //$(document).ready(function () {
    //    $('#myTable').DataTable({
    //        "processing": true,
    //    });
    //});

    //	//Slider
    //	$(document).ready(function() {
    //		var time = 14; // time in seconds

    //	 	var $progressBar,
    //	      $bar, 
    //	      $elem, 
    //	      isPause, 
    //	      tick,
    //	      percentTime;

    //	    //Init the carousel
    //	 	$("#main-slider").find('.owl-carousel').owlCarousel({
    //	 	    slideSpeed : 500,
    //	 	    paginationSpeed : 500,
    //	 	    singleItem : true,
    //	 	    navigation: true,	      
    //	 	    navigationText: [
    //			"<i class='fa fa-angle-left'></i>",
    //			"<i class='fa fa-angle-right'></i>"
    //	 	    ],
    //	 	    responsiveClass: true,
    //	      afterInit : progressBar,
    //	      afterMove : moved,
    //	      startDragging : pauseOnDragging,
    //	      autoHeight : true,
    //	      transitionStyle : "fadeUp"
    //	    });

    //	    //Init progressBar where elem is $("#owl-demo")
    //	    function progressBar(elem){
    //	      $elem = elem;
    //	      //build progress bar elements
    //	      buildProgressBar();
    //	      //start counting
    //	      start();
    //	    }

    //	    //create div#progressBar and div#bar then append to $(".owl-carousel")
    //	    function buildProgressBar(){
    //	      $progressBar = $("<div>",{
    //	        id:"progressBar"
    //	      });
    //	      $bar = $("<div>",{
    //	        id:"bar"
    //	      });
    //	      $progressBar.append($bar).appendTo($elem);
    //	    }

    //	    function start() {
    //	      //reset timer
    //	      percentTime = 0;
    //	      isPause = false;
    //	      //run interval every 0.01 second
    //	      tick = setInterval(interval, 10);
    //	    };

    //	    function interval() {
    //	      if(isPause === false){
    //	        percentTime += 1 / time;
    //	        $bar.css({
    //	           width: percentTime+"%"
    //	         });
    //	        //if percentTime is equal or greater than 100
    //	        if(percentTime >= 100){
    //	          //slide to next item 
    //	          $elem.trigger('owl.next')
    //	        }
    //	      }
    //	    }

    //	    //pause while dragging 
    //	    function pauseOnDragging(){
    //	      isPause = true;
    //	    }

    //	    //moved callback
    //	    function moved(){
    //	      //clear interval
    //	      clearTimeout(tick);
    //	      //start again
    //	      start();
    //	    }
    //	});
    //});
    //function toggleFullScreen(elem) {

    //    // ## The below if statement seems to work better ## if ((document.fullScreenElement && document.fullScreenElement !== null) || (document.msfullscreenElement && document.msfullscreenElement !== null) || (!document.mozFullScreen && !document.webkitIsFullScreen)) {
    //    if ((document.fullScreenElement !== undefined && document.fullScreenElement === null) || (document.msFullscreenElement !== undefined && document.msFullscreenElement === null) || (document.mozFullScreen !== undefined && !document.mozFullScreen) || (document.webkitIsFullScreen !== undefined && !document.webkitIsFullScreen)) {
    //        if (elem.requestFullScreen) {
    //            elem.requestFullScreen();
    //        } else if (elem.mozRequestFullScreen) {
    //            elem.mozRequestFullScreen();
    //        } else if (elem.webkitRequestFullScreen) {
    //            elem.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
    //        } else if (elem.msRequestFullscreen) {
    //            elem.msRequestFullscreen();
    //        }

    //        var c, p = $('#navbar-start');
    //        $(document).on('mousemove', function () {
    //            p.fadeIn('fast');
    //            clearTimeout(c);
    //            c = setTimeout(function () {
    //                p.fadeOut('fast');
    //            }, 5000);
    //        });
    //    } else {
    //        if (document.cancelFullScreen) {
    //            document.cancelFullScreen();
    //        } else if (document.mozCancelFullScreen) {
    //            document.mozCancelFullScreen();
    //        } else if (document.webkitCancelFullScreen) {
    //            document.webkitCancelFullScreen();
    //        } else if (document.msExitFullscreen) {
    //            document.msExitFullscreen();
    //        }
    //    }
    //}
});

//försök att flytta daJacaScript till main
$(document).ready(function () {
    $(".bxslider").bxSlider({
        pager: false,
        mode: "fade",
        captions: true,
        auto: true,
        autoControls: true,
        responsive: true,
        minSlides: 1,
        slideMargin: 0,
    });
});

//Trigger Fullscreen
function launchIntoFullscreen(element) {
    if ((document.fullScreenElement !== undefined && document.fullScreenElement === null) || (document.msFullscreenElement !== undefined && document.msFullscreenElement === null) || (document.mozFullScreen !== undefined && !document.mozFullScreen) || (document.webkitIsFullScreen !== undefined && !document.webkitIsFullScreen)) {
        if (element.requestFullscreen) {
            element.requestFullscreen();
        } else if (element.mozRequestFullScreen) {
            element.mozRequestFullScreen();
        } else if (element.webkitRequestFullscreen) {
            element.webkitRequestFullscreen();
        } else if (element.msRequestFullscreen) {
            element.msRequestFullscreen();
        }
    } else {
        //Whack fullscreen
        if (document.exitFullscreen) {
            document.exitFullscreen();
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        } else if (document.webkitExitFullscreen) {
            document.webkitExitFullscreen();
        }
    }
}

//function exitFullscreen() {
//    alert("button has been pushed");
//}

//Hide navbar on Home/Index
$("#navbar-start").hide();
$("html").mousemove(function (event) {
    $("#navbar-start").show();
    myStopFunction();
    myFunction();
});
function myFunction() {
    myVar = setTimeout(function () {
        $("#navbar-start").hide();
    }, 1000);
}
function myStopFunction() {
    if (typeof myVar != "undefined") {
        clearTimeout(myVar);
    }
}

////video test
//$("#videoOne").on("pause", function (e) {
//    slider.startShow();
//});

//$("#videoOne").on("play", function (e) {
//    slider.stopShow();
//});