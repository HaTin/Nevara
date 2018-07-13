
class ImageController {   
    init() {
        this.registeredEvent(); 
    }        
    registeredEvent() {
        var self = this;
        $('body').on('click', '.btn-images', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $('#hidId').val(id);      
            self.loadImages();
            $('#modal-image-manage').modal('show');
        });
        $('body').on('click', '.remove-image', function (e) {
            $(this).parent().remove();
        });
        $('#btn-add-image').on('click',
            function () {
                $('#hiddenFlag').val('2');
                $('#fileman').modal('show');
            });

        $("#btn-save-image").on('click', function () {
            var imageList = [];
            $.each($('#imageList').find('img'), function (i, item) {
                imageList.push($(this).data('path'));
            });
            $.ajax({
                url: '/admin/Product/SaveImages',
                data: {
                    productId: $('#hidId').val(),
                    images: imageList
                },
                type: 'post',
                dataType: 'json',
                success: function () {
                    $('#modal-image-manage').modal('hide');
                    $.toast({
                        bgColor: '#20c997',
                        heading: 'Save Successful',
                        text: '',
                        position: 'top-right',
                        loaderBg: '#20c997',
                        icon: 'success',
                        hideAfter: 1500,
                        stack: 6
                    });                  
                    $('#imageList').html('');
                }
            });
        });
    }
    loadImages() {
        $.ajax({
            url: '/admin/Product/GetImages',
            data: {
                productId: $('#hidId').val()
            },
            type: 'get',
            dataType: 'json',
            success: function (response) {
                console.log(response);
                var render = '';
                $.each(response, function (i, item) {
                    render += '<div class="col-md-3"><button type="button" class="close remove-image" aria-label="Close"><span aria-hidden="true">x</span></button><img data-path="' + item.ImagePath + '"width="100" src="' +
                        item.ImagePath + '"></div>';
                });
                $('#imageList').html(render);
            }
        });
    }
}

