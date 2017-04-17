var Diagrams = Diagrams || {};

Diagrams.Grid = (function () {
    var diagram = function() {
        this.refresh = function ($container, diagram, dataSource) {
            if ($container.data('init')) {
                $container.jsGrid("insertItem", dataSource.Rows[dataSource.Rows.length - 1]);
                $container.jsGrid("refresh");
            } else {
                var fieldsArray = [];

                for (var i = 0; i < dataSource.Columns.length; i++) {
                    if (!dataSource.Columns[i].CellStyle) {
                        fieldsArray[i] = {
                            name: dataSource.Columns[i].Name,
                            type: dataSource.Columns[i].Type,
                            width: dataSource.Columns[i].Width,
                            align: dataSource.Columns[i].Align
                        };
                    } else {
                        fieldsArray[i] = {
                            name: dataSource.Columns[i].Name,
                            type: dataSource.Columns[i].Type,
                            width: dataSource.Columns[i].Width,
                            align: dataSource.Columns[i].Align
                        };   
                    }    
                }

                $container.jsGrid({
                    height: "100%",
                    width: "50%",

                    sorting: false,
                    paging: false,

                    data: dataSource.Rows,
                    onItemInserting: function (args) {
                        args.grid.data.unshift(args.item);
                    },
                    onItemInserted: function (args) {
                        args.grid.data.splice(args.grid.data.length-1, 1);
                    },
                    fields: fieldsArray
                });

                $container.data('init', true);
            }
        }

        this.resize = function ($container, diagram, dataSource) {
            this.refresh($container, diagram, dataSource);
        }
    }

    diagram.prototype = new Diagrams.BaseDiagram();

    return new diagram();
})();