using System;
using System.Collections.Generic;
using System.Windows.Forms;

using ECDriver;
using ECDriver.Utils;

namespace DXDriver
{
	class Program
	{
		static void Main(string[] args)
		{
			using (Director director = new Director())
			{
				
				if (!director.Init())
				{
					ErrorReport.New("初始化DirectX失败！");
					return;
				}
				AnimationLoader.aaa();

				director.Show();

				MainScene scene = new MainScene();
				Director.getInstance().PerformScene(scene);

				TimerUtils.Init();
				while (director.Created)
				{
					director.Play();
					Application.DoEvents();
				}
			}
		}
	}
}
