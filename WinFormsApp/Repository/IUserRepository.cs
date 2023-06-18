using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.Model;

namespace WinFormsApp.Repository
{
    public interface IUserRepository
    {
        // IEnumerableはIListの親インタフェースです。添え字でアクセスできないようなもの。
        public IEnumerable<UserModel> FindAll();

        // UserModelがnullかもしれないのでUserModel?としてます
        public UserModel? FindById(int id);

        public void Create(UserModel userModel);

        public void Update(UserModel userModel);

        public void Delete(int id);
    }
}