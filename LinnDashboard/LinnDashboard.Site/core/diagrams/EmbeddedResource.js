var Diagrams = Diagrams || {};

Diagrams.EmbeddedResource = (function () {
    var diagram = function() {
        this.refresh = function ($container, diagram, dataSource) {
            $container.html(dataSource.Content);
        };

        this.resize = function ($container, diagram, dataSource) {

        };
    }

    diagram.prototype = new Diagrams.BaseDiagram();

    return new diagram();
})();

/*
function isScriptFileIncluded(url) {
    var scripts = document.getElementsByTagName("script");
    for (var i = 0; i < scripts.length; i++) {
        if (scripts[i].src.endsWith(url) == true)
            return true;
    }

    return false;
};

function includeScriptFile(url, callback) {
    if (isScriptFileIncluded(url) == false) {
        var script = document.createElement('script');
        script.type = 'text/javascript';
        script.src = url;

        script.isLoaded = false;

        if ("onreadystatechange" in script) {
            script.onreadystatechange = function () {
                if (this.readyState === 'loaded' || this.readyState === 'complete') {
                    this.onreadystatechange = null;

                    callback();
                }
            }
        } else {
            script.onload = script.onerror = function () {
                this.onload = null;
                this.onerror = null;

                callback();
            }
        }

        document.body.appendChild(script);
    }
};

function includeScriptFiles(urls, callback) {
    if (urls && urls.length) {
        var count = urls.length;

        for (var i = 0; i < urls.length; i++) {
            includeScriptFile(urls[i], function () {
                if (--count == 0) {
                    callback();
                }
            });
        }
    } else {
        callback();
    }
};
*/