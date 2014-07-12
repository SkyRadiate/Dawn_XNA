using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace DawnGame
{
	public class DawnConsole
	{
		public static IntPtr CreateConsole()
		{
			var console = new DawnConsole();
			return console.Hwnd;
		}

		public IntPtr Hwnd { get; private set; }

		public DawnConsole()
		{
			Initialize();
		}

		public void Initialize()
		{
			Hwnd = GetConsoleWindow();

			if (Hwnd != IntPtr.Zero)
			{
				return;
			}

			AllocConsole();
			Hwnd = GetConsoleWindow();
		}

		public static string Title
		{
			get
			{
				StringBuilder title=new StringBuilder(100);
				GetConsoleTitle(title, 100);
				return title.ToString();
			}
			set
			{
				SetConsoleTitle(Title);
			}
		}

		#region Win32

		[DllImport("kernel32")]
		static extern IntPtr GetConsoleWindow();

		[DllImport("kernel32")]
		static extern bool AllocConsole();
		[DllImport("kernel32")]
		static extern bool SetConsoleTitle(string lpConsoleTitle);
		[DllImport("kernel32")]
		static extern bool GetConsoleTitle(StringBuilder text, int size);
		
		#endregion
	}
}
