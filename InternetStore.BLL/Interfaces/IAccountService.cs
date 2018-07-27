using InternetStore.BLL.DataTransferObjects;
using InternetStore.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace InternetStore.BLL.Interfaces
{
    public interface IAccountService
    {

        int LoginGuest(string ip, HttpContextBase context);
        void LogoutGuest(object guestId);
        UserDTO GetUser(int id);
        UserDTO GetUser(string email);
        void RefreshGuest(int guestId);
        bool AddSubscriber(string email);
        ServiceResult<LoginDTO> LoginUser(LoginDTO logDTO, int? guestId);
        void LogoutUser();
        ServiceResult<RegistrationDTO> RegisterUser(RegistrationDTO regDTO,string confirmUrl);
        void ConfirmEmail(int userId);
    }
}
