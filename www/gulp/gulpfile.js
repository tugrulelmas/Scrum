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
    runSequence = require('run-sequence');

gulp.task('templates', function () {
    return gulp.src(config.templates.src)
               .pipe(minifyHtml({ empty: true }))
               .pipe(templateCache({ module: config.templates.module }))
               .pipe(gulp.dest(config.templates.dest));
});


gulp.task('inject', ['templates'], function () {
    var createOptions = function (name) {
        return {
            name: name,
            ignorePath: config.environment.build,
            addRootSlash: false
        }
    };

    return gulp.src(config.index.src)
               .pipe(inject(gulp.src(config.css.buildSrc), createOptions()))
               .pipe(inject(gulp.src(config.js.lib.buildSrc), createOptions('lib')))
               .pipe(inject(gulp.src(config.js.app.buildSrc).pipe(angularFilesort()), createOptions('app')))
               .pipe(gulp.dest(config.environment.build));
});

var resourceTasks = [];

var createTasks = function(isBuild){
  config.resources.map(function(resource){
    var taskName = "resourceTask" + resource.dest;
    var destinationFolder = config.environment.build + "/" + resource.dest;
    if(!isBuild && !resource.isBuildResource){
      destinationFolder = config.environment.dist + "/" + resource.dest;
    }
    resourceTasks.push(taskName);
    gulp.task(taskName.toString(), function() {
      return gulp.src(resource.src) 
          .pipe(gulp.dest(destinationFolder)); 
    });
  });
};

gulp.task('createBuildTasks', function(){
  createTasks(true);
  runSequence(resourceTasks, 'annotate', 'inject');
});

gulp.task('createDistTasks', function(){
  createTasks(false);
  runSequence(resourceTasks, 'annotate', 'inject');
});


gulp.task('annotate', function(){
  return gulp.src(config.js.app.src)
        .pipe(ngAnnotate())
        .pipe(gulp.dest(config.js.app.buildDest)); 
});

gulp.task('default', ['createBuildTasks'], function () {
    watch(config.watch.src, function () {
        gulp.start('createBuildTasks');
    });
});

gulp.task('optimize', ['createDistTasks'], function () {
    var assets = useref.assets('../');

    return gulp.src(config.environment.build + '/index.html')
               .pipe(assets)
               .pipe(gulpif('*.js', uglify()))
               .pipe(gulpif('*.css', minifyCss()))
               .pipe(rev())
               .pipe(assets.restore())
               .pipe(useref())
               .pipe(revReplace())
               .pipe(gulp.dest(config.environment.dist));
});
