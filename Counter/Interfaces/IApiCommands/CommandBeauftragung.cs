using Counter.Objekte;

namespace Counter.Interfaces.IApiCommands
{
    public class CommandBeauftragung : IApiCommand
    {
        public void ExecuteCommand(SafeADP adp)
        {
            adp.Beauftragung++;
        }
    }
}
