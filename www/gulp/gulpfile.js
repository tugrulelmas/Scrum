var gulp = require('gulp'),
  config = require('./gulp.config')({ lazy: true }),
  minifyCss = require('gulp-minify-css'),
  uglify = require('gulp-uglify'),
  angularFilesort = require('gulp-angular-filesort'),
  ngAnnotate = require('gulp-ng-annotate'),
  inject = require('gulp-inject'),
  templateCache = require('gulp-angular-templatecache'),
  minifyHtml = require('gulp-minify-html'),
  watch = require('gulp-watch'),
  useref = require('gulp-useref'),
  gulpif = require('gulp-if'),
  rev = require('gulp-rev'),
  revReplace = require('gulp-rev-replace'),
  runSequence = require('run-sequence'),
  addStream = require('add-stream');


var resourceTasks = [];

var createTasks = function(isBuild) {
  config.resources.map(function(resource) {
    var taskName = "resourceTask" + resource.dest;
    if (resourceTasks && resourceTasks.indexOf(taskName) > -1)
      return;

    var destinationFolder = config.environment.build + "/" + resource.dest;
    if (!isBuild && !resource.isBuildResource) {
      destinationFolder = config.environment.dist + "/" + resource.dest;
    }
    resourceTasks.push(taskName);
    gulp.task(taskName.toString(), function() {
      return gulp.src(resource.src) 
        .pipe(gulp.dest(destinationFolder)); 
    });
  });
};

gulp.task('inject', function() {
  var createOptions = function(name) {
    return { name: name, ignorePath: config.environment.build, addRootSlash: false }
  };

  var css = gulp.src(config.css.src)
            .pipe(gulp.dest(config.css.dest));

  var lib = gulp.src(config.js.lib.src)
            .pipe(gulp.dest(config.js.lib.dest));

  var template = gulp.src(config.templates.src)
      .pipe(gulp.dest(config.templates.viewDest))
      .pipe(minifyHtml({ empty: true }))
      .pipe(templateCache({ module: config.templates.module, root: config.templates.root }));

  var app = gulp.src(config.js.app.src)
            .pipe(addStream.obj(template))
            .pipe(ngAnnotate())
            .pipe(gulp.dest(config.js.app.dest))
            .pipe(angularFilesort());

  return gulp.src(config.index.src)
    .pipe(inject(css, createOptions()))
    .pipe(inject(lib, createOptions('lib')))
    .pipe(inject(app, createOptions('app')))
    .pipe(gulp.dest(config.environment.build));
});

gulp.task('default', function() {
  createTasks(true);
  runSequence(resourceTasks, 'inject');

  watch(config.watch.src, function() {
    gulp.start('default');
  });
});

gulp.task('optimize', function() {
  createTasks(false);
  runSequence(resourceTasks, 'inject', 'dist');
});

gulp.task('dist', function(){
  var assets = useref.assets('../');
  var dist = gulp.src(config.environment.build + '/index.html')
    .pipe(assets)
    .pipe(gulpif('*.js', uglify()))
    .pipe(gulpif('*.css', minifyCss()))
    .pipe(rev())
    .pipe(assets.restore())
    .pipe(useref())
    .pipe(revReplace())
    .pipe(gulp.dest(config.environment.dist));
});
