using System;

namespace Banks.Entities.Client
{
    public class ClientInfo
    {
        public ClientInfo(Guid id)
        {
            Id = id;
            IsVerified = false;
        }

        public Guid Id { get; private set; }
        public bool IsVerified { get; private set; }

        public void SetIsVerified(bool verified)
        {
            IsVerified = verified;
        }
    }
}