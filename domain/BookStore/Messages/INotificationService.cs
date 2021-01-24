using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Messages
{
    public interface INotificationService
    {
        void SendConfirmationCode(string cellPhone, int code);
    }
}
