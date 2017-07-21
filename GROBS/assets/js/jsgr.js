window.onload = function () {
    var swiper = new Swiper('.swiper-container', {
        autoplay: 5000,
        pagination: '.swiper-pagination',
        nextButton: '.swiper-button-next',
        prevButton: '.swiper-button-prev',
        slidesPerView: 1,
        paginationClickable: true,
        loop: true

    });
}