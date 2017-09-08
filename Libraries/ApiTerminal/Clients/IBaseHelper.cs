using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.Clients
{
    public interface IBaseHelper
    {
        string Signin();

        Entities.Avatar ExchangeToken(string code);

        Entities.Avatar RefreshToken(string refreshToken);

        Entities.Avatar ReadAvatar();

        List<Entities.Message> ReadMessages(int page=1);

        List<Entities.Contact> ReadContacts(int page = 1);

        List<Entities.Event> ReadEvents(int page = 1);

        Entities.Event CreateEvent(Entities.Event eventInfo);

        bool CancelEvent(Entities.Event eventInfo);

        Entities.Event UpdateEvent(Entities.Event eventInfo);

    }
}
