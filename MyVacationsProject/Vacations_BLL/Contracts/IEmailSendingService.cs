using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacations_BLL.Contracts
{
    public interface IEmailSendingService:IService
    {
        void SendEmail(string email, string message);
    }
}
