
    class ProductController {
        init() {            
            this.loadCategories();
            this.loadCollections();          
            this.loadData();            
            this.getColors();
            this.getManufacturers();
            this.getMaterials();
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
                        setTimeout(callback, 200);
                    }

                });
            }
        }

        resetForm() {
            $('#hiddenId').val(0);
            $('#txtName').val('');
            $('#collection-select-2').val('default');
            $('#category-select-2').val('default');
            $('#color-select').val('default');
            $('#manufacturer-select').val('default');
            $('#material-select').val('default');
            $('#txtLength').val('');
            $('#txtHeight').val('');
            $('#txtDepth').val('');
            $('#txtOrginalPrice').val('');
            $('#txtPrice').val('');
            $('#txtPromotionPrice').val('');
            $('#home-flag').prop('checked', false);
            $('#new-flag').prop('checked', false);
            $('#hot-flag').prop('checked', false);
            $('#txtQuantity').val('');
            $('#txtDescription').val('');
            $('#txthiddenImage').val('');
            $('#customRoxyImage').attr('src', '');
            $('.color-display').css('background', '#e9ecef');
        }
        registeredEvent() {
            var self = this; 
            $('#btnCancel').on('click', function () {
                $('#fileman').modal('hide');
            });
            $('#btnAddImage').on('click',
                function () {
                    $('#hiddenFlag').val('1');
                    $('#fileman').modal('show');
                });
            $('#btn-add').on('click',
                function () {
                    self.resetForm();
                    $('.parsley-errors-list').html('');
                    $('#modal').modal('show');
                });         
            $('#formProduct').parsley({ 
            });
            $('#color-select').on('change',
                function() {
                    var element = $(this).find('option:selected');
                    var code = element.attr('code');
                    $('.color-display').css('background',code);
                });
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
                    $('.parsley-errors-list').html('');
                    var id = $(this).data('id');
                    $.ajax({
                        type: "GET",
                        url: "/admin/product/find",
                        data: { id: id },
                        dataType: "json",
                        success: function (response) {                      
                            self.resetForm();
                            $('#hiddenId').val(response.Id);
                            $('#txtName').val(response.Name);
                            $('#collection-select-2').val(response.CollectionId);
                            $('#category-select-2').val(response.CategoryId);
                            $('#color-select').val(response.ColorId);
                            var option = $('#color-select').find('option:selected');
                            $('.color-display').css('background', option.attr('code'));
;                            $('#manufacturer-select').val(response.ManufacturerId);
                            $('#material-select').val(response.MaterialId);
                            $('#txtLength').val(response.Length);
                            $('#txtHeight').val(response.Height);
                            $('#txtDepth').val(response.Depth);
                            $('#txtOrginalPrice').val(response.OriginalPrice);
                            $('#txtPrice').val(response.Price);
                            $('#txtPromotionPrice').val(response.PromotionPrice);
                            $('#home-flag').prop('checked', response.HomeFlag);
                            $('#new-flag').prop('checked', response.NewFlag);
                            $('#hot-flag').prop('checked', response.HotFlag);
                            $('#txtQuantity').val(response.Quantity);
                            $('#txtDescription').val(response.Description);
                            $('#txthiddenImage').val(response.Thumbnail);
                            $('#customRoxyImage').attr('src', response.Thumbnail);
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
                        type: "warning",
                        title : '',
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Yes, delete it!"                        
                },
                    function(isConfirm) {
                        if (!isConfirm) return;
                        $.ajax({
                            type: "POST",
                            url: "/Admin/Product/Remove",
                            data: { id: id },
                            dataType: "json",
                            success: function (response) {                                
                                self.loadData(true);
                                    $.toast({
                                        bgColor: '#20c997',
                                        heading: 'Delete Successful',
                                        text: '',
                                        position: 'top-right',
                                        loaderBg: '#20c997',
                                        icon: 'success',
                                        hideAfter: 2500,
                                        stack: 3
                                    });                                
                            },
                            error: function (status) {
                                console.log(status);                            
                                
                            }
                        });
                    }); 
                 
                });                           
            $('#btn-save').on('click',
                function (e) {                  
                    if ($('#formProduct').parsley().isValid()) {
                        e.preventDefault();
                        $.ajax({
                            type: "POST",
                            url: "/admin/product/SaveEntity",
                            data: {
                                Id: $('#hiddenId').val(),
                                Name: $('#txtName').val(),
                                Length: $('#txtLength').val(),
                                Height: $('#txtHeight').val(),
                                Depth: $('#txtDepth').val(),
                                Quantity: $('#txtQuantity').val(),
                                CategoryId: $('#category-select-2').val(),
                                CollectionId: $('#collection-select-2').val(),
                                MaterialId: $('#material-select').val(),
                                ManufacturerId: $('#manufacturer-select').val(),
                                ColorId: $('#color-select').val(),
                                Price: $('#txtPrice').val(),
                                OriginalPrice: $('#txtOrginalPrice').val(),
                                Description: $('#txtDescription').val(),
                                PromotionPrice: $('#txtPromotionPrice').val(),
                                HomeFlag: $('#home-flag').prop('checked') === true ? true : false,
                                NewFlag: $('#new-flag').prop('checked') === true ? true : false,
                                HotFlag: $('#hot-flag').prop('checked') === true ? true : false,
                                Thumbnail: $('#txthiddenImage').val()
                    },
                            success: function (response) {                                
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
                success: function (response) {
                    console.log(response);
                    $.each(response.Results,
                        function(i, item) {
                            render += Mustache.render(template,
                                {
                                    Id: item.Id,
                                    Name: item.Name,
                                    Thumbnail: item.Thumbnail == null
                                        ? '<img src="/admin-client/images/user.png" width=35'
                                        : '<img src="' + item.Thumbnail + '" width=35 />',
                                    CategoryName: item.CategoryName,
                                    Price: common.formatNumber(item.Price, 0),
                                    Quantity: item.Quantity
                                });
                            //$('#lblTotalRecords').text(respone.RowCount);

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

        loadCategories() {
            var render = '<option value="">Select Category</option>';
            $.ajax({
                type: "GET",
                url: "/admin/product/GetCategories",
                dataType: 'json',
                success: function(respone) {
                    console.log(respone);
                    $.each(respone,
                        function(i, item) {
                            render += `<option value=${item.Id}>${item.Name}</option>`;
                        });
                    $('#category-select').html(render);
                    $('#category-select-2').html(render);
                },
                error: function(status) {
                    console.log(status);
                }
            });
        }

        loadCollections() {
            var render = "<option value=''>Select collection</option>";
            $.ajax({
                type: "GET",
                url: "/admin/product/GetCollections",
                dataType: 'json',
                success: function(respone) {
                    console.log(respone);
                    $.each(respone,
                        function(i, item) {
                            render += `<option value=${item.Id}>${item.CollectionName}</option>`;
                        });

                    $('#collection-select').html(render);
                    $('#collection-select-2').html(render);


                },
                error: function(status) {
                    console.log(status);

                }
            });
        }

        getMaterials() {
            var render = "<option value=''>Select material</option>";
            $.ajax({
                type: "GET",
                url: "/admin/product/GetMaterials",
                dataType: 'json',
                success: function(respone) {
                    console.log(respone);
                    $.each(respone,
                        function(i, item) {
                            render += `<option value=${item.Id}>${item.MaterialName}</option>`;
                        });

                    $('#material-select').html(render);
                },
                error: function(status) {
                    console.log(status);
                }
            });
        }

        getManufacturers() {
            var render = "<option value=''>Select Manufacturer</option>";
            $.ajax({
                type: "GET",
                url: "/admin/product/GetManufacturers",
                dataType: 'json',
                success: function(respone) {
                    console.log(respone);
                    $.each(respone,
                        function(i, item) {
                            render += `<option value=${item.Id}>${item.ManufacturerName}</option>`;
                        });
                    $('#manufacturer-select').html(render);

                },
                error: function(status) {
                    console.log(status);
                }
            });
        }

        getColors() {
            var render = "<option code='#e9ecef' value=''>Select Color</option>";
            $.ajax({
                type: "GET",
                url: "/admin/product/GetColors",
                dataType: 'json',
                success: function(respone) {
                    console.log(respone);
                    $.each(respone,
                        function(i, item) {
                            render += `<option code=${item.Code} value=${item.Id}>${item.ColorName}<span></span></option>`;
                        });
                    $('#color-select').html(render);
                },
                error: function(status) {
                    console.log(status);
                }
            });
        }

        // end

    }
