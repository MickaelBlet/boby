using Aion_Game;

namespace BobyScript
{
    public class Script : IBobyScript
    {
    	string step;
    	int time;

        public void OnPlay()
        {
            step = "Start";
            time = 0;
        }
        public void OnRun()
        {
            SteelRake();
        }
        public void OnStop()
        {
        	;
        }

        private void SteelRake()
        {
        	Entity shugo = null;
        	if (Player.Race == eRace.Elys)
        		shugo = EntityList.GetEntity("Uikinerk");
        	else
        		shugo = EntityList.GetEntity("Roikinerk");
        	if (shugo == null)
        		Instance();
        	else
        		Speak(shugo);
        }

        private void Speak(Entity shugo)
        {
        	Dialog.GetDialog("quest_dialog").IsOpen = false;
        	Dialog.GetDialog("quest_summary_dialog").IsOpen = false;
        	Dialog.GetDialog("quest_summary_other_dialog").IsOpen = false;

        	if (Dialog.GetDialog("dlg_dialog/html_view/1") != Dialog.Empty)
        		Dialog.GetDialog("dlg_dialog").IsOpen = true;

        	if (Dialog.GetDialog("dlg_dialog").IsOpen != true)
        	{
        		shugo.Select();
        		Player.Speak();
        		return;
        	}

        	if (Dialog.GetDialog("dlg_dialog/accept").IsVisible)
        	{
        		Dialog.GetDialog("dlg_dialog/html_view/0/plus").Click();
        		Thread.Sleep(100);
        		Dialog.GetDialog("dlg_dialog/accept").Click();
        	}
        	else
        	{
        		Dialog.GetDialog("dlg_dialog/html_view/1").Click();
        	}

        	step = "Start";
        }

        private void Instance()
        {
        	if (Time() < time)
        		return;
        	switch(step)
        	{
        		case "Start":
        			Player.X += 0.7f;
        			time = Time() + 100;
        			step = "WallHack";
        			break;
        		case "WallHack":
	       			Player.Z -= 0.3f;
	      			Thread.Sleep(120);
	       			Player.X += 0.3f;
	      			Thread.Sleep(120);
	       			Player.Z -= 0.3f;
	       			Thread.Sleep(120);
	       			Player.Y += 0.3f;
	       			if (Player.Z > 883.5f)
	       				return;

	       			break;
        	}
        }
    }
}