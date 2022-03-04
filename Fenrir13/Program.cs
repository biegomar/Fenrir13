using Fenrir13.Cli;
using PowerArgs;

try
{
    Args.InvokeMain<CommandHandler>(args);
}
catch (ArgException ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine(ArgUsage.GenerateUsageFromTemplate<CommandHandler>());
}
 
