using System;
using System.Collections.Generic;
using DesafioDiegoBenedetti.EF_Dapper.Models.Dapper;
using DesafioDiegoBenedetti.EF_Dapper.Models;
using DesafioDiegoBenedetti.EF_Dapper.Repositories.Dapper;
using DesafioDiegoBenedetti.EF_Dapper.Repositories.EntityFrameWorkRepository;
using DesafioDiegoBenedetti.EF_Dapper.Models.EntityFrameWork;
using DesafioDiegoBenedetti.EF_Dapper.Controllers.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioDiegoBenedetti.EF_Dapper.Controllers.Dapper
{
    public class MovieController : Controller
    {
        //Instancia da interface filme
        private readonly IMovieRepository _movieRepository;
        //Instancia da interface genero de filme
        private readonly IMovieGenreRepository _genreRepository;
        //Instancia da model usuário e busca os dados do usuário autenticado
        private static UserModel user = UserController._userLogged;

        //Busca a lista de genero de filme para informa na combo genero
        [HttpGet]
        public async Task<IActionResult> Movies()
        {
            if (View().ViewData.ContainsKey("Message"))
                View().ViewData["Message"] = string.Empty;

            Movie_GenreModel movie_Genre = new Movie_GenreModel();
            //busca todos os generos de filmes
            IEnumerable<MovieGenreModel> movieGenre = await _genreRepository.ListAll(); 

            //Busca os generos de filme pelo nome do genero
            movie_Genre.genre = movieGenre.ToList().Select(g => g.name).ToList(); 
            return View(movie_Genre);
        }

        //Construtor que recebe a interface do genero de filme e filme. Após receber denomina em cada variavel ->
        // privada de cada interface denominada
        public MovieController(IMovieRepository movieRepository,IMovieGenreRepository genreRepository)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
        }

        //insere os dados do Filme no DB, existe uma verificação que se caso o idMovie foi informado irá realizar a ->
        //alteração, mas caso seja um novo registro ele insere no DB e retorna para a lista de filmes
        [HttpPost]
        public async Task<IActionResult> SaveMovie(Movie_GenreModel movie)
        {
            MovieModel movieModel = new MovieModel();
            IEnumerable<MovieGenreModel> movieGenre = await _genreRepository.ListAll();

            movieModel.name = movie.nameMovie;
            movieModel.idGenreMovie = movieGenre.First(g => g.name == movie.movieGenre).idGenreMovie;
            movieModel.status = movie.status;
            movieModel.createdAt = DateTime.Now;

            if (ModelState.IsValid && !string.IsNullOrEmpty(movieModel.name))
            {
                if (movie.idMovie == Guid.Empty)
                {
                    await _movieRepository.Add(movieModel);
                    //Caso o filme seja cadastrado com sucesso informar o usuário
                    View("~/Views/Movie/Movies.cshtml").TempData["Message"] = "Filme cadastrado com sucesso";
                }
                else
                {
                    movieModel.idMovie = movie.idMovie;
                    await _movieRepository.Update(movieModel);
                    //Caso o filme seja alterado com sucesso informar o usuário
                    View("~/Views/Movie/Movies.cshtml").TempData["Message"] = "Filme alterado com sucesso";                    
                }
            }
            else
            {
                //Se o usuário não informou todos os campos no formulário irá lançar a mensagem
                View("~/Views/Movie/Movies.cshtml").TempData["Message"] = "Favor informar todos os campos necessarios";

                return  RedirectToAction("Movies", "Movie");
            }

            return RedirectToAction("ListMovies", "ListMovie");
        }

        //Exclui os filmes selecionado e que veio da ação da listagem de filmes e  redireciona para a ação ListMovies
        // que atualiza a grid de filmes
        [HttpGet]
        public async Task<IActionResult> DeleteMovies(Guid idMovie)
        {
            MovieModel movieModel = new MovieModel();
            movieModel.idMovie = idMovie;

            if (ModelState.IsValid)
            {
                await _movieRepository.Delete(movieModel);
                //Caso o filme seja removido irá lançar a mensagem de cadastrado com sucesso
                View("~/Views/ListMovie/ListMovies.cshtml").TempData["Message"] = "Filme removido com sucesso!";
            }
            else
                //Verifica se o modelo passado para excluir está correto
                View("~/Views/ListMovie/ListMovies.cshtml").TempData["Message"] = "Filme não removido, favor tentar novamente!";

            return RedirectToAction("ListMovies", "ListMovie",user);
        }

        //Deleta vários registros selecionados da grid de filmes e redireciona para a ação ListMovies
        // que atualiza a grid de filmes
        [HttpPost]
        public async Task<IActionResult> DeleteSeveralMovies(Movie_Genrel movies)
        {        
            if (movies.movie_GenrelModel != null)
            {
                foreach (var item in movies.movie_GenrelModel)
                {
                    //Verifica se existe filmes selecionados para exclusão
                    if (item.isChecked)
                    {
                        MovieModel movieModel = new MovieModel();
                        movieModel.idMovie = item.idMovie;

                        if (ModelState.IsValid)
                        {
                            //Se o filme for cadastrado com sucesso irá lançar a mensagem                             
                            await _movieRepository.Delete(movieModel);
                            View("~/Views/ListMovie/ListMovies.cshtml").TempData["Message"] = "Filmes excluidos com sucesso!";
                        }
                        else
                            //Caso o modelo que p usuário passou do formulário for inválido, lançar a mensagem
                            //para o usuário informar todos os campos
                            View("~/Views/ListMovie/ListMovies.cshtml").TempData["Message"] = "Nao foi possivel excluir os filmes, favor tentar novamente!";
                    }
                    //Caso o usuário não selecionar nenhum registro para exclui irá lanças a mensagem de informação
                    else
                        View("~/Views/ListMovie/ListMovies.cshtml").TempData["Message"] = "Favor selecionar os filmes antes de tentar exclui-los!";
                }
            }

            return RedirectToAction("ListMovies","ListMovie");
        }
        //busca todos os filmes cadastrados no DB
        [HttpGet]
        public async Task<IEnumerable<MovieModel>> ListAll()
        {            
            return await _movieRepository.ListAll();
        }
    }
}