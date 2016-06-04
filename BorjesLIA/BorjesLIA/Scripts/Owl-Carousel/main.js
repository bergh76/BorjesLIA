jQuery(function ($) {
    'use strict';
    var thisCurrentSlideURL;
    var newYT = false;
    var time = 7;
    var thisSlideIndex = 0;

    var existURL = new Array();
    var ytArrayIndex = 0;

      var videoCase = 0

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
        var getSrc = current.find(".owl-item").eq(thisSlideIndex).find(".VideoClassTag").attr('src');

        var checkOne = getSrc.includes("mp4");
        var checkTwo = getSrc.includes("youtube");

        if (checkOne) {
            var getDuration = current.find(".owl-item").eq(thisSlideIndex).find(".VideoClassTag").attr('name');
            time = getDuration;
            var video = document.getElementById(getSrc);
            video.play();
        }
        if (checkTwo) {
            if (thisCurrentSlideURL == getSrc) {
                newYT = false;
            }
            else {
                newYT = true;
                thisCurrentSlideURL = getSrc;
            }
            var getDuration = current.find(".owl-item").eq(thisSlideIndex).find(".VideoClassTag").attr('name');
            time = getDuration;

            loadPlayer();

        }
        else {
            time = 7;
        }
    }

    function loadPlayer() {
        //console.log('loadPlayer');
        if (typeof (YT) == 'undefined' || typeof (YT.Player) == 'undefined') {
            console.log('if: undefined');
            var tag = document.createElement('script');
            tag.src = "https://www.youtube.com/iframe_api";
            var firstScriptTag = document.getElementsByTagName('script')[0];
            firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

            window.onYouTubePlayerAPIReady = function () {
                //console.log('onYouTubePlayerAPIReady => onYouTubePlayer');
                onYouTubePlayer();
            };

        } else {
            //console.log('else: undefined => onPlayerReady');
            if (newYT) {
                //var videoCase = 0;
                //// kolla med loop om array (existURL) innehåller thisCurrentSlideURL. om inte kör onYouTubePlayer och lägg till den (kommer regristreras som spelad tidigare). annars skicka vidare "true" och spela thisCurrentSlideURL i player
                //var videoHasPlayed = false;
                //var arrayLength = existURL.length;
                //if (arrayLength <= 0) {
                //    ytArrayIndex += 1;
                //    existURL[ytArrayIndex] = thisCurrentSlideURL;
                //}
                //else {
                //    for (var i = 0; i < arrayLength; i++) {
                //        var checkIfExist = existURL[i].includes(thisCurrentSlideURL);
                //        if (checkIfExist) {
                //            videoHasPlayed = true;
                           
                //        }
                //    }
                //    if (!videoHasPlayed) {
                //        ytArrayIndex += 1;
                //        existURL[ytArrayIndex] = thisCurrentSlideURL;
                //    }
                //}
              


                //switch (videoCase) {
                //    case 1:
                //        console.log('videon har inte spelats innan');
                //        break;
                //    case 2:
                //        console.log('videon har spelats innan');
                //        event.target.stopVideo();
                //        break;
                //    case 3:
                //        console.log('videon har spelats men en annan ytvideo har spelats efter');
                //        break;
                //}

                //// om url är annorluna och inte har spelats innan
                //onYouTubePlayer();
                ////om url är annorlunda men har spelats innan (kanske kommer behöva köra både onyoutubeplayer och onplayer ready efter varandra.)
                ////onPlayerReady();

                //https://www.youtube.com/embed/ztgXC1e6mJI?enablejsapi=1 (första klippet i db)
                //https://www.youtube.com/embed/GJVwbyAY4Sk?enablejsapi=1 (andra klippet i db)
                videoCase += 1;
                switch (videoCase) {
                    case 1:
                        console.log("case 1: ")
                        console.log(thisCurrentSlideURL + " borde vara https://www.youtube.com/embed/GJVwbyAY4Sk?enablejsapi=1 (andra klippet )");
                          onYouTubePlayer();
                        break;
                    case 2:
                        console.log(":::case 2::: ska spela det första klippet "); // ska spela det första klippet!

                        var someName = YT.get(thisCurrentSlideURL);
                        someName.playVideo();
                        //var iframes = document.getElementsByTagName('iframe');
                        // iframes[thisSlideIndex].contentWindow.postMessage(JSON.stringify({
                        //        playerVars: { 'autoplay': 1, 'controls': 0 },
                        //        'event': 'command',
                        //        'onReady': onPlayerReady,
                        //    }), '*');
                       
                        break;
                    case 3:
                        console.log("case 3... ska spela andra klippet");
                        //onYouTubePlayer();
                        //player.playVideo(); //om case 2 inte spelar så spelar dne här rätt..

                        ////document.getElementById('thisCurrentSlideURL').playVideo();
                        ////onYouTubePlayer(); //main.js:242 Uncaught TypeError: player.playVideo is not a function
                        //onPlayerReady();
                        var someName = YT.get(thisCurrentSlideURL);
                        someName.playVideo();
                        break;
                    case 4:
                        console.log("ska spela det första klippet")
                        var someName = YT.get(thisCurrentSlideURL);
                        someName.playVideo();
                        //console.log("case 4... ska spela första klippet"); 
                        //player.nextVideo();
                        break;
                    case 5:
                        console.log("sska spela det andra klippet")
                        var someName = YT.get(thisCurrentSlideURL);
                        someName.playVideo();
                        break;
                }
            }
            else {
                //om det bara finns en yt-video och det var den redan spelats innan, spela den igen när den visas i slider. 
                onPlayerReady();
            }
        }
    }
    var player;

    function onYouTubePlayer() {
        //console.log('onYouTubePlayer');
        var stringUrl = thisCurrentSlideURL;

        //set up YT-player api
        player = new YT.Player(stringUrl, {
            events: {
                'onReady': onPlayerReady,
                'onStateChange': onPlayerStateChange,
                'onError': catchError
            }
        });
    }
    function onPlayerReady(event) {
        //console.log('onPlayerReady');
        //player.playVideo(); //main.js:232 Uncaught TypeError: player.playVideo is not a function
        event.target.playVideo(); //main.js:233 Uncaught TypeError: Cannot read property 'target' of undefined
    }

    function onPlayerStateChange(event) {
        //console.log('onPlayerStateChange:');
        switch (event.data) {
            case YT.PlayerState.UNSTARTED:
                console.log('unstarted');
                break;
            case YT.PlayerState.ENDED:
                console.log('ended');
                event.target.stopVideo();
                break;
            case YT.PlayerState.PLAYING:
                console.log('playing');
                break;
            case YT.PlayerState.PAUSED:
                console.log('paused');
                break;
            case YT.PlayerState.BUFFERING:
                console.log('buffering');
                break;
            case YT.PlayerState.CUED:
                console.log('video cued');
                break;
        }
    }

    function stopVideo() {
        //console.log('stopVideo');
        player.stopVideo();
    }
    function catchError(event) {
        //console.log('catchError');
        if (event.data == 100) console.log("catch error");
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