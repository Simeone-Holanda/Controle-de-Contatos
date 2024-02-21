using CursoYoutubeProgramadorTech.Filters;
using CursoYoutubeProgramadorTech.Models;
using CursoYoutubeProgramadorTech.Repositories;
using CursoYoutubeProgramadorTech.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CursoYoutubeProgramadorTech.Controllers
{
    [LoggedInAdmin]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IContactRepository _contactRepository;
        public UserController(IUserRepository userRepository, IContactRepository contactRepository )
        {
            _userRepository = userRepository;
            _contactRepository = contactRepository;
        }

        public IActionResult Index()
        {
            List<UserModel> users = _userRepository.GetAll();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult ListContactsByUser(int id)
        {
            List<ContactModel> contacts = _contactRepository.GetAll(id);
            return PartialView("_UserContacts", contacts);
        }

        [HttpPost]
        public IActionResult Create(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepository.Add(user);

                    TempData["successMessage"] = "Usuário cadastrado com sucesso. ";
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception error)
            {
                TempData["ErrorMessage"] = $"Ops, não conseguimos cadastrar o usuário, tente novamente. Erro: {error.Message} ";
                return RedirectToAction("Index");
            }
        }


        public IActionResult Delete(int id)
        {
            UserModel? user = _userRepository.GetById(id);
            if (user == null)
            {
                return View();
            }
            return View(user);
        }

        public IActionResult DeleteUser(int id)
        {
            try
            {
                bool result = _userRepository.Delete(id);
                if (result)
                {
                    TempData["successMessage"] = "Usuário deletado com sucesso. ";
                }
                else
                {
                    TempData["ErrorMessage"] = "Não foi possível deletar o usuário. ";
                }
                return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                TempData["ErrorMessage"] = $"Ops, não conseguimos apagar o usuário, tente novamente. Erro: {error.Message} ";
                return RedirectToAction("Index");
            }
        }
        
        public IActionResult Update(int id)
        {
            UserModel? user = _userRepository.GetById(id);
            if (user == null)
            {
                return View();
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult Update(PasswordlessUserModel passwordlessUser)
        {
            try
            {
                UserModel user = null;

                if (ModelState.IsValid)
                {
                    user = new UserModel()
                    {
                        Id = passwordlessUser.Id,
                        Name = passwordlessUser.Name,
                        Email = passwordlessUser.Email,
                        Login = passwordlessUser.Login,
                        Profile = passwordlessUser.Profile
                    };
                    user = _userRepository.Update(user);

                    TempData["successMessage"] = "Usuário Atualizado com sucesso. ";
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception error)
            {
                TempData["ErrorMessage"] = $"Ops, não conseguimos atualizar seu usuário, tente novamente. Erro: {error.Message} ";
                return RedirectToAction("Index");
            }


        }
    }
}
