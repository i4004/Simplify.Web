const resolve = require('path').resolve
const webpack = require('webpack')
const HtmlWebpackPlugin = require('html-webpack-plugin')
const url = require('url')
const publicPath = ''

module.exports = (env) =>
{
	const isDevBuild = !(env && env.prod);

	return [
	{
		entry:
		{
			vendor: './ClientApp/vendor',
			index: './ClientApp/main.js'
		},
		output:
		{
			path: resolve(__dirname, 'wwwroot/dist'),
			filename: isDevBuild ? '[name].js' : '[name].js?[chunkhash]',
			chunkFilename: '[id].js?[chunkhash]',
			publicPath: isDevBuild ? '/assets/' : publicPath
		},
		module:
		{
			rules: [
			{
				test: /\.vue$/,
				use: ['vue-loader']
			},
			{
				test: /\.js$/,
				use: ['babel-loader'],
				exclude: /node_modules/
			},
			{
				test: /\.css$/,
				use: ['style-loader', 'css-loader', 'postcss-loader']
			},
			{
				test: /\.(png|jpg|jpeg|gif|eot|ttf|woff|woff2|svg|svgz)(\?.+)?$/,
				use: [
				{
					loader: 'url-loader',
					options:
					{
						limit: 10000
					}
				}]
			}]
		},
		plugins: [
			new webpack.optimize.CommonsChunkPlugin(
			{
				names: ['vendor', 'manifest']
			}),
			new HtmlWebpackPlugin(
			{
				template: 'ClientApp/index.html'
			})
		],
		resolve:
		{
			alias:
			{
				'~': resolve(__dirname, 'ClientApp')
			},
			extensions: ['.js', '.vue', '.json', '.css']
		},
		devServer:
		{
			host: '127.0.0.1',
			port: 8010,
			proxy:
			{
				'/api/':
				{
					target: 'http://127.0.0.1:8080',
					changeOrigin: true,
					pathRewrite:
					{
						'^/api': ''
					}
				}
			},
			historyApiFallback:
			{
				index: url.parse(isDevBuild ? '/assets/' : publicPath).pathname
			}
		},
		devtool: isDevBuild ? '#eval-source-map' : '#source-map'
	}];
};