/*
 * Crée par SharpDevelop.
 * Utilisateur: Mickael-Blet
 * Date: 12/07/2013
 * Heure: 21:53
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using AddProcess;
using MemoryLib;

namespace MemoryLib
{
	/// <summary>
	/// Description of SimplyMemory.
	/// </summary>
	public class SplMemory
	{
		#region _ReadMemory
		public static byte ReadByte(long address)
		{
			return Memory.ReadByte(AionProcess.handle,address);
		}
		
		public static short ReadShort(long address)
		{
			return Memory.ReadShort(AionProcess.handle,address);
		}
		
		public static int ReadInt(long address)
		{
			return Memory.ReadInt(AionProcess.handle,address);
		}
		
		public static long ReadLong(long address)
		{
			return Memory.ReadLong(AionProcess.handle,address);
		}
		
		public static float ReadFloat(long address)
		{
			return Memory.ReadFloat(AionProcess.handle,address);
		}
		
		public static double ReadDouble(long address)
		{
			return Memory.ReadDouble(AionProcess.handle,address);
		}
		
		public static string ReadChar(long address, int length)
		{
			return Memory.ReadString(AionProcess.handle,address,length);
		}

		public static string ReadWchar(long address, int length)
		{
			return Memory.ReadString(AionProcess.handle,address,length*2,true);
		}
		#endregion _ReadMemory
		
		#region _WriteMemory
		public static void WriteMemory(long address, byte value)
		{
			Memory.WriteMemory(AionProcess.handle,address,value);
		}
		
		public static void WriteMemory(long address, short value)
		{
			Memory.WriteMemory(AionProcess.handle,address,value);
		}
		
		public static void WriteMemory(long address, int value)
		{
			Memory.WriteMemory(AionProcess.handle,address,value);
		}
		
		public static void WriteMemory(long address, long value)
		{
			Memory.WriteMemory(AionProcess.handle,address,value);
		}
		
		public static void WriteMemory(long address, float value)
		{
			Memory.WriteMemory(AionProcess.handle,address,value);
		}
		
		public static void WriteMemory(long address, double value)
		{
			Memory.WriteMemory(AionProcess.handle,address,value);
		}
		
		public static void WriteChar(long address, string value)
		{
			Memory.WriteMemory(AionProcess.handle,address,value);
		}
		
		public static void WriteWchar(long address, string value, int size)
		{
			byte[] valuebyte = GetBytes(value);
			Memory.WriteMemory(AionProcess.handle,address,valuebyte,size*2);
		}
		#endregion _WriteMemory
		
		static byte[] GetBytes(string str)
		{
			byte[] bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}
	}
}
