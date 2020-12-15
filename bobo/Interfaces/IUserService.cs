using System.Collections.Generic;

namespace bobo.Interfaces{

    public interface IUserService
    {
        ServiceResponse Register(UserCred userCred);
        ServiceResponse<string> Authenticate(UserCred userCred);
    }
}