using Counter.Objekte;

namespace Counter.Interfaces.IApiCommands
{
    public class CommandError : IApiCommand
    {
        public void ExecuteCommand(SafeADP adp)
        {
            throw new Exception("Befehl konnte nicht bearbeitet werden.");
        }
    }
}
