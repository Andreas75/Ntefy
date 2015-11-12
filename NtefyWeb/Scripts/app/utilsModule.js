var utilsModule = (function () {
    var module = {
        handlebarsTemplate: function (elem) {
            var source = elem.html();
            return Handlebars.compile(source);
        }
    };
    return module;
})();