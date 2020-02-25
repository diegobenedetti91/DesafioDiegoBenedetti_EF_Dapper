using System;
using System.Collections.Generic;
using DesafioDiegoBenedetti.EF_Dapper.Controllers.EntityFramework;
using DesafioDiegoBenedetti.EF_Dapper.Models;
using DesafioDiegoBenedetti.EF_Dapper.Repositories.Dapper;
using DesafioDiegoBenedetti.EF_Dapper.Repositories.EntityFrameWorkRepository;
using DesafioDiegoBenedetti.EF_Dapper.Models.EntityFrameWork;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioDiegoBenedetti.EF_Dapper.Controllers
{
    public class ListMovieController : Controller
    {
        //Instancia da interface genero de filme
        private readonly IMovieGenreRepository _genreRepository;
        //Instancia da interface filme
        private readonly IMovieRepository _movieRepository;
        //Instancia da model usuário e busca os dados do usuário autenticado
        private UserModel user = UserController._userLogged;

        //Construtor que recebe a interface do genero de filme e filme. Após receber denomina em cada variavel ->
        // privada de cada interface denominada
        public ListMovieController(IMovieGenreRepository genreRepository, IMovieRepository movieRepository)
        {
            _genreRepository = genreRepository;
            _movieRepository = movieRepository;
        }

        //Faz a busca dos filmes pelo cpf do usuário e insere numa lista para montar pro view a listagem de filmes
        //Também faz a busca dos filmes que ainda não foram locados e estão disponíveis
        [HttpGet]
        public async Task<IActionResult> ListMovies()
        {
            if (View().ViewData.ContainsKey("Message"))
                View().ViewData["Message"] = string.Empty;

            IEnumerable<Movie_GenreModel> movieList = await _movieRepository.ListMovies(user.cpf);
            IEnumerable<Movie_GenreModel> movieListWithOutRental = await _movieRepository.ListMoviesWithOutRental();

            Movie_Genrel listMovieModel = new Movie_Genrel();            
            listMovieModel.movie_GenrelModel = new List<Movie_GenreModel>();

            listMovieModel.movie_GenrelModel.AddRange(movieListWithOutRental);

            foreach (var item in movieList)
            {                

                listMovieModel.movie_GenrelModel.Add(item);
            }

            return View(listMovieModel);
        }
            
        //Faz a busca na classe da View e envia os dados para o formulário do filme para edição
        [HttpGet]
        public async Task<IActionResult> EditMovie(Guid idMovie,string cpf)
        {
            IEnumerable<Movie_GenreModel> movieG = await _movieRepository.ListMoviesWithOutRental();
            Movie_GenreModel movie_Genre = movieG.FirstOrDefault(g => g.idMovie == idMovie);
            IEnumerable<MovieGenreModel> movieGenre = await _genreRepository.ListAll();

            movie_Genre.idMovie = idMovie;
            movie_Genre.genre = movieGenre.Select(g => g.name).ToList();
            return View("~/Views/Movie/Movies.cshtml", movie_Genre);
        }

        //Exclui o filme informado na tela de listagem
        [HttpGet]
        public IActionResult DeleteMovie(Movie_GenreModel movie)
        {
            return RedirectToAction("DeleteMovies","Movie",movie);
        }
    }
}