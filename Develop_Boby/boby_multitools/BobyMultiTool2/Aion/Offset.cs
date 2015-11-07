using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Net;
using System.Threading;

namespace MemoryLib
{
    /// <summary>
    /// Description of Offset.
    /// </summary>
    public class Offset
    {
        public static Dictionary<string, string> offsetList;

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static bool Download(Window main)
        {
            offsetList = new Dictionary<string, string>();
            var response = string.Empty;

            try
            {
                using (WebClient Client = new WebClient())
                {
                    Client.Proxy = null;
                    response = Client.DownloadString(@"http://bobytools.com/offset/version.php");
                }
            }
            catch
            {
                MessageBox.Show(main, "Server not Found", "Error");
                return false;
            }

            var list = response.Split('|');

            foreach (var str in list)
            {
                var tmp = str.Split(':');
                offsetList.Add(tmp[0], tmp[1]);
            }

            return offsetList.Count != 0;
        }

        public static bool Loading(int Base, string version)
        {
            Dictionary<string, long> offset = new Dictionary<string, long>();

            if (!offsetList.ContainsKey(version))
            {
                MessageBox.Show("Your aion version (\"" + version + "\") is not supported.", "Error");
                return false;
            }

            var decode = Base64Decode(offsetList[version]);

            var hashkey = int.Parse(decode.Substring(0, 2));
            var hashlist = decode.Substring(2);

            var decode = "";

            int i = 0;

            foreach (var character in hashlist)
            {
                if (character - hashkey * i > 0)
                    decode += (char)(character - hashkey * i - 1);
                else
                    decode += (char)(126 + (character - hashkey * i) % 126 - 1);
                i++;
            }

            var list = decode.Split(';');

            foreach (var elem in list)
            {
                var spl = decode.Split(':');
                offset[spl[0]] = long.Parse(spl[1]);
            }

            // Game
            Game.Disconnect = offset["Game.Disconnect"];
            Game.Mouse_X = offset["Game.Mouse_X"];
            Game.Mouse_Y = offset["Game.Mouse_Y"];
            Game.Server = offset["Game.Server"];

            // Ability
            Ability.Map = offset["Ability.Map"];
            Ability.Node = offset["Ability.Node"];

            Ability.Id = offset["Ability.Id"];
            Ability.Buffer = offset["Ability.Buffer"];
            Ability.BufferSize = offset["Ability.BufferSize"];
            Ability.CooldownTime = offset["Ability.CooldownTime"];
            Ability.CooldownTimeRemaining = offset["Ability.CooldownTimeRemaining"];

            // Dialog
            Dialog.Array = offset["Dialog.Array"];
            Dialog.ArraySize = offset["Dialog.ArraySize"];

            Dialog.Buffer = offset["Dialog.Buffer"];
            Dialog.BufferSize = offset["Dialog.BufferSize"];
            Dialog.State = offset["Dialog.State"];
            Dialog.Position.Left = offset["Dialog.Position.Left"];
            Dialog.Position.Top = offset["Dialog.Position.Top"];
            Dialog.Size.Width = offset["Dialog.Size.Width"];
            Dialog.Size.Height = offset["Dialog.Size.Height"];
            Dialog.Padding.Left = offset["Dialog.Padding.Left"];
            Dialog.Padding.Top = offset["Dialog.Padding.Top"];
            Dialog.ChildMap = offset["Dialog.ChildMap"];
            Dialog.ChildSize = offset["Dialog.ChildSize"];
            Dialog.HtmlId = offset["Dialog.HtmlId"];

            Chat.Length = offset["Chat.Length"];
            Chat.Input = offset["Chat.Input"];

            Cube.List = offset["Chat.Length"];
            Cube.Id = offset["Chat.Id"];
            Cube.CooldownTimeRemaining = offset["Chat.CooldownTimeRemaining"];
            Cube.Name0 = offset["Chat.Name0"];
            Cube.Name1 = offset["Chat.Name1"];
            Cube.Name2 = offset["Chat.Name2"];

            // Party
            Party.Alliance.Map = offset["Party.Alliance.Map"];
            Party.Alliance.Size = offset["Party.Alliance.Size"];
            Party.Group.Map = offset["Party.Group.Map"];
            Party.Group.Size = offset["Party.Group.Size"];

            Member.Id = offset["Member.Id"];
            Member.Hp_Max = offset["Member.Hp_Max"];
            Member.Hp = offset["Member.Hp"];
            Member.Mp_Max = offset["Member.Mp_Max"];
            Member.Mp = offset["Member.Mp"];
            Member.Fly_Max = offset["Member.Fly_Max"];
            Member.Fly_Time = offset["Member.Fly_Time"];
            Member.Name = offset["Member.Name"];

            // Player
            Player.ID = offset["Player.ID"];
            Player.Name = offset["Player.Name"];
            Player.Guild = offset["Player.Guild"];
            Player.Class = offset["Player.Class"];
            Player.Lvl = offset["Player.Lvl"];
            Player.Race = offset["Player.Race"];

            Player.HP_Max = offset["Player.HP_Max"];
            Player.HP = offset[" Player.HP"];
            Player.MP_Max = offset["Player.MP_Max"];
            Player.MP = offset["Player.MP"];
            Player.ED_Max = offset["Player.ED_Max"];
            Player.ED = offset["Player.ED"];
            Player.Fly_Max = offset["Player.Fly_Max"];
            Player.Fly_Time = offset["Player.Fly_Time"];
            Player.Exp_Max = offset["Player.Exp_Max"];
            Player.Exp = offset["Player.Exp"];

            Player.IsFly = offset["Player.IsFly"];

            Player.Atk_Range = offset["Player.Atk_Range"];
            Player.Atk_Speed = offset["Player.Atk_Speed"];
            Player.Mov_Speed = offset["Player.Mov_Speed"];
            Player.TimeStamp = offset["Player.TimeStamp"];

            Player.IsMove = offset["Player.IsMove"];
            Player.IsJump = offset["Player.IsJump"];

            // Game
            Game.Speed = offset["Game.Speed"];

            // Player
            Player.CamRot_V = offset["Player.CamRot_V"];
            Player.CamRot_H = offset["Player.CamRot_H"];
            Player.FreeCamRot_H = offset["Player.FreeCamRot_H"];
            Player.FreeCamRot = offset["Player.FreeCamRot"];

            Player.X = offset["Player.X"];
            Player.Y = offset["Player.Y"];
            Player.Z = offset["Player.Z"];

            EntityList.Map = offset["EntityList.Map"];
            EntityList.Node = offset["EntityList.Node"];
            EntityList.UserCount = offset["EntityList.UserCount"];

            Entity.Type = offset["Entity.Type"];
            Entity.Status = offset["Entity.Status"];
            Entity.Position = offset["Entity.Position"];

            Entity.Node = offset["Entity.Node"];
            Entity.Attitude = offset["Entity.Attitude"];
            Entity.Id = offset["Entity.Id"];
            Entity.Type_Object = offset["Entity.Type_Object"];
            Entity.Lvl = offset["Entity.Lvl"];
            Entity.Hp_Percent = offset["Entity.Hp_Percent"];
            Entity.Name = offset["Entity.Name"];
            Entity.Type_NPC = offset["Entity.Type_NPC"];
            Entity.Info = offset["Entity.Info"];
            Entity.Guild = offset["Entity.Guild"];
            Entity.Type_Quest = offset["Entity.Type_Quest"];
            Entity.Type_Quest_Progress = offset["Entity.Type_Quest_Progress"];
            Entity.Mouse_Action = offset["Entity.Mouse_Action"];
            Entity.Vehicle = offset["Entity.Vehicle"];
            Entity.Class = offset["Entity.Class"];
            Entity.Stance = offset["Entity.Stance"];
            Entity.TargetId = offset["Entity.TargetId"];
            Entity.WeaponStyle = offset["Entity.WeaponStyle"];
            Entity.Hide = offset["Entity.Hide"];
            Entity.HideSeeing = offset["Entity.HideSeeing"];
            Entity.Atk_Speed = offset["Entity.Atk_Speed"];
            Entity.Mov_Speed = offset["Entity.Mov_Speed"];
            Entity.Rotation = offset["Entity.Rotation"];
            Entity.No_Grav = offset["Entity.No_Grav"];
            Entity.Buff_Count = offset["Entity.Buff_Count"];
            Entity.Buff_Node = offset["Entity.Buff_Node"];
            Entity.Rank = offset["Entity.Rank"];
            Entity.Hp_Max = offset["Entity.Hp_Max"];
            Entity.Hp = offset["Entity.Hp"];
            Entity.IsAttackable = offset["Entity.IsAttackable"];
            Entity.Action = offset["Entity.Action"];
            Entity.No_Skill = offset["Entity.No_Skill"];
            Entity.X = offset["Entity.X"];
            Entity.Y = offset["Entity.Y"];
            Entity.Z = offset["Entity.Z"];

            Extra.PetLoot = offset["Extra.PetLoot"];
            Extra.Where = offset["Extra.Where"];
            Extra.Quickbar = offset["Extra.Quickbar"];

            /*ChainsManager.ArrayStart2 = 0x490;
            ChainsManager.ArrayStart = 0x4C0;

            Chain.AbilityId = 0x3d8;
            Chain.IsElapsed = 0x428; // 0 si skill up
            Chain.Timeout = Chain.IsElapsed + 0x4; // 0 si skill up*/
        }

