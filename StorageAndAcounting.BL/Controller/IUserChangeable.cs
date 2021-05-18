using StorageAndAcounting.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAndAcounting.BL.Controller
{
    public interface IUserChangeable
    {
        bool ChangeUser(User user);
    }
}
