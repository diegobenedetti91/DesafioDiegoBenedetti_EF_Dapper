using Microsoft.AspNetCore.Mvc;
using DesafioDiegoBenedetti.EF_Dapper.Models.EntityFrameWork;
using DesafioDiegoBenedetti.EF_Dapper.Repositories.EntityFrameWorkRepository;
using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace DesafioDiegoBenedetti.EF_Dapper.Controllers.EntityFramework
{    
    public class UserController :Controller
    {
        //Instancia da interface usu�rio
        private readonly IUserRepository _interfaceUser;
        //Instancia da model usu�rio para informar o usu�rio logadoo
        public static UserModel _userLogged;

        //Construtor que recebe a interface do usu�rio e instancia uma nova classe do usu�rio
        public UserController(IUserRepository interfaceUser)
        {
            _interfaceUser = interfaceUser;
            _userLogged = new UserModel();
        }
        
        //Redireciona para a pagina Home
        [HttpGet]
        public IActionResult Users()
        {
            if (View().ViewData.ContainsKey("Message"))
                View().ViewData["Message"] = string.Empty;

            return View();
        }


        //Verifica se o modelo usu�rio passado � valido, insere os dados no DB e caso n�o ->
        //seja v�lido retorna na tela de cadastro informando os campos obrigat�rios.        
        [HttpPost]
        public async Task<IActionResult> AddUsers(UserModel user)
        {
            user.createdAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                IEnumerable<UserModel> userFound = await _interfaceUser.ListUsers();

                if (userFound.FirstOrDefault(u => u.nameUser == user.nameUser) != null)
                {
                    //Caso o nome do usu�rio existir no banco de dados ir� lan�ar a mensagem
                    View("~/Views/Home/Index.cshtml").TempData["Message"] = "Usuario ja existe, favor informar outro nome!";
                    return View("~/Views/User/Users.cshtml");
                }

                await _interfaceUser.Add(user);
                //Caso usu�rio for cadastrado no banco de dados ir� lan�ar a mensagem
                View("~/Views/Home/Index.cshtml").TempData["Message"] = "Usuario cadastrado com sucesso!";
                return View("~/Views/Home/Index.cshtml");
            }

            //Caso o modelo n�o for v�lido ir� mandar o usu�rio informar todos os campos
            View("~/Views/User/Users.cshtml").TempData["Message"] = "Informar todos os campos para cadastro!!";
            return View("~/Views/User/Users.cshtml");
        }

        //busca todos os usu�rios cadastrados no sistema
        [HttpGet]
        public async Task<IActionResult> ListAllUsers()
        {            
            return Ok(await _interfaceUser.ListUsers());
        }

        //Valida se o cpf e a senha iformada existe no DB, caso n�o exista ir� retornar para tela de login informando ->
        // que o usu�rio � inv�lido, caso encontre ir� direcionar o usu�rio para a lista de filmes 
        [HttpGet]
        public async Task<IActionResult> ValidUser(UserModel user)
        {
            IEnumerable<UserModel> users;
            UserModel userFound = new UserModel();

            if (!string.IsNullOrEmpty(user.nameUser) && !string.IsNullOrEmpty(user.password))
            {
                users = await _interfaceUser.ListUsers();
                userFound = users.ToList().Find(u => u.nameUser == user.nameUser && u.password == user.password);
            }
            else
            {
                // Caso o usu�rio n�o informe os dados de login e senha ir� lan�ar a mensagem
                View("~/Views/Home/Index.cshtml").TempData["Message"] = "Favor informar usuario e senha antes de realizar o login!";
                return View("~/Views/Home/Index.cshtml");
            }


            if (userFound == null)
            {
                // Caso os dados de login e senha n�o existirem ir� lan�ar a mensagem
                View("~/Views/Home/Index.cshtml").TempData["Message"] = "Usuario nao encontrado!";
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                _userLogged = userFound;
                return RedirectToAction("ListMovies", "ListMovie", userFound);
            }                                 
        }
    }
}