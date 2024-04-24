/// <binding />
"use strict";
var webpack = require('webpack');
var path = require('path');
var extractTextPlugin = require("extract-text-webpack-plugin");
var cleanWebpackPlugin = require('clean-webpack-plugin');
var glob = require("glob-all");


module.exports = {
    verbose: false,
    debug: false,
    entry: {
        "app": glob.sync([
                  "./Scripts/Fanex.Grid.js",
                  "./Scripts/Fanex.Pagination.js",
                  "./app/root/app.js",
                  "./app/root/app.constant.js",
                  "./app/root/**/*.js"]),
        "app-style": glob.sync([
            "./content/site.scss",
            "./scripts/Fanex.Grid.css",
            "./scripts/Fanex.Pagination.css",
            "./app/root/**/*.scss",
            "./app/root/**/*.css",
            "./app/root/**/*.less",
            "./app/root/**/*.css"]),
        "house-win-loss": glob.sync(["./app/house-win-loss/*.js",
            "./app/house-win-loss/**/*.js"],
            { ignore: ["./app/house-win-loss/daily-winloss-2h-soccer-minor/*.js"] }),
        "house-win-loss-style": ["./content/house-win-loss.scss"],
        "daily-winloss-2h-soccer-minor": glob.sync(["./app/house-win-loss/*.js",
            "./app/house-win-loss/**/*.js"],
            {
            ignore: ["./app/house-win-loss/house-win-loss.constant.js",
                    "./app/house-win-loss/tab/pane.service.js",
                    "./app/house-win-loss/casino/*.js",
                    "./app/house-win-loss/metric-gaming/*.js",
                    "./app/house-win-loss/other-sports/*.js",
                    "./app/house-win-loss/parlay/*.js",
                    "./app/house-win-loss/filter/*.js"]
            })
    },
    output: {
        path: path.join(__dirname, 'wwwroot/bundle'),
        filename: "[name].chunk.js",
        publicPath: '/wwwroot/bundle/',
    },
    devtool: 'source-map',
    devServer: {
        contentBase: ".",
        host: "localhost",
        port: 9009
    },
    plugins: initializePlugin(),
    module: {
        loaders: [
            {
                test: /\.js$/,
                loader: 'babel',
                exclude: /node_modules/,
                query: {
                    presets: ['es2015'],
                    compact: false
                }
            },
            {
                test: /\.tsx?$/,
                exclude: /(node_modules|bower_components)/,
                loader: 'ts-loader?configFileName=webpack.tsconfig.json'
            },
            {
                test: /\.scss$|\.css$/,
                loader: extractTextPlugin.extract("style-loader", "css-loader!sass-loader")
            },
             {
                 test: /\.less$/,
                 loader: extractTextPlugin.extract("style-loader", "css-loader!less-loader")
             },
            {
                test: /\.jpe?g$|\.gif$|\.png$|\.svg$|\.woff$|\.woff2$|\.ttf$|\.eot$/,
                loader: "url?limit=100000"
            },
            {
                test: require.resolve("jquery"),
                loader: "expose?$!expose?jQuery"
            }
        ]
    }
};

function initializePlugin() {
    var plugins = [
    new extractTextPlugin('[name].css', { allChunks: true }),
    //new cleanWebpackPlugin(['bundle'], {
    //    root: __dirname,
    //    verbose: true,
    //    dry: false,
    //    exclude: []
    //}),
    new webpack.optimize.CommonsChunkPlugin({
        name: 'lib',
        minChunks: 2
    })
    ];

    var environemnt = process.env.NODE_ENV || 'development';
    if (environemnt !== 'development') {
        var unify = new webpack.optimize.UglifyJsPlugin({
            compress: {
                warnings: false
            }
        });
        plugins.push(unify);
    }


    return plugins;
}