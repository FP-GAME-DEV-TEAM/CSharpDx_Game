using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace ECDriver
{
	/// <summary>
	/// 错误报告类,用于报告错误并生成错误日志
	/// </summary>
	public class ErrorReport
	{
		private static void Log(string str)
		{
			string dir = DefaultConfig.LogFolder;
			string ext = DefaultConfig.LogFileExt;
			//使用当前时间作为log文件名
			DateTime dt = DateTime.Now;
			string filename = dt.ToString("yyyyMMdd_hhmmss");

			//目录存在性判定
			if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

			try
			{
				string path = dir + filename + ext;
				FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None);
				StreamWriter sw = new StreamWriter(fs);

				sw.WriteLine("错误描述：" + str);

				sw.WriteLine("错误日期：" + dt.ToString("yyyy年MM月dd日 hh:mm:ss"));

				sw.Dispose();
				fs.Dispose();
				sw.Close();
				fs.Close();
			}
			catch (IOException)
			{
				MessageBox.Show("写入Log日志文件出错", "错误");
			}

		}

		/// <summary>
		/// 使用ErrorReport报告错误并生成错误日志
		/// </summary>
		/// <param name="str">错误详情</param>
		public static void New(string str)
		{
			MessageBox.Show(str,"错误！");
			Log(str);
		}

		/// <summary>
		/// 使用ErrorReport报告错误并生成错误日志
		/// </summary>
		/// <param name="e">产生的Exception</param>
		public static void New(Exception e)
		{
			MessageBox.Show(e.Message, "错误！");
			Log(e.Message);
		}
	}
}
