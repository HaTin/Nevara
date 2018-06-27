class ProductController {
    init() {
        this.loadCategories();
        this.loadCollections();
        this.loadData();
        this.registeredEvent();
        
    }
    wrapPaging(recordCount, callback,changePageSize) {
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
                    setTimeout(callback, 200);
                }

            });
        }
    }
    registeredEvent() {
        var self = this;
        $('#btn-search').on('click',
            function() {
                self.loadData(true);
            });
        $('#search-value').on('keypress',
            function(e) {
                if (e.which === 13) {
                    self.loadData(true);
                }
            });
    }
    loadData(isPageChanged = false) {
        var self = this;        
        const template = $('#table-template').html();
        var render = "";
        $.ajax({
            type: "GET",
            data: {
                categoryId: $('#category-select').val(),
                collectionId: $('#collection-select').val(),
                keyword: $('#search-value').val(),
                page: common.configs.pageIndex,
                pageSize: common.configs.pageSize
    },
            url: "/admin/product/GetProduct",
            dataType: 'json',        
            success: function (respone) {   
                $.each(respone.Results,
                    function (i, item) {                       
                        render += Mustache.render(template,
                            {
                                Id: item.Id,
                                Name: item.Name,
                                Thumbnail: item.Thumbnail == null
                                    ? '<img src="/admin-client/images/user.png" width=35'
                                    : '<img src="' + item.Thumbnail + '" width=35 />',
                                CategoryName: item.CategoryName,
                                Price: common.formatNumber(item.Price, 0),
                                Quantity : item.Quantity                                        
                            });
                        //$('#lblTotalRecords').text(respone.RowCount);
                             
                    });
                if (render === '') {
                    render += '<tr><td colspan="6"><h3>No Results</h3></td></tr>';
               }
                $('#tbl-content').html(render);                
                self.wrapPaging(respone.RowCount, function () {
                    self.loadData();
                },isPageChanged);
            },
            error: function(status) {
                console.log(status);
            
            }
        });
    }
    loadCategories() {     
        var render = "<option>Select Category</option>";
        $.ajax({
            type: "GET",
            url: "/admin/product/GetCategories",
            dataType: 'json',
            success: function (respone) {
                console.log(respone);
                $.each(respone,
                    function (i, item) {
                        render += `<option value=${item.Id}>${item.Name}</option>`;
                    });
                $('#category-select').html(render);
            },
            error: function (status) {
                console.log(status);

            }
        });
    }
    loadCollections() {
        var render = "<option>Select collection</option>";
        $.ajax({
            type: "GET",
            url: "/admin/product/GetCollections",
            dataType: 'json',
            success: function (respone) {
                console.log(respone);
                $.each(respone,
                    function (i, item) {
                        render += `<option value=${item.Id}>${item.CollectionName}</option>`;
                    });
                $('#collection-select').html(render);
            },
            error: function (status) {
                console.log(status);

            }
        });
    }
    // end
  
}