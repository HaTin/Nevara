class CartController {
    init() {     
        this.loadData();
        this.registeredEvent();
    }       
    registeredEvent() {
        var self = this;
        $("body").on('keypress','[type="number"]',function(e) {
            e.preventDefault();
        });
        $("body").on('change','.txtQuantity',function(e) {
            e.preventDefault();
            var me = $(this);
            var id = $(this).data('id');
            var quantity = $(this).val();
            if (quantity > 0) {
                $.ajax({
                    url: '/Cart/UpdateCart',
                    type: 'post',
                    data: {
                        productId: id,
                        quantity: quantity
                    },
                    success: function (response) {      
                        if (response.Success === true) {
                            self.loadData();
                            self.loadHeaderCart()
                        }
                        else if (response.Success === false) {
                            alert(response.Message);
                            me.val(response.Data);
                    }
                    }
                });
            }
        });
        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: '/Cart/RemoveFromCart',
                type: 'post',
                data: {
                    productId: id
                },
                success: function () {                    
                    self.loadData();
                    self.loadHeaderCart()
                }
            });
        });
    }  
    loadData() {            
        $.ajax({
            type: "GET",          
            url: "/Cart/GetCart",
            dataType: 'json',
            success: function (respone) {
                var render = "";
                var template = $('#template-cart').html();
                var totalAmount = 0;
                console.log(respone);
                $.each(respone,
                    function(i, item) {
                        render += Mustache.render(template,
                            {
                                ProductId: item.Product.Id,
                                Name: item.Product.Name,
                                Thumbnail: item.Product.Thumbnail,
                                Price: common.formatNumber(item.Price, 0),
                                Total: common.formatNumber(item.Price * item.Quantity, 0),
                                Quantity : item.Quantity

                            });
                        totalAmount += item.Price * item.Quantity
                    });
                $('#lblTotalAmount').text(common.formatNumber(totalAmount, 0)+ 'VND');
                if (render === '') {
                    $('#cart-content').html('<h3>Your cart is empty</h3>');
                }
                $('#cart-table').html(render);         
            },
            error: function(status) {
                console.log(status);
            }
        });
    }
    loadHeaderCart() {
         $.ajax({
            url: '/AjaxContent/HeaderCart',
            type: 'get',
            success: function (response) {
                $('#headerCart').html(response);
            }
        });
    }
}