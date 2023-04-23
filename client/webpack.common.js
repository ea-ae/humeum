/* eslint-disable */
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const path = require('path');

const staticFilenames = ['404'];
const staticPages = staticFilenames.map(name => {
    return new HtmlWebpackPlugin({
      template: `src/static/${name}.html`,
      filename: `${name}.html`,
      chunks: ['static'],
    })
});

module.exports = {
    entry: {
        index: {
            import: './src/index/index.tsx',
        },
    },
    resolve: {
        extensions: ['.tsx', '.ts', '.js'],
    },
    plugins: [
        new HtmlWebpackPlugin({
            template: 'src/index/index.html',
            filename: 'index.html',
            chunks: ['index'],
        }),
        new MiniCssExtractPlugin({
            filename: '[name].[contenthash].css',
        }),
    ].concat(staticPages),
    module: { rules: [
        {
            test: /\.(jsx?|tsx?)$/,
            use: [{
                loader: 'babel-loader',
                options: {
                    presets: [
                        '@babel/preset-env',
                        ['@babel/preset-react', { runtime: 'automatic' }],
                        '@babel/preset-typescript',
                    ],
                },
            },],
        },
        {
            test:/\.css$/,
            include: [
                path.resolve(__dirname, 'src'),
            ],
            use: [
                {
                    loader: MiniCssExtractPlugin.loader,
                    options: { esModule: false },
                },
                'css-loader',
                'postcss-loader',
            ],
        },
    ]},
};
