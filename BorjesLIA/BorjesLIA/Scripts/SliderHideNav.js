$(addEventListener).dblclick(function () {
    var timer;
        if (timer) {
            clearTimeout(timer);
            timer = 0;
        }
        $('.nav').fadeIn();
        timer = setTimeout(function () {
            $('.nav').fadeOut()
        }, 1000)
    })
