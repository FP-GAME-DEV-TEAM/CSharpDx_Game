using System;
using System.Collections.Generic;
using System.Drawing;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.DirectInput;
using Microsoft.DirectX.DirectSound;

namespace DXDriver.Core
{
	/// <summary>
	/// 场景类
	/// </summary>
	public abstract class Scene : IDisposable
	{
		protected List<Layer> arrLayer = null;

		/// <summary>
		/// 初始化场景
		/// </summary>
		public Scene()
		{
			arrLayer = new List<Layer>();
		}

		/// <summary>
		/// 向场景中添加层
		/// </summary>
		/// <param name="layer">指定要添加的层</param>
		/// <returns>是否正常添加</returns>
		public bool addLayer(Layer layer)
		{
			bool flag = true;

			if (!arrLayer.Contains(layer))
			{
				arrLayer.Add(layer);
			}
			else
			{
				flag = false;
			}

			return flag;
		}

		/// <summary>
		/// 绘制场景
		/// </summary>
		/// <returns>是否正常绘制</returns>
		public bool Draw()
		{
			bool flag = true;
			Microsoft.DirectX.Direct3D.Device d3dDevice = Director.getInstance().D3dDevice;
			if (d3dDevice != null)
			{
				d3dDevice.BeginScene();
				foreach (var layer in arrLayer)
				{
					layer.Draw();
				}
				d3dDevice.EndScene();
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		/// <summary>
		/// 初始化方法 必须实现
		/// </summary>
		/// <returns>是否正常执行</returns>
		public abstract bool Init();

		/// <summary>
		/// 销毁场景的方法 必须实现
		/// </summary>
		public abstract void Dispose();

		#region Getter and Setter
		/// <summary>
		/// 该场景中所有的层
		/// </summary>
		public List<Layer> Layers
		{
			get
			{
				return arrLayer;
			}
		}
		#endregion
	}
}
