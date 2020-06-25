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
    /// </summary>
    internal class MemoryEngine
    {
        private MessageService messageService = MessageService.GetInstance();
        Mem m = new Mem();

        public string GameManager { get; private set; }
        public string RoundManager { get; private set; }
        public string NetworkManager { get; private set; }

        public MemoryEngine()
        {

        }

        public void startMemEngine()
        {
            var IsRunning = "";
            try
            {
                int iProcID = m.GetProcIdFromName("RainbowSix_Vulkan");
                GameManager = "RainbowSix_Vulkan.exe+0x6713ED8";
                RoundManager = "RainbowSix_Vulkan.exe+0x68E2CF0";
                NetworkManager = "RainbowSix_Vulkan.exe+0x6923130";
                IsRunning = "Vulkan";

                if (iProcID == 0)
                {
                    iProcID = m.GetProcIdFromName("RainbowSix");
                    GameManager = "RainbowSix.exe+0x6713ED8";
                    RoundManager = "RainbowSix.exe+0x6CA0848";
                    NetworkManager = "RainbowSix.exe+0x68E2CF0";
                    IsRunning = "DirectX";
                }

                if (iProcID > 0)
                {
                    m.OpenProcess(iProcID);
                    //label4.Text = "Active";
                    //label4.ForeColor = Color.Green;
                    //timer.Start();
                    messageService.sendStringMessage("R6SCGT_IsRunning", IsRunning);
                }
            }
            catch (Exception) {
                /*timer.Start();*/
                messageService.sendStringMessage("R6SCGT_IsRunning", "Error!");
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
    }
}