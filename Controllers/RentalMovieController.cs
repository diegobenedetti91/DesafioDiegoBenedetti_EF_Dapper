using System;
using System.Collections.Generic;
using DesafioDiegoBenedetti.EF_Dapper.Models.Dapper;
using DesafioDiegoBenedetti.EF_Dapper.Models;
using DesafioDiegoBenedetti.EF_Dapper.Repositories.Dapper;
using DesafioDiegoBenedetti.EF_Dapper.Controllers.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using DesafioDiegoBenedetti.EF_Dapper.Models.EntityFrameWork;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioDiegoBenedetti.EF_Dapper.Controllers.Dapper
{
    public class RentalMovieController : Controller
    {
        //Instancia da interface loca��o de filme
        private readonly IRentalMovieRepository _rentalMovieRepository;
        //Instancia da interface filme
        private readonly IMovieRepository _movieRepository;
        //Instancia da model usu�rio e busca os dados do usu�rio autenticado
        private static UserModel user = new UserModel();

        //Construtor que recebe a interface do loca��o de filme e filme. Ap�s receber denomina em cada variavel ->
        // privada de cada interface denominada
        public RentalMovieController(IRentalMovieRepository rentalMovieRepository,IMovieRepository movieRepository)
        {
            _rentalMovieRepository = rentalMovieRepository;
            _movieRepository = movieRepository;
            user = UserController._userLogged;
        }

        //Busca os dados do usu�rio e filmes, e envia os dados para o formul�rio de cadastro de loca��o
        // a busca somente retorna os filmes que est�o ativos
        [HttpGet]
        public async Task<IActionResult> RentalMovies()
        {
            if (View().ViewData.ContainsKey("Message"))
                View().ViewData["Message"] = string.Empty;

            Movie_RentalViewModel movie_Rental = new Movie_RentalViewModel();
            IEnumerable<Movie_GenreModel> movie = await _movieRepository.ListMoviesWithOutRental();

            movie_Rental.user = user;
            movie_Rental.cpf = user.cpf;
            movie_Rental.movies = movie.Select(g => g.nameMovie).ToList();

            return View(movie_Rental);
        }

        //Grava os dados de loca��o de filme e redireciona para a a��o ListMovies para atualizar a lista de filmes
        [HttpPost]
        public async Task<IActionResult> AddRentalMovie(Movie_RentalViewModel rentalMovie, RentalMovieModel rentalMovieModel)
        {
            if (ModelState.IsValid)
            {
                rentalMovieModel = new RentalMovieModel();
                IEnumerable<MovieModel> movie = await _movieRepository.ListAll();

                rentalMovieModel.cpf = rentalMovie.cpf;
                rentalMovieModel.idMovie = movie.FirstOrDefault(m => m.name == rentalMovie.nameMovie).idMovie;
                rentalMovieModel.rentalDate = rentalMovie.rentalDate;

                if (rentalMovie.rentalDate == null)
                    rentalMovie.rentalDate = DateTime.Now;

                await _rentalMovieRepository.Add(rentalMovieModel);

                View("~/Views/ListMovie/ListMovies.cshtml").TempData["Message"] = "Locacao registrada com sucesso";
            }
            else
            {
                View("~/Views/RentalMovie/RentalMovies.cshtml").TempData["Message"] = "Favor informar todos os campos necessarios!";
                return RedirectToAction("RentalMovies", "RentalMovie");
            }                

            return RedirectToAction("ListMovies", "ListMovie");
        }

        //Exclui a loca��o pelo cpf do usu�rio e o id do filme
        [HttpGet]
        public async Task<ActionResult> DeleteRentalMovie(string cpf, Guid idMovie)
        {
            RentalMovieModel rentalMovie = new RentalMovieModel();
            rentalMovie.cpf = cpf;
            rentalMovie.idMovie = idMovie;

            await _rentalMovieRepository.DeleteRentalMovie(rentalMovie);

            return RedirectToAction("ListMovies", "ListMovie");
        }

        //Retorna todos os dados da loca��o idnependente do CPF
        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            return View(await _rentalMovieRepository.ListAll());
        }
    }
}