var requestModule = (function () {
    var requestFoundTemplate;
    var requestAddedTemplate;
    var resultContainer;

    var module = {
        init: function (requestFormId) {
            requestFoundTemplate = utilsModule.handlebarsTemplate($('#request-found-template'));
            requestAddedTemplate = utilsModule.handlebarsTemplate($('#request-added-template'));
            requestForm($(requestFormId));
            resultContainer = $('#result-message');
        }
    };

    function requestForm(form) {
        form.on('submit', function (event) {
            event.preventDefault();
            var request = form.serialize();

            $.post(form.attr('action'), request, function (response) {
                if (response.message === 'Request found') {
                    handleRequestFound(response);
                } else {
                    handleRequestAdded(response);
                }
            })
        });
    }

    function handleRequestFound(data) {
        var html = requestFoundTemplate(data);
        resultContainer.html(html);
    }

    function handleRequestAdded(data) {
        var html = requestAddedTemplate(data);
        resultContainer.html(html);
    }

    return module;
})();