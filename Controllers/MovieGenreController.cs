using Microsoft.AspNetCore.Mvc;
using DesafioDiegoBenedetti.EF_Dapper.Models.EntityFrameWork;
using DesafioDiegoBenedetti.EF_Dapper.Repositories.EntityFrameWorkRepository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioDiegoBenedetti.EF_Dapper.Controllers.EntityFramework
{    
    public class MovieGenreController : Controller
    {
        //Instancia da interface genero de filme
        private readonly IMovieGenreRepository _interfaceMovieGenre;
        //Instancia da model usuário e busca os dados do usuário autenticado
        private UserModel user = UserController._userLogged;

        //Construtor que recebe a interface do genero de filme. Após receber denomina em cada variavel ->
        // privada de cada interface denominada
        public MovieGenreController(IMovieGenreRepository interfaceMovieGenre)
        {
            _interfaceMovieGenre = interfaceMovieGenre;
        }
        
        //Grava os dados do genero de filme no DB
        [HttpPost]
        public async Task<IActionResult> AddMovieGenre(MovieGenreModel movieGenre)
        {
            if(ModelState.IsValid)
                await _interfaceMovieGenre.Add(movieGenre);

            return Ok();
        }

        //Exclui um genero de filme selecionado no DB
        [HttpDelete]
        public async Task<IActionResult> DeleteMovieGenre(MovieGenreModel movieGenre)
        {
            if (ModelState.IsValid)
                await _interfaceMovieGenre.Delete(movieGenre);

            return Ok();
        }

        //Busca todos os generos de filme no DB
        [HttpGet]
        public async  Task<ICollection<MovieGenreModel>> ListAllMovieGenres()
        {
            return await _interfaceMovieGenre.ListAll();            
        }
    }
}