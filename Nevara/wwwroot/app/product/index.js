class ProductController {
    
    init() {
        this.loadData();
        this.registeredEvent();
    }
    wrapPaging(recordCount, callback, changePageSize) {
        const totalSize = Math.ceil(recordCount / common.configs.pageSize);
        //unbind pagination if it existed or click change PageSize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        $('#paginationUL').twbsPagination({
            initiateStartPageClick: false,
            totalPages: totalSize,
            visiblePage: 7,     
            first: '',
            last: '',
            prev:'<span aria-hidden="true">&laquo;</span>',
            next:'<span aria-hidden="true">&raquo;</span>',
            onPageClick: function (event, p) {
                common.configs.pageIndex = p;
                setTimeout(callback, 200);
            }

        });
    }
    registeredEvent() {
        // binding event to controllers
        $('#ddlShowPage').on('change',
            function() {
                common.configs.pageSize = $(this).val();
                common.configs.pageIndex = 1;
                loadData(true);
            });

    }
    loadData(isPageChanged = false) {
        var self = this;        
        const template = $('#table-template').html();
        var render = "";
        $.ajax({
            type: "GET",
            data: {
                categoryId: null,
                keyword: $('#txtKeyword').val(),
                page: common.configs.pageIndex,
                pageSize: common.configs.pageSize
    },
            url: "/admin/product/GetProduct",
            dataType: 'json',        
            success: function (respone) {
                console.log(respone);
                $.each(respone.Results,
                    function(i, item) {
                        render += Mustache.render(template,
                            {
                                Id: item.Id,
                                Name: item.Name,
                                Thumbnail: item.Thumbnail == null
                                    ? '<img src="/admin-side/images/user.png" width=35'
                                    : '<img src="' + item.Thumbnail + '" width=35 />',
                                CategoryName: item.Category.Name,
                                Price: common.formatNumber(item.Price, 0),
                                Quantity : item.Quantity                                        
                            });
                        $('#lblTotalRecords').text(respone.RowCount);
                        if (render !== '') {
                            $('#tbl-content').html(render);
                        }
                        self.wrapPaging(respone.RowCount, function() {
                            self.loadData();
                        }, isPageChanged);
                    });
            },
            error: function(status) {
                console.log(status);
            
            }
        });
    }
    // end
  
}