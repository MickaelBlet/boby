/*
 * Crée par SharpDevelop.
 * Utilisateur: Mickael-Blet
 * Date: 10/06/2013
 * Heure: 13:48
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Runtime.InteropServices;

namespace BOBY_Shulack
{
	/// <summary>
	/// Description of Orverlay.
	/// </summary>
	class Win32
	{
		public const int WS_EX_TRANSPARENT = 0x00000020;
		public const int GWL_EXSTYLE = (-20);

		[DllImport("user32.dll")]
		public static extern int GetWindowLong(IntPtr hwnd, int index);

		[DllImport("user32.dll")]
		public static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

		public static void makeTransparent(IntPtr hwnd)
		{
			// Change the extended window style to include WS_EX_TRANSPARENT
			int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
			Win32.SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
		}

		public static void makeNormal(IntPtr hwnd)
		{
			//Remove the WS_EX_TRANSPARENT flag from the extended window style
			int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
			Win32.SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle & ~WS_EX_TRANSPARENT);
		}
	}
}
