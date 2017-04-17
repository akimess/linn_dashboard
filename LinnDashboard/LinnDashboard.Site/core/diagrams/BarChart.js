var Diagrams = Diagrams || {};

Diagrams.BarChart = (function () {
    var diagram = function () {
        this.refresh = function ($container, diagram, dataSource) {
            var arr = [];
            var labelArray = ["Label"];
            var colors = [];
            for (var i = 0; i < dataSource.Labels.length; i++) {
                var subArray = [];

                subArray.push(dataSource.Labels[i]);

                for (var j = 0; j < dataSource.DataSets.length; j++) {
                    if ($.inArray(labelArray, dataSource.DataSets[j].Name) < 0 && arr.length == 0) {
                        labelArray.push(dataSource.DataSets[j].Name);
                        colors.push(dataSource.DataSets[j].FillColor);
                    }
                    subArray.push(dataSource.DataSets[j].Data[i]);
                }
                if (arr.length == 0) {
                    arr.push(labelArray);
                }
                arr.push(subArray);
            }

            var data = google.visualization.arrayToDataTable(arr);

            var options = {
                legend:{
                    position: 'bottom'
                },
                title: dataSource.Title,
                titlePosition: 'in',
                colors: colors,
                'width': '100%',
                'height': '100%'
            };

            var chart = new google.visualization.BarChart($container[0]);
            chart.draw(data, options);
        },

        this.resize = function ($container, diagram, dataSource) {
            this.refresh($container, diagram, dataSource);
        }
    }

    diagram.prototype = new Diagrams.BaseDiagram();

    return new diagram();
})();

