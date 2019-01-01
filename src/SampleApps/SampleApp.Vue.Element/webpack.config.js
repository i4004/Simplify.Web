const resolve = require('path').resolve
const webpack = require('webpack')
const HtmlWebpackPlugin = require('html-webpack-plugin')
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
			publicPath: isDevBuild ? '/' : publicPath
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
				template: 'ClientApp/index.html',
				favicon: 'ClientApp/favicon.ico'
			})
		],
		resolve:
		{
			alias:
			{
				'~': resolve(__dirname, 'ClientApp')
			},
			extensions: ['.js', '.vue', '.json', '.css']
		}
	}];
}