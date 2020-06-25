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
        public MemoryEngine()
        {
            
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
            catch {
                messageService.sendBoolMessage("BattleyeIsRunning", false);
                return !battleEyeStatus;
            }
        }
    }
}