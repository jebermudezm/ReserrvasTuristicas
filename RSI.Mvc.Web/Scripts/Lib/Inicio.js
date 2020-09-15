

$(document).ready(function () {
	var NavbarSearch = '<form class="navbar-form"><div class="form-group has-feedback">';
	NavbarSearch += '<input id="text-search-any" class="form-control typeahead rounded tt-input" type="text" placeholder="Busqueda de la aplicación" autocomplete="off">';
	NavbarSearch += '<button id="btn-search-any" class="btn btn-theme fa fa-search form-control-feedback rounded" type="button"></button>';
	NavbarSearch += '</div></form>';

	$('#NavbarSearchBox').html(NavbarSearch);

	var NavbarButton = '<a href="#" class="button-notification" target="_blank"><i class="fa fa-bell-o"></i><span class="count label label-danger rounded">6</span></a>';
	$('#NavbarButtonNotification').html(NavbarButton);


	$('#IconUserProfile').addClass('fa fa-user');
	$('#IconUserProfile').css('color', 'white');
});
