namespace R6S_Custom_Game_Tool
{
    using ElectronCgi.DotNet;
    using System;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    class MessageService
    {
        private MessageService()
        {
            this.connection = new ConnectionBuilder()
                .WithLogging()
                .Build();
            this.connection.Send("helloElectron", "hello electron from c#");
            this.connection.On("changeWeapon", (WeaponChangeRequest payload) => {
                requestedWeaponChange(payload);
                return false;
            });
            Task.Run(() => this.connection.Listen());
        }
        private Connection connection;

        private static MessageService _instance;

        public static MessageService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MessageService();
            }
            return _instance;
        }

        public void sendStringMessage(string type, string message)
        {
            this.connection.Send(type, message);
        }

        public void sendBoolMessage(string type, bool message)
        {
            this.connection.Send(type, message);
        }

        /// <summary>
        /// REMINDER! When Object are sent to Electron the proprierty names have the first letter as LOWERCASE!!!!
        /// </summary>
        public void sendObjectMessage(string type, Object message)
        {
            this.connection.Send(type, message);
        }

        private void requestedWeaponChange(WeaponChangeRequest payload)
        {
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

        string playerID = MemoryEngine.PlayerIDs[payload.playerIndex];
            string slotID = MemoryEngine.SlotIDs[payload.slotIndex];
            string weaponId = MemoryEngine.Weapons[payload.weaponIndex];
            MemoryEngine.changeWeapon(playerID, slotID, weaponId);
        }
    }

    internal class WeaponChangeRequest
    {
        public int playerIndex;
        public int slotIndex;
        public int weaponIndex;
    }
}