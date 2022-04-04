using System;
using Banks.Entities.Bank;

namespace Banks.Entities.Client
{
    public class Client
    {
        private string _address = string.Empty;
        private uint _passportNumber = 0;
        private bool _isVerified = false;

        public Client(string name, string surname)
        {
            Name = name;
            Surname = surname;
            InformationAbount = new ClientInfo(Id);
            Id = Guid.NewGuid();
        }

        public string Name { get; private set; }
        public string Surname { get; private set; }
        public ClientInfo InformationAbount { get; private set; }
        public Guid Id { get; private set; }

        public void SetAddress(string address)
        {
            _address = address;
        }

        public void SetPassport(uint passportNumber)
        {
            _passportNumber = passportNumber;
            if (_address != string.Empty)
            {
                _isVerified = true;
                InformationAbount.SetIsVerified(_isVerified);
            }
        }
    }
}