namespace R6S_Custom_Game_Tool
{
    using ElectronCgi.DotNet;
    using System;
    using System.Threading.Tasks;

    class MessageService
    {
        private MessageService()
        {
            this.connection = new ConnectionBuilder()
                .WithLogging()
                .Build();
            this.connection.Send("helloElectron", "hello electron from c#");
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

        public void sendBoolMessage(string type, Boolean message)
        {
            this.connection.Send(type, message);
        }
    }
}