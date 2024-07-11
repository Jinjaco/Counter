using Counter.Interfaces.IApiCommands;
using Counter.Objekte;

namespace Counter.Interfaces.Factorys
{
    public class ApiCommandFactory
    {
        public IApiCommand ProcessCommand(string command)
        {
            switch (command)
            {
                case "buchung":
                    return new CommandBuchung();
                case "beauftragung":
                    return new CommandBeauftragung();
                default:
                    return new CommandError();
            }
        }
    }
}
