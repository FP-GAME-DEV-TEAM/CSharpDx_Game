using System;
using System.Collections.Generic;
using System.Drawing;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace ECDriver
{
	/// <summary>
	/// 动画类
	/// </summary>
	public class Animation
	{
		private Texture texture;
		private LinkedList<AnimationFrame> allAniFrames;
		private AnimationFrame currentFrame;
		private bool isPlaying;
		private bool isLoop;
		private bool isFinished;

		/// <summary>
		/// 新建动画
		/// </summary>
		/// <param name="texture">动画用贴图</param>
		/// <param name="ltFrames">动画所有帧信息的链表</param>
		public Animation(Texture texture, LinkedList<AnimationFrame> ltFrames)
		{
			this.texture = texture;
			this.allAniFrames = ltFrames;
			this.currentFrame = allAniFrames.First == null ? null : allAniFrames.First.Value;
			this.isPlaying = false;
			this.isLoop = false;
			this.isFinished = true;
		}

		private void DisplayCallBack(int n)
		{
			LinkedListNode<AnimationFrame> node = allAniFrames.Find(currentFrame).Next;
			
			if (node != null)
			{
				currentFrame = node.Value;
			}
			else if(isLoop)
			{
				currentFrame = allAniFrames.First.Value;
			}
			else
			{
				isFinished = true;
				TimerUtils.Stop(this.GetHashCode().ToString());
			}
			
		}

		/// <summary>
		/// 播放动画
		/// </summary>
		/// <param name="loop">是否循环播放</param>
		/// <returns></returns>
		public bool StartDisplay(bool loop)
		{
			isLoop = loop;
			isFinished = false;
			isPlaying = true;
			TimerUtils.Start(currentFrame.NextFrameInterval, 
				111, this.GetHashCode().ToString(), DisplayCallBack);
			return true;
		}

		/// <summary>
		/// 停止播放动画
		/// </summary>
		public void StopDisplay()
		{
			if (isPlaying)
			{
				isPlaying = false;
				TimerUtils.Stop(this.GetHashCode().ToString());
			}
		}

		/// <summary>
		/// 绘制动画当前帧
		/// </summary>
		/// <returns>是否正常绘制</returns>
		public bool Draw(Sprite sprite , Vector3 pos)
		{
			AnimationFrame aniFrame = currentFrame;
			sprite.Draw(texture, aniFrame.ClipRect, aniFrame.FrameOffset, pos, Color.White.ToArgb());
			return true;
		}

		#region Getter and Setter

		/// <summary>
		/// 动画用贴图
		/// </summary>
		public Texture Texture
		{
			get
			{
				return texture;
			}
		}

		/// <summary>
		/// 所有的帧信息
		/// </summary>
		public LinkedList<AnimationFrame> AllAniFrames
		{
			get
			{
				return allAniFrames;
			}
		}

		/// <summary>
		/// 当前帧信息
		/// </summary>
		public AnimationFrame CurrentFrame
		{
			get
			{
				return currentFrame;
			}
		}

		/// <summary>
		/// 动画是否正在播放
		/// </summary>
		public bool IsPlaying
		{
			get
			{
				return isPlaying;
			}
		}

		/// <summary>
		/// 动画是否循环播放
		/// </summary>
		public bool IsLoop
		{
			get
			{
				return isLoop;
			}
		}

		/// <summary>
		/// 动画是否播放结束
		/// </summary>
		public bool IsFinished
		{
			get
			{
				return isFinished;
			}
		}

		#endregion
	}
}
