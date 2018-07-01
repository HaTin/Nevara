
    class ProductController {
        init() {
            this.loadCategories();
            this.loadCollections();
            this.loadData();
            this.registeredEvent();
            this.getColors();
            this.getManufacturers();
            this.getMaterials();
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

        registeredEvent() {
            var self = this;
            $('#formProduct').validate({                
                rules: {
                    Name: { required: true },
                    price: {
                        required: true,
                        number: true
                    },
                    promotionPrice: {
                        required: true,
                        number: true
                    },
                    originalPrice: {
                        required: true,
                        number: true
                    },
                    depth: {
                        required: true,
                        number: true
                    },
                    height: {
                        required: true,
                        number: true
                    },
                    width: {
                        required: true,
                        number: true
                    },
                    quantity: {
                        required: true,
                        number: true
                    },
                    categorySelect: { valueNotEquals: "default" },
                    collectionSelect: { valueNotEquals: "default" },
                    materialSelect: { valueNotEquals: "default" },
                    colorSelect: { valueNotEquals: "default" },
                    manufacturerSelect: { valueNotEquals: "default" }
                }
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
                function(e) {
                    var id = $(this).data('id');
                    $.ajax({
                        type: "GET",
                        url: "/admin/product/find",
                        data: { id: id },
                        dataType: "json",
                        success: function(response) {
                            $('#hiddenId').val(response.Id);
                            $('#txtName').val(response.Name);
                            $('#collection-select-2').val(response.CollectionId);
                            $('#category-select-2').val(response.CategoryId);
                            $('#color-select').val(response.ColorId);
                            $('#manufacturer-select').val(response.ManufacturerId);
                            $('#material-select').val(response.MaterialId);
                            $('#txtWidth').val(response.Width);
                            $('#txtHeight').val(response.Height);
                            $('#txtDepth').val(response.Depth);
                            $('#txtOrginalPrice').val(response.OriginalPrice);
                            $('#txtPrice').val(response.Price);
                            $('#txtPromotionPrice').val(response.PromotionPrice);
                            $('#home-flag').prop('checked', response.HomeFlag);
                            $('#new-flag').prop('checked', response.NewFlag);
                            $('#hot-flag').prop('checked', response.HotFlag);
                            $('#txtQuantity').val(response.Quantity);
                        }
                    });
                    e.preventDefault();
                    $('#modal').modal('show');
                });
            $('#btn-save').on('click',
                function(e) {
                    if ($('#formProduct').valid()) {
                        e.preventDefault();
                        $.ajax({
                            type: "POST",
                            url: "/admin/product/SaveEntity",
                            data: {
                                Id: $('#hiddenId').val(),
                                Name: $('#txtName').val(),
                                Width: $('#txtWidth').val(),
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
                                PromotionPrice: $('#txtPromotionPrice').val(),
                                HomeFlag: $('#home-flag').prop('checked') === true ? true : false,
                                NewFlag: $('#new-flag').prop('checked') === true ? true : false,
                                HotFlag: $('#hot-flag').prop('checked') === true ? true : false
                            },
                            success: function (response) {
                                console.log(response);
                                $('#modal').modal('hide');
                                self.loadData(true);
                            },
                            error: function(err) {
                                console.log(err);
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
                success: function(respone) {
                    $.each(respone.Results,
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
                    self.wrapPaging(respone.RowCount,
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
            var render = "<option>Select Category</option>";
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
            var render = "<option>Select collection</option>";
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
            var render = "<option>Select material</option>";
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
            var render = "<option>Select Manufacturer</option>";
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
            var render = "<option>Select Color</option>";
            $.ajax({
                type: "GET",
                url: "/admin/product/GetColors",
                dataType: 'json',
                success: function(respone) {
                    console.log(respone);
                    $.each(respone,
                        function(i, item) {
                            render += `<option value=${item.Id}>${item.ColorName}</option>`;
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