        public class Game
        {
            public static long Disconnect = 0;
            public static long Mouse_X = 0;
            public static long Mouse_Y = 0;
            public static long Server = 0;
            public static long Speed = 0;
        }

        public class Ability
        {
            public static long Map = 0;
            public static long Node = 0;
            public static long Id = 0;
            public static long Buffer = 0;
            public static long BufferSize = 0;
            public static long CooldownTime = 0;
            public static long CooldownTimeRemaining = 0;
        }

        public class Dialog
        {
            public static long Array = 0;
            public static long ArraySize = 0;
            public static long Buffer = 0;
            public static long BufferSize = 0;
            public static long State = 0;
            public static long ChildMap = 0;
            public static long ChildSize = 0;

            public class Position
            {
                public static long Left = 0;
                public static long Top = 0;
            }
            public class Size
            {
                public static long Width = 0;
                public static long Height = 0;
            }
            public class Padding
            {
                public static long Left = 0;
                public static long Top = 0;
            }

            public static long HtmlId = 0;

        }

        public class Chat
        {
            public static long Length = 0;
            public static long Input = 0;
        }

        public class Cube
        {
            public static long List = 0;
            public static long Id = 0;
            public static long CooldownTimeRemaining = 0;
            public static long Name0 = 0;
            public static long Name1 = 0;
            public static long Name2 = 0;
        }

