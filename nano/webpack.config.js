const path = require('path');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const Dotenv = require('dotenv-webpack');
const ObfuscatorPlugin = require('webpack-obfuscator');

module.exports = (env) => {
  return {
    mode: 'production',
    target: 'web',
    devtool: 'source-map',
    context: path.resolve(__dirname, './'),
    resolve: {
      alias: {
        '@app': path.resolve(__dirname, 'src/static/js')
      }
    },
    entry: {
      '/src/static/Login': './src/static/js/pages/Login/router.js',
      '/src/static/Dashboard': './src/static/js/pages/Dashboard/router.js',
      '/src/static/Pedido': './src/static/js/pages/Pedido/router.js',
      '/src/static/PedidoDist': './src/static/js/pages/PedidoDist/router.js',
      '/src/static/PedidoProdutos': './src/static/js/pages/PedidoProdutos/router.js',
      '/src/static/PedidoConfirmar': './src/static/js/pages/PedidoConfirmar/router.js',
    },
    output: {
      globalObject: "this",
      path: path.resolve(__dirname, 'dist')
    },
    module: {
      rules: [{
        test: /\.js$/,
        exclude: /node_modules/,
        loader: 'babel-loader'
      },
      ]
    },
    plugins: [
      new CleanWebpackPlugin(),
      new Dotenv({
        path: env.production == true ? `.env.production` : `.env.dev`
      }),
      new ObfuscatorPlugin()
    ]
  }
};
