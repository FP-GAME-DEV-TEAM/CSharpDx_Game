using System;
using System.Collections.Generic;

using ECDriver;

namespace DXDriver
{
	public class MainScene : Scene
	{
		public MainScene() : base()
		{

		}

		public override bool Init()
		{
			//新的层
			Layer l = new Layer();
			//新建一个物件 Widget是继承Node类的
			Widget w = new Widget();
			
			//测试用
			w.ani.StartDisplay(true);

			TimerUtils.Start(4000, 1, "s", n => { w.ani.StopDisplay(); });
			
			//把物件添加到层
			l.addNode(w);
			//层添加到场景
			addLayer(l);
			return true;
		}

		public override void Dispose()
		{
			
		}
	}
}
