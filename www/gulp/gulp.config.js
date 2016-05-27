module.exports = function () {

    var rootFolder = '../',
        contentFolder = rootFolder + 'Content/',
        scriptFolder = rootFolder + 'js/',
        libFolder = scriptFolder,
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
                      libFolder + 'angular.min.js',
                      libFolder + 'angular-route.min.js',
                      libFolder + 'angular-resource.min.js',
                      libFolder + 'angular-cookies.js',
                      libFolder + 'angular-messages.min.js',
                      libFolder + 'angular-ui-router.min.js',
                      libFolder + 'ui-sortable-angular.js',
                      libFolder + 'base64.js',
                      libFolder + '/noty/jquery.noty.js',
                      libFolder + '/noty/themes/default.js',
                      libFolder + '/noty/layouts/topRight.js',
                      libFolder + '/noty/layouts/top.js']
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
