using Aion_Game;

namespace BobyScript
{
	public class Script : IBobyScript
	{
		public void OnPlay()
		{
			for (int i = 0; i < 1000; i++)
				Console.Write(i.ToString() + " ");
		}

		public void OnRun()
		{
            Console.Write("0");;
		}

		public void OnStop()
		{
			;
		}
	}
}