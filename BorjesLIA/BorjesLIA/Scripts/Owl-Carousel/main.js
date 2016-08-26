jQuery(function ($) {
    'use strict';
    var thisCurrentSlideURL;
    var time = 15;
    var thisSlideIndex = 0;
    var player;

    var $progressBar,
      $bar,
      $elem,
      isPause,
      tick,
      percentTime;

    //Slider
    $(document).ready(function () {
        loadPlayer();

        //Init the carousel
        $("#main-slider").find('.owl-carousel').owlCarousel({
            slideSpeed: 500,
            paginationSpeed: 500,
            singleItem: true,
            navigation: true,
            navigationText: [
			"<i class='fa fa-angle-left'></i>",
			"<i class='fa fa-angle-right'></i>"
            ],
            afterInit: progressBar,
            afterMove: moved,
            startDragging: pauseOnDragging,
            autoHeight: true,
            transitionStyle: "fadeUp",
            callbacks: true,
            afterAction: thisSlide
        });

        //Init progressBar where elem is $("#owl-demo")
        function progressBar(elem) {
            $elem = elem;
            //build progress bar elements
            buildProgressBar();
            //start counting
            start();
        }

        //create div#progressBar and div#bar then append to $(".owl-carousel")
        function buildProgressBar() {
            $progressBar = $("<div>", {
                id: "progressBar"
            });
            $bar = $("<div>", {
                id: "bar"
            });
            $progressBar.append($bar).appendTo($elem);
        }

        function start() {
            //reset timer
            percentTime = 0;
            isPause = false;
            //run interval every 0.01 second
            tick = setInterval(interval, 10);
        };

        function interval() {
            if (isPause === false) {
                percentTime += 1 / time;
                $bar.css({
                    width: percentTime + "%"
                });
                //if percentTime is equal or greater than 100
                if (percentTime >= 100) {
                    //slide to next item 
                    $elem.trigger('owl.next')
                }
            }
        }

        //pause while dragging 
        function pauseOnDragging() {
            isPause = true;
        }

        ////moved callback
        function moved(elem) {
            //clear interval
            clearTimeout(tick);
            //start again
            start();
        }
    });


    function thisSlide(current) {
        thisSlideIndex = this.currentItem;
        //var getSrc = current.find(".owl-item").eq(thisSlideIndex).find(".VideoClassTag").attr('src');
        var getSrc = current.find(".VideoClassTag").eq(thisSlideIndex).attr('src');
        //console.log("getSrc: " + getSrc);
        var checkOne = getSrc.includes("/Content/videos/");
        var checkTwo = getSrc.includes("youtube");
        var checkThree = getSrc.includes("vimeo");

        if (checkOne) {
            //var getDuration = current.find(".owl-item").eq(thisSlideIndex).find(".VideoClassTag").attr('name');
            var getDuration = current.find(".VideoClassTag").eq(thisSlideIndex).attr('name');
            time = getDuration;

            setTimeout(function () {
                var video = document.getElementById(getSrc);
                video.play();
            }, 3000);
            
        }
        else if (checkTwo) {

            //var getDuration = current.find(".owl-item").eq(thisSlideIndex).find(".VideoClassTag").attr('name');
            var getDuration = current.find(".VideoClassTag").eq(thisSlideIndex).attr('name');
            //console.log("getduration: " + getDuration);
            time = getDuration;
            console.log("time duration vimeo: " + time);
            setTimeout(function () {
            var data = {
                "event": "command",
                "func": "playVideo"
            };
            var player = document.getElementById(getSrc);
            player.contentWindow.postMessage(JSON.stringify(data), '*');
            }, 3000);

        }
        else if (checkThree) {

            //var getDuration = current.find(".owl-item").eq(thisSlideIndex).find(".VideoClassTag").attr('name');
            var getDuration = current.find(".VideoClassTag").eq(thisSlideIndex).attr('name');
            console.log("getduration: "+getDuration);
            time = getDuration;
            //console.log("time duration vimeo: " + time);
            setTimeout(function () {
                var data = { method: "play" };
                var player = document.getElementById(getSrc);
                player.contentWindow.postMessage(JSON.stringify(data), '*');
            }, 3000);

        }
        else {
            time = 7;
        }
    }
    // youtube. init yt player
    function loadPlayer() {

        var tag = document.createElement('script');
        tag.src = "https://www.youtube.com/iframe_api";
        var firstScriptTag = document.getElementsByTagName('script')[0];
        firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

        window.onYouTubePlayerAPIReady = function () {

            var stringUrl = thisCurrentSlideURL;
            //set up YT-player api
            player = new YT.Player(stringUrl, {
                events: {
                    'onReady': onPlayerReady()
                }
            });
        };
    }
});

//adjustIframes
function adjustIframes() {
    $('iframe').each(function () {
        var
        $this = $(this),
        proportion = $this.data('proportion'),
        w = $this.attr('width'),
        actual_w = $this.width();

        if (!proportion) {
            proportion = $this.attr('height') / w;
            $this.data('proportion', proportion);
        }

        if (actual_w != w) {
            $this.css('height', Math.round(actual_w * proportion) + 'px');
        }
    });
}
$(window).on('resize load', adjustIframes);
