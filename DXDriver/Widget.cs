using System;
using System.Collections.Generic;
using System.Drawing;

using ECDriver;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace DXDriver
{
	public class Widget : Node
	{
		public Animation ani;

		public Widget() : base()
		{
			//包含所有帧的合图
			Texture t = TextureLoader.FromFile(sprite.Device, @"F:\img\TileA1.png", 512, 384, 7, 0, Format.Unknown, Pool.Default, Filter.Linear, Filter.Linear, 0);
			//测试用 手动加帧信息 就是怎么裁剪 还有帧与帧之间的时间间隔
			LinkedList<AnimationFrame> arr = new LinkedList<AnimationFrame>();
			for (int i = 0; i < 7; i++)
			{
				arr.AddLast(new AnimationFrame(new Rectangle(i * 64, 0, 64, 64), Vector3.Empty, 200.6f));
			}
			//新建动画就是用Texture跟所有帧信息 去构造。
			ani = new Animation(t, arr);
		}

		public override bool Draw()
		{
			try
			{
				
				sprite.Begin(SpriteFlags.AlphaBlend);

				ani.Draw(sprite, new Vector3(100, 100, 0));
				
				sprite.End();
			}
			catch (Exception e)
			{
				ErrorReport.New(e);
			}
			

			return true;
		}
	}
}
