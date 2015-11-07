﻿/*
    Copyright © 2009, AionHacker.net
    All rights reserved.
    http://www.aionhacker.net
    http://www.assembla.com/spaces/AionMemory


    This file is part of AionMemory.

    AionMemory is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    AionMemory is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with AionMemory.  If not, see <http://www.gnu.org/licenses/>.
 */

// Custom Includes
using MemoryLib;

// Standard Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows;

using System.Runtime.InteropServices; // DllImport

namespace AddProcess
{
	/// <summary>
	/// AION process handler.
	/// </summary>
	public class AionProcess
	{
		delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);
		
		[DllImport("user32.dll")]
		static extern bool EnumThreadWindows(uint dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);
		
		#region Structs

		/// <summary>
		/// Contains base addresses for AION's essential modules.
		/// </summary>
		public struct ModuleBase
		{
			public int Game;
			public int Aion;
			public int CryEntitySystem;
			public int Cry3DEngine;
			public string Version_game;

			/// <summary>
			/// Struct instance initializer.
			/// </summary>
			/// <param name="GenerateMembers">Retrieve base addresses and set struct values accordingly if TRUE.</param>
			public ModuleBase(bool GenerateMembers)
			{
				this.Aion = GetModuleBase("aion.bin");
				this.Game = GetModuleBase("Game.dll");
				this.Version_game = "";
				this.CryEntitySystem = GetModuleBase("CryEntitySystem.dll");
				this.Cry3DEngine = GetModuleBase("Cry3DEngine.dll");
			}
			
			public ModuleBase(bool GenerateMembers, int pid)
			{
				this.Aion = GetModuleBase("aion.bin", pid);
				this.Game = GetModuleBase("Game.dll", pid);
				this.Version_game = GetVersion("Game.dll", pid);
				this.CryEntitySystem = GetModuleBase("CryEntitySystem.dll", pid);
				this.Cry3DEngine = GetModuleBase("Cry3DEngine.dll", pid);
			}
		}

		#endregion

		/// <summary>Handle of current AION process.</summary>
		public static IntPtr handle;
		public static IntPtr handle2;
        /// <summary>Contains base addresses for AION's essential modules.</summary>
        public static ModuleBase Modules;
        public static ModuleBase Modules2;
        /// <summary>Handle of current AION window.</summary>
        public static IntPtr whandle;
		public static IntPtr whandle2;
        /// <summary>Handle of current AION window.</summary>
        public static int pid;
        public static int pid2;

        public static List<IntPtr> listhandle;

		/// <summary>
		/// Open current AION process for reading/writing.
		/// </summary>
		/// <returns>TRUE if success.</returns>
		public static bool Open()
		{
			handle = Memory.OpenProcess(Memory.GetProcessIdByProcessName("aion.bin"));
			whandle = Memory.FindWindowByTitle("AION Client");
			if (handle != IntPtr.Zero)
				Modules = new ModuleBase(true);
			return (handle != IntPtr.Zero);
		}
		/// <summary>
		/// Open AION process by pid for reading/writing.
		/// </summary>
		/// <param name="pid">Process ID.</param>
		/// <returns>TRUE if success.</returns>
		public static bool Open(int Pid)
		{
			handle = Memory.OpenProcess(Pid);
			whandle = WHandleFromPid(Pid);
			pid = Pid;
            if (handle != IntPtr.Zero)
            {
                Modules = new ModuleBase(true, Pid);
                SplMemory.SetHanble(handle);
            }
			return (handle != IntPtr.Zero);
		}

        public static bool Open2(int Pid)
        {
            handle2 = Memory.OpenProcess(Pid);
            whandle2 = WHandleFromPid(Pid);
            pid2 = Pid;
            if (handle2 != IntPtr.Zero)
            {
                Modules2 = new ModuleBase(true, Pid);
                SplMemory2.SetHanble(handle2);
            }
            return (handle2 != IntPtr.Zero);
        }
        /// <summary>
        /// Find whandle from pid
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        private static IntPtr WHandleFromPid(int pid)
		{
			IntPtr wHandle = IntPtr.Zero;
			Process[] procs = Process.GetProcesses();
			listhandle = new List<IntPtr>();

			for (int i = 0; i < procs.Length; i++)
			{
				if (procs[i].Id == pid)
				{
					wHandle = procs[i].MainWindowHandle;
					if (wHandle == IntPtr.Zero)
					{
						foreach (ProcessThread pt in procs[i].Threads)
						{
							EnumThreadWindows((uint) pt.Id, new EnumThreadDelegate(EnumThreadCallback), IntPtr.Zero);
							wHandle = listhandle.First();
							break;
						}
					}
					break;
				}
			}
			return wHandle;
		}
		
		static bool EnumThreadCallback(IntPtr hWnd, IntPtr lParam)
		{
			listhandle.Add(hWnd);
			return true;
		}
		/// <summary>
		/// Open AION process by character name for reading/writing.
		/// </summary>
		/// <param name="name">Character name.</param>
		/// <returns>TRUE if success.</returns>

		/// <summary>
		/// Closes handle generated by Open().
		/// </summary>
		/// <returns>TRUE if success.</returns>
		public static bool Close()
		{
			return Memory.CloseHandle(handle);
		}

		/// <summary>
		/// Gets base address of module.
		/// </summary>
		/// <param name="modulename">Module to get base address of.</param>
		/// <returns>Base address if module found, else 0.</returns>
		public static int GetModuleBase(string modulename)
		{

			System.Diagnostics.Process[] HandleP = System.Diagnostics.Process.GetProcessesByName("aion.bin");

			foreach (System.Diagnostics.ProcessModule Module in HandleP[0].Modules)
			{
				if (modulename == Module.ModuleName)
				{
					return Module.BaseAddress.ToInt32();
				}
			}
			return 0;
		}
		
		public static int GetModuleBase(string modulename, int pid)
		{
			
			System.Diagnostics.Process HandleP = System.Diagnostics.Process.GetProcessById(pid);

			foreach (System.Diagnostics.ProcessModule Module in HandleP.Modules)
			{
				if (modulename == Module.ModuleName)
				{
					return Module.BaseAddress.ToInt32();
				}
			}
			return 0;
		}
		
		public static string GetVersion(string modulename, int pid)
		{
			
			System.Diagnostics.Process HandleP = System.Diagnostics.Process.GetProcessById(pid);

			foreach (System.Diagnostics.ProcessModule Module in HandleP.Modules)
			{
				if (modulename == Module.ModuleName)
				{
					return Module.FileVersionInfo.FileVersion.ToString();
				}
			}
			return "";
		}
	}
}
