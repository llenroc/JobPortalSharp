function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

$(function () {
    $('.slider').slider({
        ticks: [0, 25000, 50000, 75000, 100000],
        ticks_labels: ['$0', '$25K', '$50K', '$75K', '$100K']
    });
    $('.datepicker').datepicker({ autoclose: true });
    $('.datepicker-closing-before').datepicker({
        startDate: $.format.date(new Date(), 'MM/dd/yyyy')
    });
    $('.multiselect').multiselect();
})