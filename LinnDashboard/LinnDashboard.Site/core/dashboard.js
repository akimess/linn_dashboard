var Dashboard = (function (jQuery) {
    return function ($container, opts) {
        var self = this;

        var opts = {
            baseUrl: opts.baseUrl,

            switchTabTimeout: opts.switchTabTimeout,
            switchTabLastTime: new Date(),

            $container: $container,

            $headerContainer: $("<div class='header-container'></div>").appendTo($container),
            $bodyContainer: $("<div class='body-container'></div>").appendTo($container)
        }

        self.start = function () {
            $.ajax({
                type: "GET",
                crossDomain: true,
                dataType: "json",
                url: opts.baseUrl + "/api/Dashboard/GetDashboards",
                data: null,
                success: function (dashboards) {
                    for (var i = dashboards.length - 1; i >= 0; i--) {
                        addDashboard(dashboards[i]);
                    }
                }
            });
        }
        
        // events
        opts.$container.click(".header-element button", function (e) {
            var $button = $(e.toElement).closest("button")
            var $header = $button.parent(".header-element");

            var metadata = $header.data("metadata");

            if ($button.hasClass("header-switch") == true) {
                switchDashboard(metadata);
            }

            if ($button.hasClass("header-refresh") == true) {
                refreshDashboard(metadata, true);
            }

            if ($button.hasClass("header-lock") == true) {
                var $subButton = $button.find("i");
                if ($subButton.hasClass("fa-lock")) {
                    opts.$container.find(".header-element").removeClass("locked");

                    $subButton.removeClass("fa-lock");
                    $subButton.addClass("fa-unlock-alt");
                } else if ($subButton.hasClass("fa-unlock-alt")) {
                    opts.$container.find(".header-element").addClass("locked");

                    $subButton.addClass("fa-lock");
                    $subButton.removeClass("fa-unlock-alt");

                    $header.removeClass("locked");

                    switchDashboard(metadata);
                }           
            }
        });

        opts.$container.click(".header-refresh", function (e) {
            var $filter = $(e.toElement);
            if ($filter.hasClass("filter-selector")) {
                var filterName = $filter.parents('.dropdown').find('.filter-dropdown-button').text().trim();
                var filterValue = $filter.text();

                var $visualizationContainer = $filter.parents(".visualization");
                var metadata = $visualizationContainer.data("metadata");

                var filterIndex = findWithAttr(metadata.Diagram.Filters, 'Name', filterName);
                metadata.Diagram.Filters[filterIndex].SelectedValue = filterValue;
                refreshVisualization(metadata, true);
            }
        });

        var resizeTimeout;

        $(window).resize(function () {
            clearTimeout(resizeTimeout);
            resizeTimeout = setTimeout(resize, 250);
        });

        var resize = function () {
            var $dashboard = opts.$bodyContainer.find(".body-element.active");
            $dashboard.children(".visualization").each(function (index, element) {
                var $visualizationContainer = $(element);

                var metadata = $visualizationContainer.data("metadata");
                var dataSource = $visualizationContainer.data("datasource");

                if (Diagrams[metadata.Diagram.Name].resize) {
                    Diagrams[metadata.Diagram.Name].resize($visualizationContainer, metadata.Diagram, dataSource);
                }
            });
        }

        var switchDashboard = function (metadata) {
            opts.switchTabLastTime = new Date();

            opts.$container.find(".header-element").removeClass("active");
            opts.$container.find(".header-element-" + metadata.Id).addClass("active");

            opts.$container.find(".body-element").removeClass("active");
            opts.$container.find(".body-element-" + metadata.Id).addClass("active");

            refreshDashboard(metadata, false);
        };

        var refreshDashboard = function (metadata, forceRefresh) {
            $.each($(".body-element-" + metadata.Id + " .visualization"), function (index, element) {
                var $visualizationContainer = $(element);

                var metadata = $visualizationContainer.data("metadata");

                refreshVisualization(metadata, forceRefresh);

                var dataSource = $visualizationContainer.data("datasource");
            });
        };

        var refreshVisualization = function (metadata, forceRefresh) {
            var $visualizationContainer = opts.$container.find(".visualization-" + metadata.Id);
            if ($visualizationContainer.is(":visible") == true) {
                var isRefresh = forceRefresh;
                
                if (!isRefresh) {
                    if (metadata.Timeout <= 0) {
                        isRefresh = $visualizationContainer.data("datasource") == undefined;
                    } else {
                        isRefresh = metadata.LastTimeout == null || new Date().getTime() - metadata.LastTimeout.getTime() > metadata.Timeout
                    }
                }

                if (isRefresh) {
                    metadata.LastTimeout = new Date();

                    $.ajax({
                        type: "GET",
                        crossDomain: true,
                        dataType: "json",
                        url: opts.baseUrl + "/api/Dashboard/GetDataSource",
                        data: {
                            dataSource: metadata.DataSource.Name,
                            diagram: metadata.Diagram.Name,
                            filters: JSON.stringify(metadata.Diagram.Filters)
                        },
                        success: function (dataSource) {
                            $visualizationContainer.data("datasource", dataSource);

                            var opts = { filters: metadata.Diagram.Filters };

                            Diagrams[metadata.Diagram.Name].refresh($visualizationContainer, metadata.Diagram, dataSource);
                            Diagrams[metadata.Diagram.Name].refreshHeader($visualizationContainer, metadata.Diagram, dataSource);
                        }
                    });
                }
            }
        };

        var addDashboard = function (metadata) {
            var $body = $('<div class="body-element body-element-' + metadata.Id + '"></div>').prependTo(opts.$bodyContainer);
            $body.data("metadata", metadata);

            var $header = $('<div class="header-element header-element-' + metadata.Id + '"></span>').prependTo(opts.$headerContainer);
            $header.append('<button class="header-switch">' + metadata.Name + '</button>'); // switch button
            $header.append('<button class="header-lock"><i class="fa fa-unlock-alt"></i></button>'); // lock button
            $header.append('<button class="header-refresh"><i class="fa fa-refresh"></i></button>'); // refresh button
            
            $header.data("metadata", metadata);

            switchDashboard(metadata);

            for (var i = 0; i < metadata.Visualizations.length; i++) {
                var $visualization = $("<div class='visualization visualization-" + metadata.Visualizations[i].Id + "'></div>");
                $visualization.data("metadata", metadata.Visualizations[i]);
                $visualization.width(metadata.Visualizations[i].Width+"%");
                $visualization.height(metadata.Visualizations[i].Height+"%");
                
                var positionOpts = {
                    left: metadata.Visualizations[i].Left + "%",
                    top: metadata.Visualizations[i].Top + "%"
                };

                $visualization.css(positionOpts);
                $body.append($visualization);
            }            
        }

        function findWithAttr(array, attr, value) {
            for (var i = 0; i < array.length; i += 1) {
                if (array[i][attr] === value) {
                    return i;
                }
            }
        }

        var worker = function () {
            if (new Date().getTime() - opts.switchTabLastTime.getTime() > opts.switchTabTimeout) {
                var $headerElements = opts.$container.find(".header-element:not(.locked)");
                if ($headerElements.length > 1) {
                    var $activeHeaderElement = $headerElements.filter(".active");

                    var $nextHeaderElement = $headerElements.eq($activeHeaderElement.index() + 1);
                    if ($nextHeaderElement.length == 0) {
                        $nextHeaderElement = $headerElements.eq(0);
                    }

                    var nextHeaderMetadata = $nextHeaderElement.data("metadata");

                    switchDashboard(nextHeaderMetadata);
                }
            }

            $.each($(".visualization"), function (index, element) {
                var $visualizationContainer = $(element);

                var metadata = $visualizationContainer.data("metadata");

                refreshVisualization(metadata, false);
            });
            
            setTimeout(worker, 1000);
        };

        worker();
    }
})(jQuery);