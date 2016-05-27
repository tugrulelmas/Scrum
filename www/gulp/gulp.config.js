module.exports = function () {

    var rootFolder = '../',
        contentFolder = rootFolder + 'Content/',
        scriptFolder = rootFolder + 'js/',
        libFolder = scriptFolder,
        packageFolder = rootFolder + 'node_modules/',
        clientAppFolder = rootFolder + 'app/',
        templateFolder = rootFolder + 'Views/';

    var config = {
        css: {
            src: [contentFolder + '*.css']
        },
        js: {
            lib: {
                src: [libFolder + 'jquery.js',
                      libFolder + 'bootstrap.min.js',
                      libFolder + 'jquery-ui.min.js',
                      packageFolder + 'angular/angular.js',
                      packageFolder + 'angular-resource/angular-resource.js',
                      packageFolder + 'angular-cookies/angular-cookies.js',
                      packageFolder + 'angular-messages/angular-messages.js',
                      packageFolder + 'angular-ui-router/release/angular-ui-router.js',
                      libFolder + 'ui-sortable-angular.js',
                      libFolder + 'base64.js',
                      packageFolder + '/noty/js/noty/packaged/jquery.noty.packaged.js'
                    ]
            },
            app: {
                src: [clientAppFolder + '*.js', clientAppFolder + 'directives/*.js', clientAppFolder + 'controllers/*.js', clientAppFolder + 'filters/*.js', clientAppFolder + 'injectors/*.js']
            }
        },
        templates: {
            src: [templateFolder + '**/*.html', '!' + templateFolder + 'shared/index.html'],
            dest: clientAppFolder,
            module: 'abioka'
        },
        index: {
            src: templateFolder + 'shared/index.html',
            dest: rootFolder
        },
        sync: {
            proxy: 'localhost:50501',
            src: [contentFolder + '**/*.*', scriptFolder + "**/*.*"]
        },
        watch: {
            src: [contentFolder + '**/*.*', scriptFolder + "**/*.*", '!' + rootFolder + 'index.html', '!' + clientAppFolder + 'templates.js']
        }
    };

   return config;
};
