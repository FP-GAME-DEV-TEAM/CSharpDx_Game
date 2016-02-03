using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;

using ECDriver;

namespace ECDriver.Utils
{
    public static class AnimationLoader
    {
		private static int curId;
		private static string filename;
		private static List<AniData> arrDatas;

		public static void BeginInput(string name)
		{
			curId = 1;
			filename = name;
			arrDatas = new List<AniData>();
		}

		public static void InputAniDate(AniInfo aniInfo, List<AniFrame> arrFrames)
		{
			AniData aniData = new AniData();
			aniData.aniInfo = aniInfo;
			aniData.arrFrames = arrFrames;
			arrDatas.Add(aniData);
			curId++;
		}

		public static void EndInputAndSave()
		{
			FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
			long startOffset = 0;

			fs.Write(BitConverter.GetBytes(curId), 0, 4);
			startOffset += 4 + arrDatas.Count * Marshal.SizeOf(arrDatas[0].aniInfo);
			for (int i = 0; i < arrDatas.Count; i++)
			{
				AniData data = arrDatas[i];
				data.aniInfo.offset = startOffset;
				startOffset += data.arrFrames.Count * Marshal.SizeOf(data.arrFrames[0]);
				byte[] bys = Common.StructToBytes(data.aniInfo);
				for (int j = 0; j < bys.Length; j++)
				{
					fs.WriteByte(bys[j]);
				}
			}

			for (int i = 0; i < arrDatas.Count; i++)
			{
				AniData data = arrDatas[i];
				
				for (int j = 0; j < data.arrFrames.Count; j++)
				{
					AniFrame frame = data.arrFrames[j];
					byte[] bys = Common.StructToBytes(frame);
					for (int k = 0; k < bys.Length; k++)
					{
						fs.WriteByte(bys[k]);
					}
				}
			}


			fs.Close();
		}

		public static void aaa()
		{
			BeginInput(@"F:\te.txt");

			AniInfo ai = new AniInfo();
			ai.aniName = @"attack";
			ai.fileName = @"att1.png";
			ai.frameCount = 2;
			ai.id = 1001;
			ai.version = 1000;
			ai.offset = 0;

			List<AniFrame> arrFrames = new List<AniFrame>();
			AniFrame af = new AniFrame();
			af.rectX = 0;
			af.rectY = 0;
			af.rectWidth = 32;
			af.rectHeight = 32;
			af.offsetX = 0;
			af.offsetY = 0;
			af.offsetZ = 0;
			af.num = 1;
			af.nextInterval = 1000f;

			arrFrames.Add(af);

			af = new AniFrame();
			af.rectX = 32;
			af.rectY = 0;
			af.rectWidth = 32;
			af.rectHeight = 32;
			af.offsetX = 0;
			af.offsetY = 0;
			af.offsetZ = 0;
			af.num = 2;
			af.nextInterval = 1001f;

			arrFrames.Add(af);


			InputAniDate(ai,arrFrames);
			InputAniDate(ai, arrFrames);
			EndInputAndSave();
			
			//byte[] br = File.ReadAllBytes("test.txt");

			//AniInfo a2 = (AniInfo)Common.BytesToStruct(br, typeof(AniInfo));

			//System.Console.WriteLine(a2.aniName + " " + ai.id);
			//FileStream fs = new FileStream("test.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
			//byte[] bys = StructToBytes(ai);
			//MemoryStream ms = new MemoryStream(bys);
			
			//do
			//{
			//	byte[] b = 
			//} while (true);
			//fs.Dispose();
			//fs.Close(); 
			
		}

		
	}

	[StructLayout(LayoutKind.Sequential,CharSet = CharSet.Unicode),Serializable]
	public struct AniInfo
	{
		public int id;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string aniName;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string fileName;
		public int version;
		public int frameCount;
		public long offset;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode), Serializable]
	public struct AniFrame
	{
		public int num;
		public int rectX;
		public int rectY;
		public int rectWidth;
		public int rectHeight;
		public float offsetX;
		public float offsetY;
		public float offsetZ;
		public float nextInterval;
	}

	public struct AniData
	{
		public AniInfo aniInfo;
		public List<AniFrame> arrFrames;
	}
}
