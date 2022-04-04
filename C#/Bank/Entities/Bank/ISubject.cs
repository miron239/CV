using Banks.Entities.Client;

namespace Banks.Entities.Bank
{
    public interface ISubject
    {
            void Attach(IObserver observer);

            void Detach(IObserver observer);

            void Notify();
        }
}