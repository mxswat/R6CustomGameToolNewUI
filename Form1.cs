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
    public partial class Form1 : Form
    {

        Mem m = new Mem();

        public Form1()
        {
            InitializeComponent();
        }

        public string GameManager; //48 8b 0d ? ? ? ? 48 8b 01 ff 90 ? ? ? ? 48 85 c0 74 ? 48 8b 10 48 89 c1 ff 92 ? ? ? ? 48 89 c5 eb ? 31 ed
        public string NetworkManager; //48 8b 05 ? ? ? ? 0f b7 40 ? 66 85 c0
        public string RoundManager; //48 8b 05 ? ? ? ? 83 b8 ? ? ? ? ? 74 ? 83 b8
        // Loadout Sig : 48 87 5e ? 48 85 db 74 ? 48 b8 ? ? ? ? ? ? ? ? f0 48 0f c1 43 ? 48 b9 ? ? ? ? ? ? ? ? 48 01 c8 48 81 c9 ? ? ? ? 48 85 c8 74 / 48 87 56 ? 48 85 ? 74 ? 48 b8 ? ? ? ? ? ? ? ? f0 48 0f c1 42 ? 48 b9 ? ? ? ? ? ? ? ? 48 01 c8 48 81 c9 ? ? ? ? 48 85 c8 74 ? f6 46
        // Local Team: 8b 80 ? ? ? ? 41 89 87 ? ? ? ? 0f 28 74 24
        // Weapon Properties : 48 8b 0d ? ? ? ? e8 ? ? ? ? 48 8b 0d ? ? ? ? e8 ? ? ? ? 48 8b 8e
        // Timer : 80 be ? ? ? ? ? 0f 84 ? ? ? ? 85 d2
        public string LoadoutOffset1 = "508";
        public string LoadoutOffset2 = "0";
        public string LoadoutOffset3 = "A8";
        public string LoadoutOffset4 = "0";
        public string LoadoutOffset5 = "40";
        public string LoadoutOffset6 = "20";
        public string LoadoutOffset7 = "0";
        public string LoadoutOffset8 = "18";
        public string PlayerID = "0";
        public int CountPlayer = 0;
        public string SlotID = "10";
        public string GSlotID = "28";
        public string[] PlayerPrimWeapons = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        public string[] PlayerSecWeapons = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        public string[] PlayerPrimGadget = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        public string[] PlayerSecGadget = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        public int integervar;
        public int AloneCheck;
        public string[] PlayersID = { "0", "8", "28", "30", "20", "28", "30", "38", "40", "48" };
        public string[] Gadgets = { "F8", "118", "148", "1C8", "198", "1F8", "248", "D8", "158", "230", "270", "278", "E0", "F0", "100", "108", "168", "1A8", "1C0", "290", "E8", "170", "190", "1E0", "1E8", "210", "220", "228", "288", "258", "268", "120", "140", "150", "1D8", "160", "188", "1A0", "298", "218", "1F0", "238", "240", "128", "208", "110", "130", "1B0", "280", "180", "200", "250", "138", "1B8" };
        public string[] Weapons = { "63840", "63808", "59456", "59584", "60768", "61632", "57184", "58752", "59296", "57408", "58016", "57984", "58592", "54752", "56128", "59008", "55200", "60320", "60384", "55936", "54880", "55552", "54624", "59776", "54656", "59136", "63648", "58784", "57952", "58944", "54944", "62560", "59104", "57120", "56960", "58624", "56064", "61120", "55584", "62208", "56608", "60576", "58112", "58304", "60000", "60032", "56288", "56864", "57824", "60928", "62912", "55168", "58368", "56672", "57312", "62784", "61088", "61024", "60128", "59680", "63680", "57760", "58240", "60064", "56224", "57440", "56736", "55008", "55328", "58464", "58496", "55968", "56352", "57280", "61056", "60224", "63072", "59168", "56576", "59488", "59552", "59968", "57504", "62400", "62304", "57344", "58560", "56416", "60864", "58912", "63040", "63328", "58144", "57248", "60160", "60192", "55488", "57856", "59840", "63552", "55456", "62432", "60672", "59648", "59328", "58848", "55136", "54816", "55616", "55712", "57472", "63616" };
        public string[] WeaponsDependant = { "55456", "56128", "56960", "59648", "58592", "60672", "62208" };//Weapon Depedant Gadgets, Le Roc, MK17 CQB, OTs-03, CCE SHIELD, C8-SFW, G52-Tactical Shield, CSRX 300
        private MessageService messageService = MessageService.GetInstance();
        private MemoryEngine MemoryEngine = new MemoryEngine();
        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            bool isBattleEyeRunning = MemoryEngine.CheckBattleEyeStatus();
            label9.Text = "Battleye:On";
            if (!isBattleEyeRunning)
            {
                label9.Text = "Battleye:Off";
            }
            MemoryEngine.startMemEngine();
            try
            {
                int iProcID = m.GetProcIdFromName("RainbowSix_Vulkan");
                GameManager = "RainbowSix_Vulkan.exe+0x6713ED8";
                RoundManager = "RainbowSix_Vulkan.exe+0x68E2CF0";
                NetworkManager = "RainbowSix_Vulkan.exe+0x6923130";
                label3.Text = "Vulkan";

                if (iProcID == 0)
                {
                    iProcID = m.GetProcIdFromName("RainbowSix");
                    GameManager = "RainbowSix.exe+0x6713ED8";
                    RoundManager = "RainbowSix.exe+0x6CA0848";
                    NetworkManager = "RainbowSix.exe+0x68E2CF0";
                    label3.Text = "DirectX";
                }

                if (iProcID > 0)
                {
                    m.OpenProcess(iProcID);
                    label4.Text = "Active";
                    label4.ForeColor = Color.Green;
                    timer.Start();
                }
            }
            catch (Exception) { timer.Start(); }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                SlotID = "10";
            }
            else if (radioButton2.Checked)
            {
                SlotID = "18";
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // 65% Code lengh reduced from the first version 258 line to 90 lines
            bool[] PlayerRadioButtonsChecked = new bool[] {
                radioButton6.Checked,
                radioButton5.Checked,
                radioButton7.Checked,
                radioButton8.Checked,
                radioButton9.Checked,
                radioButton10.Checked,
                radioButton11.Checked,
                radioButton12.Checked,
                radioButton13.Checked,
                radioButton14.Checked
            };

            string[] PlayerBytes = new string[]
            {
                $"{GameManager},508,0,0,30,8,1A8,0",
                $"{GameManager},508,8,0,30,8,1A8,0",
                $"{GameManager},508,10,0,30,8,1A8,0",
                $"{GameManager},508,18,0,30,8,1A8,0",
                $"{GameManager},508,20,0,30,8,1A8,0",
                $"{GameManager},508,28,0,30,8,1A8,0",
                $"{GameManager},508,30,0,30,8,1A8,0",
                $"{GameManager},508,38,0,30,8,1A8,0",
                $"{GameManager},508,40,0,30,8,1A8,0",
                $"{GameManager},508,48,0,30,8,1A8,0"
            };

            string[] PlayerIDs = new string[]
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

            try
            {
                //// Loop 9 times? why?
                //for (int i = 0; i < 9; i++)
                //{
                int RadioButtonIndex = Array.FindIndex(PlayerRadioButtonsChecked, item => item == true);
                PlayerID = PlayerIDs[RadioButtonIndex];
                if (m.ReadByte(PlayerBytes[RadioButtonIndex], "") == 0)
                {
                    label10.Text = "CurrentPlayer:Empty";
                    label8.Text = "Primary Weapon:Empty";
                    label13.Text = "Secondary Weapon:Empty";
                    label14.Text = "Primary Gadget:Empty";
                    label16.Text = "Secondary Gadget:Empty";
                }
                else
                {
                    label10.Text = "CurrentPlayer:" + m.ReadString(PlayerBytes[RadioButtonIndex], "", 15);
                    label8.Text = "Primary Weapon:" + PlayerPrimWeapons[RadioButtonIndex];
                    label13.Text = "Secondary Weapon:" + PlayerSecWeapons[RadioButtonIndex];
                    label14.Text = "Primary Gadget:" + PlayerPrimGadget[RadioButtonIndex];
                    label16.Text = "Secondary Gadget:" + PlayerSecGadget[RadioButtonIndex];
                    CountPlayer = RadioButtonIndex;
                    label5.Visible = true;
                    timer.Start();
                    messageService.sendObjectMessage("PlayerUpdated", new
                    {
                        PrimaryWeapon = label8.Text,
                        SecondaryWeapon = label13.Text,
                        PrimaryGadget = label14.Text,
                        SecondaryGadget = label16.Text,
                    });
                }
                //}
            }
            catch (Exception) {; }
            if (label5.Visible)
            {
                label5.Visible = false;
            }
            else
            {
                integervar++;
                if (integervar == 3)
                {
                    label5.Visible = true;
                    if (label5.Visible)
                    {
                        label5.Visible = false;
                    }
                    integervar = 0;
                    timer.Stop();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)//Randomize All
        {
            Random random = new Random();
            string[] GadgetsNoLaunchers = { "D8", "158", "230", "270", "278", "E8", "170", "190", "1E0", "1E8", "210", "220", "228", "288", "258", "268", "120", "140", "150", "1D8", "160", "188", "1A0", "298", "218", "1F0", "238", "240", "128", "208", "110", "130", "1B0", "280", "180", "200", "250" };
            string[] WeaponsP = { "63840", "63808", "59456", "59584", "60768", "61632", "57184", "58752", "59296", "57408", "58016", "57984", "58592", "54752", "56128", "59008", "55200", "60320", "60384", "55936", "54880", "55552", "54624", "59776", "54656", "59136", "63648", "58784", "57952", "58944", "54944", "62560", "59104", "57120", "56960", "58624", "56064", "61120", "55584", "62208", "56608", "60576", "39712", "58112", "58304", "60000", "60032", "56288", "56864", "57824", "60928", "62912", "55168", "58368", "56672", "57312", "62784", "61088", "61024", "60128", "59680", "63680", "57760", "58240", "60064", "56224", "57440", "56736", "55008", "55328", "58464", "58496", "55968", "56352", "57280", "61056", "60224", "63072", "59168", "56576", "59488", "59552", "59968", "57504", "62400", "62304", "57344", "58560", "56416", "60864", "58912", "63040", "63328", "58144", "57248", "60160", "60192", "55488", "57856", "59840", "63552" };
            string[] WeaponsS = { "63840", "63808", "59456", "59584", "60768", "61632", "57184", "58752", "59296", "57408", "58016", "57984", "58592", "54752", "56128", "59008", "55200", "60320", "60384", "55936", "54880", "55552", "54624", "59776", "54656", "59136", "63648", "58784", "57952", "58944", "54944", "62560", "59104", "57120", "56960", "58624", "56064", "61120", "55584", "62208", "56608", "60576", "39712", "58112", "58304", "60000", "60032", "56288", "56864", "57824", "60928", "62912", "55168", "58368", "56672", "57312", "62784", "61088", "61024", "60128", "59680", "63680", "57760", "58240", "60064", "56224", "57440", "56736", "55008", "55328", "58464", "58496", "55968", "56352", "57280", "61056", "60224", "63072", "59168", "56576", "59488", "59552", "59968", "57504", "62400", "62304", "57344", "58560", "56416", "60864", "58912", "63040", "63328", "58144", "57248", "60160", "60192", "55488", "57856", "59840" };
            try
            {
                for (int j = 0; j < 10; j++)
                {
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},28,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{m.ReadInt($"{NetworkManager},B0,40,A8,D0,1E0,{GadgetsNoLaunchers[random.Next(GadgetsNoLaunchers.Length)]},38,20,190,A0,40,58,20,0,E0,F8,978")}");
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},38,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{m.ReadInt($"{NetworkManager},B0,40,A8,D0,1E0,{GadgetsNoLaunchers[random.Next(GadgetsNoLaunchers.Length)]},38,20,190,A0,40,58,20,0,E0,F8,978")}");
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},10,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", $"{WeaponsP[random.Next(WeaponsP.Length)]}");
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},10,{LoadoutOffset6},{LoadoutOffset7},40,20,20,0,18", "int", "0");
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},18,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", $"{WeaponsS[random.Next(WeaponsS.Length)]}");
                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},18,{LoadoutOffset6},{LoadoutOffset7},40,20,20,0,18", "int", "0");
                    PlayerPrimWeapons[j] = "Random";
                    PlayerSecWeapons[j] = "Random";
                    PlayerPrimGadget[j] = "Random";
                    PlayerSecGadget[j] = "Random";
                    label5.Visible = true;
                    timer.Start();
                }
            }
            catch (Exception) {; }
        }

        private void button2_Click(object sender, EventArgs e)//Give All Weapons
        {
            try
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int i = 0; i < 114; i++)
                    {
                        if (treeView1.SelectedNode != null)
                        {
                            if (treeView1.SelectedNode.Name == $"{i}")
                            {
                                m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},{SlotID},{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", Weapons[i]);
                                m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},{SlotID},{LoadoutOffset6},{LoadoutOffset7},40,20,20,0,18", "int", "0");
                                if (SlotID == "10")
                                {
                                    PlayerPrimWeapons[j] = treeView1.SelectedNode.Text;
                                }
                                else
                                {
                                    PlayerSecWeapons[j] = treeView1.SelectedNode.Text;
                                }
                                label5.Visible = true;
                                timer.Start();
                                treeView1.SelectedNode = null;
                            }
                            else if (treeView1.SelectedNode.Name == "114")
                            {
                                m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},{SlotID},{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", "0");
                                if (SlotID == "10")
                                {
                                    PlayerPrimWeapons[j] = "None";
                                }
                                else
                                {
                                    PlayerSecWeapons[j] = "None";
                                }
                                label5.Visible = true;
                                timer.Start();
                                treeView1.SelectedNode = null;
                            }
                        }
                    }

                }
            }
            catch (Exception) {; }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/u7PBEr7");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                radioButton6.Text = m.ReadString($"{GameManager},508,0,0,30,8,1A8,0", "", 15);
                radioButton5.Text = m.ReadString($"{GameManager},508,8,0,30,8,1A8,0", "", 15);
                radioButton7.Text = m.ReadString($"{GameManager},508,10,0,30,8,1A8,0", "", 15);
                radioButton8.Text = m.ReadString($"{GameManager},508,18,0,30,8,1A8,0", "", 15);
                radioButton9.Text = m.ReadString($"{GameManager},508,20,0,30,8,1A8,0", "", 15);
                radioButton10.Text = m.ReadString($"{GameManager},508,28,0,30,8,1A8,0", "", 15);
                radioButton11.Text = m.ReadString($"{GameManager},508,30,0,30,8,1A8,0", "", 15);
                radioButton12.Text = m.ReadString($"{GameManager},508,38,0,30,8,1A8,0", "", 15);
                radioButton13.Text = m.ReadString($"{GameManager},508,40,0,30,8,1A8,0", "", 15);
                radioButton14.Text = m.ReadString($"{GameManager},508,48,0,30,8,1A8,0", "", 15);
            }
            catch {; }
            try
            {
                if (textBox1.Focused == false)
                {
                    textBox1.Text = $"{m.ReadInt($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},10,{LoadoutOffset6},{LoadoutOffset7},40,28,20,0,18", "")}";
                }
                if (textBox2.Focused == false)
                {
                    textBox2.Text = $"{m.ReadInt($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},10,{LoadoutOffset6},{LoadoutOffset7},40,10,20,0,18", "")}";
                }
                if (textBox3.Focused == false)
                {
                    textBox3.Text = $"{m.ReadInt($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},10,{LoadoutOffset6},{LoadoutOffset7},40,30,20,0,18", "")}";
                }
                if (textBox4.Focused == false)
                {
                    textBox4.Text = $"{m.ReadInt($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},10,{LoadoutOffset6},{LoadoutOffset7},40,18,20,0,18", "")}";
                }
                if (textBox6.Focused == false)
                {
                    textBox6.Text = $"{m.ReadInt($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},10,{LoadoutOffset6},{LoadoutOffset7},40,20,20,0,18", "")}";
                }
                if (textBox13.Focused == false)
                {
                    textBox13.Text = $"{m.ReadInt($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},18,{LoadoutOffset6},{LoadoutOffset7},40,28,20,0,18", "")}";
                }
                if (textBox12.Focused == false)
                {
                    textBox12.Text = $"{m.ReadInt($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},18,{LoadoutOffset6},{LoadoutOffset7},40,10,20,0,18", "")}";
                }
                if (textBox11.Focused == false)
                {
                    textBox11.Text = $"{m.ReadInt($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},18,{LoadoutOffset6},{LoadoutOffset7},40,30,20,0,18", "")}";
                }
                if (textBox10.Focused == false)
                {
                    textBox10.Text = $"{m.ReadInt($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},18,{LoadoutOffset6},{LoadoutOffset7},40,18,20,0,18", "")}";
                }
                if (textBox9.Focused == false)
                {
                    textBox9.Text = $"{m.ReadInt($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},18,{LoadoutOffset6},{LoadoutOffset7},40,20,20,0,18", "")}";
                }
                if (textBox16.Focused == false)
                {
                    textBox16.Text = $"{m.ReadInt($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},68,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "")}";
                }
                if (textBox7.Focused == false)
                {
                    textBox7.Text = $"{m.ReadInt($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},0,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "")}";
                }
                if (textBox8.Focused == false)
                {
                    textBox8.Text = $"{m.ReadInt($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},8,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "")}";
                }
                if (textBox14.Focused == false)
                {
                    textBox14.Text = $"{m.ReadInt($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},50,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "")}";
                }
            }
            catch {; }
            try
            {

                if (this.checkBox1.Checked)
                {
                    //Timer block moved from here to Memory Engine
                    MemoryEngine.setTimerBlocked(true);
                }
            }
            catch (Exception) {; }
            try
            {
                if (m.ReadByte($"{RoundManager},2E8", "") == 0)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        PlayerPrimWeapons[i] = "0";
                        PlayerSecWeapons[i] = "0";
                        PlayerPrimGadget[i] = "0";
                        PlayerSecGadget[i] = "0";
                    }
                    timer.Start();
                }
            }
            catch (Exception) {; }
            try
            {
                if (m.ReadByte($"{RoundManager},2E8", "") == 2)
                {
                    this.ActiveControl = null;
                }
            }
            catch (Exception) {; }
        }

        private void button3_Click(object sender, EventArgs e)//Normal Weapon Swapper
        {
            try
            {
                for (int i = 0; i < 112; i++)
                {
                    if (treeView1.SelectedNode != null)
                    {
                        if (treeView1.SelectedNode.Name == $"{i}")
                        {
                            MemoryEngine.changeWeapon(PlayerID, SlotID, Weapons[i]);
                            // Write moved to MemoryEngine
                            if (SlotID == "10")
                            {
                                PlayerPrimWeapons[CountPlayer] = treeView1.SelectedNode.Text;
                            }
                            else
                            {
                                PlayerSecWeapons[CountPlayer] = treeView1.SelectedNode.Text;
                            }
                            label5.Visible = true;
                            timer.Start();
                            treeView1.SelectedNode = null;
                        }
                        // Hands
                        else if (treeView1.SelectedNode.Name == "114")
                        {
                            // MX:TODO: Do it later
                            m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},{SlotID},{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", "0");
                            if (SlotID == "10")
                            {
                                PlayerPrimWeapons[CountPlayer] = "None";
                            }
                            else
                            {
                                PlayerSecWeapons[CountPlayer] = "None";
                            }
                            label5.Visible = true;
                            timer.Start();
                            treeView1.SelectedNode = null;
                        }
                    }
                }
            }
            catch (Exception) {; }
        }

        private void button4_Click(object sender, EventArgs e) //Normal Gadget Giver
        {
            try
            {
                for (int j = 0; j < 9; j++)
                {
                    for (int i = 0; i < Gadgets.Length; i++)
                    {
                        if (treeView3.SelectedNode != null)
                        {
                            if (treeView3.SelectedNode.Name == Gadgets[i])
                            {
                                string GadgetID = Gadgets[i];
                                string WeaponsDependantG = i <= WeaponsDependant.Length ? WeaponsDependant[i] : null;
                                MemoryEngine.changeGadget(PlayerID, GSlotID, GadgetID, WeaponsDependantG);
                                // All the gadget switch code moved to MemoryEngine
                                if (GSlotID == "28")
                                {
                                    PlayerPrimGadget[CountPlayer] = treeView3.SelectedNode.Text;
                                }
                                else
                                {
                                    PlayerSecGadget[CountPlayer] = treeView3.SelectedNode.Text;
                                }
                                label5.Visible = true;
                                timer.Start();
                                treeView3.SelectedNode = null;
                            }
                        }
                    }
                }
            }
            catch (Exception) {; }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                GSlotID = "28";
            }
            else if (radioButton4.Checked)
            {
                GSlotID = "38";
            }
        }

        private void button5_Click(object sender, EventArgs e) //Give all gadgets
        {
            try
            {
                for (int j = 0; j < 9; j++)
                {
                    for (int i = 0; i < Gadgets.Length; i++)
                    {
                        if (treeView3.SelectedNode != null)
                        {
                            if (treeView3.SelectedNode.Name == Gadgets[i])
                            {
                                if (i < 7)
                                {
                                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},10,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", WeaponsDependant[i]);
                                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},{GSlotID},{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{m.ReadInt($"{NetworkManager},B0,40,A8,D0,1E0,{Gadgets[i]},38,20,190,A0,40,58,20,0,E0,F8,978")}");
                                    if (GSlotID == "28")
                                    {
                                        PlayerPrimGadget[j] = treeView3.SelectedNode.Text;
                                    }
                                    else
                                    {
                                        PlayerSecGadget[j] = treeView3.SelectedNode.Text;
                                    }
                                }
                                if (i == 12)
                                {
                                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},20,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", "54816");//Zofia Launcher
                                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},{GSlotID},{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{m.ReadInt($"{NetworkManager},B0,40,A8,D0,1E0,{Gadgets[i]},38,20,190,A0,40,58,20,0,E0,F8,978")}");
                                    if (GSlotID == "28")
                                    {
                                        PlayerPrimGadget[j] = treeView3.SelectedNode.Text;
                                    }
                                    else
                                    {
                                        PlayerSecGadget[j] = treeView3.SelectedNode.Text;
                                    }
                                }
                                else if (i == 13)
                                {
                                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},20,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", "55136");//Hibana Launcher
                                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},{GSlotID},{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{m.ReadInt($"{NetworkManager},B0,40,A8,D0,1E0,{Gadgets[i]},38,20,190,A0,40,58,20,0,E0,F8,978")}");
                                    if (GSlotID == "28")
                                    {
                                        PlayerPrimGadget[j] = treeView3.SelectedNode.Text;
                                    }
                                    else
                                    {
                                        PlayerSecGadget[j] = treeView3.SelectedNode.Text;
                                    }
                                }
                                else if (i == 14)
                                {
                                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},20,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", "55616");//BlowTorch
                                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},{GSlotID},{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{m.ReadInt($"{NetworkManager},B0,40,A8,D0,1E0,{Gadgets[i]},38,20,190,A0,40,58,20,0,E0,F8,978")}");
                                    if (GSlotID == "28")
                                    {
                                        PlayerPrimGadget[j] = treeView3.SelectedNode.Text;
                                    }
                                    else
                                    {
                                        PlayerSecGadget[j] = treeView3.SelectedNode.Text;
                                    }
                                }
                                else if (i == 15)
                                {
                                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},20,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", "55712");//GaraHook
                                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},{GSlotID},{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{m.ReadInt($"{NetworkManager},B0,40,A8,D0,1E0,{Gadgets[i]},38,20,190,A0,40,58,20,0,E0,F8,978")}");
                                    if (GSlotID == "28")
                                    {
                                        PlayerPrimGadget[j] = treeView3.SelectedNode.Text;
                                    }
                                    else
                                    {
                                        PlayerSecGadget[j] = treeView3.SelectedNode.Text;
                                    }
                                }
                                else if (i == 16)
                                {
                                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},20,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", "57472");//Stim Pistol
                                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},{GSlotID},{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{m.ReadInt($"{NetworkManager},B0,40,A8,D0,1E0,{Gadgets[i]},38,20,190,A0,40,58,20,0,E0,F8,978")}");
                                    if (GSlotID == "28")
                                    {
                                        PlayerPrimGadget[j] = treeView3.SelectedNode.Text;
                                    }
                                    else
                                    {
                                        PlayerSecGadget[j] = treeView3.SelectedNode.Text;
                                    }
                                }
                                else if (i == 17)
                                {
                                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},20,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", "58848");//Capitao Crossbow
                                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},{GSlotID},{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{m.ReadInt($"{NetworkManager},B0,40,A8,D0,1E0,{Gadgets[i]},38,20,190,A0,40,58,20,0,E0,F8,978")}");
                                    if (GSlotID == "28")
                                    {
                                        PlayerPrimGadget[j] = treeView3.SelectedNode.Text;
                                    }
                                    else
                                    {
                                        PlayerSecGadget[j] = treeView3.SelectedNode.Text;
                                    }
                                }
                                else if (i == 18)
                                {
                                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},20,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", "59328");//Ash Launcher
                                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},{GSlotID},{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{m.ReadInt($"{NetworkManager},B0,40,A8,D0,1E0,{Gadgets[i]},38,20,190,A0,40,58,20,0,E0,F8,978")}");
                                    if (GSlotID == "28")
                                    {
                                        PlayerPrimGadget[j] = treeView3.SelectedNode.Text;
                                    }
                                    else
                                    {
                                        PlayerSecGadget[j] = treeView3.SelectedNode.Text;
                                    }
                                }
                                else if (i == 19)
                                {
                                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},20,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "2bytes", "63616");//Mozzie Pest Launcher
                                    m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},{GSlotID},{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{m.ReadInt($"{NetworkManager},B0,40,A8,D0,1E0,{Gadgets[i]},38,20,190,A0,40,58,20,0,E0,F8,978")}");
                                    if (GSlotID == "28")
                                    {
                                        PlayerPrimGadget[j] = treeView3.SelectedNode.Text;
                                    }
                                    else
                                    {
                                        PlayerSecGadget[j] = treeView3.SelectedNode.Text;
                                    }
                                }
                                m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayersID[j]},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},{GSlotID},{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{m.ReadInt($"{NetworkManager},B0,40,A8,D0,1E0,{Gadgets[i]},38,20,190,A0,40,58,20,0,E0,F8,978")}");
                                if (GSlotID == "28")
                                {
                                    PlayerPrimGadget[j] = treeView3.SelectedNode.Text;
                                }
                                else
                                {
                                    PlayerSecGadget[j] = treeView3.SelectedNode.Text;
                                }
                                label5.Visible = true;
                                timer.Start();
                                treeView3.SelectedNode = null;
                            }
                        }
                    }

                }
            }
            catch (Exception) {; }
        }

        private void radioButton6_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == false)
            {
                //Timer block moved from here to Memory Engine
                MemoryEngine.setTimerBlocked(false);
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                m.WriteMemory($"{ GameManager},{ LoadoutOffset1},{ PlayerID},{ LoadoutOffset2},{ LoadoutOffset3},{ LoadoutOffset4},{ LoadoutOffset5},10,{ LoadoutOffset6},{ LoadoutOffset7},40,28,20,0,18", "int", $"{textBox1.Text}");
            }
            catch (Exception) {; }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                m.WriteMemory($"{ GameManager},{ LoadoutOffset1},{ PlayerID},{ LoadoutOffset2},{ LoadoutOffset3},{ LoadoutOffset4},{ LoadoutOffset5},10,{ LoadoutOffset6},{ LoadoutOffset7},40,10,20,0,18", "int", $"{textBox2.Text}");
            }
            catch (Exception) {; }
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                m.WriteMemory($"{ GameManager},{ LoadoutOffset1},{ PlayerID},{ LoadoutOffset2},{ LoadoutOffset3},{ LoadoutOffset4},{ LoadoutOffset5},10,{ LoadoutOffset6},{ LoadoutOffset7},40,30,20,0,18", "int", $"{textBox3.Text}");
            }
            catch (Exception) {; }
        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                m.WriteMemory($"{ GameManager},{ LoadoutOffset1},{ PlayerID},{ LoadoutOffset2},{ LoadoutOffset3},{ LoadoutOffset4},{ LoadoutOffset5},10,{ LoadoutOffset6},{ LoadoutOffset7},40,18,20,0,18", "int", $"{textBox4.Text}");
            }
            catch (Exception) {; }
        }

        private void textBox6_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                m.WriteMemory($"{ GameManager},{ LoadoutOffset1},{ PlayerID},{ LoadoutOffset2},{ LoadoutOffset3},{ LoadoutOffset4},{ LoadoutOffset5},10,{ LoadoutOffset6},{ LoadoutOffset7},40,20,20,0,18", "int", $"{textBox6.Text}");
            }
            catch (Exception) {; }
        }

        private void textBox13_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                m.WriteMemory($"{ GameManager},{ LoadoutOffset1},{ PlayerID},{ LoadoutOffset2},{ LoadoutOffset3},{ LoadoutOffset4},{ LoadoutOffset5},18,{ LoadoutOffset6},{ LoadoutOffset7},40,28,20,0,18", "int", $"{textBox13.Text}");
            }
            catch (Exception) {; }
        }

        private void textBox12_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                m.WriteMemory($"{ GameManager},{ LoadoutOffset1},{ PlayerID},{ LoadoutOffset2},{ LoadoutOffset3},{ LoadoutOffset4},{ LoadoutOffset5},18,{ LoadoutOffset6},{ LoadoutOffset7},40,10,20,0,18", "int", $"{textBox12.Text}");
            }
            catch (Exception) {; }
        }

        private void textBox11_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                m.WriteMemory($"{ GameManager},{ LoadoutOffset1},{ PlayerID},{ LoadoutOffset2},{ LoadoutOffset3},{ LoadoutOffset4},{ LoadoutOffset5},18,{ LoadoutOffset6},{ LoadoutOffset7},40,30,20,0,18", "int", $"{textBox11.Text}");
            }
            catch (Exception) {; }
        }

        private void textBox10_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                m.WriteMemory($"{ GameManager},{ LoadoutOffset1},{ PlayerID},{ LoadoutOffset2},{ LoadoutOffset3},{ LoadoutOffset4},{ LoadoutOffset5},18,{ LoadoutOffset6},{ LoadoutOffset7},40,18,20,0,18", "int", $"{textBox10.Text}");
            }
            catch (Exception) {; }
        }

        private void textBox9_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                m.WriteMemory($"{ GameManager},{ LoadoutOffset1},{ PlayerID},{ LoadoutOffset2},{ LoadoutOffset3},{ LoadoutOffset4},{ LoadoutOffset5},18,{ LoadoutOffset6},{ LoadoutOffset7},40,20,20,0,18", "int", $"{textBox9.Text}");
            }
            catch (Exception) {; }
        }

        private void textBox7_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},0,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{textBox7.Text}");
            }
            catch (Exception) {; }
        }

        private void textBox8_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},8,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{textBox8.Text}");
            }
            catch (Exception) {; }
        }

        private void textBox14_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},50,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{textBox14.Text}");
            }
            catch (Exception) {; }
        }

        private void textBox16_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                m.WriteMemory($"{GameManager},{LoadoutOffset1},{PlayerID},{LoadoutOffset2},{LoadoutOffset3},{LoadoutOffset4},{LoadoutOffset5},68,{LoadoutOffset6},{LoadoutOffset7},{LoadoutOffset8}", "int", $"{textBox16.Text}");
            }
            catch (Exception) {; }
        }
    }
}