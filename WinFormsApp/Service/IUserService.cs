using System.Collections.Generic;
using WinFormsApp.Model;
using WinFormsApp.Repository;

namespace WinFormsApp.Services
{
    public interface IUserService
    {
        public IEnumerable<UserModel> FindAll();

        public UserModel? FindById(int id);

        public void Create(UserModel userModel);

        public void Update(UserModel userModel);

        public void Delete(int id);
    }
}
