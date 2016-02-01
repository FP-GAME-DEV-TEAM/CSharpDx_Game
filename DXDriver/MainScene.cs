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
			Layer l = new Layer();
			Widget w = new Widget();

			w.ani.StartDisplay(true);

			TimerUtils.Start(4000, 1, "s", n => { w.ani.StopDisplay(); });
			
			l.addNode(w);
			addLayer(l);
			return true;
		}

		public override void Dispose()
		{
			
		}
	}
}
