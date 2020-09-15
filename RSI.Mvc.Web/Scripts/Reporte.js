function pickerOnChange() {
    var picker = kendo.toString(kendo.parseDate(this.value()), "dd / MM / yyyy");
    if (picker !== "") {
        /*
         * queryParameters -> handles the query string parameters
         * queryString -> the query string without the fist '?' character
         * re -> the regular expression
         * m -> holds the string matching the regular expression
         */
        var queryParameters = {}, queryString = location.search.substring(1),
            re = /([^&=]+)=([^&]*)/g, m;

        // Creates a map with the query string parameters
        while (m = re.exec(queryString)) {
            queryParameters[decodeURIComponent(m[1])] = decodeURIComponent(m[2]);
        }

        // Replace or add parameter
        queryParameters['fecha'] = picker;

        /*
         * Replace the query portion of the URL.
         * jQuery.param() -> create a serialized representation of an array or
         *     object, suitable for use in a URL query string or Ajax request.
         */
        location.search = $.param(queryParameters); // Causes page to reload
    }

}

setInterval(function () { pickerOnChange; }, $("#refreshInterval").val());