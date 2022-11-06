jQuery(function ($) {
    'use strict';

    // Start Menu JS
	$(window).on('scroll', function() {
		if ($(this).scrollTop() > 50) {
			$('.main-nav').addClass('menu-shrink');
		} else {
			$('.main-nav').removeClass('menu-shrink');
		}
    });

	// Banner Bottom Click Animate JS
	$('.banner-bottom-btn a').on('click', function(e){
		var anchor = $(this);
		$('html, body').stop().animate({
			scrollTop: $(anchor.attr('href')).offset().top - 50
		}, 1500);
		e.preventDefault();
	});

	// Mean Menu JS
	$('.mean-menu').meanmenu({
		meanScreenWidth: '991'
	});

	// Nice Select JS
	$('select').niceSelect();

    // Mixitup JS
	try {
        var mixer = mixitup('#container', {
            controls: {
                toggleDefault: 'none'
            }
        });
    } catch (err) {}

	// Popup Youtube JS
	$('.popup-youtube').magnificPopup({
		disableOn: 320,
		type: 'iframe',
		mainClass: 'mfp-fade',
		removalDelay: 160,
		preloader: false,
		fixedContentPos: false
	});
	
	// Odometer JS
	$('.odometer').appear(function(e) {
		var odo = $('.odometer');
		odo.each(function() {
			var countNumber = $(this).attr('data-count');
			$(this).html(countNumber);
		});
	});

	// Location Slider JS
	$('.location-slider').owlCarousel({
		loop: true,
		margin: 15,
		nav: true,
		dots: false,
		smartSpeed: 1000,
		autoplay: true,
		autoplayTimeout: 4000,
		autoplayHoverPause: true,
		navText: [
			"<i class='flaticon-left-arrow'></i>",
			"<i class='flaticon-right-arrow'></i>"
		],
		responsive:{
			0:{
				items: 1,
			},
			600:{
				items: 2,
			},
			1000:{
				items: 4,
			}
		}
	});

	// Feedback Slider JS
	$('.feedback-slider').owlCarousel({
		loop: true,
		margin: 0,
		nav: true,
		dots: false,
		smartSpeed: 1000,
		autoplay: true,
		autoplayTimeout: 4000,
		autoplayHoverPause: true,
		navText: [
			"<i class='flaticon-left-arrow'></i>",
			"<i class='flaticon-right-arrow'></i>"
		],
		responsive:{
			0:{
				items: 1,
			},
			600:{
				items: 1,
			},
			1000:{
				items: 2,
			}
		}
	});

	// Partner Slider JS
	$('.partner-slider').owlCarousel({
		loop: true,
		margin: 0,
		nav: false,
		dots: false,
		smartSpeed: 1000,
		autoplay: true,
		autoplayTimeout: 4000,
		autoplayHoverPause: true,
		responsive:{
			0:{
				items: 2,
			},
			600:{
				items: 3,
			},
			1000:{
				items: 5,
			}
		}
	});

	// Support Slider JS
	$('.support-slider').owlCarousel({
		loop: true,
		margin: 0,
		nav: true,
		dots: false,
		smartSpeed: 1000,
		autoplay: true,
		autoplayTimeout: 4000,
		autoplayHoverPause: true,
		navText: [
			"<i class='flaticon-left-arrow'></i>",
			"<i class='flaticon-right-arrow'></i>"
		],
		responsive:{
			0:{
				items: 1,
			},
			600:{
				items: 3,
			},
			1000:{
				items: 6,
			}
		}
	});

	// Company Slider JS
	$('.company-slider').owlCarousel({
		loop: true,
		margin: 15,
		nav: true,
		dots: false,
		smartSpeed: 1000,
		autoplay: true,
		autoplayTimeout: 4000,
		autoplayHoverPause: true,
		navText: [
			"<i class='flaticon-left-arrow'></i>",
			"<i class='flaticon-right-arrow'></i>"
		],
		responsive:{
			0:{
				items: 1,
			},
			600:{
				items: 2,
			},
			1100:{
				items: 5,
			}
		}
	});

	// Candidate Slider JS
	$('.candidate-slider').owlCarousel({
		loop: true,
		margin: 20,
		nav: true,
		dots: false,
		smartSpeed: 1000,
		autoplay: true,
		autoplayTimeout: 4000,
		autoplayHoverPause: true,
		navText: [
			"<i class='flaticon-left-arrow'></i>",
			"<i class='flaticon-right-arrow'></i>"
		],
		responsive:{
			0:{
				items: 1,
			},
			600:{
				items: 1,
			},
			900:{
				items: 2,
			}
		}
	});

	// Testimonial Slider JS
	$('.testimonial-slider').owlCarousel({
		items: 1,
		loop: true,
		margin: 20,
		nav: true,
		dots: false,
		smartSpeed: 1000,
		autoplay: true,
		autoplayTimeout: 4000,
		autoplayHoverPause: true,
		navText: [
			"<i class='flaticon-left-arrow'></i>",
			"<i class='flaticon-right-arrow'></i>"
		],
	});

	// Client Slider JS
	$('.client-slider').owlCarousel({
		items: 1,
		loop: true,
		margin: 20,
		nav: true,
		dots: false,
		smartSpeed: 1000,
		autoplay: true,
		autoplayTimeout: 4000,
		autoplayHoverPause: true,
		navText: [
			"<i class='flaticon-left-arrow'></i>",
			"<i class='flaticon-right-arrow'></i>"
		],
	});

	// Job Details Slider JS
	$('.job-details-slider').owlCarousel({
		items: 1,
		loop: true,
		margin: 20,
		nav: true,
		dots: false,
		smartSpeed: 1000,
		autoplay: true,
		autoplayTimeout: 4000,
		autoplayHoverPause: true,
		navText: [
			"<i class='flaticon-left-arrow'></i>",
			"<i class='flaticon-right-arrow'></i>"
		],
	});

	// Progress Bar JS
	$('.progress-bar').loading();

	// Accordion JS
	$('.accordion > li:eq(0) a').addClass('active').next().slideDown();
	$('.accordion a').on('click', function(j) {
		var dropDown = $(this).closest('li').find('p');
		$(this).closest('.accordion').find('p').not(dropDown).slideUp();
		if ($(this).hasClass('active')) {
			$(this).removeClass('active');
		} else {
			$(this).closest('.accordion').find('a.active').removeClass('active');
			$(this).addClass('active');
		}
		dropDown.stop(false, true).slideToggle();
		j.preventDefault();
	});

	// Back to Top JS 
	$('body').append('<div id="toTop" class="back-to-top-btn"><i class="bx bxs-up-arrow"></i></div>');
	$(window).on('scroll', function() {
		if ($(this).scrollTop() != 0) {
			$('#toTop').fadeIn();
		} 
		else {
			$('#toTop').fadeOut();
		}
	}); 
	$('#toTop').on('click', function(){
		$('html, body').animate({ scrollTop: 0 }, 900);
		return false;
	});
	
}(jQuery));