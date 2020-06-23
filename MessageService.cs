namespace R6S_Custom_Game_Tool
{
    using ElectronCgi.DotNet;
    using System;

    internal class MessageService
    {
        private Connection connection;
        public MessageService()
        {
            this.connection = new ConnectionBuilder()
                        .WithLogging()
                        .Build();

            // expects a request named "greeting" with a string argument and returns a string
            // connection.On("greeting", (string name) =>
            // {
            //     Console.WriteLine($"Hello {name}!");
            // });

            this.connection.Send("helloElectron", "hello electron from c#");

            //connection.Send()

            // wait for incoming requests
            this.connection.Listen();
        }

        public void sendStringMessage(string type, string message)
        {
            this.connection.Send(type, message);
        }
    }
}