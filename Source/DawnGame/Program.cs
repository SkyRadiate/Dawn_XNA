using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DawnGame
{
	class Program
	{
		static void Main(string[] args)
		{
#if DEBUG
			DawnConsole.CreateConsole();
			Console.Title = Dawn.Engine.Define.GameConst.GameTitleName() + " Console";
#endif
			Dawn.Engine.Basic.Game game = new DawnGame();
			Dawn.DGE.Run(game);

		}
	}
}
