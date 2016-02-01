using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace ECDriver
{
	/// <summary>
	/// 常用工具类
	/// </summary>
	public static class Common
	{
		/// <summary>
		/// 结构体对象转byte数组
		/// </summary>
		/// <param name="structObj">结构体对象</param>
		/// <returns>转换后的数组</returns>
		public static byte[] StructToBytes(object structObj)
		{
			int size = Marshal.SizeOf(structObj);
			byte[] bytes = new byte[size];
			IntPtr structPtr = Marshal.AllocHGlobal(size);
			//将结构体拷到分配好的内存空间
			Marshal.StructureToPtr(structObj, structPtr, false);
			//从内存空间拷贝到byte 数组
			Marshal.Copy(structPtr, bytes, 0, size);
			//释放内存空间
			Marshal.FreeHGlobal(structPtr);
			return bytes;

		}

		/// <summary>
		/// byte数组转结构体对象
		/// </summary>
		/// <param name="bytes">byte数组</param>
		/// <param name="type">转换到的结构体类型</param>
		/// <returns>结构体对象obj</returns>
		public static object BytesToStruct(byte[] bytes, Type type)
		{
			int size = Marshal.SizeOf(type);
			if (size > bytes.Length)
			{
				return null;
			}
			//分配结构体内存空间
			IntPtr structPtr = Marshal.AllocHGlobal(size);
			//将byte数组拷贝到分配好的内存空间
			Marshal.Copy(bytes, 0, structPtr, size);
			//将内存空间转换为目标结构体
			object obj = Marshal.PtrToStructure(structPtr, type);
			//释放内存空间
			Marshal.FreeHGlobal(structPtr);
			return obj;
		}
	}
}
