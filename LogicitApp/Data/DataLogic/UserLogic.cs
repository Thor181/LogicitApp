using LogicitApp.Data.Models;
using LogicitApp.Shared.Results;

namespace LogicitApp.Data.DataLogic
{
    public class UserLogic : BaseLogic
    {
        public BaseResult IsValidUser(string login, string passwordHash)
        {
            var result = new BaseResult();

            var user = GetAll<User>(x => x.Login == login).SingleOrDefault();
            
            if (user == null)
            {
                result.Success = false;
                result.Message = $"Пользователь с логином \"{login}\" не найден в базе данных";
                return result;
            }

            if (passwordHash != user.PasswordHash)
            {
                result.Success = false;
                result.Message = $"Введен неверный пароль";
                return result;
            }

            return result;
        }
    }
}
