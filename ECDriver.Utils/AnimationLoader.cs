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


		private static void BeginInput(string aniName, string fileName, int version)
		{
			curId = 1;
		}

		public static void aaa()
		{
			AniInfo ai = new AniInfo();
			ai.aniName = @"attack";
			ai.fileName = @"att1.png";
			ai.id = 1001;
			ai.version = 1000;


			byte[] bys = Common.StructToBytes(ai);
			File.WriteAllBytes("test.txt", bys);

			byte[] br = File.ReadAllBytes("test.txt");

			AniInfo a2 = (AniInfo)Common.BytesToStruct(br, typeof(AniInfo));

			System.Console.WriteLine(a2.aniName + " " + ai.id);
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
	struct AniInfo
	{
		public int id;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string aniName;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string fileName;
		public int version;
		public int frameCount;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode), Serializable]
	struct AniFrame
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
}
