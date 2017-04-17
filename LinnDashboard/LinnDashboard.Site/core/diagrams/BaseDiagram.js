var Diagrams = Diagrams || {};

Diagrams.BaseDiagram = function () {
    this.refreshHeader = function ($container, diagram, dataSource) {
        if (diagram.Filters != null) {

            for (var x = 0; x < diagram.Filters.length; x++) {
                var leftPx = 25;
                var $dropdown = $("<div class = 'dropdown " + diagram.Filters[x].Name.toLowerCase() + "'></div>");
                $dropdown.css("left", "25px");
                $dropdown.css("display", "inline-block");
                var $button = $("<button class='btn btn-default dropdown-toggle filter-dropdown-button' type='button' id='dropdownMenu1' data-toggle='dropdown' aria-haspopup='true' aria-expanded='true'>" + diagram.Filters[x].Name + " <span class='caret'></span></button>");
                var $content = $("<ul class='dropdown-menu' aria-labelledby='dropdownMenu1'></ul>");

            
                for (var i = 0; i < diagram.Filters[x].PossibleValues.length; i++) {
                    var className = "filter-selector";
                    if (diagram.Filters[x].SelectedValue == diagram.Filters[x].PossibleValues[i]) {
                        className = "filter-selector selected"
                    }
                    if (diagram.Filters[x].DisabledValues) {
                        var $dependantFilter = $container.find("."+diagram.Filters[x].Dependant.toLowerCase());
                        var $selected = $dependantFilter.find(".selected").text();
                        var disabledValues = diagram.Filters[x].DisabledValues[$selected];
                        if ($.inArray(diagram.Filters[x].PossibleValues[i], disabledValues) > -1) {
                            continue;
                        }
                    }
                    $content.append("<li class='filter-refresh'><a class = '" + className + "' href='#'>" + diagram.Filters[x].PossibleValues[i] + "</a></li>");
                }
                $dropdown.append($button);
                $dropdown.append($content);
                $container.prepend($dropdown);
            }
     }
    }
};