using System;
using System.Collections.Generic;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.DirectInput;
using Microsoft.DirectX.DirectSound;

namespace ECDriver
{
	/// <summary>
	/// 基础节点类 
	/// 游戏中描述可被显示的类都应该继承此类
	/// 例如：单位 英雄 地图 场景物件等
	/// </summary>
	public abstract class Node : IDrawable
	{
		/// <summary>
		/// 节点精灵
		/// </summary>
		protected Sprite sprite;

		/// <summary>
		/// 新建节点
		/// </summary>
		public Node()
		{
			sprite = null;
			if (Director.getInstance() != null)
			{
				sprite = new Sprite(Director.getInstance().D3dDevice);
			}
		}

		/// <summary>
		/// 绘制节点
		/// </summary>
		/// <returns>是否正常绘制</returns>
		public abstract bool Draw();
	}
}
