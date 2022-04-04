using Banks.Entities.Bank;

namespace Banks.Entities.Client
{
    public interface IObserver
    {
        void Update(ISubject subject);
        }
}