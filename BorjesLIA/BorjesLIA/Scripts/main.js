jQuery(function ($) {
    'use strict';

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
//$(document).ready(function () {
//    $('.bxslider').bxSlider({
//        pager: false,
//        mode: 'fade',
//        captions: true,
//        auto: true,
//        autoControls: true,
//        responsive: true,
//        minSlides: 1,
//        slideMargin: 0,
//    });
//});

//Trigger Fullscreen
//function launchIntoFullscreen(element) {
//    if ((document.fullScreenElement !== undefined && document.fullScreenElement === null) || (document.msFullscreenElement !== undefined && document.msFullscreenElement === null) || (document.mozFullScreen !== undefined && !document.mozFullScreen) || (document.webkitIsFullScreen !== undefined && !document.webkitIsFullScreen)) {
//        if (element.requestFullscreen) {
//            element.requestFullscreen();
//        } else if (element.mozRequestFullScreen) {
//            element.mozRequestFullScreen();
//        } else if (element.webkitRequestFullscreen) {
//            element.webkitRequestFullscreen();
//        } else if (element.msRequestFullscreen) {
//            element.msRequestFullscreen();
//        }
//    } else {
//        Whack fullscreen
//        if (document.exitFullscreen) {
//            document.exitFullscreen();
//        } else if (document.mozCancelFullScreen) {
//            document.mozCancelFullScreen();
//        } else if (document.webkitExitFullscreen) {
//            document.webkitExitFullscreen();
//        }
//    }
//}

//function exitFullscreen() {
//    alert("button has been pushed");
//}

//Hide navbar on Home/Index
//$('#navbar-start').hide();
//$("html").mousemove(function (event) {
//    $('#navbar-start').show();
//    myStopFunction();
//    myFunction();
//});
//function myFunction() {
//    myVar = setTimeout(function () {
//        $('#navbar-start').hide();
//    }, 1000);
//}
//function myStopFunction() {
//    if (typeof myVar != 'undefined') {
//        clearTimeout(myVar);
//    }
//}

////video test
//$("#videoOne").on("pause", function (e) {
//    slider.startShow();
//});

//$("#videoOne").on("play", function (e) {
//    slider.stopShow();
//});