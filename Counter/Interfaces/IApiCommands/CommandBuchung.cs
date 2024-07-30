using Counter.Objekte;

namespace Counter.Interfaces.IApiCommands
{
    public class CommandBuchung : IApiCommand
    {
        public void ExecuteCommand(SafeADP adp)
        {
            adp.Buchung++;
        }
    }
}
