if (sessionStorage.navbar_minimize === undefined) { sessionStorage.navbar_minimize = 'false'; }

$(document).ready(function() {
	var myWindow = $(window), miniText = 0, myBodyClass = '';
	function checkWidth() {
		var windowsize = myWindow.width();

		$('#sidebar-left .sidebar-menu').height(myWindow.height() - 125);

		if (document.getElementById('NavbarToolbarLeft') === null) { return; }
		var ulWidth = document.getElementById('NavbarToolbarLeft').clientWidth;
		var ulLeft = document.getElementById('NavbarToolbarRight').offsetLeft;

		if (windowsize > 1190) {
			$('body').removeClass('page-sidebar-minimize-auto');
			if (sessionStorage.navbar_minimize === 'true') { $('.navbar-minimize a').click(); }
		}

		if (windowsize >= 770 && windowsize <= 1190) {
			if (sessionStorage.navbar_minimize === 'false') { $('.navbar-minimize a').click(); }
		}

		if (windowsize < 770) {
			$('#NavbarHeader').css('z-index', '1' );
			$('.navbar-minimize-mobile').css('display', 'block' );
			if(document.getElementsByTagName('body')[0].className !== '') { myBodyClass = document.getElementsByTagName('body')[0].className; }
			$('body').removeClass();
		}
		else {
			$('#NavbarHeader').css('z-index', '' );
			$('.navbar-minimize-mobile').css('display', 'none' );
			if(document.getElementsByTagName('body')[0].className === '') { document.getElementsByTagName('body')[0].className = myBodyClass; }
		}

		var NavbarHeaderRight = document.getElementById('NavbarHeaderRight').clientHeight;
	}

	checkWidth();
	$(window).resize(checkWidth);

	$('.submenu > a').click(function() {
		var parentElement = $(this).parent('.submenu'),
			nextElement = $(this).nextAll(),
			arrowIcon = $(this).find('.arrow'),
			plusIcon = $(this).find('.plus');

		// Add effect sound button click
		if(parentElement.parent('ul').find('ul:visible')) {
			parentElement.parent('ul').find('ul:visible').slideUp('fast');
			parentElement.parent('ul').find('.open').removeClass('open');
		}

		if(nextElement.is('ul:visible')) {
			arrowIcon.removeClass('open');
			plusIcon.removeClass('open');
			nextElement.slideUp('fast');
			arrowIcon.removeClass('fa-angle-double-down').addClass('fa-angle-double-right');
		}

		if(!nextElement.is('ul:visible')) {
			arrowIcon.addClass('open');
			plusIcon.addClass('open');
			nextElement.slideDown('fast');
			arrowIcon.removeClass('fa-angle-double-right').addClass('fa-angle-double-down');
		}

	});

	// When the minimize trigger is clicked
	function ButtonMinimizeSideBar() {
		$('body').toggleClass('page-sidebar-minimize');
		if ($(this).hasClass('navbar-minimize-pressed')) { $(this).removeClass('navbar-minimize-pressed'); sessionStorage.navbar_minimize = 'false'; }
		else { $(this).addClass('navbar-minimize-pressed'); sessionStorage.navbar_minimize = 'true'; }
	};

	$('.navbar-minimize a').on('click', ButtonMinimizeSideBar);
	$('.navbar-minimize-mobile').on('click', function() {
		if ($('body').hasClass('page-sidebar-left-show')) { $('body').removeClass(); }
		else { $('body').addClass('page-sidebar-left-show'); }
	});

	if (sessionStorage.navbar_minimize === 'true') { $('.navbar-minimize a').click(); }

	$('[data-toggle="tooltip"]').tooltip();
	$('[data-toggle="popover"]').popover();
	if ($('.chosen-select').length) {
		$('.chosen-select').chosen();
	}

	$('#sidebar-left .sidebar-menu').niceScroll({ cursorwidth: '10px', cursorborder: '0px', railalign: 'left' });

	$('.niceScrollRight').niceScroll({ cursorwidth: '10px', cursorborder: '0px', railalign: 'right' });
	$('.niceScrollLeft').niceScroll({ cursorwidth: '10px', cursorborder: '0px', railalign: 'left' });


	//--- Paneles
	function StartPanels() {
			// Collapse panel
			$('[data-action=collapse]').on('click', function(e){
				var targetCollapse = $(this).parents('.panel').find('.panel-body'),
					targetCollapse2 = $(this).parents('.panel').find('.panel-sub-heading'),
					targetCollapse3 = $(this).parents('.panel').find('.panel-footer')
				if((targetCollapse.is(':visible'))) {
					$(this).find('i').removeClass('fa-angle-up').addClass('fa-angle-down');
					targetCollapse.slideUp();
					targetCollapse2.slideUp();
					targetCollapse3.slideUp();
				}else{
					$(this).find('i').removeClass('fa-angle-down').addClass('fa-angle-up');
					targetCollapse.slideDown();
					targetCollapse2.slideDown();
					targetCollapse3.slideDown();
				}
				e.stopImmediatePropagation();
			});

			// Remove panel
			$('[data-action=remove]').on('click', function(){
				$(this).parents('.panel').fadeOut();
				// Remove backdrop element panel full size
				if($('body').find('.panel-fullsize').length) {
					$('body').find('.panel-fullsize-backdrop').remove();
				}
			});

			// Refresh panel
			$('[data-action=refresh]').on('click', function(){
				var targetElement = $(this).parents('.panel').children('.panel-body');
				targetElement.append('<div class="indicator"><span class="spinner"></span></div>');
				setInterval(function(){
					$.getJSON(BlankonApp.handleBaseURL()+'/assets/admin/data/reload-sample.json', function(json) {
						$.each(json, function() {
							// Retrieving data from json...
							console.log('Retrieving data from json...');
						});
						targetElement.find('.indicator').hide();
					});
				},5000);
			});

			// Expand panel
			$('[data-action=expand]').on('click', function(){
				if($(this).parents(".panel").hasClass('panel-fullsize'))
				{
					$('body').find('.panel-fullsize-backdrop').remove();
					$(this).data('bs.tooltip').options.title = 'Expand';
					$(this).find('i').removeClass('fa-compress').addClass('fa-expand');
					$(this).parents(".panel").removeClass('panel-fullsize');
				}
				else
				{
					$('body').append('<div class="panel-fullsize-backdrop"></div>');
					$(this).data('bs.tooltip').options.title = 'Minimize';
					$(this).find('i').removeClass('fa-expand').addClass('fa-compress');
					$(this).parents(".panel").addClass('panel-fullsize');
				}
			});

			// Search panel
			$('[data-action=search]').on('click', function(){
				$(this).parents('.panel').find('.panel-search').toggle(100);
				return false;
			});
		}
		StartPanels();
	//--- Fin paneles

});

function ClickHelpItem(Me) {
	var ActivedItems = document.getElementById('HelpMenuSystem').querySelectorAll('li.active'), Id = '';
	for (var i in ActivedItems) { ActivedItems[i].className = ''; }
	Me.parentElement.className = 'active';
	Id = 'data-item="' + Me.getAttribute('data-item') + '"';

	ActivedItems = document.getElementById('HelpMenuSystemItem').querySelectorAll('div.active');
	for (var i in ActivedItems) { ActivedItems[i].className = 'itmesmenuhelp jumbotron'; }
	ActivedItems = document.getElementById('HelpMenuSystemItem').querySelector('div[' + Id + ']');
	if (ActivedItems !== null) { ActivedItems.className += ' active'; }
}
