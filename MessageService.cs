﻿namespace R6S_Custom_Game_Tool
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

        public void sendObjectMessage(string type, Object message)
        {
            var jsonString = JsonConvert.SerializeObject(message);
            this.connection.Send(type, jsonString);
        }
    }
}