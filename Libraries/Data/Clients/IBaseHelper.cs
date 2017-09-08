using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Kalendar.Zero.DB.Entity.Base;

namespace Kalendar.Zero.Data.Clients
{
    public interface IBaseHelper
    {
        string Signin();

        AccountAvatarsPO ExchangeToken(string code);

        AccountAvatarsPO RefreshToken(string refreshToken);

        AccountAvatarsPO ReadAvatar();

        List<AccountMessagesPO> ReadMessages(int page=1);

        List<AccountContactsPO> ReadContacts(int page = 1);

        List<SchedulePO> ReadSchedules(int page = 1);

        SchedulePO CreateSchedules(SchedulePO schedule);

        bool CancelSchedules(SchedulePO schedule);

        SchedulePO UpdateSchedules(SchedulePO schedule);

    }
}
