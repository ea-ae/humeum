/* eslint-disable */
const path = require('path');
const { merge } = require('webpack-merge');
const common = require('./webpack.common.js');

module.exports = merge(common, {
  mode: 'development',
  devtool: 'eval',
  output: {
    filename: '[name].bundle.js',
    path: path.resolve(__dirname, 'dist', 'dev'),
    publicPath: '/',
    clean: true,
  },
  devServer: {
    port: 8080,
    hot: true,
    magicHtml: true,
    client: { progress: true },
    compress: false,
    watchFiles: { paths: ['./src/**/*'] },
    historyApiFallback: {
      index: '/',
      // rewrites: [
      //     { from: /^\/app/, to: '/app.html' },
      //     { from: /./, to: '/404.html' },
      // ],
    },
    proxy: {
      '/api': {
        target: 'http://localhost:7117',
        secure: false,
      },
    },
  },
});
