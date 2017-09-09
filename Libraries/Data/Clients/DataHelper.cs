using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kalendar.Zero.DB.Entity.Base;

namespace Kalendar.Zero.Data.Clients
{
    public class DataHelper
    {
        public string Signin()
        {
            return "";
        }

        public AccountAvatarsPO ExchangeToken(string code)
        {
            return new AccountAvatarsPO();
        }

        public AccountAvatarsPO RefreshToken(string refreshToken)
        {
            return new AccountAvatarsPO();
        }

        public AccountAvatarsPO ReadAvatar()
        {
            return new AccountAvatarsPO();
        }

        public List<AccountMessagesPO> ReadMessages(int page = 1)
        {
            return new List<AccountMessagesPO>();
        }

        public List<AccountContactsPO> ReadContacts(int page = 1)
        {
            return new List<AccountContactsPO>();
        }

        public List<SchedulePO> ReadSchedules(int page = 1)
        {
            return new List<SchedulePO>();
        }

        public SchedulePO CreateSchedules(SchedulePO schedule)
        {
            return new SchedulePO();
        }

        public bool CancelSchedules(SchedulePO schedule)
        {
            return true;
        }

        public SchedulePO UpdateSchedules(SchedulePO schedule)
        {
            return new SchedulePO();
        }
    }
}
