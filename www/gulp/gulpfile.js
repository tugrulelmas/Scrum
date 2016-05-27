var gulp = require('gulp'),
    config = require('./gulp.config')({ lazy: true }),
    concat = require('gulp-concat'),
    minifyCss = require('gulp-minify-css'),
    uglify = require('gulp-uglify'),
    angularFilesort = require('gulp-angular-filesort'),
    inject = require('gulp-inject'),
    es = require('event-stream'),
    templateCache = require('gulp-angular-templatecache'),
    minifyHtml = require('gulp-minify-html'),
    watch = require('gulp-watch'),
    useref = require('gulp-useref'),
    gulpif = require('gulp-if'),
    rev = require('gulp-rev'),
    revReplace = require('gulp-rev-replace'),
    browserSync = require('browser-sync');

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
            ignorePath: '../',
            addRootSlash: false
        }
    };

    return gulp.src(config.index.src)
               .pipe(inject(gulp.src(config.css.src), createOptions()))
               .pipe(inject(gulp.src(config.js.lib.src), createOptions('lib')))
               .pipe(inject(gulp.src(config.js.app.src).pipe(angularFilesort()), createOptions('app')))
               .pipe(gulp.dest(config.index.dest));
});


gulp.task('default', ['inject'], function () {
    watch(config.watch.src, function () {
        gulp.start('inject');
    });
});


gulp.task('sync', ['inject'], function () {
    if (browserSync.active) {
        return;
    }

    browserSync({
        files: config.sync.src,
        proxy: config.sync.proxy,
        ghostMode: {
            clicks: true,
            location: false,
            forms: true,
            scroll: true
        },
        injectChanges: true,
        logLevel: 'debug',
        notify: true,
        reloadDelay: 1000
    });

});


gulp.task('optimize', ['inject'], function () {
    var assets = useref.assets('../');

    return gulp.src(config.index.dest + 'index.html')
               .pipe(assets)
               .pipe(gulpif('*.js', uglify()))
               .pipe(gulpif('*.css', minifyCss()))
               .pipe(rev())
               .pipe(assets.restore())
               .pipe(useref())
               .pipe(revReplace())
               .pipe(gulp.dest(config.index.dest));
});