        public class Party
        {
            public class Alliance
            {
                public static long Map = 0;
                public static long Size = 0;
            }
            public class Group
            {
                public static long Map = 0;
                public static long Size = 0;
            }
        }

        public class Member
        {
            public static long Id = 0;
            public static long Hp_Max = 0;
            public static long Hp = 0;
            public static long Mp_Max = 0;
            public static long Mp = 0;
            public static long Fly_Max = 0;
            public static long Fly_Time = 0;
            public static long Name = 0;
        }

        public class Player
        {
            public static long ID = 0;
            public static long Name = 0;
            public static long Guild = 0;
            public static long Class = 0;
            public static long Lvl = 0;
            public static long Race = 0;
            public static long HP_Max = 0;
            public static long HP = 0;
            public static long MP_Max = 0;
            public static long MP = 0;
            public static long ED_Max = 0;
            public static long ED = 0;
            public static long Fly_Max = 0;
            public static long Fly_Time = 0;
            public static long Exp_Max = 0;
            public static long Exp = 0;
            public static long IsFly = 0;
            public static long Atk_Range = 0;
            public static long Atk_Speed = 0;
            public static long Mov_Speed = 0;
            public static long TimeStamp = 0;
            public static long IsMove = 0;
            public static long IsJump = 0;
            public static long CamRot_V = 0;
            public static long CamRot_H = 0;
            public static long FreeCamRot_H = 0;
            public static long FreeCamRot = 0;
            public static long X = 0;
            public static long Y = 0;
            public static long Z = 0;
        }

        public class EntityList
        {
            public static long Map = 0;
            public static long Node = 0;
            public static long UserCount = 0;
        }

        public class Entity
        {
            public static long Type = 0;
            public static long Status = 0;
            public static long Position = 0;
            public static long Node = 0;
            public static long Attitude = 0;
            public static long Id = 0;
            public static long Type_Object = 0;
            public static long Lvl = 0;
            public static long Hp_Percent = 0;
            public static long Name = 0;
            public static long Type_NPC = 0;
            public static long Info = 0;
            public static long Guild = 0;
            public static long Type_Quest = 0;
            public static long Type_Quest_Progress = 0;
            public static long Mouse_Action = 0;
            public static long Vehicle = 0;
            public static long Class = 0;
            public static long Stance = 0;
            public static long TargetId = 0;
            public static long WeaponStyle = 0;
            public static long Hide = 0;
            public static long HideSeeing = 0;
            public static long Atk_Speed = 0;
            public static long Mov_Speed = 0;
            public static long Rotation = 0;
            public static long No_Grav = 0;
            public static long Buff_Count = 0;
            public static long Buff_Node = 0;
            public static long Rank = 0;
            public static long Hp_Max = 0;
            public static long Hp = 0;
            public static long IsAttackable = 0;
            public static long Action = 0;
            public static long No_Skill = 0;
            public static long X = 0;
            public static long Y = 0;
            public static long Z = 0;
        }

        public class Extra
        {
            public static long PetLoot = 0;
            public static long Where = 0;
            public static long Quickbar = 0;
        }
    }

