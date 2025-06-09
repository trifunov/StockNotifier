
$(document).ready(function () {
    $('#createAlertBtn').on('click', function (e) {
        e.preventDefault();

        var alertData = {
            name: $('#Name').val(),
            thresholdType: parseInt($('#ThresholdType').val()),
            thresholdValue: parseFloat($('#ThresholdValue').val()),
            stockId: parseInt($('#StockId').val()),
            isActive: $('#IsActive').is(':checked')
        };

        $.ajax({
            url: 'https://localhost:7293/api/alerts',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(alertData),
            success: function (response) {
                alert('Alert created successfully!');
            },
            error: function (xhr, status, error) {
                alert('Error creating alert: ' + xhr.responseText);
            }
        });
    });

    $.ajax({
        url: 'https://localhost:7293/api/alerts?onlyActive=true',
        type: 'GET',
        dataType: 'json',
        success: function (alerts) {
            alerts = alerts.data;
            var alertsTableBody = '';
            $.each(alerts, function (index, alert) {
                alertsTableBody += '<tr>' +
                    '<td>' + alert.name + '</td>' +
                    '<td>' + alert.thresholdType + '</td>' +
                    '<td>' + alert.thresholdValue + '</td>' +
                    '<td>' + alert.stockId + '</td>' +
                    '<td>' +
                        (alert.isActive
                            ? '<span class="badge bg-success">Active</span>'
                            : '<span class="badge bg-secondary">Inactive</span>') +
                    '</td>' +
                '</tr>';
            });
            $('#alertsTableBody').html(alertsTableBody);
        },
        error: function (xhr, status, error) {
            alert('Error fetching alerts: ' + xhr.responseText);
        }
    });
});

const connection = new signalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .withUrl("https://localhost:7293/alert-hub")
    .build();

connection.on("ReceiveOverAlert", function (alert) {
    alert('Over Alert: ' + alert.name + ' has exceeded the threshold of ' + alert.thresholdValue + ' for stock ID ' + alert.stockId)
});

connection.on("ReceiveUnderAlert", function (alert) {
    alert('Under Alert: ' + alert.name + ' has fallen below the threshold of ' + alert.thresholdValue + ' for stock ID ' + alert.stockId)
});

connection.on("ReceiveEqualAlert", function (alert) {
    alert('Equal Alert: ' + alert.name + ' has reached the threshold of ' + alert.thresholdValue + ' for stock ID ' + alert.stockId)
});
