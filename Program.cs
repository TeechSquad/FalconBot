using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static void Main(string[] args) => new Program().Start();

    private DiscordClient _client;

    CommandService commands;

    public void Start()
    {
        _client = new DiscordClient(x => {
            x.LogLevel = LogSeverity.Info;
            x.LogHandler = Log;
        });

        _client.UsingCommands(x =>
       {
           x.PrefixChar = '~';
           x.HelpMode = HelpMode.Public;
       });

        commands = _client.GetService<CommandService>();

        //returnMessage();
        greetPeople();
        joinEvent();
        banEvent();
        kickEvent();
        unbanEvent();
        trainingEvent();
        

        _client.ExecuteAndWait(async () =>
        {
            await _client.Connect("MjQ5NTkwMTAxMjYzMjUzNTE1.CxNhAQ.O448r6Gw-3JiPtK6WHZOzgz5Tk0", TokenType.Bot);
        });
    }

    private void returnMessage()
    {
        _client.MessageReceived += async (s, e) =>
        {
            if (!e.Message.IsAuthor)
                await e.Channel.SendMessage(e.Message.Text);
        };
    }
    private void greetPeople()
    {
        commands.CreateCommand("Hi")
            .Alias(new string[] { "Hey", "Yo", "Hello", "Sup" }) //Command can be triggered with those words aswell
            .Description("Greets a person.")
            .Parameter("GreetedPerson", ParameterType.Required) //As an argument we will have a person we want to greet
            .Do(async (e) =>
           {
               await e.Channel.SendMessage($"{e.User.Name} greets {e.GetArg("GreetedPerson")}"); //Sends a message to channel with a given text
            });
    }
    private void joinEvent()
    {
        _client.UserJoined += async (s, e) =>
        {
            await e.User.SendMessage("Welcome on crappy server. I hope u don't lag :ccc!");
        };
    }
    private void banEvent()
    {
        _client.UserBanned += async (s, e) =>
        {
            await e.User.SendMessage("LOL, scrub we banned. Good luck joining again xaxaxaxa");
        };
    }
    private void kickEvent()
    {
        _client.UserLeft += async (s, e) =>
        {
            await e.User.SendMessage("You've out of server, why u left ;(");
        };
    }
    private void unbanEvent()
    {
        _client.UserUnbanned += async (s, e) =>
        {
            await e.User.SendMessage("Lord Kayl has unbanned you. ro drop him 55 golds k");
        };
    }
    private void trainingEvent()
    {
        commands.CreateCommand("Training")
            .Do(async (e) =>
            {
                //int time[2];
                await e.User.SendMessage("Please set the time of the training: ");
                returnMessage();
            });
    }

    private void Log(object sender, LogMessageEventArgs e)
    {
        Console.WriteLine(e.Message); 
    }
}