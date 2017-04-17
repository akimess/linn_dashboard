var Diagrams = Diagrams || {};

Diagrams.PieChart = (function () {
    var diagram = function() {
        this.refresh = function ($container, diagram, dataSource) {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Label');
            data.addColumn('number', 'Value');
            var colors = [];
            for (var i = 0; i < dataSource.DataSets.length; i++) {
                data.addRow([dataSource.DataSets[i].Label, dataSource.DataSets[i].Value]);
                colors.push(dataSource.DataSets[i].FillColor);
            }

            var options = {
                title: dataSource.Title,
                
                'width': '100%',
                'height': '100%',
                'colors': colors,
                pieHole: 0.4,
                legend: {
                    position: 'bottom'
                }
            };

            var chart = new google.visualization.PieChart($container[0]);
            chart.draw(data, options);
        },

        this.resize = function ($container, diagram, dataSource) {
            this.refresh($container, diagram, dataSource);
        }
    }

    diagram.prototype = new Diagrams.BaseDiagram();

    return new diagram();
})();