    public class EnumAion
    {
        public class Gather
        {
            public static Hashtable gather = new Hashtable();
            public Gather()
            {
                gather.Add(400001, 20);
                gather.Add(400002, 55);
                gather.Add(400003, 75);
                gather.Add(400004, 120);
                gather.Add(400005, 180);
                gather.Add(400006, 180);
                gather.Add(400007, 240);
                gather.Add(400008, 280);
                gather.Add(400009, 330);
                gather.Add(400010, 375);
                gather.Add(400011, 75);
                gather.Add(400012, 280);
                gather.Add(400013, 330);
                gather.Add(400014, 375);
                gather.Add(400015, 375);
                gather.Add(400016, 410);
                gather.Add(400017, 455);
                gather.Add(400018, 485);
                gather.Add(400019, 410);
                gather.Add(400020, 455);
                gather.Add(400021, 455);
                gather.Add(400022, 485);
                gather.Add(400023, 499);
                gather.Add(400051, 20);
                gather.Add(400052, 55);
                gather.Add(400053, 75);
                gather.Add(400054, 140);
                gather.Add(400055, 190);
                gather.Add(400056, 230);
                gather.Add(400057, 290);
                gather.Add(400058, 290);
                gather.Add(400059, 335);
                gather.Add(400060, 375);
                gather.Add(400061, 75);
                gather.Add(400062, 140);
                gather.Add(400063, 290);
                gather.Add(400064, 335);
                gather.Add(400065, 375);
                gather.Add(400066, 375);
                gather.Add(400067, 425);
                gather.Add(400068, 440);
                gather.Add(400069, 490);
                gather.Add(400070, 375);
                gather.Add(400071, 425);
                gather.Add(400072, 440);
                gather.Add(400073, 490);
                gather.Add(400101, 35);
                gather.Add(400102, 160);
                gather.Add(400103, 210);
                gather.Add(400104, 320);
                gather.Add(400105, 160);
                gather.Add(400106, 320);
                gather.Add(400107, 370);
                gather.Add(400108, 320);
                gather.Add(400109, 460);
                gather.Add(400110, 460);
                gather.Add(400111, 440);
                gather.Add(400112, 499);
                gather.Add(400151, 35);
                gather.Add(400152, 160);
                gather.Add(400153, 210);
                gather.Add(400154, 320);
                gather.Add(400155, 160);
                gather.Add(400156, 320);
                gather.Add(400157, 320);
                gather.Add(400158, 460);
                gather.Add(400159, 460);
                gather.Add(400160, 460);
                gather.Add(400161, 460);
                gather.Add(400201, 15);
                gather.Add(400202, 15);
                gather.Add(400203, 15);
                gather.Add(400204, 100);
                gather.Add(400205, 100);
                gather.Add(400206, 200);
                gather.Add(400207, 200);
                gather.Add(400208, 300);
                gather.Add(400209, 100);
                gather.Add(400210, 300);
                gather.Add(400211, 360);
                gather.Add(400212, 300);
                gather.Add(400213, 400);
                gather.Add(400214, 499);
                gather.Add(400215, 499);
                gather.Add(400251, 15);
                gather.Add(400252, 15);
                gather.Add(400253, 15);
                gather.Add(400254, 100);
                gather.Add(400255, 100);
                gather.Add(400256, 200);
                gather.Add(400257, 200);
                gather.Add(400258, 300);
                gather.Add(400259, 100);
                gather.Add(400260, 300);
                gather.Add(400261, 400);
                gather.Add(400262, 400);
                gather.Add(400263, 400);
                gather.Add(400264, 499);
                gather.Add(400301, 40);
                gather.Add(400302, 80);
                gather.Add(400303, 60);
                gather.Add(400304, 145);
                gather.Add(400305, 155);
                gather.Add(400306, 185);
                gather.Add(400307, 215);
                gather.Add(400308, 245);
                gather.Add(400309, 280);
                gather.Add(400310, 315);
                gather.Add(400311, 350);
                gather.Add(400312, 380);
                gather.Add(400313, 145);
                gather.Add(400314, 155);
                gather.Add(400315, 185);
                gather.Add(400316, 315);
                gather.Add(400317, 315);
                gather.Add(400318, 350);
                gather.Add(400319, 350);
                gather.Add(400320, 380);
                gather.Add(400321, 380);
                gather.Add(400322, 380);
                gather.Add(400323, 450);
                gather.Add(400324, 420);
                gather.Add(400325, 495);
                gather.Add(400326, 499);
                gather.Add(400327, 499);
                gather.Add(400351, 40);
                gather.Add(400352, 80);
                gather.Add(400353, 60);
                gather.Add(400354, 145);
                gather.Add(400355, 155);
                gather.Add(400356, 185);
                gather.Add(400357, 215);
                gather.Add(400358, 245);
                gather.Add(400359, 280);
                gather.Add(400360, 315);
                gather.Add(400361, 350);
                gather.Add(400362, 380);
                gather.Add(400363, 145);
                gather.Add(400364, 155);
                gather.Add(400365, 185);
                gather.Add(400366, 315);
                gather.Add(400367, 350);
                gather.Add(400368, 350);
                gather.Add(400369, 380);
                gather.Add(400370, 380);
                gather.Add(400371, 380);
                gather.Add(400372, 450);
                gather.Add(400373, 420);
                gather.Add(400374, 495);
                gather.Add(400375, 450);
                gather.Add(400376, 420);
                gather.Add(400377, 450);
                gather.Add(400378, 420);
                gather.Add(400379, 495);
                gather.Add(400401, 30);
                gather.Add(400402, 30);
                gather.Add(400403, 140);
                gather.Add(400404, 140);
                gather.Add(400405, 140);
                gather.Add(400406, 250);
                gather.Add(400407, 250);
                gather.Add(400408, 250);
                gather.Add(400409, 250);
                gather.Add(400410, 345);
                gather.Add(400411, 345);
                gather.Add(400412, 140);
                gather.Add(400413, 345);
                gather.Add(400414, 480);
                gather.Add(400415, 480);
                gather.Add(400416, 480);
                gather.Add(400417, 499);
                gather.Add(400418, 499);
                gather.Add(400451, 30);
                gather.Add(400452, 30);
                gather.Add(400453, 110);
                gather.Add(400454, 110);
                gather.Add(400455, 250);
                gather.Add(400456, 250);
                gather.Add(400457, 340);
                gather.Add(400458, 110);
                gather.Add(400459, 340);
                gather.Add(400460, 410);
                gather.Add(400461, 410);
                gather.Add(400462, 410);
                gather.Add(400463, 410);
                gather.Add(400464, 410);
                gather.Add(400465, 499);
                gather.Add(400501, 25);
                gather.Add(400502, 25);
                gather.Add(400503, 150);
                gather.Add(400504, 150);
                gather.Add(400505, 150);
                gather.Add(400506, 150);
                gather.Add(400507, 235);
                gather.Add(400508, 235);
                gather.Add(400509, 335);
                gather.Add(400510, 150);
                gather.Add(400511, 335);
                gather.Add(400512, 415);
                gather.Add(400513, 410);
                gather.Add(400514, 415);
                gather.Add(400515, 415);
                gather.Add(400516, 499);
                gather.Add(400551, 25);
                gather.Add(400552, 25);
                gather.Add(400553, 120);
                gather.Add(400554, 120);
                gather.Add(400555, 260);
                gather.Add(400556, 260);
                gather.Add(400557, 260);
                gather.Add(400558, 330);
                gather.Add(400559, 330);
                gather.Add(400560, 120);
                gather.Add(400561, 330);
                gather.Add(400562, 470);
                gather.Add(400563, 470);
                gather.Add(400564, 470);
                gather.Add(400601, 1);
                gather.Add(400602, 1);
                gather.Add(400603, 1);
                gather.Add(400604, 45);
                gather.Add(400605, 45);
                gather.Add(400606, 105);
                gather.Add(400607, 105);
                gather.Add(400608, 170);
                gather.Add(400609, 225);
                gather.Add(400610, 270);
                gather.Add(400611, 310);
                gather.Add(400612, 310);
                gather.Add(400613, 365);
                gather.Add(400614, 105);
                gather.Add(400615, 270);
                gather.Add(400616, 310);
                gather.Add(400617, 365);
                gather.Add(400618, 430);
                gather.Add(400619, 470);
                gather.Add(400620, 430);
                gather.Add(400621, 470);
                gather.Add(400622, 499);
                gather.Add(400623, 499);
                gather.Add(400651, 1);
                gather.Add(400652, 1);
                gather.Add(400653, 45);
                gather.Add(400654, 45);
                gather.Add(400655, 130);
                gather.Add(400656, 170);
                gather.Add(400657, 220);
                gather.Add(400658, 270);
                gather.Add(400659, 305);
                gather.Add(400660, 365);
                gather.Add(400661, 130);
                gather.Add(400662, 305);
                gather.Add(400663, 365);
                gather.Add(400664, 445);
                gather.Add(400665, 485);
                gather.Add(400666, 445);
                gather.Add(400667, 485);
                gather.Add(400668, 485);
                gather.Add(400701, 10);
                gather.Add(400702, 10);
                gather.Add(400703, 50);
                gather.Add(400704, 50);
                gather.Add(400705, 90);
                gather.Add(400706, 110);
                gather.Add(400707, 165);
                gather.Add(400708, 220);
                gather.Add(400709, 260);
                gather.Add(400710, 325);
                gather.Add(400711, 355);
                gather.Add(400712, 385);
                gather.Add(400713, 90);
                gather.Add(400714, 325);
                gather.Add(400715, 355);
                gather.Add(400716, 355);
                gather.Add(400717, 385);
                gather.Add(400718, 385);
                gather.Add(400719, 435);
                gather.Add(400720, 490);
                gather.Add(400721, 420);
                gather.Add(400722, 435);
                gather.Add(400723, 490);
                gather.Add(400724, 499);
                gather.Add(400725, 499);
                gather.Add(400751, 10);
                gather.Add(400752, 10);
                gather.Add(400753, 50);
                gather.Add(400754, 50);
                gather.Add(400755, 90);
                gather.Add(400756, 135);
                gather.Add(400757, 180);
                gather.Add(400758, 240);
                gather.Add(400759, 275);
                gather.Add(400760, 275);
                gather.Add(400761, 325);
                gather.Add(400762, 355);
                gather.Add(400763, 390);
                gather.Add(400764, 135);
                gather.Add(400765, 275);
                gather.Add(400766, 355);
                gather.Add(400767, 355);
                gather.Add(400768, 390);
                gather.Add(400769, 430);
                gather.Add(400770, 480);
                gather.Add(400771, 390);
                gather.Add(400772, 430);
                gather.Add(400773, 480);
                gather.Add(400801, 5);
                gather.Add(400802, 65);
                gather.Add(400803, 65);
                gather.Add(400804, 95);
                gather.Add(400805, 95);
                gather.Add(400806, 130);
                gather.Add(400807, 190);
                gather.Add(400808, 230);
                gather.Add(400809, 265);
                gather.Add(400810, 290);
                gather.Add(400811, 445);
                gather.Add(400812, 495);
                gather.Add(400813, 430);
                gather.Add(400814, 445);
                gather.Add(400815, 495);
                gather.Add(400816, 499);
                gather.Add(400851, 5);
                gather.Add(400852, 65);
                gather.Add(400853, 65);
                gather.Add(400854, 95);
                gather.Add(400855, 115);
                gather.Add(400856, 150);
                gather.Add(400857, 255);
                gather.Add(400858, 310);
                gather.Add(400859, 345);
                gather.Add(400860, 310);
                gather.Add(400861, 345);
                gather.Add(400862, 345);
                gather.Add(400863, 345);
                gather.Add(400864, 430);
                gather.Add(400865, 480);
                gather.Add(400866, 430);
                gather.Add(400867, 480);
                gather.Add(400901, 1);
                gather.Add(400902, 85);
                gather.Add(400903, 305);
                gather.Add(400904, 305);
                gather.Add(400905, 440);
                gather.Add(400906, 475);
                gather.Add(400907, 440);
                gather.Add(400908, 475);
                gather.Add(400909, 499);
                gather.Add(400910, 499);
                gather.Add(400951, 1);
                gather.Add(400952, 85);
                gather.Add(400953, 280);
                gather.Add(400954, 85);
                gather.Add(400955, 415);
                gather.Add(400956, 465);
                gather.Add(400957, 415);
                gather.Add(400958, 465);
                gather.Add(401001, 1);
                gather.Add(401002, 1);
                gather.Add(401003, 25);
                gather.Add(401004, 50);
                gather.Add(401005, 75);
                gather.Add(401006, 75);
                gather.Add(401007, 100);
                gather.Add(401008, 100);
                gather.Add(401009, 100);
                gather.Add(401010, 125);
                gather.Add(401011, 150);
                gather.Add(401012, 175);
                gather.Add(401013, 200);
                gather.Add(401014, 200);
                gather.Add(401015, 225);
                gather.Add(401016, 250);
                gather.Add(401017, 275);
                gather.Add(401018, 300);
                gather.Add(401019, 300);
                gather.Add(401020, 325);
                gather.Add(401021, 350);
                gather.Add(401022, 375);
                gather.Add(401023, 125);
                gather.Add(401024, 150);
                gather.Add(401025, 175);
                gather.Add(401026, 225);
                gather.Add(401027, 250);
                gather.Add(401028, 275);
                gather.Add(401029, 325);
                gather.Add(401030, 350);
                gather.Add(401031, 375);
                gather.Add(401032, 225);
                gather.Add(401033, 250);
                gather.Add(401034, 275);
                gather.Add(401035, 325);
                gather.Add(401036, 399);
                gather.Add(401037, 400);
                gather.Add(401038, 425);
                gather.Add(401039, 450);
                gather.Add(401040, 475);
                gather.Add(401041, 400);
                gather.Add(401042, 425);
                gather.Add(401043, 450);
                gather.Add(401044, 375);
                gather.Add(401045, 499);
                gather.Add(401051, 1);
                gather.Add(401052, 1);
                gather.Add(401053, 25);
                gather.Add(401054, 50);
                gather.Add(401055, 75);
                gather.Add(401056, 75);
                gather.Add(401057, 100);
                gather.Add(401058, 100);
                gather.Add(401059, 100);
                gather.Add(401060, 125);
                gather.Add(401061, 150);
                gather.Add(401062, 175);
                gather.Add(401063, 200);
                gather.Add(401064, 200);
                gather.Add(401065, 225);
                gather.Add(401066, 250);
                gather.Add(401067, 275);
                gather.Add(401068, 300);
                gather.Add(401069, 300);
                gather.Add(401070, 325);
                gather.Add(401071, 350);
                gather.Add(401072, 375);
                gather.Add(401073, 399);
                gather.Add(401074, 400);
                gather.Add(401075, 425);
                gather.Add(401076, 450);
                gather.Add(401077, 475);
                gather.Add(401078, 375);
                gather.Add(401079, 400);
                gather.Add(401080, 425);
                gather.Add(401081, 450);
                gather.Add(401082, 475);
                gather.Add(401083, 400);
                gather.Add(401084, 425);
                gather.Add(401085, 450);
                gather.Add(401086, 475);
                gather.Add(401087, 400);
                gather.Add(401101, 100);
                gather.Add(401102, 210);
                gather.Add(401103, 155);
                gather.Add(401104, 235);
                gather.Add(401105, 220);
                gather.Add(401106, 250);
                gather.Add(401107, 225);
                gather.Add(401108, 240);
                gather.Add(401109, 315);
                gather.Add(401110, 380);
                gather.Add(401111, 300);
                gather.Add(401112, 399);
                gather.Add(401113, 400);
                gather.Add(401114, 450);
                gather.Add(401115, 495);
                gather.Add(401116, 480);
                gather.Add(401117, 415);
                gather.Add(401118, 499);
                gather.Add(401119, 499);
                gather.Add(401151, 130);
                gather.Add(401152, 315);
                gather.Add(401153, 400);
                gather.Add(401154, 450);
                gather.Add(401155, 495);
                gather.Add(401156, 410);
                gather.Add(401157, 470);
                gather.Add(401158, 400);
                gather.Add(401159, 400);
                gather.Add(401160, 480);
                gather.Add(401161, 410);
                gather.Add(401162, 400);
                gather.Add(401163, 450);
                gather.Add(401164, 495);
                gather.Add(401165, 420);
                gather.Add(401166, 400);
                gather.Add(401167, 499);
                gather.Add(402001, 400);
                gather.Add(402002, 400);
                gather.Add(402101, 400);
                gather.Add(402102, 400);
                gather.Add(403000, 1);
                gather.Add(403001, 1);
                gather.Add(403002, 1);
                gather.Add(403003, 40);
                gather.Add(403004, 40);
                gather.Add(403005, 40);
                gather.Add(403006, 80);
                gather.Add(403007, 80);
                gather.Add(403008, 80);
                gather.Add(403009, 120);
                gather.Add(403010, 120);
                gather.Add(403011, 120);
                gather.Add(403012, 160);
                gather.Add(403013, 160);
                gather.Add(403014, 160);
                gather.Add(403015, 200);
                gather.Add(403016, 200);
                gather.Add(403017, 200);
                gather.Add(403018, 240);
                gather.Add(403019, 240);
                gather.Add(403020, 240);
                gather.Add(403021, 280);
                gather.Add(403022, 280);
                gather.Add(403023, 280);
                gather.Add(403500, 1);
                gather.Add(403501, 1);
                gather.Add(403502, 1);
                gather.Add(403503, 40);
                gather.Add(403504, 40);
                gather.Add(403505, 40);
                gather.Add(403506, 80);
                gather.Add(403507, 80);
                gather.Add(403508, 80);
                gather.Add(403509, 120);
                gather.Add(403510, 120);
                gather.Add(403511, 120);
                gather.Add(403512, 160);
                gather.Add(403513, 160);
                gather.Add(403514, 160);
                gather.Add(403515, 200);
                gather.Add(403516, 200);
                gather.Add(403517, 200);
                gather.Add(403518, 240);
                gather.Add(403519, 240);
                gather.Add(403520, 240);
                gather.Add(403521, 280);
                gather.Add(403522, 280);
                gather.Add(403523, 280);
                gather.Add(405000, 350);
                gather.Add(405001, 350);
                gather.Add(408001, 400);
                gather.Add(408002, 400);
                gather.Add(409000, 320);
                gather.Add(409001, 300);
                gather.Add(409002, 300);
                gather.Add(409003, 485);
                gather.Add(409004, 490);
                gather.Add(409005, 460);
                gather.Add(409006, 400);
                gather.Add(409007, 450);
                gather.Add(409008, 420);
                gather.Add(409009, 495);
                gather.Add(409010, 435);
                gather.Add(409011, 430);
                gather.Add(409012, 475);
                gather.Add(409013, 350);
                gather.Add(409014, 375);
                gather.Add(409015, 315);
                gather.Add(409016, 350);
                gather.Add(409017, 380);
                gather.Add(409018, 400);
                gather.Add(409019, 400);
                gather.Add(409020, 400);
                gather.Add(409021, 400);
                gather.Add(409022, 375);
                gather.Add(409023, 375);
                gather.Add(409024, 365);
                gather.Add(409025, 365);
                gather.Add(409026, 385);
                gather.Add(409027, 390);
                gather.Add(409028, 415);
                gather.Add(409029, 465);
                gather.Add(409030, 440);
                gather.Add(409031, 475);
                gather.Add(409032, 430);
                gather.Add(409033, 480);
                gather.Add(409034, 445);
                gather.Add(409035, 495);
                gather.Add(409036, 480);
                gather.Add(409037, 490);
                gather.Add(409038, 445);
                gather.Add(409039, 485);
                gather.Add(409040, 430);
                gather.Add(409041, 470);
                gather.Add(409042, 470);
                gather.Add(409043, 415);
                gather.Add(409044, 410);
                gather.Add(409045, 480);
                gather.Add(409046, 410);
                gather.Add(409047, 455);
                gather.Add(409048, 425);
                gather.Add(409049, 440);
                gather.Add(409050, 399);
                gather.Add(409051, 430);
                gather.Add(409052, 480);
                gather.Add(409053, 465);
                gather.Add(409054, 500);
                gather.Add(409055, 500);
                gather.Add(409056, 440);
                gather.Add(409057, 410);
                gather.Add(409058, 455);
                gather.Add(409059, 455);
                gather.Add(409060, 485);
                gather.Add(409061, 425);
                gather.Add(409062, 440);
                gather.Add(409063, 490);
                gather.Add(409064, 415);
                gather.Add(409065, 415);
                gather.Add(409066, 470);
                gather.Add(409067, 470);
                gather.Add(409068, 430);
                gather.Add(409069, 470);
                gather.Add(409070, 445);
                gather.Add(409071, 485);
                gather.Add(409072, 410);
                gather.Add(409073, 455);
                gather.Add(409075, 485);
                gather.Add(409076, 425);
                gather.Add(409077, 440);
                gather.Add(409078, 490);
                gather.Add(409079, 460);
                gather.Add(409081, 400);
                gather.Add(409083, 450);
                gather.Add(409084, 420);
                gather.Add(409087, 495);
                gather.Add(409088, 480);
                gather.Add(409090, 410);
                gather.Add(409092, 415);
                gather.Add(409094, 470);
                gather.Add(409096, 430);
                gather.Add(409097, 470);
                gather.Add(409098, 445);
                gather.Add(409099, 485);
                gather.Add(409101, 435);
                gather.Add(409102, 490);
                gather.Add(409103, 430);
                gather.Add(409104, 480);
                gather.Add(409105, 445);
                gather.Add(409106, 495);
                gather.Add(409107, 430);
                gather.Add(409108, 480);
                gather.Add(409109, 440);
                gather.Add(409110, 475);
                gather.Add(409111, 415);
                gather.Add(409112, 465);
                gather.Add(409113, 400);
                gather.Add(409114, 400);
                gather.Add(409115, 400);
                gather.Add(409116, 1);
                gather.Add(409117, 1);
                gather.Add(409118, 1);
                gather.Add(409119, 40);
                gather.Add(409120, 40);
                gather.Add(409121, 40);
                gather.Add(409122, 80);
                gather.Add(409123, 80);
                gather.Add(409124, 80);
                gather.Add(409125, 120);
                gather.Add(409126, 120);
                gather.Add(409127, 120);
                gather.Add(409128, 160);
                gather.Add(409129, 160);
                gather.Add(409130, 160);
                gather.Add(409131, 200);
                gather.Add(409132, 200);
                gather.Add(409133, 200);
                gather.Add(409134, 240);
                gather.Add(409135, 240);
                gather.Add(409136, 240);
                gather.Add(409137, 280);
                gather.Add(409138, 280);
                gather.Add(409139, 280);
                gather.Add(409140, 1);
                gather.Add(409141, 1);
                gather.Add(409142, 1);
                gather.Add(409143, 40);
                gather.Add(409144, 40);
                gather.Add(409145, 40);
                gather.Add(409146, 80);
                gather.Add(409147, 80);
                gather.Add(409148, 80);
                gather.Add(409149, 120);
                gather.Add(409150, 120);
                gather.Add(409151, 120);
                gather.Add(409152, 160);
                gather.Add(409153, 160);
                gather.Add(409154, 160);
                gather.Add(409155, 200);
                gather.Add(409156, 200);
                gather.Add(409157, 200);
                gather.Add(409158, 240);
                gather.Add(409159, 240);
                gather.Add(409160, 240);
                gather.Add(409161, 280);
                gather.Add(409162, 280);
                gather.Add(409163, 280);
                gather.Add(409164, 40);
                gather.Add(409165, 45);
                gather.Add(409166, 50);
                gather.Add(409167, 55);
                gather.Add(409168, 60);
                gather.Add(409169, 65);
                gather.Add(409170, 75);
                gather.Add(409171, 80);
                gather.Add(409172, 85);
                gather.Add(409173, 90);
                gather.Add(409174, 95);
                gather.Add(409175, 100);
                gather.Add(409176, 105);
                gather.Add(409177, 110);
                gather.Add(409178, 120);
                gather.Add(409179, 130);
                gather.Add(409180, 140);
                gather.Add(409181, 145);
                gather.Add(409182, 150);
                gather.Add(409183, 155);
                gather.Add(409184, 160);
                gather.Add(409185, 165);
                gather.Add(409186, 170);
                gather.Add(409187, 180);
                gather.Add(409188, 185);
                gather.Add(409189, 190);
                gather.Add(409190, 200);
                gather.Add(409191, 210);
                gather.Add(409192, 215);
                gather.Add(409193, 220);
                gather.Add(409194, 225);
                gather.Add(409195, 230);
                gather.Add(409196, 235);
                gather.Add(409197, 240);
                gather.Add(409198, 245);
                gather.Add(409199, 250);
                gather.Add(409200, 260);
                gather.Add(409201, 265);
                gather.Add(409202, 270);
                gather.Add(409203, 280);
                gather.Add(409204, 290);
                gather.Add(409205, 300);
                gather.Add(409206, 50);
                gather.Add(409207, 75);
                gather.Add(409208, 100);
                gather.Add(409209, 125);
                gather.Add(409210, 150);
                gather.Add(409211, 175);
                gather.Add(409212, 200);
                gather.Add(409213, 225);
                gather.Add(409214, 250);
                gather.Add(409215, 275);
                gather.Add(409216, 300);
                gather.Add(409217, 499);
                gather.Add(409218, 499);
                gather.Add(409219, 499);
                gather.Add(409220, 499);
                gather.Add(409221, 499);
                gather.Add(409222, 499);
                gather.Add(409223, 499);
                gather.Add(409224, 499);
                gather.Add(409225, 499);
                gather.Add(409226, 499);
                gather.Add(409227, 499);
                gather.Add(409228, 499);
                gather.Add(409229, 499);
                gather.Add(409230, 499);
                gather.Add(409231, 499);
                gather.Add(409232, 499);
                gather.Add(409233, 499);
                gather.Add(409234, 499);
            }
        }
    }
}
