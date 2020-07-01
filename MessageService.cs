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
            this.connection.On("changeWeapon", (WeaponChangeRequest payload) =>
            {
                requestedWeaponChange(payload);
                return false;
            });
            this.connection.On("changeGadget", (GadgetChangeRequest payload) =>
            {
                requestedGadgetChange(payload);
                return false;
            });
            this.connection.On("stopTimer", (bool payload) =>
            {
                stopTimer(payload);
                return false;
            });
            Task.Run(() => this.connection.Listen());
            this.connection.On("randomizeAll", (string notthinghere) =>
            {
                randomizeAll();
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
            MemoryEngine MemoryEngine = MemoryEngine.GetInstance();
            string playerID = MemoryEngine.PlayerIDs[payload.playerIndex];
            string slotID = MemoryEngine.SlotIDs[payload.slotIndex];
            string weaponId = MemoryEngine.Weapons[payload.weaponIndex];
            MemoryEngine.changeWeapon(playerID, slotID, weaponId);
        }

        private void requestedGadgetChange(GadgetChangeRequest payload)
        {
            MemoryEngine MemoryEngine = MemoryEngine.GetInstance();
            string playerID = MemoryEngine.PlayerIDs[payload.playerIndex];
            string slotID = MemoryEngine.SlotIDs[payload.slotIndex];
            int gadgetIdx = Array.FindIndex(MemoryEngine.Gadgets, item => item == payload.gadgetIndex);

            string GadgetID = MemoryEngine.Gadgets[gadgetIdx];
            string WeaponsDependantG = gadgetIdx <= MemoryEngine.WeaponsDependant.Length ? MemoryEngine.WeaponsDependant[gadgetIdx] : null;
            MemoryEngine.changeGadget(playerID, slotID, GadgetID, WeaponsDependantG);
        }

        private void stopTimer(bool value)
        {
            MemoryEngine MemoryEngine = MemoryEngine.GetInstance();
            MemoryEngine.setTimerBlocked(value);
        }

        private void randomizeAll()
        {
            MemoryEngine MemoryEngine = MemoryEngine.GetInstance();
            MemoryEngine.randomizeAll();
        }
    }

    internal class WeaponChangeRequest
    {
        public int playerIndex;
        public int slotIndex;
        public int weaponIndex;
    }

    internal class GadgetChangeRequest
    {
        public int playerIndex;
        public int slotIndex;
        public string gadgetIndex;
    }
}