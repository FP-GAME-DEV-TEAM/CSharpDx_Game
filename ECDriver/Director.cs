using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.DirectInput;
using Microsoft.DirectX.DirectSound;


namespace ECDriver
{
	/// <summary>
	/// 导演类
	/// </summary>
	public class Director : Form
	{
		private static Director instance = null;

		private Microsoft.DirectX.Direct3D.Device d3dDevice = null;
		private Microsoft.DirectX.DirectInput.Device kbDevice = null;
		private Point mousePos = Point.Empty;
		private Scene currentScene = null;

		private static float PerFrameTick;
		private static float frameTick = 0f;
		private static long totalFrameCount = 0;

		//测试用
		private Matrix matrix1;
		private Matrix matrix2;

		/// <summary>
		/// 导演类
		/// </summary>
		public Director()
		{
			//this.Size = new Size(DefaultConfig.WndWidth, DefaultConfig.WndHeight);
			this.Size = new Size(1024, 720);
			this.Text = DefaultConfig.WndTitle + @" v1.0 开发版";
			this.FormBorderStyle = FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			//this.Icon = new Icon(@"icon.ico"); 

			this.MouseMove += new MouseEventHandler(MouseMoveEvent);
		}

		/// <summary>
		/// 初始导演类
		/// </summary>
		/// <returns>是否初始化成功</returns>
		public bool Init()
		{
			try
			{
				//预设参数
				PresentParameters pp = new PresentParameters();
				pp.Windowed = true;
				pp.SwapEffect = SwapEffect.Discard;

				//创建设备
				d3dDevice = new Microsoft.DirectX.Direct3D.Device(
					0,
					Microsoft.DirectX.Direct3D.DeviceType.Hardware,
					this,
					CreateFlags.SoftwareVertexProcessing,
					pp
				);

				//input设备
				kbDevice = new Microsoft.DirectX.DirectInput.Device(SystemGuid.Keyboard);
				kbDevice.SetCooperativeLevel(this, CooperativeLevelFlags.NonExclusive | CooperativeLevelFlags.Background);
				kbDevice.Acquire();

				//if (instance != null) return false;
				instance = this;

				//设置每帧时间间隔
				PerFrameTick = 1000.0f / DefaultConfig.GameFPS;
				
				return true;
			}
			catch (DirectXException e)
			{
				ErrorReport.New(e);
				return false;
			}


		}

		/// <summary>
		/// 绘制屏幕
		/// </summary>
		public void Render()
		{
			if (d3dDevice == null) return;

			d3dDevice.Clear(ClearFlags.Target, Color.YellowGreen, 1f, 0);

			//当前场景绘制
			if (currentScene != null) currentScene.Draw();

			d3dDevice.Present();

		}

		/// <summary>
		/// 更新帧
		/// </summary>
		/// <param name="passTick"></param>
		public void UpdateFrame(float passTick)
		{

		}

		/// <summary>
		/// 程序主流程
		/// </summary>
		public void Play()
		{
			KeyboardState kbState = kbDevice.GetCurrentKeyboardState();


			Vector3 vec = Vector3.Empty;
			if (kbState[Key.W])
			{
				vec += new Vector3(0, -1, 0);
			}
			if (kbState[Key.A])
			{
				vec += new Vector3(-1, 0, 0);
			}
			if (kbState[Key.S])
			{
				vec += new Vector3(0, 1, 0);
			}
			if (kbState[Key.D])
			{
				vec += new Vector3(1, 0, 0);
			}

			float passTick = TimerUtils.CPUTick();
			if (passTick > 0) TimerUtils.Update(passTick);

			//刷新帧
			frameTick += passTick;
			if (frameTick > PerFrameTick)
			{
				int a = 0;
				while (frameTick > PerFrameTick)
				{
					//=====
					a++;
					//=====
					frameTick -= PerFrameTick;
					totalFrameCount++;
					UpdateFrame(PerFrameTick);
				}
				Render();
			}

		}

		/// <summary>
		/// 切换显示场景
		/// </summary>
		/// <param name="scene">要切换到的场景</param>
		public void PerformScene(Scene scene)
		{
			if (scene.Init())
			{
				if (currentScene != null) currentScene.Dispose();
				currentScene = scene;
			}
		}

		//鼠标移动事件
		private void MouseMoveEvent(object sender, MouseEventArgs e)
		{
			this.mousePos = e.Location;
		}

		/// <summary>
		/// 退出程序
		/// </summary>
		public void Exit()
		{
			currentScene.Dispose();
			d3dDevice.Dispose();
			instance = null;
			this.Dispose();
			this.Close();
		}

		/// <summary>
		/// 窗口重绘事件
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			Render();
		}

		/// <summary>
		/// 获取导演类单例
		/// </summary>
		/// <returns>导演类单例</returns>
		public static Director getInstance() { return instance; }

		#region Getter and Setter
		/// <summary>
		/// 获取D3D设备
		/// </summary>
		public Microsoft.DirectX.Direct3D.Device D3dDevice
		{
			get
			{
				return d3dDevice;
			}
		}

		/// <summary>
		/// DInput设备 用于键盘输入
		/// </summary>
		public Microsoft.DirectX.DirectInput.Device KbDevice
		{
			get
			{
				return kbDevice;
			}
		}
		
		/// <summary>
		/// 获取当前场景
		/// </summary>
		public Scene CurrentScene
		{
			get
			{
				return currentScene;
			}
		}
		#endregion
	}
}
