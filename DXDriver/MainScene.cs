using System;
using System.Collections.Generic;

using DXDriver.Core;

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


			//层添加到场景
			addLayer(l);
			return true;
		}

		public override void Dispose()
		{
			
		}
	}
}
