module.exports = function () {

    var rootFolder = '../',
        contentFolder = rootFolder + 'Content/',
        scriptFolder = rootFolder + 'js/',
        libFolder = scriptFolder,
        packageFolder = rootFolder + 'node_modules/',
        clientAppFolder = rootFolder + 'app/',
        templateFolder = rootFolder + 'Views/',
        buildFolder = rootFolder + 'build';

    var config = {
        environment: {
          build: buildFolder,
          dist: rootFolder + 'dist'
        },
        css: {
            buildSrc: [buildFolder + '/Content/**/*.css'],
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
                  src: [scriptFolder + '**/*.*'],
                  dest: 'js',
                  isBuildResource : true
                },{
                  src: [scriptFolder + 'respond.min.js',
                        scriptFolder + 'html5shiv.js'
                       ],
                  dest: 'scripts'
                },{
                  src: [contentFolder + '*.css',
                        packageFolder + 'bootstrap/dist/css/bootstrap.css',
                        packageFolder + 'font-awesome/css/font-awesome.css',
                       ],
                  dest: 'content',
                  isBuildResource : true
                },{
                  src: [templateFolder + '**/*.*', '!' + templateFolder + 'shared/index.html'],
                  dest: 'Views'
                },{
                  src: [packageFolder + 'jquery/dist/jquery.js',
                        packageFolder + 'bootstrap/dist/js/bootstrap.js',
                        packageFolder + 'jquery-ui/jquery-ui.js',
                        packageFolder + 'angular/angular.js',
                        packageFolder + 'angular-resource/angular-resource.js',
                        packageFolder + 'angular-cookies/angular-cookies.js',
                        packageFolder + 'angular-messages/angular-messages.js',
                        packageFolder + 'angular-ui-router/release/angular-ui-router.js',
                        libFolder + 'ui-sortable-angular.js',
                        libFolder + 'base64.js',
                        packageFolder + '/noty/js/noty/packaged/jquery.noty.packaged.js',
                        libFolder + 'custom.js',
                        libFolder + 'pace.min.js',
                        libFolder + 'alert.js',
                      ],
                    dest: 'lib',
                    isBuildResource: true
                }],
        js: {
            lib: {
                buildSrc: [buildFolder + '/lib/' + 'jquery.js',
                      buildFolder + '/lib/' + 'bootstrap.js',
                      buildFolder + '/lib/' + 'jquery-ui.min.js',
                      buildFolder + '/lib/' + 'angular.js',
                      buildFolder + '/lib/' + 'angular-resource.js',
                      buildFolder + '/lib/' + 'angular-cookies.js',
                      buildFolder + '/lib/' + 'angular-messages.js',
                      buildFolder + '/lib/' + 'angular-ui-router.js',
                      buildFolder + '/lib/' + 'ui-sortable-angular.js',
                      buildFolder + '/lib/' + 'base64.js',
                      buildFolder + '/lib/' + 'jquery.noty.packaged.js',
                      buildFolder + '/lib/' + 'custom.js',
                      buildFolder + '/lib/' + 'pace.min.js',
                      buildFolder + '/lib/' + 'alert.js'
                    ],
            },
            app: {
                src: [clientAppFolder + '**/*.js'],
                buildSrc: [buildFolder + '/app/**/*.js'],
                buildDest: buildFolder + '/app'
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
        watch: {
            src: [contentFolder + '**/*.*', scriptFolder + "**/*.*",  clientAppFolder + "**/*.*", '!' + rootFolder + 'index.html', '!' + clientAppFolder + 'templates.js']
        }
    };

   return config;
};
