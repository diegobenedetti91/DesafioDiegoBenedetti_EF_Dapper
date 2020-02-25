using System;
using System.Collections.Generic;
using System.Linq;
using DesafioDiegoBenedetti.EF_Dapper.Models.EntityFrameWork;
using DesafioDiegoBenedetti.EF_Dapper.Context;
using System.Threading.Tasks;

namespace DesafioDiegoBenedetti.EF_Dapper.Repositories.EntityFrameWorkRepository
{
    public class UserRepository : IUserRepository
    {
        private AppDBContext _context;

        //Todo repositório de usuário recebe como parametro a conexão do DBContext
        public UserRepository(AppDBContext context)
        {
            _context = context;
        }

        //Adiciona usuário
        public async Task<int> Add(UserModel user)
        {
            _context.users.Add(user);
            return await _context.SaveChangesAsync();
        }

        //Exclui usuário 
        public async Task<int> Delete(UserModel user)
        {
            _context.users.Remove(user);
            return await _context.SaveChangesAsync();
        }


        //Busca todos os usuários da tabela
        public async Task<IEnumerable<UserModel>> ListUsers()
        {
            return await _context.users.ToAsyncEnumerable().ToList();
        }

        //Busca os dados do usuário através do CPF
        public async Task<UserModel> ListOfId(string cpf)
        {
            return await _context.users.ToAsyncEnumerable().FirstOrDefault(u => u.cpf == cpf);
        }

        //Altera os dados da tabela usuário
        public async Task<int> Update(UserModel user)
        {
            _context.users.Update(user);
            return await _context.SaveChangesAsync();
        }
    }
}