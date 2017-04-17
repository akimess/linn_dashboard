var Diagrams = Diagrams || {};

Diagrams.LineChart = (function () {
    var diagram = function() {
        this.refresh = function ($container, diagram, dataSource) {
            var data = new google.visualization.DataTable();

            data.addColumn('datetime', dataSource.XName);
            for (var i = 0; i < dataSource.Lines.length; i++) {
                data.addColumn('number', dataSource.Lines[i].Name)
            }
            var fullData = [];
            var timeData = [];
            for (var i = 0; i < dataSource.Lines[0].Info.Time.length; i++) {
                var dataRow = [new Date(dataSource.Lines[0].Info.Time[i])];
                timeData.push(new Date(dataSource.Lines[0].Info.Time[i]));
                for (var y = 0; y < dataSource.Lines.length; y++){
                    dataRow.push(dataSource.Lines[y].Info.Data[i]);
                }
                fullData.push(dataRow);
            }
            data.addRows(fullData);

            
            

            var options = {
                title: dataSource.Title,
                hAxis: {
                    ticks: timeData
                },
                width:"100%",

                height: "100%",
                legend: {
                    position: 'top'
                },
                vAxis: {
                    gridlines: {
                        color: 'transparent'
                    }
                },
                hAxis: {
                    gridlines: {
                        color: 'transparent'
                    }
                }

            };

            var chart = new google.visualization.LineChart($container[0]);
            chart.draw(data, options);
        },

        this.resize = function ($container, diagram, dataSource) {
            this.refresh($container, diagram, dataSource);
        }
    }

    diagram.prototype = new Diagrams.BaseDiagram();

    return new diagram();
})();