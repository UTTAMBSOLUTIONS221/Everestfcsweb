var todayDate = new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' ');
$.get("/Home/Getsystemdashboarddata?date=" + new Date(new Date().toString().split('GMT')[0] + ' UTC').toISOString().split('.')[0].replace('T', ' '), null, function (data) {
	$('#SystemstaffdataCount').text(data.Systemstaffs);
	$('#PrepaidCustomerdataCount').text(data.Prepaidcustomer);
	$('#PostpaidCustomerdataCount').text(data.Postpaidcustomer);
	$('#OnlineSalesTransactiondataCount').text(data.OnlineSales);
	$('#OnfflineSalesTransactiondataCount').text(data.OfflineSales);
	$('#AwardedPointTransactiondataCount').text(data.AwardedPoints);
	$('#RedeemedPointTransactiondataCount').text(data.RedeemedPoints);
	BindDailySalesTransactionsToGraph(data.DailySales);
	BindMonthlySalesTransactionsToGraph(data.MonthlySales);
	BindDailyPointsTransactionsToGraph(data.DailyAwards);
	BindMonthlyPointsTransactionsToGraph(data.MonthlyAwards);
});


function BindDailySalesTransactionsToGraph(salesTransactionsForGraph) {
	var datasets = [];
	var labels = [];

	// Separate arrays for online and offline sales
	var onlineSalesData = [];
	var offlineSalesData = [];

	$.each(salesTransactionsForGraph, function (index, item) {
		labels.push(item.StationName); // Assuming you want each station as a label

		// Push online and offline sales data to separate arrays
		onlineSalesData.push(item.OnlineSaleAmount);
		offlineSalesData.push(item.OfflineSaleAmount);
	});

	// Create datasets for online and offline sales
	datasets.push({
		label: 'Online Sales',
		data: onlineSalesData,
		backgroundColor: 'rgba(75, 192, 192, 0.2)', // Set your desired color
		borderColor: 'rgba(75, 192, 192, 1)', // Set your desired color
		borderWidth: 1
	});

	datasets.push({
		label: 'Offline Sales',
		data: offlineSalesData,
		backgroundColor: 'rgba(255, 99, 132, 0.2)', // Set your desired color
		borderColor: 'rgba(255, 99, 132, 1)', // Set your desired color
		borderWidth: 1
	});

	DestroyCanvasIfExists('CanvasSalesByDay');

	const context = $('#CanvasSalesByDay');
	context.height(200);
	const myChart = new Chart(context, {
		type: 'bar',
		data: {
			labels: labels,
			datasets: datasets,
		},
		options: {
			scales: {
				y: {
					beginAtZero: true
				}
			}
		}
	});
}
function BindMonthlySalesTransactionsToGraph(salesTransactionsForGraph) {
	var datasets = [];
	var labels = [];

	// Separate arrays for online and offline sales
	var onlineSalesData = [];
	var offlineSalesData = [];

	$.each(salesTransactionsForGraph, function (index, item) {
		labels.push(item.StationName); // Assuming you want each station as a label

		// Push online and offline sales data to separate arrays
		onlineSalesData.push(item.OnlineSaleAmount);
		offlineSalesData.push(item.OfflineSaleAmount);
	});

	// Create datasets for online and offline sales
	datasets.push({
		label: 'Online Sales',
		data: onlineSalesData,
		backgroundColor: 'rgba(75, 192, 192, 0.2)', // Set your desired color
		borderColor: 'rgba(75, 192, 192, 1)', // Set your desired color
		borderWidth: 1
	});

	datasets.push({
		label: 'Offline Sales',
		data: offlineSalesData,
		backgroundColor: 'rgba(255, 99, 132, 0.2)', // Set your desired color
		borderColor: 'rgba(255, 99, 132, 1)', // Set your desired color
		borderWidth: 1
	});

	DestroyCanvasIfExists('CanvasSalesByMonth');

	const context = $('#CanvasSalesByMonth');
	const myChart = new Chart(context, {
		type: 'bar',
		data: {
			labels: labels,
			datasets: datasets,
		},
		options: {
			scales: {
				y: {
					beginAtZero: true
				}
			}
		}
	});
}
function BindDailyPointsTransactionsToGraph(pointsTransactionsForGraph) {
	var datasets = [];
	var labels = [];

	// Separate arrays for online and offline sales
	var awardedPointsData = [];
	var redeemedPointsData = [];

	$.each(pointsTransactionsForGraph, function (index, item) {
		labels.push(item.StationName); // Assuming you want each station as a label

		// Push online and offline sales data to separate arrays
		awardedPointsData.push(item.PointAwardAmount);
		redeemedPointsData.push(item.PointRedeemAmount);
	});

	// Create datasets for online and offline sales
	datasets.push({
		label: 'Awarde Points',
		data: awardedPointsData,
		backgroundColor: 'rgba(75, 192, 192, 0.2)', // Set your desired color
		borderColor: 'rgba(75, 192, 192, 1)', // Set your desired color
		borderWidth: 1
	});

	datasets.push({
		label: 'Redeemed Points',
		data: redeemedPointsData,
		backgroundColor: 'rgba(255, 99, 132, 0.2)', // Set your desired color
		borderColor: 'rgba(255, 99, 132, 1)', // Set your desired color
		borderWidth: 1
	});

	DestroyCanvasIfExists('CanvasPointSummaryByDay');

	const context = $('#CanvasPointSummaryByDay');
	const myChart = new Chart(context, {
		type: 'bar',
		data: {
			labels: labels,
			datasets: datasets,
		},
		options: {
			scales: {
				y: {
					beginAtZero: true
				}
			}
		}
	});
}
function BindMonthlyPointsTransactionsToGraph(pointsTransactionsForGraph) {
	var datasets = [];
	var labels = [];

	// Separate arrays for online and offline sales
	var awardedPointsData = [];
	var redeemedPointsData = [];

	$.each(pointsTransactionsForGraph, function (index, item) {
		labels.push(item.StationName); // Assuming you want each station as a label

		// Push online and offline sales data to separate arrays
		awardedPointsData.push(item.PointAwardAmount);
		redeemedPointsData.push(item.PointRedeemAmount);
	});

	// Create datasets for online and offline sales
	datasets.push({
		label: 'Awarde Points',
		data: awardedPointsData,
		backgroundColor: 'rgba(75, 192, 192, 0.2)', // Set your desired color
		borderColor: 'rgba(75, 192, 192, 1)', // Set your desired color
		borderWidth: 1
	});

	datasets.push({
		label: 'Redeemed Points',
		data: redeemedPointsData,
		backgroundColor: 'rgba(255, 99, 132, 0.2)', // Set your desired color
		borderColor: 'rgba(255, 99, 132, 1)', // Set your desired color
		borderWidth: 1
	});

	DestroyCanvasIfExists('CanvasPointSummaryByMonth');

	const context = $('#CanvasPointSummaryByMonth');
	const myChart = new Chart(context, {
		type: 'bar',
		data: {
			labels: labels,
			datasets: datasets,
		},
		options: {
			scales: {
				y: {
					beginAtZero: true
				}
			}
		}
	});
}
//end region

// supporting functions for Graphs
function DestroyCanvasIfExists(canvasId) {
	let chartStatus = Chart.getChart(canvasId);
	if (chartStatus != undefined) {
		chartStatus.destroy();
	}
}

var backgroundColors = [
	'rgba(255, 99, 132, 0.2)',
	'rgba(54, 162, 235, 0.2)',
	'rgba(255, 206, 86, 0.2)',
	'rgba(75, 192, 192, 0.2)',
	'rgba(153, 102, 255, 0.2)',
	'rgba(255, 159, 64, 0.2)'
];
var borderColors = [
	'rgba(255, 99, 132, 1)',
	'rgba(54, 162, 235, 1)',
	'rgba(255, 206, 86, 1)',
	'rgba(75, 192, 192, 1)',
	'rgba(153, 102, 255, 1)',
	'rgba(255, 159, 64, 1)'
];