using System;
using System.Collections.Generic;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace ECDriver
{
	/// <summary>
	/// 默认配置类
	/// </summary>
	public class DefaultConfig
	{
		//游戏相关
		/// <summary>
		/// 游戏帧率
		/// </summary>
		public const int GameFPS = 60;
		/// <summary>
		/// 动画帧间隔
		/// </summary>
		public const float AnimationUpdateMS = 200f;

		//窗口相关
		/// <summary>
		/// 窗口宽度
		/// </summary>
		public const int WndWidth = 840;
		/// <summary>
		/// 窗口高度
		/// </summary>
		public const int WndHeight = 680;
		/// <summary>
		/// 窗口标题
		/// </summary>
		public const string WndTitle = @"wjh的DX引擎";

		//文件路径，名称，后缀相关
		/// <summary>
		/// 日志文件目录
		/// </summary>
		public const string LogFolder = @".\Logs\";
		/// <summary>
		/// 日志文件后缀
		/// </summary>
		public const string LogFileExt = @".log";
		/// <summary>
		/// 资源文件目录
		/// </summary>
		public const string ResFolder = @".\Res\";
		/// <summary>
		/// 图像资源目录
		/// </summary>
		public const string ResImagesFolder = @"Images\";

		//资源相关
		//public const int AnimationCellWidth = 32;       /*动画纹理默认的裁剪宽度*/
		//public const int AnimationCellHeight = 32;      /*动画纹理默认的裁剪高度*/
	}
}
