using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeftiniLetoviData.APICalls
{
    public interface IAuthenticationTokenAPICall
    {
        public Task<string> GetBearerToken();
    }
}
