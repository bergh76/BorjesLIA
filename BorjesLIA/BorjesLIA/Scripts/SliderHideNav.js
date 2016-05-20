$(document).ready(function () {
    var timer;
    $(document).mousemove(function () {
        if (timer) {
            clearTimeout(timer);
            timer = 0;
        }

        $('nav').fadeIn();
        timer = setTimeout(function () {
            $('nav').fadeOut()
        }, 1000)
    })
})