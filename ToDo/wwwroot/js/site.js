autosize($('textarea'));

$('textarea').keypress(function (event) {
    if (event.which === 13 && !event.shiftKey) {
        $(this).closest("form").submit();
        event.preventDefault();
    }
});