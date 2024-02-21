using CursoYoutubeProgramadorTech.Helper;
using CursoYoutubeProgramadorTech.Models;
using CursoYoutubeProgramadorTech.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CursoYoutubeProgramadorTech.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessionUser _session;
        private readonly IEmail _email;

        public LoginController(IUserRepository userRepository, ISessionUser session, IEmail email)
        {
            _userRepository = userRepository;
            _session = session;
            _email = email;
        }

        public IActionResult Index()
        {
            if (_session.FindUserSession() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult Logout()
        {
            _session.RemoveUserSession();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Singin(LoginModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel? user = _userRepository.GetUserByLogin(data.Login);
                    if (user != null)
                    {
                        if (user.CheckPassword(data.Password))
                        {
                            _session.CreateUserSession(user);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["ErrorMessage"] = $"Senha inválida, tente novamente. ";
                        return View("Index");

                    }
                    TempData["ErrorMessage"] = $"Login e/ou Senha inválido(s)";
                }
                return View("Index");

            }
            catch (Exception error)
            {
                TempData["ErrorMessage"] = $"Ops, não conseguimos realizar ao login, tente novamente. Erro: {error.Message} ";
                return RedirectToAction("Index");
            }
        }

        public IActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendLinkToRecoverPassword(RecoverPasswordModel data)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    UserModel? user = _userRepository.GetByLoginAndEmail(data.Login, data.Email);
                    if (user != null)
                    {
                        string newPassword = user.GenerateNewPassowrd();
                        string message = $"Sua nova senha é {newPassword}";
                        bool sendedMail = _email.Send(data.Email, "Sistema de Contatos - Nova senha", message);

                        if (sendedMail)
                        {
                            TempData["SuccessMessage"] = $"Enviamos para seu e-mail cadastrado uma nova senha.";
                            _userRepository.Update(user);
                        }
                        else
                        {
                            TempData["ErrorMessage"] = $"Ops, não conseguimos enviar o email, tente novamente.";
                        }
                        return RedirectToAction("Index", "Login");

                    }
                    TempData["ErrorMessage"] = $"Ops, não conseguimos redefinir sua senha, verifique os dados informados. ";
                }
                return View("Index");

            }
            catch (Exception error)
            {
                TempData["ErrorMessage"] = $"Ops, não conseguimos redefinir sua senha, tente novamente. Erro: {error.Message} ";
                return RedirectToAction("Index");
            }
        }
    }
}
