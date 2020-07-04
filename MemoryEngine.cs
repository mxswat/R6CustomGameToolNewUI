using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Memory;
using System.Threading;
using System.ServiceProcess;

namespace R6S_Custom_Game_Tool
{
    /// <summary>
    /// MemoryEngine contains most of the mememory editing code. 
    /// I believe it's correct to separate the UI code from the Memory editing code, to have a better code readability
    /// More code will be moved here when I have more time
    /// - Mx
    /// </summary>
    internal class MemoryEngine
    {
        private MessageService messageService = MessageService.GetInstance();
        Mem m = new Mem();

        public string GameManager { get; private set; }
        public string RoundManager { get; private set; }
        public string NetworkManager { get; private set; }
        public string LoadoutOffset1 = "508";
        public string LoadoutOffset2 = "0";
        public string LoadoutOffset3 = "A8";
        public string LoadoutOffset4 = "0";
        public string LoadoutOffset5 = "40";
        public string LoadoutOffset6 = "20";
        public string LoadoutOffset7 = "0";
        public string LoadoutOffset8 = "18";
        public string[] PlayerIDs = new string[]
        {
            "0",
            "8",
            "10",
            "18",
            "20",
            "28",
            "30",
            "38",
            "40",
            "48"
        };
        public string[] SlotIDs = new string[]{
            "10", // Primary
            "18"  // Secondary
        };
        public string[] Weapons = { "63840", "63808", "59456", "59584", "60768", "61632", "57184", "58752", "59296", "57408", "58016", "57984", "58592", "54752", "56128", "59008", "55200", "60320", "60384", "55936", "54880", "55552", "54624", "59776", "54656", "59136", "63648", "58784", "57952", "58944", "54944", "62560", "59104", "57120", "56960", "58624", "56064", "61120", "55584", "62208", "56608", "60576", "58112", "58304", "60000", "60032", "56288", "56864", "57824", "60928", "62912", "55168", "58368", "56672", "57312", "62784", "61088", "61024", "60128", "59680", "63680", "57760", "58240", "60064", "56224", "57440", "56736", "55008", "55328", "58464", "58496", "55968", "56352", "57280", "61056", "60224", "63072", "59168", "56576", "59488", "59552", "59968", "57504", "62400", "62304", "57344", "58560", "56416", "60864", "58912", "63040", "63328", "58144", "57248", "60160", "60192", "55488", "57856", "59840", "63552", "55456", "62432", "60672", "59648", "59328", "58848", "55136", "54816", "55616", "55712", "57472", "63616" };
        public string[] Gadgets = { "F8", "118", "148", "1C8", "198", "1F8", "248", "D8", "158", "230", "270", "278", "E0", "F0", "100", "108", "168", "1A8", "1C0", "290", "E8", "170", "190", "1E0", "1E8", "210", "220", "228", "288", "258", "268", "120", "140", "150", "1D8", "160", "188", "1A0", "298", "218", "1F0", "238", "240", "128", "208", "110", "130", "1B0", "280", "180", "200", "250", "138", "1B8" };
        /// <summary>
        /// Weapon Depedant Gadgets, Le Roc, MK17 CQB, OTs-03, CCE SHIELD, C8-SFW, G52-Tactical Shield, CSRX 300
        /// </summary>
        public string[] WeaponsDependant = { "55456", "56128", "56960", "59648", "58592", "60672", "62208" };

        private static MemoryEngine _instance;

