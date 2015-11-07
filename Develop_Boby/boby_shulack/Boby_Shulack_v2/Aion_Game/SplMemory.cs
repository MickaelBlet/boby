/*
 * Crée par SharpDevelop.
 * Utilisateur: Mickael-Blet
 * Date: 12/07/2013
 * Heure: 21:53
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using MemoryLib;

namespace NS_Aion_Game
{
	public class SplMemory
    {
        private static IntPtr st_hanble = IntPtr.Zero;

        public static void SetHanble(IntPtr ptr)
        {
            st_hanble = ptr;
        }
		
		#region _ReadMemory
		public static byte ReadByte(long address)
		{
			return Memory.ReadByte(st_hanble, address);
		}
		
		public static short ReadShort(long address)
		{
            return Memory.ReadShort(st_hanble, address);
		}
		
		public static int ReadInt(long address)
		{
            return Memory.ReadInt(st_hanble, address);
		}
		
		public static long ReadLong(long address)
		{
            return Memory.ReadLong(st_hanble, address);
		}
		
		public static float ReadFloat(long address)
		{
            return Memory.ReadFloat(st_hanble, address);
		}
		
		public static double ReadDouble(long address)
		{
            return Memory.ReadDouble(st_hanble, address);
		}
		
		public static string ReadChar(long address, int length)
		{
            return Memory.ReadString(st_hanble, address, length);
		}

		public static string ReadWchar(long address, int length)
		{
            return Memory.ReadString(st_hanble, address, length * 2, true);
		}
		#endregion _ReadMemory
		
		#region _WriteMemory
		public static void WriteMemory(long address, byte value)
		{
            Memory.WriteMemory(st_hanble, address, value);
		}
		
		public static void WriteMemory(long address, short value)
		{
            Memory.WriteMemory(st_hanble, address, value);
		}
		
		public static void WriteMemory(long address, int value)
		{
            Memory.WriteMemory(st_hanble, address, value);
		}
		
		public static void WriteMemory(long address, long value)
		{
            Memory.WriteMemory(st_hanble, address, value);
		}
		
		public static void WriteMemory(long address, float value)
		{
            Memory.WriteMemory(st_hanble, address, value);
		}
		
		public static void WriteMemory(long address, double value)
		{
            Memory.WriteMemory(st_hanble, address, value);
		}
		
		public static void WriteChar(long address, string value)
		{
            Memory.WriteMemory(st_hanble, address, value);
		}
		
		public static void WriteWchar(long address, string value, int size)
		{
			byte[] valuebyte = GetBytes(value);
            Memory.WriteMemory(st_hanble, address, valuebyte, size * 2);
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
