let paymentList = [];
let orderStatusList = [];
class OrderController {
    init() {
        this.loadPaymentMethod();
        this.loadOrderStatus();
        this.loadData();
        this.registeredEvent();
    }

    wrapPaging(recordCount, callback, changePageSize) {
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        if (recordCount > 0) {
            const totalSize = Math.ceil(recordCount / common.configs.pageSize);
            $('#paginationUL').twbsPagination({
                initiateStartPageClick: false,
                totalPages: totalSize,
                visiblePage: 7,
                first: '',
                last: '',
                prev: '<span aria-hidden="true">&laquo;</span>',
                next: '<span aria-hidden="true">&raquo;</span>',
                onPageClick: function(event, p) {
                    common.configs.pageIndex = p;
                    callback();
                }

            });
        }
    }

    resetForm() {
        $('#hiddenId').val(0);
        $('#customerName').text('');
        $('#phone').text('');
        $('#payment').text('');
        $('#email').text('');
        $('#address').text('');
        $('#date').text('');
        $('#message').text('');
    }
    registeredEvent() {
        var self = this;
        $('#btn-search').on('click',
            function() {
                common.configs.pageIndex = 1;
                self.loadData(true);
            });
        $('#search-value').on('keypress',
            function(e) {
                common.configs.pageIndex = 1;
                if (e.which === 13) {
                    self.loadData(true);
                }
            });
        $('body').on('click',
            '.edit',
            function (e) {
                const template = $('#table-order-detail').html();
                var id = $(this).data('id');
                var render = '';
                var orderTotal = 0;
                $.ajax({
                    type: "GET",
                    url: "/admin/order/find",
                    data: { id: id },
                    dataType: "json",
                    success: function (response) {
                        console.log(response);
                        self.resetForm();
                        $('#content-id').text('#'+response.Id);
                        $('#hiddenId').val(response.Id);
                        $('#customerName').text(response.CustomerName);
                        $('#phone').text(response.CustomerMobile);
                        $('#payment').text(self.getPaymentValue(response.PaymentMethod));
                        $('#email').text(response.CustomerEmail);
                        $('#address').text(response.CustomerAddress);
                        $('#date').text(common.dateTimeFormatJson(response.CreatedDate));
                        $('#message').text(response.CustomerMessage);
                        $('#status-select').val(response.BillStatus);
                        $.each(response.DetailViewModels,
                            function(i, item) {
                                render += Mustache.render(template,
                                    {
                                        No : i+1,
                                        ProductName : item.ProductViewModel.Name,
                                        Quantity : item.Quantity,
                                        Total : common.formatNumber((item.Quantity*item.Price),0)
                                    });
                                orderTotal += item.Quantity*item.Price;
                            });
                        $('#order-details').html(render);
                        $('#total').html('<b>Total :</b> '+common.formatNumber(orderTotal)+' VND')
                    }
                });
                e.preventDefault();
                $('#modal').modal('show');
            });
        $('body').on('click', '.remove', function (e) {
            e.preventDefault();
      
            var id = $(this).data('id');
            swal({
                text: 'Are you sure ?',
                icon : "warning",
                buttons : true,
                dangerMode: true
            }).then((isConfirm) => {
                if (isConfirm) {
                    $.ajax({
                        type: "POST",
                        url: "/Admin/Product/Remove",
                        data: { id: id },
                        dataType: "json",
                        success: function(response) {
                            if (response.Success === false) {
                                $.toast({
                                    bgColor: '#dc3545',
                                    heading: response.Message,
                                    text: '',
                                    position: 'top-right',
                                    loaderBg: '#dc3545',
                                    icon: 'warning',
                                    hideAfter: 1500,
                                    stack: 1
                                });
                            } else {
                                self.loadData(true);
                                $.toast({
                                    bgColor: '#20c997',
                                    heading: 'Delete Successful',
                                    text: '',
                                    position: 'top-right',
                                    loaderBg: '#20c997',
                                    icon: 'success',
                                    hideAfter: 1500,
                                    stack: 2
                                });
                            }
                        },
                        error: function (status) {
                            console.log(status);
                        }
                    });
                }
            });

        });
        $('#btn-save').on('click',
            function (e) {
                    e.preventDefault();
                    $.ajax({
                        type: "POST",
                        url: "/admin/order/UpdateStatus",
                        data: {
                            orderId: $('#hiddenId').val(),
                            status: $('#status-select').val()
                        },
                        success: function () {
                            $('#modal').modal('hide');
                            self.loadData(true);
                            $.toast({
                                bgColor:'#20c997',
                                heading: 'Save Successful',
                                text: '',
                                position: 'top-right',
                                loaderBg: '#20c997',
                                icon: 'success',
                                hideAfter: 2500,
                                stack: 6
                            });

                        },
                        error: function(err) {
                            console.log(err);
                            $.toast({
                                bgColor: '#20c997',
                                heading: 'Save failed',
                                text: '',
                                position: 'top-right',
                                loaderBg: 'red',
                                icon: 'success',
                                hideAfter: 3500,
                                stack: 6
                            });
                        }
                    });
                
            });
    }

    loadData(isPageChanged = false) {
        var self = this;
        const template = $('#table-template').html();
        var render = "";
        $.ajax({
            type: "GET",
            data: {
                keyword: $('#search-value').val(),
                page: common.configs.pageIndex,
                pageSize: common.configs.pageSize
            },
            url: "/admin/order/GetOrders",
            dataType: 'json',
            success: function (response) {
                console.log(response);
                $.each(response.Results,
                    function(i, item) {
                        render += Mustache.render(template,
                            {
                                Id: item.Id,
                                CustomerName: item.CustomerName,
                                BillStatus: self.getOrderStatusValue(item.BillStatus),
                                CreatedDate : '<i class="fa fa-clock-o"></i> ' + common.dateTimeFormatJson(item.CreatedDate),
                            });
                    });
                if (render === '') {
                    render += '<tr><td colspan="6"><h3>No Results</h3></td></tr>';
                }
                $('#tbl-content').html(render);
                self.wrapPaging(response.RowCount,
                    function() {
                        self.loadData();
                    },
                    isPageChanged);
            },
            error: function(status) {
                console.log(status);

            }
        });
    }

    loadPaymentMethod() {
        $.ajax({
            type: "GET",
            url: "/admin/order/GetPaymentMethod",
            dataType: 'json',
            success: function(response) {
                paymentList = response;
                    console.log(response);
            },
            error: function(status) {
                console.log(status);
            }
        });
    }
    getPaymentValue(payment){
        var status = paymentList.find(function(element){
            return element.Value === payment;
        });
        return status.Name;
    }
    getOrderStatusValue(status){
        var status = orderStatusList.find(function(element){
            return element.Value === status;
        });
        return status.Name;
    }
    loadOrderStatus() {
        var render = '';
        $.ajax({
            type: "GET",
            url: "/admin/order/GetBillStatus",
            dataType: 'json',
            success: function(response) {
                orderStatusList = response;
                //console.log(response);
                $.each(response,
                    function(i, item) {
                        render += `<option value=${item.Value}>${item.Name}</option>`;
                    });
                $('#status-select').html(render);
                
            },
            error: function(status) {
                console.log(status);
            }
        });
    }

    // end

}
