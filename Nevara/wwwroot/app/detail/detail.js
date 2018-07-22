
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
        $('body').on('click','.add-to-cart',
            function(e) {
            console.log(e);
                e.preventDefault();
                var id = parseInt($(this).data('id'));
                var quantity = parseInt($('#txtQuantity').val());
                console.log(quantity);
                $.ajax({
                    url: '/Cart/AddToCart',
                    type: 'post',
                    dataType: 'json',
                    data: {
                        productId: id,
                        quantity: quantity != null ? 1 : quantity
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
                }, 2000);
            }
        });      
    }
}

