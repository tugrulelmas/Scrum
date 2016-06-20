module.exports = function () {

    var rootFolder = '../src/',
        contentFolder = rootFolder + 'Content/',
        scriptFolder = rootFolder + 'js/',
        packageFolder = '../node_modules/',
        clientAppFolder = rootFolder + 'app/',
        templateFolder = rootFolder + 'Views/',
        buildFolder = '../build';

    var config = {
        environment: {
          build: buildFolder,
          dist: '../dist'
        },
        resources: [{
                  src: [packageFolder + 'font-awesome/fonts/**/*.*'],
                  dest: 'fonts'
                },{
                  src: [rootFolder + 'images/**/*.*'],
                  dest: 'images'
                },{
                  src: [rootFolder + 'Resources/**/*.*'],
                  dest: 'Resources'
                },{
                  src: [scriptFolder + 'respond.min.js',
                        scriptFolder + 'html5shiv.js'
                       ],
                  dest: 'scripts'
                }],
       css: {
            src: [contentFolder + '*.css',
                  packageFolder + 'bootstrap/dist/css/bootstrap.css',
                  packageFolder + 'font-awesome/css/font-awesome.css',
                 ],
            dest: buildFolder + '/content'
            },
        js: {
            lib: {
                src: [packageFolder + 'jquery/dist/jquery.js',
                      packageFolder + 'bootstrap/dist/js/bootstrap.js',
                      packageFolder + 'jquery-ui-browserify/dist/jquery-ui.js',
                      packageFolder + 'angular/angular.js',
                      packageFolder + 'angular-resource/angular-resource.js',
                      packageFolder + 'angular-cookies/angular-cookies.js',
                      packageFolder + 'angular-messages/angular-messages.js',
                      packageFolder + 'angular-ui-router/release/angular-ui-router.js',
                      scriptFolder + 'ui-sortable-angular.js',
                      scriptFolder + 'base64.js',
                      packageFolder + '/noty/js/noty/packaged/jquery.noty.packaged.js',
                      scriptFolder + 'custom.js',
                      scriptFolder + 'pace.min.js',
                      scriptFolder + 'alert.js'
                    ],
                dest: buildFolder + '/lib'
            },
            app: {
                src: [clientAppFolder + '**/*.js'],
                dest: buildFolder + '/app'
            }
        },
        templates: {
            src: [templateFolder + '**/*.html', '!' + templateFolder + 'shared/index.html'],
            dest: buildFolder + '/app',
            viewDest: buildFolder + '/Views',
            root: 'Views',
            module: 'abioka.router'
        },
        index: {
            src: templateFolder + 'shared/index.html',
            dest: rootFolder
        },
        watch: {
            src: [contentFolder + '**/*.*', scriptFolder + "**/*.*", clientAppFolder + "**/*.*", "!" + clientAppFolder + "/templates.js", templateFolder + '**/*.html']
        }
    };

   return config;
};
