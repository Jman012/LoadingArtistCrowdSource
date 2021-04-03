if (typeof navigator.serviceWorker !== "undefined") {
	navigator.serviceWorker.getRegistrations().then(function(registrations) {
		for(let registration of registrations) {
		registration.unregister()
		}
	});
}
window.getSelectedValues = function (sel) {
	var results = [];
	var i;
	for (i = 0; i < sel.options.length; i++) {
		if (sel.options[i].selected) {
			results[results.length] = sel.options[i].value;
		}
	}
	return results;
};
$(document).on("click", ".allow-focus", function (e) {
	e.stopPropagation();
});
window.WriteCookie = function (name, value, days) {
	var expires;
	if (days) {
		var date = new Date();
		date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
		expires = "; expires=" + date.toGMTString();
	}
	else {
		expires = "";
	}
	document.cookie = name + "=" + value + expires + "; path=/";
}
window.ReadCookie = function (cname) {
	var name = cname + "=";
	var decodedCookie = decodeURIComponent(document.cookie);
	var ca = decodedCookie.split(';');
	for (var i = 0; i < ca.length; i++) {
		var c = ca[i];
		while (c.charAt(0) == ' ') {
			c = c.substring(1);
		}
		if (c.indexOf(name) == 0) {
			return c.substring(name.length, c.length);
		}
	}
	return "";
}
window.ScrollToTop = function (selector) {
	document.querySelectorAll(selector).forEach(function(el) {
		el.scrollTo(0, 0);
	});
}