jQuery(function($) {'use strict';
	//Slider
	$(document).ready(function() {
		var time = 7; // time in seconds

	 	var $progressBar,
	      $bar, 
	      $elem, 
	      isPause, 
	      tick,
	      percentTime;

	 
	    //Init the carousel
	    $("#main-slider").find('.owl-carousel').owlCarousel({
	      slideSpeed : 500,
	      paginationSpeed : 500,
	      singleItem : true,
	      navigation : true,
			navigationText: [
			"<i class='fa fa-angle-left'></i>",
			"<i class='fa fa-angle-right'></i>"
			],
	      afterInit : progressBar,
	      afterMove : moved,
	      startDragging : pauseOnDragging,
	      autoHeight : true,
	      transitionStyle : "fadeUp"
	    });
	 
	    //Init progressBar where elem is $("#owl-demo")
	    function progressBar(elem){
	      $elem = elem;
	      //build progress bar elements
	      buildProgressBar();
	      //start counting
	      start();
	    }
	 
	    //create div#progressBar and div#bar then append to $(".owl-carousel")
	    function buildProgressBar(){
	      $progressBar = $("<div>",{
	        id:"progressBar"
	      });
	      $bar = $("<div>",{
	        id:"bar"
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
	      if(isPause === false){
	        percentTime += 1 / time;
	        $bar.css({
	           width: percentTime+"%"
	         });
	        //if percentTime is equal or greater than 100
	        if(percentTime >= 100){
	          //slide to next item 
	          $elem.trigger('owl.next')
	        }
	      }
	    }
	 
	    //pause while dragging 
	    function pauseOnDragging(){
	      isPause = true;
	    }
	 
	    //moved callback
	    function moved(){
	      //clear interval
	      clearTimeout(tick);
	      //start again
	      start();
	    }
	});
/*
	////Initiat WOW JS
	//new WOW().init();
	////smoothScroll
	//smoothScroll.init();

	//// portfolio filter
	//$(window).load(function(){'use strict';
	//	var $portfolio_selectors = $('.portfolio-filter >li>a');
	//	var $portfolio = $('.portfolio-items');
	//	$portfolio.isotope({
	//		itemSelector : '.portfolio-item',
	//		layoutMode : 'fitRows'
	//	});
		
	//	$portfolio_selectors.on('click', function(){
	//		$portfolio_selectors.removeClass('active');
	//		$(this).addClass('active');
	//		var selector = $(this).attr('data-filter');
	//		$portfolio.isotope({ filter: selector });
	//		return false;
	//	});
	//});



	//// Contact form
	//var form = $('#main-contact-form');
	//form.submit(function(event){
	//	event.preventDefault();
	//	var form_status = $('<div class="form_status"></div>');
	//	$.ajax({
	//		url: $(this).attr('action'),
	//		beforeSend: function(){
	//			form.prepend( form_status.html('<p><i class="fa fa-spinner fa-spin"></i> Email is sending...</p>').fadeIn() );
	//		}
	//	}).done(function(data){
	//		form_status.html('<p class="text-success">Thank you for contact us. As early as possible  we will contact you</p>').delay(3000).fadeOut();
	//	});
	//});

	////Pretty Photo
	//$("a[rel^='prettyPhoto']").prettyPhoto({
	//	social_tools: false
	//});


    */
});
//var owl = $('.owl-carousel');

//owl.owlCarousel({
//    items: 1,
//    loop: true,

//    autoplay: true,
//    autoplayTimeout: 5000,
//    autoplayHoverPause: true,

//    nav: true
//});

//// Open Accordion
//$('figcaption').on('click', function () {
//    /* 	    owl.trigger('stop.owl.autoplay'); */
//    /* 	    owl.trigger('autoplay.stop.owl'); */
//    owl.trigger('stop.autoplay.owl');
//    $(this).toggleClass('active');
//});
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