﻿function searchControllerPath() {
    var path = window.location.href;
    var a = path.split("/");
    if (path.indexOf("http://") + 1) {
        return a[0] + '//' + a[2] + '/' + (a[3].split("?"))[0];
    }
    else {
        return a[0] + '/' + a[1];
    }
}

$(document).ready(function () {
    $('#ShopCartTable').DataTable({
        "processing": true,
        "serverSide": true,
        "responsive": true,
        "autoWidth": false,
        "columnDefs": [
            { responsivePriority: 1, targets: 0 },
            { responsivePriority: 2, targets: -1 }
        ],
        "ajax": {
            "type": "GET",
            "url": searchControllerPath() + "/JsonTableFill",

            "data": {
                "json": localStorage.getItem("cart"),
            },
            "dataSrc": function (json) {
                //Make your callback here.
                $.each(json.data, function (index, item) {
                    item.Remove = '<a href= "' + searchControllerPath() + '/Delete?id=' + item.ID + '"/> <i class="fa fa-remove"></i></a >';
                    item.TotalPrice=item.UnitPrice*item.Quantity;
                    item.Quantity = '<input type="number" value="' + item.Quantity + '" onchange="ChangeQuantity(' + item.ID + ',value)" /> ';
                })
                return json.data;
            },
        },

        "columns": [
            { 'data': 'ProductName' },
            { 'data': 'Quantity' },
            { 'data': 'UnitPrice' },
            { 'data': 'TotalPrice' },
            { 'data': 'Remove' },
        ]

    });

});