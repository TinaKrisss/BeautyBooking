        $('.service-item [type="checkbox"]').change(function() {
            let $this = $(this);
            $this.closest('label').find('.select-type').toggleClass('active');

            calculateTotals();
        });

        function calculateTotals() {
            const $wrapper = $('#services_form');
            let $checkedServices = $('.select-type.active', $wrapper);
            let totalTime = 0;
            let totalSum = 0;

            $checkedServices.each(function(i, item) {
                totalTime += +$('[name=duration]', item).val();
                totalSum += +$('.price:not(.hide)', item).text();
            });

            totalSum = isNaN(totalSum) ? 0 : totalSum;

            let formattedTime = (new Date(totalTime * 1000)).toISOString().substr(11, 5);

            $('#total-time').text(formattedTime);
            $('[name="total_time"]').val(totalTime);
            $('#total-sum').text(totalSum);
        }