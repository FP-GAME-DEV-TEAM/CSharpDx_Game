using System;
using System.Collections.Generic;


namespace ECDriver
{
	/// <summary>
	/// 中心计时器
	/// </summary>
	public class TimerUtils
	{
		private static Dictionary<string, Clock> mapClock = new Dictionary<string, Clock>();

		/// <summary>
		/// 计时器回调函数(delegate)
		/// </summary>
		/// <param name="count">当前计时器已运行次数</param>
		public delegate void TimerCallBackHandle(int count);

		/// <summary>
		/// 启动计时器
		/// </summary>
		/// <param name="interval">计时器间隔</param>
		/// <param name="count">执行次数</param>
		/// <param name="key">计时器标志</param>
		/// <param name="func">回调函数</param>
		public static bool Start(float interval, int count, string key, TimerCallBackHandle func)
		{
			Clock ck = new Clock();
			ck.Start(interval, count, func);

			if (!mapClock.ContainsKey(key)) mapClock.Add(key, ck);
			else return false;

			return true;
		}

		/// <summary>
		/// 刷新TimerUtils
		/// </summary>
		/// <param name="passT">CPU经过时间</param>
		public static void Update(float passT)
		{
			List<string> keys = new List<string>();
			foreach (var item in mapClock)
			{
				if (item.Value.IsRunning == false)
				{
					keys.Add(item.Key);
				}
				else
				{
					item.Value.Update(passT);
				}
			}

			//遍历搜索到的要删除的计时器
			foreach (var item in keys)
			{
				mapClock.Remove(item);
			}
		}

		/// <summary>
		/// 停止指定key的计时器
		/// </summary>
		/// <param name="key">计时器标志</param>
		public static void Stop(string key)
		{
			if (mapClock.ContainsKey(key))
			{
				mapClock[key].Stop();
			}
		}

		/// <summary>
		/// 初始化全局计时器
		/// </summary>
		public static void Init()
		{
			Tick.Init();
		}

		/// <summary>
		/// 获取CPU经过的时间
		/// </summary>
		/// <returns>距上次调用的时间差</returns>
		public static float CPUTick()
		{
			return Tick.Next();
		}

		/// <summary>
		/// 获取程序运行到此刻的总时间
		/// </summary>
		/// <returns></returns>
		public static float CPUTotalTick()
		{
			return (float)Tick.TotalTick;
		}

		/// <summary>
		/// 获取CPU时间间隔的类
		/// </summary>
		class Tick
		{
			private static DateTime nowTick;
			private static DateTime preTick;
			private static TimeSpan passTick;
			private static double totalTick;

			public static void Init()
			{
				preTick = DateTime.Now;
				totalTick = 0;
			}

			public static float Next()
			{
				nowTick = DateTime.Now;
				passTick = nowTick.Subtract(preTick);
				preTick = nowTick;

				double passMS = passTick.TotalMilliseconds;
				totalTick += passMS;

				return (float)passMS;
			}

			#region Getter and Setter
			public static double TotalTick
			{
				get
				{
					return totalTick;
				}
			}
			#endregion
		}

		/// <summary>
		/// 计时器类
		/// </summary>
		class Clock
		{
			private float interval;
			private float remain;
			private int limitCount;
			private int currentCount;

			private TimerUtils.TimerCallBackHandle callback;

			private bool isRunning;
			private bool isExpired;
			private bool isPaused;

			/// <summary>
			/// 启动计时器
			/// </summary>
			/// <param name="interval">计时器间隔</param>
			/// <param name="count">计时次数</param>
			/// <param name="func">回调函数</param>
			/// <returns>是否启动成功</returns>
			public bool Start(float interval, int count, TimerUtils.TimerCallBackHandle func)
			{
				if (count <= 0 || interval < 0 || func == null) return false;

				if (isRunning || isPaused)
				{
					return false;
				}
				else
				{
					isPaused = false;
					isExpired = false;
					isRunning = true;

					this.interval = interval;
					this.remain = this.interval;
					this.limitCount = count;
					this.currentCount = 0;

					this.callback = func;
				}

				return true;
			}

			/// <summary>
			/// 刷新计时器
			/// </summary>
			/// <param name="passT">CPU经过时间</param>
			/// <returns>刷新后是否到期</returns>
			public bool Update(float passT)
			{
				if (isRunning && !isPaused)
				{
					remain -= passT;
					isExpired = false;

					if (remain <= 0)
					{
						isExpired = true;

						currentCount++;
						callback(currentCount);

						if (currentCount >= limitCount)
						{
							isRunning = false;
						}
						else
						{
							remain = interval;
						}
					}
				}

				return isExpired;
			}

			/// <summary>
			/// 暂停计时器
			/// </summary>
			/// <returns>是否异常</returns>
			public bool Pause()
			{
				if (!isRunning)
				{
					return false;
				}
				else
				{
					isPaused = true;
				}

				return true;
			}

			/// <summary>
			/// 恢复计时器
			/// </summary>
			/// <returns>是否异常</returns>
			public bool Resume()
			{
				if (!isPaused || !isRunning)
				{
					return false;
				}
				else
				{
					isPaused = false;
				}

				return true;
			}

			/// <summary>
			/// 终止计时器
			/// </summary>
			public void Stop()
			{
				isExpired = false;
				isPaused = false;
				isRunning = false;
			}

			/// <summary>
			/// 新建一个空计时器
			/// </summary>
			public Clock()
			{
				this.interval = 0;
				this.remain = 0;
				this.limitCount = 1;
				this.currentCount = 0;

				isExpired = true;
				isPaused = false;
				isRunning = false;
			}

			#region Geter and Setter
			/// <summary>
			/// 计时间隔
			/// </summary>
			public float Interval
			{
				get
				{
					return interval;
				}

				set
				{
					interval = value;
				}
			}

			/// <summary>
			/// 当前计时剩余时间
			/// </summary>
			public float Remain
			{
				get
				{
					return remain;
				}
			}

			/// <summary>
			/// 计时器是否正在运行
			/// </summary>
			public bool IsRunning
			{
				get
				{
					return isRunning;
				}
			}

			/// <summary>
			/// 计时器是否到期，短暂
			/// </summary>
			public bool IsExpired
			{
				get
				{
					return isExpired;
				}
			}

			/// <summary>
			/// 计时器回调函数
			/// </summary>
			public TimerUtils.TimerCallBackHandle Callback
			{
				get
				{
					return callback;
				}

				set
				{
					callback = value;
				}
			}

			/// <summary>
			/// 计时器运行次数
			/// </summary>
			public int LimitCount
			{
				get
				{
					return limitCount;
				}

				set
				{
					limitCount = value;
				}
			}

			/// <summary>
			/// 计时器已运行过的次数
			/// </summary>
			public int CurrentCount
			{
				get
				{
					return currentCount;
				}
			}
			#endregion
		}
	}
}