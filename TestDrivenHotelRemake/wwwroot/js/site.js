// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function () {
    console.log('JavaScript loaded!');
    // Use data-date-range attribute to identify forms with date range
    var formsWithDateRange = document.querySelectorAll('[data-date-range]');

    formsWithDateRange.forEach(function (form) {
        form.addEventListener('submit', function (event) {
            var startDateInput = form.querySelector('[name="StartDate"]');
            var endDateInput = form.querySelector('[name="EndDate"]');

            // Check if end date is after start date
            if (startDateInput.value && endDateInput.value && startDateInput.value >= endDateInput.value) {
                alert('End date must be after start date.');
                event.preventDefault(); // Prevent form submission
            }
        });
    });
});