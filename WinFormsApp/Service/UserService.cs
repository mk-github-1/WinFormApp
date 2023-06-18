using System;
using System.Collections.Generic;
using System.Transactions;
using WinFormsApp.Model;
using WinFormsApp.Repository;

namespace WinFormsApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public IEnumerable<UserModel> FindAll()
        {
            return this._userRepository.FindAll();
        }

        public UserModel? FindById(int id)
        {
            return this._userRepository.FindById(id);
        }

        public void Create(UserModel userModel)
        {
            using (var transactionScope = new TransactionScope())
            {
                try
                {
                    this._userRepository.Create(userModel);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    throw;
                }
            };
        }

        public void Update(UserModel userModel)
        {
            using (var transactionScope = new TransactionScope())
            {
                try
                {
                    this._userRepository.Update(userModel);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public void Delete(int id)
        {
            using (var transactionScope = new TransactionScope())
            {
                try
                {
                    this._userRepository.Delete(id);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
