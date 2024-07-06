using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TurismoGoDOMAIN.Core.DTO.AuthDTO;

namespace TurismoGoDOMAIN.Core.Interfaces
{
    public interface IAuthService
    {
        AuthResponse Authenticate(AuthRequest request);
        bool Register(RegisterRequest request);
    }
}
