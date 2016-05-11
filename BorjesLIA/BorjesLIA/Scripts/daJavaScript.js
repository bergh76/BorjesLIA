$(document).ready(function () {
    $('.bxslider').bxSlider({
        pager: false,
        mode: 'fade',
        captions: true,
        auto: true,
        autoControls: true,
        responsive: true,
        minSlides: 1,
        slideMargin: 0,
    });
});

// Trigger Fullscreen
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
        // Whack fullscreen
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
$('.navbar').hide();
$("html").mousemove(function (event) {
    $('.navbar').show();
    myStopFunction();
    myFunction();
});
function myFunction() {
    myVar = setTimeout(function () {
        $('.navbar').hide();
    }, 1000);
}
function myStopFunction() {
    if (typeof myVar != 'undefined') {
        clearTimeout(myVar);
    }
}

