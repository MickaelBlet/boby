using Aion_Game;

namespace BobyScript
{
	public class Script : IBobyScript
	{
		int gCount = 0;
		bool find = false;

		public void OnPlay()
		{
			Console.WriteLine("test");
		}

		public void OnRun()
		{
            if (gCount == 0)
			{
				find = false;
				var users = EntityList.GetEntity(eType.User);
				foreach (var user in users)
				{
					find = true;
					Console.WriteLine(user.Name);			
				}
			}
			else
			{
				Console.Write("|");
			}
			gCount++;
			if (gCount > 10)
			{
				if (find)
					Console.Write("\n");
				gCount = 0;
			}
		}

		public void OnStop()
		{
			Console.WriteLine("Stop");
		}
	}
}