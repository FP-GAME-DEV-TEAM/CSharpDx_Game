using System;
using System.Collections.Generic;
using System.Drawing;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace ECDriver
{
	/// <summary>
	/// 动画帧
	/// </summary>
	public class AnimationFrame
	{
		private Rectangle clipRect;
		private Vector3 frameOffset;
		private float nextFrameInterval;

		/// <summary>
		/// 新建动画帧
		/// </summary>
		/// <param name="rect">裁剪区域</param>
		/// <param name="vec">帧显示偏移</param>
		/// <param name="interval">下一帧间隔时间</param>
		public AnimationFrame(Rectangle rect, Vector3 vec, float interval)
		{
			this.clipRect = rect;
			this.frameOffset = vec;
			this.nextFrameInterval = interval;
		}

		#region Getter and Setter

		/// <summary>
		/// 裁剪区域
		/// </summary>
		public Rectangle ClipRect
		{
			get
			{
				return clipRect;
			}

			set
			{
				clipRect = value;
			}
		}

		/// <summary>
		/// 帧显示偏移
		/// </summary>
		public Vector3 FrameOffset
		{
			get
			{
				return frameOffset;
			}

			set
			{
				frameOffset = value;
			}
		}

		/// <summary>
		/// 与下一帧的时间间隔
		/// </summary>
		public float NextFrameInterval
		{
			get
			{
				return nextFrameInterval;
			}

			set
			{
				nextFrameInterval = value;
			}
		}

		#endregion
	}
}
