jQuery(document).ready(function ($) {
    var today = new Date();
    $("#date").val(today.getFullYear() + '-' + ('0' + (today.getMonth() + 1)).slice(-2) + '-' + ('0' + today.getDate()).slice(-2));

    var url = "/api/event/"
    getEvents(url);

    $("#filter").click(function () {
        var date = $("#date").val();
        getEvents(url + date)
    });

    $("#showAll").click(function () {
        getEvents(url)
    });

});

function getEvents(url) {
    $.getJSON(url, function (data) {
        $('.loading-block').html("");
        $('#events').html("");
        $('#events').append('<thead><tr><th> Объект </th><th> Тип объекта </th><th> Тип события </th><th> Дата события </th></tr></thead>');
        $('#events').append('<tbody>');
        for (var i = 0; i < data.length; i++) {
            $('#events').append('<tr><td>' + data[i].Path + '</td><td>' + data[i].TypeObj +
                '</td><td>' + data[i].TypeEvent + '</td><td>' + data[i].DateEvent + '</td></tr>');
        }
        $('#events').append('</tbody>');
        $("#events").tablesorter();
    });
};



