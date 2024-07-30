using Counter.Objekte;

namespace Counter.Interfaces
{
    public interface IApiCommand
    {
        public void ExecuteCommand(SafeADP adp);
    }
}
