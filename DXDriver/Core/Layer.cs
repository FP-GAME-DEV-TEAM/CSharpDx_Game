using System;
using System.Collections.Generic;

namespace DXDriver.Core
{
	/// <summary>
	/// 场景中的层
	/// </summary>
	public class Layer
	{
		private List<Node> arrNode = null;

		/// <summary>
		/// 新建层
		/// </summary>
		public Layer()
		{
			arrNode = new List<Node>();
		}

		/// <summary>
		/// 添加节点
		/// </summary>
		/// <param name="node">节点</param>
		/// <returns>是否正常添加</returns>
		public bool addNode(Node node)
		{
			bool flag = true;

			if (!arrNode.Contains(node))
			{
				arrNode.Add(node);
			}
			else
			{
				flag = false;
			}

			return flag;
		}

		/// <summary>
		/// 绘制层
		/// </summary>
		/// <returns></returns>
		public bool Draw()
		{
			bool flag = true;
			foreach (var node in arrNode)
			{
				node.Draw();
			}
			return flag;
		}
	}
}
