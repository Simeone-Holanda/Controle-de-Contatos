using CursoYoutubeProgramadorTech.Helper;
using CursoYoutubeProgramadorTech.Models;
using CursoYoutubeProgramadorTech.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursoYoutubeProgramadorTech.Controllers
{
    public class ChangePasswordController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessionUser _session;

        public ChangePasswordController(IUserRepository userRepository, ISessionUser session)
        {
            _userRepository = userRepository;
            _session = session;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Update(ChangePasswordModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel? user = _session.FindUserSession();

                    if(user == null)
                    {
                        return View("Index", "Login");
                    }
                    data.Id = user.Id;

                    _userRepository.ChangePassword(data);
                    TempData["successMessage"] = "Senha alterada com sucesso. ";
                }
                return View("Index", data);
            }
            catch (Exception error)
            {
                TempData["ErrorMessage"] = $"Ops, não conseguimos alterar sua senha, tente novamente. Erro: {error.Message} ";
                return View("Index", data);
            }
            return View();
        }
    }
}
