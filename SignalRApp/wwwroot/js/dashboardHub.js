"use strict";
var connection = new
signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();
$(function () {
    connection.start().then(function () {
        InvokeProducts();
    }).catch(function (err) {
        return console.error(err.toString());
    });
});
connection.on("ReceivedProducts", function (products) {
    BindProductsToGrid(products);
});
connection.on("ReceivedProductsGraphData", function (products) {
    BindProductsToGraph(products);
});
function InvokeProducts() {
    connection.invoke("SendProducts").catch(function (err) {
        return console.error(err.toString());
    });
}
function BindProductsToGrid(products) {
    $("#tblProduct tbody").empty();
    var tr;
    $.each(products, function (index, product) {
        tr = $("<tr/>");
        tr.append("<td>" + (index + 1) + "</td>");
        tr.append("<td>" + product.name + "</td>");
        tr.append("<td>" + product.category + "</td>");
        tr.append("<td>" + product.price + "</td>");
        $("#tblProduct").append(tr);
    });
}
function BindProductsToGraph(products) {
    const labels = [];
    const data = [];
    const backgroundColors= [];
    const borderColors = [];
    $.each(products, function (index, category) {
        labels.push(category.name);
        data.push(category.count);
    });
    DestroyCanvasIfExists('canvasProducts');
    for (let i = 0; i < data.length; i++) {
        backgroundColors.push(getRandomColor());
        borderColors.push(getRandomColor());
    }
    
    const context = $('#canvasProducts');
    const myChart = new Chart(context, {
        type: 'doughnut',
        data: {
            labels: labels,
            datasets: [{
                label: '# of Products',
                data: data,
                backgroundColor: backgroundColors,
                borderColor: borderColors,
                borderWidth: 1
            }]
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

function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}

function DestroyCanvasIfExists(canvasId) {
    let chartStatus = Chart.getChart(canvasId);
    if (chartStatus != undefined) {
        chartStatus.destroy();
    }
}