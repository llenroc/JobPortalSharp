$(function () {
    $('.slider').slider({
        ticks: [0, 25000, 50000, 75000, 100000],
        ticks_labels: ['$0', '$25K', '$50K', '$75K', '$100K']
    });
    $('.datepicker').datepicker();
    $('.datepicker-closing-before').datepicker({
        startDate: $.format.date(new Date(), 'MM/dd/yyyy')
    });
})