
class DetailController {   
    init() {
        this.registeredEvent(); 
    }        
    registeredEvent() {
        var self = this;
        $('body').on('click', '.dec', function(e) {
            if ($('#txtQuantity').val() === '0')
            $('#txtQuantity').val('1');
        });
        $('#btnAddToCart').on('click',
            function(e) {
                e.preventDefault();
                var id = parseInt($(this).data('id'));
                var quantity = parseInt($('#txtQuantity').val());
                $.ajax({
                    url: '/Cart/AddToCart',
                    type: 'post',
                    dataType: 'json',
                    data: {
                        productId: id,
                        quantity: quantity
                    },
                    success: function () {
                        self.loadHeaderCart();
                    }
                });
            });
    }   
    loadHeaderCart() {
          $.ajax({
            url: '/AjaxContent/HeaderCart',
            type: 'get',
            success: function (response) {
                $('#headerCart').html(response);
                $('.main-cart-box').css('visibility', 'visible');
                $('.main-cart-box').css('opacity', '1');
                setTimeout(function () {                    	                
                $('.main-cart-box').css('opacity', 0);
                    console.log('a');
                }, 2000);
            }
        });      
    }
}

