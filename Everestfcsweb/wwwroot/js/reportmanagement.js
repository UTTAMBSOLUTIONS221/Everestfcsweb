function Generatepumpreadingreport() {
    document.getElementById("Pumpreadingreportbtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Generating...";
    if ($('#PumpreadingstartdateId').val() == '') {
        Swal.fire("Report Not Generated", 'Start Date is Required', "warning");
        document.getElementById("Pumpreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#PumpreadingenddateId').val() == '') {
        Swal.fire("Report Not Generated", 'End Date is Required', "warning");
        document.getElementById("Pumpreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#PumpreadingStationId').val() == '') {
        Swal.fire("Report Not Generated", 'Station is Required', "warning");
        document.getElementById("Pumpreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#PumpreadingAttendantId').val() == '') {
        Swal.fire("Report Not Generated", 'Attendant is Required', "warning");
        document.getElementById("Pumpreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#PumpreadingStationShiftId').val() == '') {
        Swal.fire("Report Not Generated", 'Shift is Required', "warning");
        document.getElementById("Pumpreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    var uil1 = {
        TenantId: $('#systemLoggedinedTenantid').val(),
        Startdate: $('#PumpreadingstartdateId').val(),
        Enddate: $('#PumpreadingenddateId').val(),
        Station: $('#PumpreadingStationId').val(),
        Attendant: $('#PumpreadingAttendantId').val(),
        Shift: $('#PumpreadingStationShiftId').val()
    }

    // Send a POST request to generate the report
    $.post("/ReportManagement/Generateshiftpumpreadingdata", uil1).done(function (response) {
        var docDefinition = {
            pageOrientation: 'landscape', // Set the page orientation to landscape
            content: [
                { text: "SHIFT PUMP READING REPORT", style: 'header' },
                { text: ' ' },
                { text: 'Customer and Date Information', style: 'subheader' },
                // Include customer and date information table here...
                {
                    style: 'tableExample',
                    table: {
                        widths: ['*', '*'],
                        body: [
                            ['Station:', { text: response.StationName || 'N/A', noWrap: true }],
                            ['Shift:', { text: response.ShiftCode || 'N/A', noWrap: true }],
                            ['Attendant:', { text: response.AttendantName || 'N/A', noWrap: true }],
                            ['Start Date:', { text: formatDate(response.StartDate), noWrap: true }],
                            ['End Date:', { text: formatDate(response.EndDate), noWrap: true }]
                        ]
                    }
                }
            ],
            styles: {
                header: {
                    fontSize: 9,
                    bold: true,
                    alignment: 'center'
                },
                subheader: {
                    fontSize: 9,
                    bold: true,
                    margin: [0, 15, 0, 0]
                },
                tableExample: {
                    margin: [0, 5, 0, 15],
                    fontSize: 9 // Reduced font size for the table
                },
                summaryFooter: {
                    bold: true,
                    margin: [0, 10, 0, 0]
                }
            }
        };

        // Check if data is available
        if (!response.ShiftPumpReading || response.ShiftPumpReading.length === 0) {
            // If no data available, add a message
            docDefinition.content.push({ text: "No data available", style: 'noData' });
        } else {
            // If data available, include pump details table
            var tableBody = [
                ["Shift Date", "Pump", "Price", "Opening Litres", "Closing Litres", "Litres Sold", "Litres Sales", "Opening Meter", "Closing Meter", "Amount", "Difference", "Cashier"]
            ];

            // Initialize variables to store totals
            var totalLitresSold = 0;
            var totalLitresSales = 0;
            var totalAmount = 0;
            var totalDifference = 0;

            for (var i = 0; i < response.ShiftPumpReading.length; i++) {
                var report = response.ShiftPumpReading[i];

                // Calculate totals
                totalLitresSold += parseFloat(report.ElectronicSold) || 0;
                totalLitresSales += parseFloat(report.ElectronicAmount) || 0;
                totalAmount += parseFloat(report.TotalShilling) || 0;
                totalDifference += parseFloat(report.ShillingDifference) || 0;

                var rowData = [
                    formatDate(report.Datecreated) || 'N/A',
                    report.WareHouse || 'N/A',
                    report.ProductPrice || 'N/A',
                    report.OpeningElectronic || '0',
                    report.ClosingElectronic || '0',
                    report.ElectronicSold || '0',
                    report.ElectronicAmount || '0',
                    report.OpeningShilling || '0',
                    report.ClosingShilling || '0',
                    report.TotalShilling || '0',
                    report.ShillingDifference || '0',
                    report.AttendantName || 'N/A',
                ];
                tableBody.push(rowData);
            }

            // Add totals row
            var totalsRow = [
                'Total:',
                '', // Leave empty for pump column
                '', // Leave empty for price column
                '', // Leave empty for opening litres column
                '', // Leave empty for closing litres column
                totalLitresSold.toFixed(2), // Total litres sold
                totalLitresSales.toFixed(2), // Total litres sales
                '', // Leave empty for opening meter column
                '', // Leave empty for closing meter column
                totalAmount.toFixed(2), // Total amount
                totalDifference.toFixed(2), // Total difference
                '' // Leave empty for cashier column
            ];
            tableBody.push(totalsRow);

            docDefinition.content.push(
                { text: 'Pump Details', style: 'subheader' },
                {
                    style: 'tableExample',
                    table: {
                        headerRows: 1,
                        widths: new Array(tableBody[0].length).fill('auto'),
                        body: tableBody
                    }
                }
            );
        }
        // Generate a unique code with minute only
        var timestamp = new Date().toISOString().slice(-14, -8); // Extracting minute component

        // Generate and download the PDF with unique code
        pdfMake.createPdf(docDefinition).download('ShiftPumpReadingReport' + timestamp + '.pdf');
        document.getElementById("Pumpreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        $('#FuelcardsystemModal').hide();
        setTimeout(function () { location.reload(); }, 1000);

    }).fail(function (xhr, status, error) {
        // Show an error message if the request fails
        Swal.fire("Report Not Generated", xhr.responseText, "error");
    }).always(enableButton);
}

function Generatetankreadingreport() {
    document.getElementById("Tankreadingreportbtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Generating...";
    if ($('#TankreadingstartdateId').val() == '') {
        Swal.fire("Report Not Generated", 'Start Date is Required', "warning");
        document.getElementById("Tankreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#TankreadingenddateId').val() == '') {
        Swal.fire("Report Not Generated", 'End Date is Required', "warning");
        document.getElementById("Tankreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#TankreadingStationId').val() == '') {
        Swal.fire("Report Not Generated", 'Station is Required', "warning");
        document.getElementById("Tankreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#TankreadingAttendantId').val() == '') {
        Swal.fire("Report Not Generated", 'Attendant is Required', "warning");
        document.getElementById("Tankreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#TankreadingStationShiftId').val() == '') {
        Swal.fire("Report Not Generated", 'Shift is Required', "warning");
        document.getElementById("Tankreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    var requestData = {
        TenantId: $('#systemLoggedinedTenantid').val(),
        StartDate: $('#TankreadingstartdateId').val(),
        EndDate: $('#TankreadingenddateId').val(),
        Station: $('#TankreadingStationId').val(),
        Attendant: $('#TankreadingAttendantId').val(),
        Shift: $('#TankreadingStationShiftId').val()
    }

    // Send a POST request to generate the report
    $.post("/ReportManagement/GenerateShiftTankReadingData", requestData).done(function (response) {
        var docDefinition = {
            pageOrientation: 'landscape',
            content: [
                { text: "SHIFT TANK READING REPORT", style: 'header' },
                { text: ' ' },
                { text: 'Customer and Date Information', style: 'subheader' },
                {
                    style: 'tableExample',
                    table: {
                        widths: ['*', '*'],
                        body: [
                            ['Station:', { text: response.StationName || 'N/A', noWrap: true }],
                            ['Shift:', { text: response.ShiftCode || 'N/A', noWrap: true }],
                            ['Attendant:', { text: response.AttendantName || 'N/A', noWrap: true }],
                            ['Start Date:', { text: formatDate(response.StartDate), noWrap: true }],
                            ['End Date:', { text: formatDate(response.EndDate), noWrap: true }]
                        ]
                    }
                },
                { text: 'Tank Reading Details', style: 'subheader' },
                {
                    style: 'tableExample',
                    table: {
                        headerRows: 1,
                        widths: ['*', '*', '*', '*', '*', '*', '*', '*', '*', '*'],
                        body: [
                            ["Shift Date", "Tank", "Price", "Open", "Add", "Close", "Tank Litres", "Pump Litres", "Difference","Value"]
                        ].concat(response.ShiftTankReading.map(function (data) {
                            return [
                                formatDate(data.ShiftDateTime),
                                { text: data.TankName || 'N/A', noWrap: true },
                                { text: data.ProductPrice.toFixed(2), noWrap: true },
                                { text: data.OpeningQuantity.toFixed(2), noWrap: true },
                                { text: data.TankPurchase.toFixed(2), noWrap: true },
                                { text: data.ClosingQuantity.toFixed(2), noWrap: true },
                                { text: data.QuantitySold.toFixed(2), noWrap: true },
                                { text: data.PumpLitres.toFixed(2), noWrap: true },
                                { text: data.Differences.toFixed(2), noWrap: true },
                                { text: data.DifferenceValue.toFixed(2), noWrap: true }
                            ];
                        }))
                    }
                }
            ],
            styles: {
                header: {
                    fontSize: 9,
                    bold: true,
                    alignment: 'center'
                },
                subheader: {
                    fontSize: 9,
                    bold: true,
                    margin: [0, 15, 0, 0]
                },
                tableExample: {
                    margin: [0, 5, 0, 15],
                    fontSize: 9
                },
                summaryFooter: {
                    bold: true,
                    margin: [0, 10, 0, 0]
                }
            }
        };

        // Calculate totals
        var totalDifferences = response.ShiftTankReading.reduce((total, data) => total + data.Differences, 0);
        var totalDifferenceValue = response.ShiftTankReading.reduce((total, data) => total + data.DifferenceValue, 0);
        // Add totals row
        var totalsRow = [
            'Total:',
            '', // Leave empty for tank column
            '', // Leave empty for price column
            '', // Leave empty for opening quantity column
            '', // Leave empty for add column
            '', // Leave empty for closing quantity column
            '', // Leave empty for quantity sold column
            '', // Leave empty for pump litres column
            totalDifferences.toFixed(2), // Total differences
            totalDifferenceValue.toFixed(2), // Total difference value
        ];
        docDefinition.content[docDefinition.content.length - 1].table.body.push(totalsRow);
         // Generate a unique code with minute only
        var timestamp = new Date().toISOString().slice(-14, -8); // Extracting minute component

        // Generate and download the PDF with unique code
        pdfMake.createPdf(docDefinition).download('ShiftTankReadingReport' + timestamp + '.pdf');
        document.getElementById("Tankreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        $('#FuelcardsystemModal').hide();
        setTimeout(function () { location.reload(); }, 1000);

    }).fail(function (xhr, status, error) {
        // Show an error message if the request fails
        Swal.fire("Report Not Generated", xhr.responseText, "error");
    }).always(enableButton);
}

function calculateTotalAmount(tankSummary) {
    var total = 0;
    for (var i = 0; i < tankSummary.length; i++) {
        total += tankSummary[i].TotalTankAmount;
    }
    return total;
}


function GenerateLubelpgreadingreport() {
    document.getElementById("Lubelpgreadingreportbtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Generating...";
    if ($('#LubelpgreadingstartdateId').val() == '') {
        Swal.fire("Report Not Generated", 'Start Date is Required', "warning");
        document.getElementById("Lubelpgreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#LubelpgreadingenddateId').val() == '') {
        Swal.fire("Report Not Generated", 'End Date is Required', "warning");
        document.getElementById("Lubelpgreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#LubelpgreadingStationId').val() == '') {
        Swal.fire("Report Not Generated", 'Station is Required', "warning");
        document.getElementById("Lubelpgreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#LubelpgreadingAttendantId').val() == '') {
        Swal.fire("Report Not Generated", 'Attendant is Required', "warning");
        document.getElementById("Lubelpgreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#LubelpgreadingStationShiftId').val() == '') {
        Swal.fire("Report Not Generated", 'Shift is Required', "warning");
        document.getElementById("Lubelpgreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    var requestData = {
        TenantId: $('#systemLoggedinedTenantid').val(),
        StartDate: $('#LubelpgreadingstartdateId').val(),
        EndDate: $('#LubelpgreadingenddateId').val(),
        Station: $('#LubelpgreadingStationId').val(),
        Attendant: $('#LubelpgreadingAttendantId').val(),
        Shift: $('#LubelpgreadingStationShiftId').val()
    }

    // Send a POST request to generate the report
    $.post("/ReportManagement/Generateshiftlubelpgreadingdata", requestData).done(function (response) {
        var columnWidths = [55, 65, 65, 55, 65, 50, 45, 55, 50, 55, 55, 45]; // Adjust widths as needed

        var docDefinition = {
            pageOrientation: 'landscape',
            content: [
                { text: "SHIFT OTHERS READING REPORT", style: 'header' },
                { text: ' ' },
                { text: 'Customer and Date Information', style: 'subheader' },
                // Include customer and date information table here...
                {
                    style: 'tableExample',
                    table: {
                        widths: ['*', '*'],
                        body: [
                            ['Station:', { text: response.StationName || 'N/A', noWrap: true }],
                            ['Shift:', { text: response.ShiftCode || 'N/A', noWrap: true }],
                            ['Attendant:', { text: response.AttendantName || 'N/A', noWrap: true }],
                            ['Start Date:', { text: formatDate(response.StartDate), noWrap: true }],
                            ['End Date:', { text: formatDate(response.EndDate), noWrap: true }]
                        ]
                    }
                }
            ],

            styles: {
                header: {
                    fontSize: 9,
                    bold: true,
                    alignment: 'center'
                },
                subheader: {
                    fontSize: 9,
                    bold: true,
                    margin: [0, 15, 0, 0]
                },
                tableExample: {
                    margin: [0, 5, 0, 15],
                    fontSize: 9
                },
                summaryFooter: {
                    bold: true,
                    margin: [0, 10, 0, 0]
                }
            }
        };

        if (!response.ShiftLpgLubeReading || response.ShiftLpgLubeReading.length === 0) {
            docDefinition.content.push({ text: "No data available", style: 'noData' });
        } else {
            var tableBody = [
                ["Shift Date", "Shift Code", "Attendant", "Item Code", "Item Description", "Price", "Open", "Add", "Close", "Sold", "Amount", "Vat"]
            ];

            var totalAmount = 0;
            var totalVat = 0;

            response.ShiftLpgLubeReading.forEach(function (report) {
                var rowData = [
                    formatDate(report.ShiftDateTime) || 'N/A',
                    report.ShiftCode || 'N/A',
                    report.AttendantName || 'N/A',
                    report.ProductCode || 'N/A',
                    report.ProductVariationName || 'N/A',
                    parseFloat(report.ProductPrice || 0).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ","), // Price with 2 decimal places and thousand separator
                    parseFloat(report.OpeningUnits || 0).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ","),
                    parseFloat(report.AddedUnits || 0).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ","),
                    parseFloat(report.ClosingUnits || 0).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ","),
                    parseFloat(report.UnitsSold || 0).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ","),
                    parseFloat(report.UnitsTotal || 0).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ","),
                    parseFloat(report.TotalVat || 0).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",")
                ];
                tableBody.push(rowData);

                // Sum up Amount and Vat
                totalAmount += parseFloat(report.UnitsTotal) || 0;
                totalVat += parseFloat(report.TotalVat) || 0;
            });

            // Add totals row
            var totalsRow = [
                "Totals", "", "", "", "", "", "", "", "", "", parseFloat(totalAmount).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ","), parseFloat(totalVat).toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",")
            ];
            tableBody.push(totalsRow);

            docDefinition.content.push(
                { text: 'Shift LPG Lube Reading Data', style: 'subheader' },
                {
                    style: 'tableExample',
                    table: {
                        headerRows: 1,
                        widths: columnWidths,
                        body: tableBody
                    }
                }
            );
        }


        // Generate a unique code with minute only
        var timestamp = new Date().toISOString().slice(-14, -8); // Extracting minute component

        // Generate and download the PDF with unique code
        pdfMake.createPdf(docDefinition).download('ShiftLpgLubeReadingReport' + timestamp + '.pdf');
        document.getElementById("Lubelpgreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        $('#FuelcardsystemModal').hide();
        setTimeout(function () { location.reload(); }, 1000);
    }).fail(function (xhr, status, error) {
        // Show an error message if the request fails
        Swal.fire("Report Not Generated", xhr.responseText, "error");
    }).always(enableButton);
}

function Generateexpensesreadingreport() {
    document.getElementById("Expensesreadingreportbtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Generating...";

    if ($('#ExpensesreadingstartdateId').val() == '') {
        Swal.fire("Report Not Generated", 'Start Date is Required', "warning");
        document.getElementById("Expensesreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#ExpensesreadingenddateId').val() == '') {
        Swal.fire("Report Not Generated", 'End Date is Required', "warning");
        document.getElementById("Expensesreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#ExpensesreadingStationId').val() == '') {
        Swal.fire("Report Not Generated", 'Station is Required', "warning");
        document.getElementById("Expensesreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#ExpensesreadingAttendantId').val() == '') {
        Swal.fire("Report Not Generated", 'Attendant is Required', "warning");
        document.getElementById("Expensesreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#ExpensesreadingStationShiftId').val() == '') {
        Swal.fire("Report Not Generated", 'Shift is Required', "warning");
        document.getElementById("Expensesreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }

    var requestData = {
        TenantId: $('#systemLoggedinedTenantid').val(),
        StartDate: $('#ExpensesreadingstartdateId').val(),
        EndDate: $('#ExpensesreadingenddateId').val(),
        Station: $('#ExpensesreadingStationId').val(),
        Attendant: $('#ExpensesreadingAttendantId').val(),
        Shift: $('#ExpensesreadingStationShiftId').val()
    };

    // Send a POST request to generate the report
    $.post("/ReportManagement/Generateshiftexpensesreadingdata", requestData).done(function (response) {
        var docDefinition = {
            content: [
                { text: "SHIFT EXPENSES REPORT", style: 'header' },
                { text: ' ' },
                { text: 'Station and Date Information', style: 'subheader' },
                // Include station and date information table here...
                {
                    style: 'tableExample',
                    table: {
                        widths: [80, '*'],
                        body: [
                            ['Station:', { text: response.StationName || 'N/A', noWrap: true }],
                            ['Shift:', { text: response.ShiftCode || 'N/A', noWrap: true }],
                            ['Attendant:', { text: response.AttendantName || 'N/A', noWrap: true }],
                            ['Start Date:', { text: formatDate(response.StartDate), noWrap: true }],
                            ['End Date:', { text: formatDate(response.EndDate), noWrap: true }]
                        ]
                    }
                }
            ],
            styles: {
                header: {
                    fontSize: 9,
                    bold: true,
                    alignment: 'center'
                },
                subheader: {
                    fontSize: 9,
                    bold: true,
                    margin: [0, 15, 0, 0]
                },
                tableExample: {
                    margin: [0, 5, 0, 15],
                    fontSize: 8 // Reduced font size for the table
                }
            }
        };

        // Check if data is available
        if (!response.ShiftExpenses || response.ShiftExpenses.length === 0) {
            // If no data available, add a message
            docDefinition.content.push({ text: "No data available", style: 'noData' });
        } else {
            // If data available, include ShiftExpensesData table
            var tableBody = [
                ["Shift Date", "Shift Code", "Attendant", "Expense Name", "Expense Amount"]
            ];

            for (var i = 0; i < response.ShiftExpenses.length; i++) {
                var expense = response.ShiftExpenses[i];
                var rowData = [
                    formatDate(expense.ShiftDateTime) || 'N/A',
                    expense.ShiftCode || 'N/A',
                    expense.AttendantName || 'N/A',
                    expense.VoucherName || 'N/A',
                    expense.VoucherAmount || 'N/A',
                ];
                tableBody.push(rowData);
            }

            // Calculate column widths dynamically
            var colWidths = new Array(tableBody[0].length).fill('*');

            docDefinition.content.push(
                { text: 'Shift Expenses Data', style: 'subheader' },
                // Include ShiftExpensesData table here...
                {
                    style: 'tableExample',
                    table: {
                        headerRows: 1,
                        widths: colWidths,
                        body: tableBody
                    }
                }
            );
        }

        // Generate a unique code with minute only
        var timestamp = new Date().toISOString().slice(-14, -8); // Extracting minute component

        // Generate and download the PDF with unique code
        pdfMake.createPdf(docDefinition).download('ShiftExpensesReport' + timestamp + '.pdf');
        document.getElementById("Expensesreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        $('#FuelcardsystemModal').hide();
        setTimeout(function () { location.reload(); }, 1000);

    }).fail(function (xhr, status, error) {
        // Show an error message if the request fails
        Swal.fire("Report Not Generated", xhr.responseText, "error");
    }).always(enableButton);
}

// Helper function to format numbers with thousand separators
function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}


function Generatecreditsalereadingreport() {
    document.getElementById("Creditsalereadingreportbtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Generating...";

    if ($('#CreditsalereadingstartdateId').val() == '') {
        Swal.fire("Report Not Generated", 'Start Date is Required', "warning");
        document.getElementById("Creditsalereadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#CreditsalereadingenddateId').val() == '') {
        Swal.fire("Report Not Generated", 'End Date is Required', "warning");
        document.getElementById("Creditsalereadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#CreditsalereadingStationId').val() == '') {
        Swal.fire("Report Not Generated", 'Station is Required', "warning");
        document.getElementById("Creditsalereadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#CreditsalereadingAttendantId').val() == '') {
        Swal.fire("Report Not Generated", 'Attendant is Required', "warning");
        document.getElementById("Creditsalereadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#CreditsalereadingStationShiftId').val() == '') {
        Swal.fire("Report Not Generated", 'Shift is Required', "warning");
        document.getElementById("Creditsalereadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }

    var requestData = {
        TenantId: $('#systemLoggedinedTenantid').val(),
        StartDate: $('#CreditsalereadingstartdateId').val(),
        EndDate: $('#CreditsalereadingenddateId').val(),
        Station: $('#CreditsalereadingStationId').val(),
        Attendant: $('#CreditsalereadingAttendantId').val(),
        Shift: $('#CreditsalereadingStationShiftId').val()
    };

    // Send a POST request to generate the report
    $.post("/ReportManagement/Generateshiftcreditsalereadingdata", requestData).done(function (response) {
        var docDefinition = {
            pageOrientation: 'landscape', // Set the page orientation to landscape
            content: [
                { text: "SHIFT CREDIT SALES REPORT", style: 'header' },
                { text: ' ' },
                { text: 'Station and Date Information', style: 'subheader' },
                // Include station and date information table here...
                {
                    style: 'tableExample',
                    table: {
                        widths: [80, '*'],
                        body: [
                            ['Station:', { text: response.StationName || 'N/A', noWrap: true }],
                            ['Shift:', { text: response.ShiftCode || 'N/A', noWrap: true }],
                            ['Attendant:', { text: response.AttendantName || 'N/A', noWrap: true }],
                            ['Start Date:', { text: formatDate(response.StartDate), noWrap: true }],
                            ['End Date:', { text: formatDate(response.EndDate), noWrap: true }]
                        ]
                    }
                }
            ],
            styles: {
                header: {
                    fontSize: 9,
                    bold: true,
                    alignment: 'center'
                },
                subheader: {
                    fontSize: 9,
                    bold: true,
                    margin: [0, 15, 0, 0]
                },
                tableExample: {
                    margin: [0, 5, 0, 15],
                    fontSize: 8 // Reduced font size for the table
                }
            }
        };

        // Check if data is available
        if (!response.ShiftCreditsales || response.ShiftCreditsales.length === 0) {
            // If no data available, add a message
            docDefinition.content.push({ text: "No data available", style: 'noData' });
        } else {
            // If data available, include ShiftCreditsaleData table
            var tableBody = [
                ["Shift Date", "Shift", "Attendant", "Customer", "Equipment", "Item Code", "Item Name", "Units", "Price", "Discount", "Total", "VAT Amount", "Receipt #.", "Order #.", "Reference"]
            ];

            var totalProductTotal = 0;
            var totalVATTotal = 0;

            for (var i = 0; i < response.ShiftCreditsales.length; i++) {
                var sale = response.ShiftCreditsales[i];
                var productTotal = parseFloat(sale.ProductTotal) || 0;
                var vatTotal = parseFloat(sale.VatTotal) || 0;

                totalProductTotal += productTotal;
                totalVATTotal += vatTotal;

                // Inside the loop where you populate the table data:
                var rowData = [
                    formatDate(sale.ShiftDateTime) || 'N/A',
                    sale.ShiftCode || 'N/A',
                    sale.AttendantName || 'N/A',
                    sale.CustomerName || 'N/A',
                    sale.EquipmentNo || 'N/A',
                    sale.ProductCode || 'N/A',
                    sale.ProductVariationName || 'N/A',
                    numberWithCommas(sale.ProductUnits) || '0',
                    numberWithCommas(sale.ProductPrice) || '0',
                    numberWithCommas(sale.ProductDiscount) || '0',
                    numberWithCommas(productTotal.toFixed(2)),
                    numberWithCommas(vatTotal.toFixed(2)),
                    sale.RecieptNumber || 'N/A',
                    sale.OrderNumber || 'N/A',
                    sale.Reference || 'N/A',
                ];
                tableBody.push(rowData);
            }

            // In the totals row:
            var totalsRow = [
                'Total:',
                '', // Leave empty for shift column
                '', // Leave empty for attendant column
                '', // Leave empty for customer name column
                '', // Leave empty for equipment no column
                '', // Leave empty for product name column
                '', // Leave empty for product name column
                '', // Leave empty for product units column
                '', // Leave empty for product price column
                '', // Leave empty for product discount column
                numberWithCommas(totalProductTotal.toFixed(2)),
                numberWithCommas(totalVATTotal.toFixed(2)),
                '', // Leave empty for reference column
                '', // Leave empty for reference column
                '' // Leave empty for reference column
            ];
            tableBody.push(totalsRow);

            docDefinition.content.push(
                { text: 'Shift Credit Sales Data', style: 'subheader' },
                // Include ShiftCreditsaleData table here...
                {
                    style: 'tableExample',
                    table: {
                        headerRows: 1,
                        widths: new Array(tableBody[0].length).fill('auto'),
                        body: tableBody
                    }
                }
            );
        }

        // Generate a unique code with minute only
        var timestamp = new Date().toISOString().slice(-14, -8); // Extracting minute component

        // Generate and download the PDF with unique code
        pdfMake.createPdf(docDefinition).download('ShiftCreditSalesReport' + timestamp + '.pdf');
        document.getElementById("Creditsalereadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        $('#FuelcardsystemModal').hide();
        setTimeout(function () { location.reload(); }, 1000);

    }).fail(function (xhr, status, error) {
        // Show an error message if the request fails
        Swal.fire("Report Not Generated", xhr.responseText, "error");
    }).always(enableButton);
}

function Generatecashdropsreadingreport() {
    document.getElementById("Cashdropreadingreportbtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Generating...";

    if ($('#CashdropreadingstartdateId').val() == '') {
        Swal.fire("Report Not Generated", 'Start Date is Required', "warning");
        document.getElementById("Cashdropreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#CashdropreadingenddateId').val() == '') {
        Swal.fire("Report Not Generated", 'End Date is Required', "warning");
        document.getElementById("Cashdropreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#CashdropreadingStationId').val() == '') {
        Swal.fire("Report Not Generated", 'Station is Required', "warning");
        document.getElementById("Cashdropreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#CashdropreadingAttendantId').val() == '') {
        Swal.fire("Report Not Generated", 'Attendant is Required', "warning");
        document.getElementById("Cashdropreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#CashdropreadingStationShiftId').val() == '') {
        Swal.fire("Report Not Generated", 'Shift is Required', "warning");
        document.getElementById("Cashdropreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }

    var requestData = {
        TenantId: $('#systemLoggedinedTenantid').val(),
        StartDate: $('#CashdropreadingstartdateId').val(),
        EndDate: $('#CashdropreadingenddateId').val(),
        Station: $('#CashdropreadingStationId').val(),
        Attendant: $('#CashdropreadingAttendantId').val(),
        Shift: $('#CashdropreadingStationShiftId').val()
    };

    // Send a POST request to generate the report
    $.post("/ReportManagement/Generateshiftcashdropreadingdata", requestData).done(function (response) {
        var docDefinition = {
            pageOrientation: 'landscape', // Set the page orientation to landscape
            content: [
                { text: "SHIFT CASH COLLECTION REPORT", style: 'header' },
                { text: ' ' },
                { text: 'Customer and Date Information', style: 'subheader' },
                // Include customer and date information table here...
                {
                    style: 'tableExample',
                    table: {
                        widths: ['*', '*'],
                        body: [
                            ['Station:', { text: response.StationName || 'N/A', noWrap: true }],
                            ['Shift:', { text: response.ShiftCode || 'N/A', noWrap: true }],
                            ['Attendant:', { text: response.AttendantName || 'N/A', noWrap: true }],
                            ['Start Date:', { text: formatDate(response.StartDate), noWrap: true }],
                            ['End Date:', { text: formatDate(response.EndDate), noWrap: true }]
                        ]
                    }
                }
            ],
            styles: {
                header: {
                    fontSize: 9,
                    bold: true,
                    alignment: 'center'
                },
                subheader: {
                    fontSize: 9,
                    bold: true,
                    margin: [0, 15, 0, 0]
                },
                tableExample: {
                    margin: [0, 5, 0, 15],
                    fontSize: 9 // Reduced font size for the table
                },
                summaryFooter: {
                    bold: true,
                    margin: [0, 10, 0, 0]
                }
            }
        };

        // Check if data is available
        if (!response.ShiftCashDrop || response.ShiftCashDrop.length === 0) {
            // If no data available, add a message
            docDefinition.content.push({ text: "No data available", style: 'noData' });
        } else {
            // Initialize overall totals
            var overallTotalAmount = 0;
            var overallTotalVatAmount = 0;

            // Initialize table body
            var tableBody = [
                ["Voucher Mode", "Shift Date", "Shift", "Merchant Name", "Item Code", "Item Name", "Price", "Quantity", "Discount", "Amount", "Vat Amount"]
            ];

            var currentVoucherMode = null;
            var modeTotalAmount = 0;
            var modeTotalVatAmount = 0;

            // Iterate over each report
            for (var i = 0; i < response.ShiftCashDrop.length; i++) {
                var report = response.ShiftCashDrop[i];

                // Check if voucher mode changes
                if (report.VoucherMode !== currentVoucherMode) {
                    // If it changes, add a row for mode totals
                    if (currentVoucherMode !== null) {
                        tableBody.push([
                            'Subtotal:', '', '', '', '', '', '', '', '',
                            modeTotalAmount.toLocaleString(),
                            modeTotalVatAmount.toLocaleString()
                        ]);
                    }
                    // Add a row for the new voucher mode
                    tableBody.push([report.VoucherMode, '', '', '', '', '', '', '', '', '', '']);
                    // Reset mode totals
                    modeTotalAmount = 0;
                    modeTotalVatAmount = 0;
                    // Update current voucher mode
                    currentVoucherMode = report.VoucherMode;
                }

                // Calculate totals
                overallTotalAmount += parseFloat(report.VoucherAmount) || 0;
                overallTotalVatAmount += parseFloat(report.VatAmount) || 0;
                modeTotalAmount += parseFloat(report.VoucherAmount) || 0;
                modeTotalVatAmount += parseFloat(report.VatAmount) || 0;

                // Add row for the current report
                var rowData = [
                    '',
                    formatDate(report.ShiftDateTime) || 'N/A',
                    report.ShiftCode || 'N/A',
                    report.VoucherMode || 'N/A',
                    report.ProductCode || 'N/A',
                    report.ProductVariationName || 'N/A',
                    report.ProductPrice.toLocaleString(),
                    report.ProductQuantity.toLocaleString(),
                    report.ProductDiscount.toLocaleString(),
                    report.VoucherAmount.toLocaleString(),
                    report.VatAmount.toLocaleString()
                ];
                tableBody.push(rowData);
            }

            // Add row for the last mode's subtotal
            if (currentVoucherMode !== null) {
                tableBody.push([
                    'Subtotal:', '', '', '', '', '', '', '', '',
                    modeTotalAmount.toLocaleString(),
                    modeTotalVatAmount.toLocaleString()
                ]);
            }

            // Add totals row for all voucher modes
            tableBody.push([
                'Overall Total:', '', '', '', '', '', '', '', '',
                overallTotalAmount.toLocaleString(),
                overallTotalVatAmount.toLocaleString()
            ]);

            // Add the table to the document definition
            docDefinition.content.push(
                { text: 'Cash Collection Details', style: 'subheader' },
                {
                    style: 'tableExample',
                    table: {
                        headerRows: 1,
                        widths: [70, 70, 80, 70, 70, 80, 40, 40, 40, 50, 50],
                        body: tableBody
                    }
                }
            );
        }
        // Generate a unique code with minute only
        var timestamp = new Date().toISOString().slice(-14, -8); // Extracting minute component

        // Generate and download the PDF with unique code
        pdfMake.createPdf(docDefinition).download('ShiftCashDropsReport' + timestamp + '.pdf');
        document.getElementById("Cashdropreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        $('#FuelcardsystemModal').hide();
        setTimeout(function () { location.reload(); }, 1000);
    }).fail(function (xhr, status, error) {
        // Show an error message if the request fails
        Swal.fire("Report Not Generated", xhr.responseText, "error");
    })
}

function groupByAttendantName(cashDrops) {
    var groupedData = {};

    cashDrops.forEach(function (cashDrop) {
        var attendantName = cashDrop.AttendantName;
        if (!groupedData[attendantName]) {
            groupedData[attendantName] = [];
        }
        groupedData[attendantName].push(cashDrop);
    });

    return groupedData;
}

function Generatepurchasesreadingreport() {
    document.getElementById("Purchasereadingreportbtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Generating...";

    if ($('#PurchasereadingstartdateId').val() == '') {
        Swal.fire("Report Not Generated", 'Start Date is Required', "warning");
        document.getElementById("Purchasereadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#PurchasereadingenddateId').val() == '') {
        Swal.fire("Report Not Generated", 'End Date is Required', "warning");
        document.getElementById("Purchasereadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#PurchasereadingStationId').val() == '') {
        Swal.fire("Report Not Generated", 'Station is Required', "warning");
        document.getElementById("Purchasereadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#PurchasereadingAttendantId').val() == '') {
        Swal.fire("Report Not Generated", 'Attendant is Required', "warning");
        document.getElementById("Purchasereadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#PurchasereadingStationShiftId').val() == '') {
        Swal.fire("Report Not Generated", 'Shift is Required', "warning");
        document.getElementById("Purchasereadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }

    var requestData = {
        TenantId: $('#systemLoggedinedTenantid').val(),
        StartDate: $('#PurchasereadingstartdateId').val(),
        EndDate: $('#PurchasereadingenddateId').val(),
        Station: $('#PurchasereadingStationId').val(),
        Attendant: $('#PurchasereadingAttendantId').val(),
        Shift: $('#PurchasereadingStationShiftId').val()
    };

    // Send a POST request to generate the report
    $.post("/ReportManagement/Generateshiftpurchasesreadingdata", requestData).done(function (response) {
        var docDefinition = {
            content: [
                { text: "SHIFT PURCHASES RECEIPT", style: 'header' },
                { text: ' ' },
                { text: 'Station and Date Information', style: 'subheader' },
                {
                    style: 'tableExample',
                    table: {
                        widths: ['*', '*'],
                        body: [
                            ['Station:', { text: response.StationName || 'N/A', noWrap: true }],
                            ['Shift:', { text: response.ShiftCode || 'N/A', noWrap: true }],
                            ['Attendant:', { text: response.AttendantName || 'N/A', noWrap: true }],
                            ['Start Date:', { text: formatDate(response.StartDate), noWrap: true }],
                            ['End Date:', { text: formatDate(response.EndDate), noWrap: true }]
                        ]
                    }
                },
                { text: 'Receipt Details', style: 'subheader' }
            ],
            styles: {
                header: {
                    fontSize: 6,
                    bold: true,
                    alignment: 'center',
                    margin: [0, 0, 0, 10]
                },
                subheader: {
                    fontSize: 6,
                    bold: true,
                    margin: [0, 5, 0, 5]
                },
                tableExample: {
                    margin: [0, 5, 0, 10],
                    fontSize: 6, // Adjust the font size for the table
                    width: '100%' // Set table width to 100%
                },
                noData: {
                    fontSize: 6, // Adjust the font size for the no data message
                    italics: true
                },
                // Add a style for normal text
                normalText: {
                    fontSize: 6
                }
            }
        };

        // Check if data is available
        if (!response.Purchases || response.Purchases.length === 0) {
            // If no data available, add a message
            docDefinition.content.push({ text: "No data available", style: 'noData' });
        } else {
            // If data available, include PurchaseData and PurchaseItemData
            response.Purchases.forEach(function (purchase) {
                var purchaseDetails = [
                    ["Invoice Number", "Invoice Date", "Invoice Amount", "Transport Amount", "Truck Number", "Driver Name", "Supplier Name"]
                ];

                purchaseDetails.push([
                    purchase.InvoiceNumber || 'N/A',
                    formatDate(purchase.InvoiceDate) || 'N/A',
                    purchase.InvoiceAmount || 'N/A',
                    purchase.TransportAmount || 'N/A',
                    purchase.TruckNumber || 'N/A',
                    purchase.DriverName || 'N/A',
                    purchase.SupplierName || 'N/A'
                ]);

                docDefinition.content.push(
                    { text: 'Invoice Number: ' + (purchase.InvoiceNumber || 'N/A'), style: 'normalText' },
                    { text: 'Invoice Date: ' + formatDate(purchase.InvoiceDate), style: 'normalText' },
                    { text: 'Supplier: ' + (purchase.SupplierName || 'N/A'), style: 'normalText' },
                    { text: 'Total Amount: ' + purchase.InvoiceAmount, style: 'normalText' },
                    { text: ' ', style: 'normalText' }, // Add some space
                    { text: 'Items:', style: 'subheader' }
                );

                if (purchase.PurchaseItems && purchase.PurchaseItems.length > 0) {
                    // Initialize arrays to hold items with and without 'N/A' tank names
                    var itemsWithTankName = [];
                    var itemsWithoutTankName = [];

                    // Separate items based on tank name
                    purchase.PurchaseItems.forEach(function (item) {
                        if (item.TankName === 'N/A') {
                            itemsWithoutTankName.push(item);
                        } else {
                            itemsWithTankName.push(item);
                        }
                    });

                    // Generate a table for items with 'N/A' tank names if any exist
                    if (itemsWithoutTankName.length > 0) {
                        var naTankTableBody = [
                            ["Product", "Purchase Quantity", "Price", "Tax", "Discount", "Gross Total", "Net Total", "Tax Amount"]
                        ];

                        itemsWithoutTankName.forEach(function (item) {
                            var itemRowData = [
                                item.ProductVariationname || 'N/A',
                                item.PurchaseQuantity || 'N/A',
                                item.PurchasePrice || 'N/A',
                                item.PurchaseTax || 'N/A',
                                item.PurchaseDiscount || 'N/A',
                                item.PurchaseGTotal || 'N/A',
                                item.PurchaseNTotal || 'N/A',
                                item.PurchaseTaxAmount || 'N/A'
                            ];
                            naTankTableBody.push(itemRowData);
                        });

                        // Add the new table to the document definition
                        docDefinition.content.push(
                            {
                                style: 'tableExample',
                                table: {
                                    headerRows: 1,
                                    widths: ['*', '*', '*', '*', '*', '*', '*', '*'], // Adjust column widths based on content
                                    body: naTankTableBody
                                }
                            }
                        );
                    }

                    // Generate a table for items with actual tank names if any exist
                    if (itemsWithTankName.length > 0) {
                        var tankTableBody = [
                            ["Product", "Tank", "Dips Before", "Purchase Quantity", "Dips After", "Expected Quantity", "Gain/Loss", "% Gain/Loss", "Price", "Tax", "Discount", "Gross Total", "Net Total", "Tax Amount"]
                        ];

                        itemsWithTankName.forEach(function (item) {
                            var itemRowData = [
                                item.ProductVariationname || 'N/A',
                                item.TankName || 'N/A',
                                item.DipsBeforeOffloading || 'N/A',
                                item.PurchaseQuantity || 'N/A',
                                item.DipsAfterOffloading || 'N/A',
                                item.ExpectedQuantity || 'N/A',
                                item.GainLoss || 'N/A',
                                item.PercentageGainLoss || 'N/A',
                                item.PurchasePrice || 'N/A',
                                item.PurchaseTax || 'N/A',
                                item.PurchaseDiscount || 'N/A',
                                item.PurchaseGTotal || 'N/A',
                                item.PurchaseNTotal || 'N/A',
                                item.PurchaseTaxAmount || 'N/A'
                            ];
                            tankTableBody.push(itemRowData);
                        });

                        // Add the new table to the document definition
                        docDefinition.content.push(
                            {
                                style: 'tableExample',
                                table: {
                                    headerRows: 1,
                                    widths: [28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 30, 30], // Adjust column widths based on content
                                    body: tankTableBody
                                }
                            }
                        );
                    }
                }
            });
        }
        // Generate a unique code with minute only
        var timestamp = new Date().toISOString().slice(-14, -8); // Extracting minute component

        // Generate and download the PDF with unique code
        pdfMake.createPdf(docDefinition).download('ShiftPurchasesDetailsReport' + timestamp + '.pdf');
        document.getElementById("Purchasereadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        $('#FuelcardsystemModal').hide();
        setTimeout(function () { location.reload(); }, 1000);
    }).fail(function (xhr, status, error) {
        // Show an error message if the request fails
        Swal.fire("Report Not Generated", xhr.responseText, "error");
    }).always(enableButton);
}

function Generatestationshiftreadingreport() {
    document.getElementById("Stationshiftreadingreportbtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Generating...";

    if ($('#StationshiftreadingstartdateId').val() == '') {
        Swal.fire("Report Not Generated", 'Start Date is Required', "warning");
        document.getElementById("Stationshiftreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#StationshiftreadingenddateId').val() == '') {
        Swal.fire("Report Not Generated", 'End Date is Required', "warning");
        document.getElementById("Stationshiftreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#StationshiftreadingStationId').val() == '') {
        Swal.fire("Report Not Generated", 'Station is Required', "warning");
        document.getElementById("Stationshiftreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#StationshiftreadingAttendantId').val() == '') {
        Swal.fire("Report Not Generated", 'Attendant is Required', "warning");
        document.getElementById("Stationshiftreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#StationshiftreadingStationShiftId').val() == '') {
        Swal.fire("Report Not Generated", 'Shift is Required', "warning");
        document.getElementById("Stationshiftreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }

    var requestData = {
        TenantId: $('#systemLoggedinedTenantid').val(),
        StartDate: $('#StationshiftreadingstartdateId').val(),
        EndDate: $('#StationshiftreadingenddateId').val(),
        Station: $('#StationshiftreadingStationId').val(),
        Attendant: $('#StationshiftreadingAttendantId').val(),
        Shift: $('#StationshiftreadingStationShiftId').val()
    };

    // Send a POST request to generate the report
    $.post("/ReportManagement/Generatestationshiftreadingdata", requestData).done(function (response) {
        var tableBody = [
            ["Station", "ShiftCode", "ShiftCategory", "ShiftDate", "Status", "Balance", "Reference"]
        ];

        for (var i = 0; i < response.StationShiftDetails.length; i++) {
            var shift = response.StationShiftDetails[i];
            var rowData = [
                shift.StationName || 'N/A',
                shift.ShiftCode || 'N/A',
                shift.ShiftCategory || 'N/A',
                formatDate(shift.ShiftDateTime),
                shift.ShiftStatus || 'N/A',
                shift.ShiftBalance.toFixed(2),
                shift.ShiftReference || 'N/A',
            ];
            tableBody.push(rowData);
        }

        var docDefinition = {
            content: [
                { text: "SHIFT SALE REPORT", style: 'header' },
                { text: ' ' },
                { text: 'Station and Date Information', style: 'subheader' },
                {
                    style: 'tableExample',
                    table: {
                        widths: [80, '*'],
                        body: [
                            ['Station:', { text: response.StationName || 'N/A', noWrap: true }],
                            ['Shift:', { text: response.ShiftCode || 'N/A', noWrap: true }],
                            ['Attendant:', { text: response.AttendantName || 'N/A', noWrap: true }],
                            ['Start Date:', { text: formatDate(response.StartDate), noWrap: true }],
                            ['End Date:', { text: formatDate(response.EndDate), noWrap: true }]
                        ]
                    }
                },
                { text: ' ', margin: [0, 5, 0, 5] }, // Add space between tables
                { text: 'Shift Details', style: 'subheader' },
                {
                    style: 'tableExample',
                    table: {
                        headerRows: 1,
                        widths: new Array(tableBody[0].length).fill('auto'),
                        body: tableBody
                    }
                }
            ],
            styles: {
                header: {
                    fontSize: 7,
                    bold: true,
                    alignment: 'center'
                },
                subheader: {
                    fontSize: 7,
                    bold: true,
                    margin: [0, 15, 0, 0]
                },
                tableExample: {
                    margin: [0, 5, 0, 15]
                }
            }
        };


        // Generate a unique code with minute only
        var timestamp = new Date().toISOString().slice(-14, -8); // Extracting minute component

        // Generate and download the PDF with unique code
        pdfMake.createPdf(docDefinition).download('ShiftSaleReport' + timestamp + '.pdf');
        document.getElementById("Stationshiftreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        $('#FuelcardsystemModal').hide();
        setTimeout(function () { location.reload(); }, 1000);
    }).fail(function (xhr, status, error) {
        // Show an error message if the request fails
        Swal.fire("Report Not Generated", xhr.responseText, "error");
    });
}
function Generateshiftsummaryreadingreport() {
    document.getElementById("Shiftsummaryreadingreportbtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Generating...";

    if ($('#ShiftsummaryreadingstartdateId').val() == '') {
        Swal.fire("Report Not Generated", 'Start Date is Required', "warning");
        document.getElementById("Shiftsummaryreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#ShiftsummaryreadingenddateId').val() == '') {
        Swal.fire("Report Not Generated", 'End Date is Required', "warning");
        document.getElementById("Shiftsummaryreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#ShiftsummaryreadingStationId').val() == '') {
        Swal.fire("Report Not Generated", 'Station is Required', "warning");
        document.getElementById("Shiftsummaryreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#ShiftsummaryreadingAttendantId').val() == '') {
        Swal.fire("Report Not Generated", 'Attendant is Required', "warning");
        document.getElementById("Shiftsummaryreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#ShiftsummaryreadingStationShiftId').val() == '') {
        Swal.fire("Report Not Generated", 'Shift is Required', "warning");
        document.getElementById("Shiftsummaryreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }

    var requestData = {
        TenantId: $('#systemLoggedinedTenantid').val(),
        StartDate: $('#ShiftsummaryreadingstartdateId').val(),
        EndDate: $('#ShiftsummaryreadingenddateId').val(),
        Station: $('#ShiftsummaryreadingStationId').val(),
        Attendant: $('#ShiftsummaryreadingAttendantId').val(),
        Shift: $('#ShiftsummaryreadingStationShiftId').val()
    };

    // Send a POST request to generate the report
    $.post("/ReportManagement/Generatestationshiftsummaryreadingdata", requestData).done(function (response) {
        const { StartDate, EndDate, StationName, ShiftCode, FinancialDetails, ShiftPumpSaleSummary, ShiftTankSaleSummary, AttendantShiftSummary, ExpectedCash } = response;
        // Group financial details by category name
        const groupedFinancialDetails = {};
        FinancialDetails.forEach(item => {
            if (!groupedFinancialDetails[item.Categoryname]) {
                groupedFinancialDetails[item.Categoryname] = [];
            }
            groupedFinancialDetails[item.Categoryname].push(item);
        });

        // Create an array to hold the grouped financial details with totals
        const financialDetailsWithTotals = [];

        // Iterate over groupedFinancialDetails to calculate totals for each category
        for (const categoryName in groupedFinancialDetails) {
            if (groupedFinancialDetails.hasOwnProperty(categoryName)) {
                const categoryItems = groupedFinancialDetails[categoryName];
                let totalAmount = 0;

                categoryItems.forEach(item => {
                    totalAmount += item.Amount;
                });

                // Add a total row for the category
                financialDetailsWithTotals.push({
                    CategoryName: categoryName,
                    TotalAmount: totalAmount.toFixed(2),
                    Items: categoryItems
                });
            }
        }
        const docDefinition = {
            content: [
                { text: 'Shift Summary Reading Report', style: 'header' },       
                {
                    style: 'tableExample',
                    table: {
                        widths: [80, '*'],
                        body: [
                            ['Station:', { text: response.StationName || 'N/A', noWrap: true }],
                            ['Shift:', { text: response.ShiftCode || 'N/A', noWrap: true }],
                            ['Attendant:', { text: response.AttendantName || 'N/A', noWrap: true }],
                            ['Start Date:', { text: formatDate(response.StartDate), noWrap: true }],
                            ['End Date:', { text: formatDate(response.EndDate), noWrap: true }]
                        ]
                    }
                },
                { text: 'Financial Details', style: 'subheader' },
                {
                    style: 'tableExample',
                    table: {
                        headerRows: 1,
                        widths: ['auto', '*', 'auto'],
                        body: [
                            [{ text: 'Category Name', style: 'tableHeader' }, { text: 'Attendant Name', style: 'tableHeader' }, { text: 'Amount', style: 'tableHeader' }]
                        ].concat(
                            financialDetailsWithTotals.map(category => {
                                const rows = [
                                    [{ text: category.CategoryName, colSpan: 3, style: 'categoryHeader' }, {}, {}]
                                ];
                                category.Items.forEach(item => {
                                    rows.push([item.Descriptions, item.AttendantName, item.Amount.toFixed(2)]);
                                });
                                rows.push([{ text: 'Total', style: 'tableHeader' }, {}, { text: category.TotalAmount, style: 'tableHeader' }]);
                                return rows;
                            }).flat()
                        )
                    }
                },
                // Shift Pump Sale Summary
                { text: 'Shift Pump Sale Summary', style: 'subheader' },
                {
                    style: 'tableExample',
                    table: {
                        headerRows: 1,
                        widths: ['*', '*'],
                        body: [
                            [{ text: 'Pump Name', style: 'tableHeader' }, { text: 'Units Solds', style: 'tableHeader' }]
                        ].concat(
                            ShiftPumpSaleSummary.map(item => [item.PumpName || 'N/A', item.TotalShillings ? item.TotalShillings.toFixed(2) : 'N/A'])
                        )
                    }
                },

                // Shift Tank Sale Summary
                { text: 'Shift Tank Sale Summary', style: 'subheader' },
                {
                    style: 'tableExample',
                    table: {
                        headerRows: 1,
                        widths: ['*', '*'],
                        body: [
                            [{ text: 'Tank Name', style: 'tableHeader' }, { text: 'Amount Sold', style: 'tableHeader' }]
                        ].concat(
                            ShiftTankSaleSummary.map(item => [item.TankName || 'N/A', item.AmountSold ? item.AmountSold.toFixed(2) : 'N/A'])
                        )
                    }
                },

                // Attendant Shift Summary
                { text: 'Attendant Shift Summary', style: 'subheader' },
                {
                    style: 'tableExample',
                    table: {
                        headerRows: 1,
                        widths: ['*', '*'],
                        body: [
                            [{ text: 'Attendant Name', style: 'tableHeader' }, { text: 'Total Amount', style: 'tableHeader' }]
                        ].concat(
                            AttendantShiftSummary.map(item => [item.Attendantname || 'N/A', item.Amount ? item.Amount.toFixed(2) : 'N/A'])
                        )
                    }
                },

                // Expected Cash
                {
                    alignment: 'right', // Align the text to the right
                    text: `Expected Cash: ${ExpectedCash ? ExpectedCash.toFixed(2) : 'N/A'}`,
                    style: 'subheader'
                }
            ],
            styles: {
                header: {
                    fontSize: 9,
                    bold: true,
                    alignment: 'center'
                },
                subheader: {
                    fontSize: 9,
                    bold: true,
                    margin: [0, 15, 0, 0]
                },
                tableExample: {
                    margin: [0, 5, 0, 15],
                    fontSize: 8 // Reduced font size for the table
                }
            }
        };

        // Generate a unique code with minute only
        var timestamp = new Date().toISOString().slice(-14, -8); // Extracting minute component

        // Generate and download the PDF with unique code
        pdfMake.createPdf(docDefinition).download('ShiftSummaryReport' + timestamp + '.pdf');
        document.getElementById("Shiftsummaryreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        $('#FuelcardsystemModal').hide();
        setTimeout(function () { location.reload(); }, 1000);
    }).fail(function (xhr, status, error) {
        // Show an error message if the request fails
        Swal.fire("Report Not Generated", xhr.responseText, "error");
    });
}
function Generatecustomerstatementreadingreport() {
    document.getElementById("Customerstatementreadingreportbtn").disabled = true;
    document.getElementById("processingSpinner").style.display = "inline-block";
    document.getElementById("buttonText").innerText = "Generating...";

    if ($('#CustomerstatementreadingstartdateId').val() == '') {
        Swal.fire("Report Not Generated", 'Start Date is Required', "warning");
        document.getElementById("Customerstatementreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#CustomerstatementreadingenddateId').val() == '') {
        Swal.fire("Report Not Generated", 'End Date is Required', "warning");
        document.getElementById("Customerstatementreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#CustomerstatementreadingStationId').val() == '') {
        Swal.fire("Report Not Generated", 'Station is Required', "warning");
        document.getElementById("Customerstatementreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#CustomerstatementreadingAttendantId').val() == '') {
        Swal.fire("Report Not Generated", 'Attendant is Required', "warning");
        document.getElementById("Customerstatementreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }
    if ($('#CustomerstatementcustomerId').val() == '' || $('#CustomerstatementcustomerId').val() == 0) {
        Swal.fire("Report Not Generated", 'Customer is Required', "warning");
        document.getElementById("Customerstatementreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        return;
    }

    var requestData = {
        TenantId: $('#systemLoggedinedTenantid').val(),
        StartDate: $('#CustomerstatementreadingstartdateId').val(),
        EndDate: $('#CustomerstatementreadingenddateId').val(),
        Station: $('#CustomerstatementreadingStationId').val(),
        Attendant: $('#CustomerstatementreadingAttendantId').val(),
        Customer: $('#CustomerstatementcustomerId').val()
    };

    // Send a POST request to generate the report
    $.post("/ReportManagement/Generatestationshiftcustomerstatementreportdata", requestData).done(function (response) {
        const docDefinition = {
            defaultStyle: { fontSize: 8 },
            content: [
                { text: 'Customer Statement Reading Report', style: 'header' },
                {
                    style: 'tableExample',
                    table: {
                        widths: [80, '*'],
                        body: [
                            ['Station:', { text: response.StationName || 'N/A', noWrap: true }],
                            ['Customer:', { text: response.CustomerName || 'N/A', noWrap: true }],
                            ['Attendant:', { text: response.AttendantName || 'N/A', noWrap: true }],
                            ['Start Date:', { text: formatDate(response.StartDate), noWrap: true }],
                            ['End Date:', { text: formatDate(response.EndDate), noWrap: true }]
                        ]
                    }
                },
                {
                    style: 'tableExample',
                    table: {
                        headerRows: 1,
                        widths: ['auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto'],
                        body: [
                            [{ text: 'Date', style: 'tableHeader' }, { text: 'Customer', style: 'tableHeader' }, { text: 'Equipment', style: 'tableHeader' },
                            { text: 'Attendant', style: 'tableHeader' }, { text: 'Product Name', style: 'tableHeader' }, { text: 'Product Units', style: 'tableHeader' },
                            { text: 'Product Price', style: 'tableHeader' }, { text: 'Payment Mode', style: 'tableHeader' }, { text: 'Payment Reference', style: 'tableHeader' },
                            { text: 'CreditDebit', style: 'tableHeader' }, { text: 'Product Total', style: 'tableHeader' }, { text: 'Balance', style: 'tableHeader' }]
                        ].concat(
                            response.ShiftCustomerStatement ? response.ShiftCustomerStatement.map(statement => {
                                return [
                                    formatDate(statement.Datecreated), statement.Customername, statement.Equipment ? statement.Equipment : 'N/A', statement.Attendant,
                                    statement.Productname ? statement.Productname : 'N/A', statement.ProductUnits, statement.ProductPrice, statement.Paymentmode,
                                    statement.PaymentReference ? statement.PaymentReference : 'N/A', statement.DebitCredit ? statement.DebitCredit : 'N/A', statement.ProductTotal, statement.RunningBalance
                                ];
                            }) : [['No data available', '', '', '', '', '', '', '', '', '', '', '']]

                        )
                    }
                },
            ],
            styles: {
                header: {
                    fontSize: 9,
                    bold: true,
                    alignment: 'center'
                },
                subheader: {
                    fontSize: 9,
                    bold: true,
                    margin: [0, 15, 0, 0]
                },
                tableExample: {
                    margin: [0, 5, 0, 15],
                    fontSize: 8 // Reduced font size for the table
                }
            }
        };

        // Generate a unique code with minute only
        var timestamp = new Date().toISOString().slice(-14, -8); // Extracting minute component

        // Generate and download the PDF with unique code
        pdfMake.createPdf(docDefinition).download('CustomerStatementReport' + timestamp + '.pdf');
        document.getElementById("Customerstatementreadingreportbtn").disabled = false;
        document.getElementById("processingSpinner").style.display = "none";
        document.getElementById("buttonText").innerText = "GENERATE";
        $('#FuelcardsystemModal').hide();
        setTimeout(function () { location.reload(); }, 1000);
    }).fail(function (xhr, status, error) {
        // Show an error message if the request fails
        Swal.fire("Report Not Generated", xhr.responseText, "error");
    });
}


function formatDate(dateString) {
    return dateString.split('T')[0]; // Extracting date part only
}