$(document).ready(function () {
    var ShiftId = $("#SystemstationShiftId").val();
    var ShiftStatus = $("#SystemStationShiftStatusId").val();
    var creditInvoiceDataTable;

    // Initialize DataTable
    creditInvoiceDataTable = $('#ShiftCreditInvoiceTable').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/StationManagement/GetSystemStationShiftCreditInvoiceData",
            "type": "GET",
            "dataType": "json",
            "data": function (d) {
                // Construct parameters object
                var params = {
                    draw: d.draw,
                    searchValue: d.search.value,
                    length: d.length,
                    start: d.start,
                    ShiftId: ShiftId // Set ShiftId as needed
                    // You can include additional parameters if needed
                };
                return params;
            },
            "dataSrc": function (json) {
                // Manipulate data as needed before rendering
                return json.data;
            }
        },
        "columns": [
            { "data": "ShiftCreditInvoiceId", "name": "ShiftCreditInvoiceId", "visible": false },
            { "data": "ShiftId", "name": "ShiftId", "visible": false },
            { "data": "AttendantId", "name": "AttendantId", "visible": false },
            { "data": "AttendantName", "name": "AttendantName", "autoWidth": true },
            { "data": "CustomerId", "name": "CustomerId", "visible": false },
            { "data": "CustomerName", "name": "CustomerName", "autoWidth": true },
            { "data": "EquipmentId", "name": "EquipmentId", "visible": false },
            { "data": "EquipmentNo", "name": "EquipmentNo", "autoWidth": true },
            { "data": "ProductVariationId", "name": "ProductVariationId", "visible": false },
            { "data": "ProductVariationName", "name": "ProductVariationName", "autoWidth": true },
            { "data": "ProductUnits", "name": "ProductUnits", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "ProductPrice", "name": "ProductPrice", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "ProductDiscount", "name": "ProductDiscount", "autoWidth": true, "visible": false, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "ProductTotal", "name": "ProductTotal", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "VatTotal", "name": "VatTotal", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "OrderNumber", "name": "OrderNumber", "autoWidth": true },
            { "data": "RecieptNumber", "name": "RecieptNumber", "autoWidth": true },
            { "data": "Reference", "name": "Reference", "autoWidth": true },
            {
                "data": null,
                "render": function (data, type, row) {
                    // Show the Edit button only if ShiftStatus is not equal to 1
                    if (ShiftStatus != 1) {
                        return "<button class='btn-info btn-xs btn-outline-info editcreditinvoice text-white font-weight-bold text-uppercase m-1'>Edit</button>";
                    } else {
                        return "<button class='btn-info btn-xs btn-outline-info text-white font-weight-bold text-uppercase m-1'>Edit</button>";
                    }
                }
            }
        ]
    });
    $('#ShiftCreditInvoiceTable').on('click', '.editcreditinvoice', function () {
        clearCreditInvoiceForm();
        var rowData = creditInvoiceDataTable.row($(this).parents('tr')).data(); // Get the data of the row containing the clicked button
        var customerId = rowData.CustomerId;
        Getsystemcustomerdetaildata(customerId);
        Getdryproductdetail(rowData.ProductVariationId);
        $('#CreditInvoiceCustomerEquipmentId').val(rowData.EquipmentId);
        $('#CreditInvoiceId').val(rowData.ShiftCreditInvoiceId);
        $('#CreditInvoiceShiftId').val(rowData.ShiftId);
        $('#CreditInvoiceCashierId').val(rowData.AttendantId);
        $('#CreditInvoiceSearchCustomerId').val(customerId);
        $('#CreditInvoiceProductVariationId').val(rowData.ProductVariationId);
        $('#CreditInvoiceItemQuantityId').val(rowData.ProductUnits);
        $('#CreditInvoiceItemPriceId').val(rowData.ProductPrice);
        $('#CreditInvoiceItemDiscountId').val(rowData.ProductDiscount);
        $('#CreditInvoiceItemtotaAmountId').val(rowData.ProductTotal);
        $('#CreditInvoiceReferenceId').val(rowData.Reference);
        $('#CreditInvoiceVatTotalId').val(rowData.VatTotal);
        $('#CreditInvoiceOrderNumberId').val(rowData.OrderNumber);
        $('#CreditInvoiceRecieptNumberId').val(rowData.RecieptNumber);
    });

    var shiftWetStockPurchaseDataTable;
    // Initialize DataTable
    shiftWetStockPurchaseDataTable = $('#ShiftWetStockPurchaseTable').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/StationManagement/Getsystemstationshiftwetstockpurchasedata",
            "type": "GET",
            "dataType": "json",
            "data": function (d) {
                // Construct parameters object
                var params = {
                    draw: d.draw,
                    searchValue: d.search.value,
                    length: d.length,
                    start: d.start,
                    ShiftId: ShiftId // Set ShiftId as needed
                    // You can include additional parameters if needed
                };
                return params;
            },
            "dataSrc": function (json) {
                // Manipulate data as needed before rendering
                return json.data;
            }
        },
        "columns": [
            { "data": "PurchaseItemId", "name": "PurchaseItemId", "visible": false },
            { "data": "PurchaseId", "name": "PurchaseId", "visible": false },
            { "data": "TankId", "name": "TankId", "visible": false },
            { "data": "TankName", "name": "TankName", "autoWidth": true },
            { "data": "ProductVariationId", "name": "ProductVariationId", "visible": false },
            { "data": "ProductVariationName", "name": "ProductVariationName", "autoWidth": true },
            { "data": "DipsBeforeOffloading", "name": "DipsBeforeOffloading", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "PurchaseQuantity", "name": "PurchaseQuantity", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "DipsAfterOffloading", "name": "DipsAfterOffloading", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "ExpectedQuantity", "name": "ExpectedQuantity", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "Gainloss", "name": "Gainloss", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "PercentGainloss", "name": "PercentGainloss", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "PurchasePrice", "name": "PurchasePrice", "autoWidth": true },
            { "data": "PurchaseTax", "name": "PurchaseTax", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "PurchaseDiscount", "name": "PurchaseDiscount", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "PurchaseGTotal", "name": "PurchaseGTotal", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "PurchaseNTotal", "name": "PurchaseNTotal", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "PurchaseTaxAmount", "name": "PurchaseTaxAmount", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            {
                "data": null,
                "render": function (data, type, row) {
                    // Show the Edit button only if ShiftStatus is not equal to 1
                    if (ShiftStatus != 1) {
                        return "<button class='btn-info btn-xs btn-outline-info editshiftwetstockpurchase text-white font-weight-bold text-uppercase m-1'>Edit</button>";
                    } else {
                        return "<button class='btn-info btn-xs btn-outline-info text-white font-weight-bold text-uppercase m-1'>Edit</button>";
                    }
                }
            }
        ]
    });
    $('#ShiftWetStockPurchaseTable').on('click', '.editshiftwetstockpurchase', function () {
        clearWetStockForm();
        var rowData = shiftWetStockPurchaseDataTable.row($(this).parents('tr')).data(); // Get the data of the row containing the clicked button
        Getwetsupplierproductprice(rowData.ProductVariationId);
        $('#SystemWetPurchaseItemId').val(rowData.PurchaseItemId);
        $('#SystemWetPurchaseId').val(rowData.PurchaseId);
        $('#SystemWetTankId').val(rowData.TankId);
        $('#SystemWetProductVariationId').val(rowData.ProductVariationId);
        $('#DipsBeforeOffloadingId').val(rowData.DipsBeforeOffloading);
        $('#SystemWetproductquantityId').val(rowData.PurchaseQuantity);
        $('#DipsAfterOffloadingId').val(rowData.DipsAfterOffloading);
        $('#DipsExpectedAfterOffloadId').val(rowData.ExpectedQuantity);
        $('#GainLossAfterOffloadId').val(rowData.Gainloss);
        $('#PercentageGainLossAfterOffloadId').val(rowData.PercentGainloss);
        $('#SystemWetproductpriceId').val(rowData.PurchasePrice);
        $('#SystemWetproducttaxId').val(rowData.PurchaseTax);
        $('#SystemWetproductDiscountId').val(rowData.PurchaseDiscount);
        $('#SystemWetPurchaseGTotalAmountId').val(rowData.PurchaseGTotal);
        $('#SystemWetPurchaseNTotalAmountId').val(rowData.PurchaseNTotal);
        $('#SystemWetPurchaseTaxAmountId').val(rowData.PurchaseTaxAmount);
    });

    var shiftDryStockPurchaseDataTable;
    // Initialize DataTable
    shiftDryStockPurchaseDataTable = $('#ShiftDryStockPurchaseTable').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/StationManagement/Getsystemstationshiftdrystockpurchasedata",
            "type": "GET",
            "dataType": "json",
            "data": function (d) {
                // Construct parameters object
                var params = {
                    draw: d.draw,
                    searchValue: d.search.value,
                    length: d.length,
                    start: d.start,
                    ShiftId: ShiftId // Set ShiftId as needed
                    // You can include additional parameters if needed
                };
                return params;
            },
            "dataSrc": function (json) {
                // Manipulate data as needed before rendering
                return json.data;
            }
        },
        "columns": [
            { "data": "PurchaseItemId", "name": "PurchaseItemId", "visible": false },
            { "data": "PurchaseId", "name": "PurchaseId", "visible": false },
            { "data": "ProductVariationId", "name": "ProductVariationId", "visible": false },
            { "data": "ProductVariationName", "name": "ProductVariationName", "autoWidth": true },
            { "data": "DipsBeforeOffloading", "name": "DipsBeforeOffloading", "visible": false, "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "PurchaseQuantity", "name": "PurchaseQuantity", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "DipsAfterOffloading", "name": "DipsAfterOffloading", "visible": false, "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "ExpectedQuantity", "name": "ExpectedQuantity", "visible": false, "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "Gainloss", "name": "Gainloss", "visible": false, "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "PercentGainloss", "name": "PercentGainloss", "visible": false, "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "PurchasePrice", "name": "PurchasePrice", "autoWidth": true },
            { "data": "PurchaseTax", "name": "PurchaseTax", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "PurchaseDiscount", "name": "PurchaseDiscount", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "PurchaseGTotal", "name": "PurchaseGTotal", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "PurchaseNTotal", "name": "PurchaseNTotal", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "PurchaseTaxAmount", "name": "PurchaseTaxAmount", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            {
                "data": null,
                "render": function (data, type, row) {
                    // Show the Edit button only if ShiftStatus is not equal to 1
                    if (ShiftStatus != 1) {
                        return "<button class='btn-info btn-xs btn-outline-info editshiftdrystockpurchase text-white font-weight-bold text-uppercase m-1'>Edit</button>";
                    } else {
                        return "<button class='btn-info btn-xs btn-outline-info text-white font-weight-bold text-uppercase m-1'>Edit</button>";
                    }
                }
            }
        ]
    });
    $('#ShiftDryStockPurchaseTable').on('click', '.editshiftdrystockpurchase', function () {
        clearDryStockForm();
        var rowData = shiftDryStockPurchaseDataTable.row($(this).parents('tr')).data(); // Get the data of the row containing the clicked button
        Getdrysupplierproductprice(rowData.ProductVariationId);
        $('#SystemDryPurchaseItemId').val(rowData.PurchaseItemId);
        $('#SystemItemDryPurchaseId').val(rowData.PurchaseId);
        $('#SystemDryProductVariationId').val(rowData.ProductVariationId);
        $('#SystemDryDipsBeforeOffloadingId').val(rowData.DipsBeforeOffloading);
        $('#SystemDryproductquantityId').val(rowData.PurchaseQuantity);
        $('#SystemDryDipsAfterOffloadingId').val(rowData.DipsAfterOffloading);
        $('#SystemDryExpectedQuantityId').val(rowData.ExpectedQuantity);
        $('#SystemDryGainLossId').val(rowData.Gainloss);
        $('#SystemDryPercentageGainLossId').val(rowData.PercentGainloss);
        $('#SystemDryproductpriceId').val(rowData.PurchasePrice);
        $('#SystemDryproducttaxId').val(rowData.PurchaseTax);
        $('#SystemDryproductDiscountId').val(rowData.PurchaseDiscount);
        $('#SystemDryPurchaseGTotalAmountId').val(rowData.PurchaseGTotal);
        $('#SystemDryPurchaseNTotalAmountId').val(rowData.PurchaseNTotal);
        $('#SystemDryPurchaseTaxAmountId').val(rowData.PurchaseTaxAmount);
    });

    var shiftExpenseDataTable;
    // Initialize DataTable
    shiftExpenseDataTable = $('#ShiftExpensesTable').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/StationManagement/GetSystemStationShiftExpenseData",
            "type": "GET",
            "dataType": "json",
            "data": function (d) {
                // Construct parameters object
                var params = {
                    draw: d.draw,
                    searchValue: d.search.value,
                    length: d.length,
                    start: d.start,
                    ShiftId: ShiftId // Set ShiftId as needed
                    // You can include additional parameters if needed
                };
                return params;
            },
            "dataSrc": function (json) {
                // Manipulate data as needed before rendering
                return json.data;
            }
        },
        "columns": [
            { "data": "ShiftVoucherId", "name": "ShiftVoucherId", "visible": false },
            { "data": "ShiftId", "name": "ShiftId", "visible": false },
            { "data": "AttendantId", "name": "AttendantId", "visible": false },
            { "data": "AttendantName", "name": "AttendantName", "autoWidth": true },
            { "data": "VoucherModeId", "name": "VoucherModeId", "visible": false },
            { "data": "VoucherModeName", "name": "VoucherModeName", "autoWidth": true },
            { "data": "VoucherName", "name": "VoucherName", "visible": true },
            { "data": "VoucherAmount", "name": "VoucherAmount", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            {
                "data": null,
                "render": function (data, type, row) {
                    // Show the Edit button only if ShiftStatus is not equal to 1
                    if (ShiftStatus != 1) {
                        return "<button class='btn-info btn-xs btn-outline-info editshiftexpense text-white font-weight-bold text-uppercase m-1'>Edit</button>";
                    } else {
                        return "<button class='btn-info btn-xs btn-outline-info text-white font-weight-bold text-uppercase m-1'>Edit</button>";
                    }
                }
            }
        ]
    });
    $('#ShiftExpensesTable').on('click', '.editshiftexpense', function () {
        clearExpensesForm();
        var rowData = shiftExpenseDataTable.row($(this).parents('tr')).data(); // Get the data of the row containing the clicked button
        $('#ExpensesCollectionVoucherId').val(rowData.ShiftVoucherId);
        $('#ExpensesCollectionShiftId').val(rowData.ShiftId);
        $('#ExpensesCollectionCashierId').val(rowData.AttendantId);
        $('#ExpensesCollectionModeId').val(rowData.VoucherModeId);
        $('#ExpensesCollectionTypeNameId').val(rowData.VoucherName);
        $('#ExpensesCollectionAmountId').val(rowData.VoucherAmount);

    });


    var shiftMpesaCollectionDataTable;
    // Initialize DataTable
    shiftMpesaCollectionDataTable = $('#ShiftMpesaCollectionTable').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/StationManagement/GetSystemStationShiftMpesaCollectionData",
            "type": "GET",
            "dataType": "json",
            "data": function (d) {
                // Construct parameters object
                var params = {
                    draw: d.draw,
                    searchValue: d.search.value,
                    length: d.length,
                    start: d.start,
                    ShiftId: ShiftId // Set ShiftId as needed
                    // You can include additional parameters if needed
                };
                return params;
            },
            "dataSrc": function (json) {
                // Manipulate data as needed before rendering
                return json.data;
            }
        },
        "columns": [
            { "data": "ShiftVoucherId", "name": "ShiftVoucherId", "visible": false },
            { "data": "ShiftId", "name": "ShiftId", "visible": false },
            { "data": "AttendantId", "name": "AttendantId", "visible": false },
            { "data": "AttendantName", "name": "AttendantName", "autoWidth": true },
            { "data": "VoucherModeId", "name": "VoucherModeId", "visible": false },
            { "data": "VoucherModeName", "name": "VoucherModeName", "autoWidth": true },
            { "data": "ProductVariationId", "name": "ProductVariationId", "visible": false },
            { "data": "ProductVariationName", "name": "ProductVariationName", "autoWidth": true },
            { "data": "ProductPrice", "name": "ProductPrice", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "ProductQuantity", "name": "ProductQuantity", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "ProductDiscount", "name": "ProductDiscount", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "VoucherAmount", "name": "VoucherAmount", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "VatAmount", "name": "VatAmount", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            {
                "data": null,
                "render": function (data, type, row) {
                    // Show the Edit button only if ShiftStatus is not equal to 1
                    if (ShiftStatus != 1) {
                        return "<button class='btn-info btn-xs btn-outline-info editshiftmpesacollection text-white font-weight-bold text-uppercase m-1'>Edit</button>";
                    } else {
                        return "<button class='btn-info btn-xs btn-outline-info text-white font-weight-bold text-uppercase m-1'>Edit</button>";
                    }
                }
            }
        ]
    });
    $('#ShiftMpesaCollectionTable').on('click', '.editshiftmpesacollection', function () {
        clearMpesaForm();
        var rowData = shiftMpesaCollectionDataTable.row($(this).parents('tr')).data(); // Get the data of the row containing the clicked button
        Getmpesaproductpricedata(rowData.ProductVariationId);
        $('#MpesaCollectionVoucherId').val(rowData.ShiftVoucherId);
        $('#MpesaCollectionShiftId').val(rowData.ShiftId);
        $('#MpesaCollectionCashierId').val(rowData.AttendantId);
        $('#MpesaCollectionModeId').val(rowData.VoucherModeId);
        $('#MpesaProductVariationId').val(rowData.ProductVariationId);
        $('#MpesaCollectionPriceId').val(rowData.ProductPrice);
        $('#MpesaCollectionQuantityId').val(rowData.ProductQuantity);
        $('#MpesaCollectionDiscountId').val(rowData.ProductDiscount);
        $('#MpesaCollectionAmountId').val(rowData.VoucherAmount);
        $('#MpesaCollectionVatAmountId').val(rowData.VatAmount);
    });

    var shiftFuelCardCollectionDataTable;
    // Initialize DataTable
    shiftFuelCardCollectionDataTable = $('#ShiftFuelCardCollectionTable').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/StationManagement/Getsystemstationshiftfuelcarddata",
            "type": "GET",
            "dataType": "json",
            "data": function (d) {
                // Construct parameters object
                var params = {
                    draw: d.draw,
                    searchValue: d.search.value,
                    length: d.length,
                    start: d.start,
                    ShiftId: ShiftId // Set ShiftId as needed
                    // You can include additional parameters if needed
                };
                return params;
            },
            "dataSrc": function (json) {
                // Manipulate data as needed before rendering
                return json.data;
            }
        },
        "columns": [
            { "data": "ShiftVoucherId", "name": "ShiftVoucherId", "visible": false },
            { "data": "ShiftId", "name": "ShiftId", "visible": false },
            { "data": "AttendantId", "name": "AttendantId", "visible": false },
            { "data": "AttendantName", "name": "AttendantName", "autoWidth": true },
            { "data": "VoucherModeId", "name": "VoucherModeId", "visible": false },
            { "data": "VoucherModeName", "name": "VoucherModeName", "autoWidth": true },
            { "data": "ProductVariationId", "name": "ProductVariationId", "visible": false },
            { "data": "ProductVariationName", "name": "ProductVariationName", "autoWidth": true },
            { "data": "ProductPrice", "name": "ProductPrice", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "ProductQuantity", "name": "ProductQuantity", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "ProductDiscount", "name": "ProductDiscount", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "VoucherAmount", "name": "VoucherAmount", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "VatAmount", "name": "VatAmount", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            {
                "data": null,
                "render": function (data, type, row) {
                    // Show the Edit button only if ShiftStatus is not equal to 1
                    if (ShiftStatus != 1) {
                        return "<button class='btn-info btn-xs btn-outline-info editshiftfuelcardcollection text-white font-weight-bold text-uppercase m-1'>Edit</button>";
                    } else {
                        return "<button class='btn-info btn-xs btn-outline-info text-white font-weight-bold text-uppercase m-1'>Edit</button>";
                    }
                }
            }
        ]
    });
    $('#ShiftFuelCardCollectionTable').on('click', '.editshiftfuelcardcollection', function () {
        clearMpesaForm();
        var rowData = shiftFuelCardCollectionDataTable.row($(this).parents('tr')).data(); // Get the data of the row containing the clicked button
        Getfuelcardproductpricedata(rowData.ProductVariationId);
        $('#FuelCardCollectionVoucherId').val(rowData.ShiftVoucherId);
        $('#FuelCardCollectionShiftId').val(rowData.ShiftId);
        $('#FuelCardCollectionCashierId').val(rowData.AttendantId);
        $('#FuelCardCollectionModeId').val(rowData.VoucherModeId);
        $('#SystemFuelCardProductVariationId').val(rowData.ProductVariationId);
        $('#FuelCardCollectionPriceId').val(rowData.ProductPrice);
        $('#FuelCardCollectionQuantityId').val(rowData.ProductQuantity);
        $('#FuelCardCollectionDiscountId').val(rowData.ProductDiscount);
        $('#FuelCardCollectionAmountId').val(rowData.VoucherAmount);
        $('#FuelCardCollectionVatAmountId').val(rowData.VatAmount);

    });

    var shiftMerchantCollectionDataTable;
    // Initialize DataTable
    shiftMerchantCollectionDataTable = $('#ShiftMerchantCollectionTable').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/StationManagement/Getsystemstationshiftmerchantdata",
            "type": "GET",
            "dataType": "json",
            "data": function (d) {
                // Construct parameters object
                var params = {
                    draw: d.draw,
                    searchValue: d.search.value,
                    length: d.length,
                    start: d.start,
                    ShiftId: ShiftId // Set ShiftId as needed
                    // You can include additional parameters if needed
                };
                return params;
            },
            "dataSrc": function (json) {
                // Manipulate data as needed before rendering
                return json.data;
            }
        },
        "columns": [
            { "data": "ShiftVoucherId", "name": "ShiftVoucherId", "visible": false },
            { "data": "ShiftId", "name": "ShiftId", "visible": false },
            { "data": "AttendantId", "name": "AttendantId", "visible": false },
            { "data": "AttendantName", "name": "AttendantName", "autoWidth": true },
            { "data": "VoucherModeId", "name": "VoucherModeId", "visible": false },
            { "data": "VoucherModeName", "name": "VoucherModeName", "autoWidth": true },
            { "data": "ProductVariationId", "name": "ProductVariationId", "visible": false },
            { "data": "ProductVariationName", "name": "ProductVariationName", "autoWidth": true },
            { "data": "ProductPrice", "name": "ProductPrice", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "ProductQuantity", "name": "ProductQuantity", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "ProductDiscount", "name": "ProductDiscount", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "VoucherAmount", "name": "VoucherAmount", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "VatAmount", "name": "VatAmount", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "RecieptNumber", "name": "RecieptNumber", "visible": true, "autoWidth": true },
            { "data": "CardNumber", "name": "CardNumber", "autoWidth": true, "autoWidth": true },
            {
                "data": null,
                "render": function (data, type, row) {
                    // Show the Edit button only if ShiftStatus is not equal to 1
                    if (ShiftStatus != 1) {
                        return "<button class='btn-info btn-xs btn-outline-info editshiftmerchantcollection text-white font-weight-bold text-uppercase m-1'>Edit</button>";
                    } else {
                        return "<button class='btn-info btn-xs btn-outline-info text-white font-weight-bold text-uppercase m-1'>Edit</button>";
                    }
                }
            }
        ]
    });
    $('#ShiftMerchantCollectionTable').on('click', '.editshiftmerchantcollection', function () {
        clearMerchantForm();
        var rowData = shiftMerchantCollectionDataTable.row($(this).parents('tr')).data(); // Get the data of the row containing the clicked button
        Getmerchantproductpricedata(rowData.ProductVariationId);
        $('#MerchantCollectionVoucherId').val(rowData.ShiftVoucherId);
        $('#MerchantCollectionShiftId').val(rowData.ShiftId);
        $('#MerchantCollectionCashierId').val(rowData.AttendantId);
        $('#MerchantCollectionModeId').val(rowData.VoucherModeId);
        $('#SystemMerchantProductVariationId').val(rowData.ProductVariationId);
        $('#MerchantCollectionPriceId').val(rowData.ProductPrice);
        $('#MerchantCollectionQuantityId').val(rowData.ProductQuantity);
        $('#MerchantCollectionDiscountId').val(rowData.ProductDiscount);
        $('#MerchantCollectionAmountId').val(rowData.VoucherAmount);
        $('#MerchantCollectionVatAmountId').val(rowData.VatAmount);
        $('#MerchantCollectionCardNumberId').val(rowData.RecieptNumber);
        $('#MerchantCollectionReceiptNumberId').val(rowData.CardNumber);
    });

    var shiftTopupDataTable;
    // Initialize DataTable
    shiftTopupDataTable = $('#ShiftTopupTable').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/StationManagement/Getsystemstationshifttopupdata",
            "type": "GET",
            "dataType": "json",
            "data": function (d) {
                // Construct parameters object
                var params = {
                    draw: d.draw,
                    searchValue: d.search.value,
                    length: d.length,
                    start: d.start,
                    ShiftId: ShiftId // Set ShiftId as needed
                    // You can include additional parameters if needed
                };
                return params;
            },
            "dataSrc": function (json) {
                // Manipulate data as needed before rendering
                return json.data;
            }
        },
        "columns": [
            { "data": "ShiftTopupId", "name": "ShiftTopupId", "visible": false },
            { "data": "ShiftId", "name": "ShiftId", "visible": false },
            { "data": "AttendantId", "name": "AttendantId", "visible": false },
            { "data": "AttendantName", "name": "AttendantName", "autoWidth": true },
            { "data": "TopupAmount", "name": "TopupAmount", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "TopupReference", "name": "TopupReference", "autoWidth": true, "autoWidth": true },
            {
                "data": null,
                "render": function (data, type, row) {
                    // Show the Edit button only if ShiftStatus is not equal to 1
                    if (ShiftStatus != 1) {
                        return "<button class='btn-info btn-xs btn-outline-info editshifttopup text-white font-weight-bold text-uppercase m-1'>Edit</button>";
                    } else {
                        return "<button class='btn-info btn-xs btn-outline-info text-white font-weight-bold text-uppercase m-1'>Edit</button>";
                    }
                }
            }
        ]
    });
    $('#ShiftTopupTable').on('click', '.editshifttopup', function () {
        clearShiftTopupForm();
        var rowData = shiftTopupDataTable.row($(this).parents('tr')).data(); // Get the data of the row containing the clicked button
        $('#shiftTopupId').val(rowData.ShiftTopupId);
        $('#SystemstationShiftId').val(rowData.ShiftId);
        $('#ShiftTopupCashierId').val(rowData.AttendantId);
        $('#ShiftTopupAmountId').val(rowData.TopupAmount);
        $('#ShiftTopupReferenceId').val(rowData.TopupReference);
    });

    var shiftPaymentDataTable;
    // Initialize DataTable
    shiftPaymentDataTable = $('#ShiftPaymentTable').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/StationManagement/Getsystemstationshiftpaymentdata",
            "type": "GET",
            "dataType": "json",
            "data": function (d) {
                // Construct parameters object
                var params = {
                    draw: d.draw,
                    searchValue: d.search.value,
                    length: d.length,
                    start: d.start,
                    ShiftId: ShiftId // Set ShiftId as needed
                    // You can include additional parameters if needed
                };
                return params;
            },
            "dataSrc": function (json) {
                // Manipulate data as needed before rendering
                return json.data;
            }
        },
        "columns": [
            { "data": "ShiftPaymentId", "name": "ShiftPaymentId", "visible": false },
            { "data": "ShiftId", "name": "ShiftId", "visible": false },
            { "data": "CustomerId", "name": "CustomerId", "visible": false },
            { "data": "CustomerName", "name": "CustomerName", "autoWidth": true },
            { "data": "AttendantId", "name": "AttendantId", "visible": false },
            { "data": "AttendantName", "name": "AttendantName", "autoWidth": true },
            { "data": "PaymentModeId", "name": "PaymentModeId", "visible": false },
            { "data": "PaymentMode", "name": "PaymentMode", "autoWidth": true },
            { "data": "PaymentAmount", "name": "PaymentAmount", "autoWidth": true, "render": $.fn.dataTable.render.number(',', '.', 2) },
            { "data": "PaymentReference", "name": "PaymentReference", "autoWidth": true, "autoWidth": true },
            {
                "data": null,
                "render": function (data, type, row) {
                    // Show the Edit button only if ShiftStatus is not equal to 1
                    if (ShiftStatus != 1) {
                        return "<button class='btn-info btn-xs btn-outline-info editshiftpayment text-white font-weight-bold text-uppercase m-1'>Edit</button>";
                    } else {
                        return "<button class='btn-info btn-xs btn-outline-info text-white font-weight-bold text-uppercase m-1'>Edit</button>";
                    }
                }
            }
        ]
    });
    $('#ShiftPaymentTable').on('click', '.editshiftpayment', function () {
        clearShiftPaymentForm();
        var rowData = shiftPaymentDataTable.row($(this).parents('tr')).data(); // Get the data of the row containing the clicked button
        $('#ShiftPaymentId').val(rowData.ShiftPaymentId);
        $('#SystemstationShiftId').val(rowData.ShiftId);
        $('#ShiftPayementCustomerId').val(rowData.CustomerId);
        $('#ShiftPaymentCashierId').val(rowData.AttendantId);
        $('#ShiftPaymentModeId').val(rowData.PaymentModeId);
        $('#ShiftPaymentAmountId').val(rowData.PaymentAmount);
        $('#ShiftPaymentReferenceId').val(rowData.PaymentReference);
    });

});
