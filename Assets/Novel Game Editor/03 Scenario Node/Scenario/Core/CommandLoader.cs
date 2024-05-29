using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Glib.NovelGameEditor.Scenario.Commands
{
    public static class CommandLoader
    {
        public static ICommand[] LoadSheet(string commandSheet, Config config)
        {
            var commandStrData = ParseCommands(commandSheet);
            var commandCount = commandStrData.Count;
            ICommand[] commands = new ICommand[commandCount];

            for (int i = 0; i < commandCount; i++)
            {
                commands[i] = commandStrData[i].ToCommand(config);
            }

            return commands;
        }

        public static ICommand ToCommand(this string[] commandData, Config config)
        {
            string commandName = commandData[0];
            string[] commandArgs = commandData[1..];

            var types = GetTypesDerivedFrom<ICommand>();

            foreach (var type in types)
            {
                if (commandName == type.Name)
                {
                    var constructor = type.GetConstructor(new[] { typeof(Config), typeof(string[]) });
                    if (constructor != null)
                    {
                        return constructor.Invoke(new object[] { config, commandArgs }) as ICommand;
                    }
                }
            }

            throw new ArgumentException(nameof(commandName));
        }

        public static List<string[]> ParseCommands(string input)
        {
            List<string[]> commands = new List<string[]>();

            string pattern = @"\[(.*?)\]";
            MatchCollection matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                string commandText = match.Groups[1].Value;
                string[] commandArgs = commandText.Split(',');

                commands.Add(commandArgs);
            }

            return commands;
        }

        private static IEnumerable<Type> GetTypesDerivedFrom<T>()
        {
            return Assembly.GetAssembly(typeof(T)).GetTypes();
        }
    }
}