using System;
using System.Collections.Generic;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.DirectInput;
using Microsoft.DirectX.DirectSound;

namespace DXDriver.Core
{
	/// <summary>
	/// 基础节点类 
	/// 游戏中描述可被显示的类都应该继承此类
	/// 例如：单位 英雄 地图 场景物件等
	/// </summary>
	public abstract class Node
	{
		/// <summary>
		/// 节点精灵
		/// </summary>
		protected Sprite sprite;

		/// <summary>
		/// 新建节点
		/// </summary>
		public Node(Microsoft.DirectX.Direct3D.Device d3dDevice)
		{
			sprite = new Sprite(d3dDevice);
		}

		/// <summary>
		/// 绘制节点
		/// </summary>
		/// <returns>是否正常绘制</returns>
		public abstract bool Draw();
	}
}
