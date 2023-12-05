        $('.service-item [type="checkbox"]').change(function() {
            let $this = $(this);
            $this.closest('label').find('.select-type').toggleClass('active');
        });