        public static MemoryEngine GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MemoryEngine();
            }
            return _instance;
        }

        public EngineStartData startMemEngine()
        {
            var IsRunning = "";
            try
            {
                int iProcID = m.GetProcIdFromName("RainbowSix_Vulkan");
                GameManager = "RainbowSix_Vulkan.exe+0x6560438";
                RoundManager = "RainbowSix_Vulkan.exe+0x68E2CF0";
                NetworkManager = "RainbowSix_Vulkan.exe+0x6923130";
                IsRunning = "Vulkan";

                if (iProcID == 0)
                {
                    // get new iProcID because 0 means no process found
                    iProcID = m.GetProcIdFromName("RainbowSix");
                    GameManager = "RainbowSix.exe+0x6C21B48";
                    RoundManager = "RainbowSix.exe+0x6CAA248";
                    NetworkManager = "RainbowSix.exe+0x5FCFC68";
                    IsRunning = "DirectX";
                }

                if (iProcID > 0)
                {
                    m.OpenProcess(iProcID);
                    //label4.Text = "Active";
                    //label4.ForeColor = Color.Green;
                    //timer.Start();
                    messageService.sendStringMessage("R6SCGT_IsRunning", IsRunning);
                    CheckBattleEyeStatus();
                }
                return new EngineStartData(m, GameManager, RoundManager, NetworkManager);
            }
            catch (Exception)
            {
                /*timer.Start();*/
                messageService.sendStringMessage("R6SCGT_IsRunning", "Error!");
                return null;
            }
        }

        public bool CheckBattleEyeStatus()
        {
            var battleEyeStatus = true;
            try
            {
                var srv = new ServiceController("BEService");
                if (srv.Status != ServiceControllerStatus.Running)
                {
                    //label9.Text = "Battleye:Off";
                    battleEyeStatus = false;
                }
                messageService.sendBoolMessage("BattleyeIsRunning", battleEyeStatus);
                return battleEyeStatus;
            }
            catch
            {
                messageService.sendBoolMessage("BattleyeIsRunning", false);
                return !battleEyeStatus;
            }
        }

        public void setTimerBlocked(bool blockTimer)
        {
            string TimerBlocked = blockTimer ? "0" : "1";
            m.WriteMemory($"{GameManager},4C1", "byte", TimerBlocked);
        }

        public void changeWeapon(string PlayerID, string SlotID, string WeaponID)
        {
            m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},{SlotID},{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", WeaponID);
            m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},{SlotID},{LoadoutOffset6},{LoadoutOffset7},40,20,20,0,18", "int", "0");
        }

        public void giveHands(string PlayerID, string SlotID)
        {
            m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},{SlotID},{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", "0");
        }

        public void randomizeAll()
        {
            Random random = new Random();
            string[] GadgetsNoLaunchers = { "D8", "158", "230", "270", "278", "E8", "170", "190", "1E0", "1E8", "210", "220", "228", "288", "258", "268", "120", "140", "150", "1D8", "160", "188", "1A0", "298", "218", "1F0", "238", "240", "128", "208", "110", "130", "1B0", "280", "180", "200", "250" };
            string[] WeaponsP = { "63840", "63808", "59456", "59584", "60768", "61632", "57184", "58752", "59296", "57408", "58016", "57984", "58592", "54752", "56128", "59008", "55200", "60320", "60384", "55936", "54880", "55552", "54624", "59776", "54656", "59136", "63648", "58784", "57952", "58944", "54944", "62560", "59104", "57120", "56960", "58624", "56064", "61120", "55584", "62208", "56608", "60576", "39712", "58112", "58304", "60000", "60032", "56288", "56864", "57824", "60928", "62912", "55168", "58368", "56672", "57312", "62784", "61088", "61024", "60128", "59680", "63680", "57760", "58240", "60064", "56224", "57440", "56736", "55008", "55328", "58464", "58496", "55968", "56352", "57280", "61056", "60224", "63072", "59168", "56576", "59488", "59552", "59968", "57504", "62400", "62304", "57344", "58560", "56416", "60864", "58912", "63040", "63328", "58144", "57248", "60160", "60192", "55488", "57856", "59840", "63552" };
            string[] WeaponsS = { "63840", "63808", "59456", "59584", "60768", "61632", "57184", "58752", "59296", "57408", "58016", "57984", "58592", "54752", "56128", "59008", "55200", "60320", "60384", "55936", "54880", "55552", "54624", "59776", "54656", "59136", "63648", "58784", "57952", "58944", "54944", "62560", "59104", "57120", "56960", "58624", "56064", "61120", "55584", "62208", "56608", "60576", "39712", "58112", "58304", "60000", "60032", "56288", "56864", "57824", "60928", "62912", "55168", "58368", "56672", "57312", "62784", "61088", "61024", "60128", "59680", "63680", "57760", "58240", "60064", "56224", "57440", "56736", "55008", "55328", "58464", "58496", "55968", "56352", "57280", "61056", "60224", "63072", "59168", "56576", "59488", "59552", "59968", "57504", "62400", "62304", "57344", "58560", "56416", "60864", "58912", "63040", "63328", "58144", "57248", "60160", "60192", "55488", "57856", "59840" };
            try
            {
                for (int j = 0; j < 10; j++)
                {
                    // To lazy to Replace this lines with the actuall functions
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerIDs[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},28,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{m.ReadInt($"{NetworkManager},B0,40,A8,D0,1E0,{GadgetsNoLaunchers[random.Next(GadgetsNoLaunchers.Length)]},38,20,190,A0,40,58,20,0,E0,F8,978")}");
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerIDs[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},38,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{m.ReadInt($"{NetworkManager},B0,40,A8,D0,1E0,{GadgetsNoLaunchers[random.Next(GadgetsNoLaunchers.Length)]},38,20,190,A0,40,58,20,0,E0,F8,978")}");
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerIDs[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},10,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", $"{WeaponsP[random.Next(WeaponsP.Length)]}");
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerIDs[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},10,{LoadoutOffset6},{LoadoutOffset7},40,20,20,0,18", "int", "0");
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerIDs[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},18,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", $"{WeaponsS[random.Next(WeaponsS.Length)]}");
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerIDs[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},18,{LoadoutOffset6},{LoadoutOffset7},40,20,20,0,18", "int", "0");
                }
            }
            catch (Exception) {; }
        }

        public void changeGadget(string PlayerID, string GSlotID, string GadgetID, string WeaponsDependantG)
        {
            switch (GadgetID)
            {
                // Weapon dependant gadgets e.g BB shield, or monty shield
                case "F8":
                case "118":
                case "148":
                case "1C8":
                case "198":
                case "1F8":
                case "248":
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},10,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", WeaponsDependantG);
                    break;
                case "E0":
                    //Zofia Launcher
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},20,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", "54816");
                    break;
                case "F0":
                    //Hibana Launcher
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},20,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", "55136");
                    break;
                case "100":
                    //BlowTorch
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},20,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", "55616");
                    break;
                case "108":
                    //GaraHook
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},20,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", "55712");
                    break;
                case "168":
                    //Stim Pistol
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},20,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", "57472");
                    break;
                case "1A8":
                    //Capitao Crossbow
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},20,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", "58848");
                    break;
                case "1C0":
                    //Ash Launcher
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},20,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", "59328");
                    break;
                case "290":
                    //Mozzie Pest Launcher
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},20,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", "63616");
                    break;
                default:
                    break;
            }
            m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},{GSlotID},{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{m.ReadInt($"{NetworkManager},B0,40,A8,D0,1E0,{GadgetID},38,20,190,A0,40,58,20,0,E0,F8,978")}");
        }
    }

    class EngineStartData
    {
        public Mem Mem;
        public string GameManager;
        public string RoundManager;
        public string NetworkManager;

        public EngineStartData(Mem mem, string gameManager, string roundManager, string networkManager)
        {
            Mem = mem;
            GameManager = gameManager;
            RoundManager = roundManager;
            NetworkManager = networkManager;
        }
    }
}