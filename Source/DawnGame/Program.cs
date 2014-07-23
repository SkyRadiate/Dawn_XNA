using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dawn;

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
/*
#if DEBUG
			Dawn.DGE.Run(game);
#else
			try
			{
				Dawn.DGE.Run(game);
			}
			catch (Exception e)
			{
				DGE.Debug.Error(game, e.ToString(), Dawn.Engine.Define.EngineErrorDetail.Empty());
				throw (e);
			}
#endif
*/
			Dawn.DGE.Run(game);
		}
	}
}
