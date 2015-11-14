using Aion_Game;

namespace BobyScript
{
	public class Script : BobyScript, IBobyScript
	{
		public void OnPlay()
		{
			Console.WriteLine("Play");
            Console.WriteLine(this.index.ToString());
            BobyScriptStop();
            //Chat.Send("/skill " + AbilityList.GetAbility(50006).Name);
            //var ab = AbilityList.GetAbility(4203); //Appel de mecha
            //Console.WriteLine(ab.Node.ToString("X") + "\t" + ab.Id + " " + ab.Name + "\t" + ab.Maintain);
		}

		public void OnRun()
		{
            //AbilityList.GetAbility(3939) //Light of Rejuvenation
            //MessageBox.Show("test");
		}

		public void OnStop()
		{
			Console.WriteLine("Stop");
		}
	}
}