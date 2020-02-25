using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioDiegoBenedetti.EF_Dapper.Models.EntityFrameWork;

namespace DesafioDiegoBenedetti.EF_Dapper.Repositories.EntityFrameWorkRepository
{
    //Criação do CRUD Usuário
    public interface IUserRepository
    {
        Task<int> Add(UserModel user);

        Task<int> Update(UserModel user);

        Task<int> Delete (UserModel user);

        Task<IEnumerable<UserModel>> ListUsers();

        Task<UserModel> ListOfId(string cpf);
    }
}