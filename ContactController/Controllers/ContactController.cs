using CursoYoutubeProgramadorTech.Filters;
using CursoYoutubeProgramadorTech.Helper;
using CursoYoutubeProgramadorTech.Models;
using CursoYoutubeProgramadorTech.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CursoYoutubeProgramadorTech.Controllers
{
    [LoggedInUser]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly ISessionUser _session;

        public ContactController(IContactRepository contactRepository, ISessionUser session)
        {
            _contactRepository = contactRepository;
            _session = session;
        }

        public IActionResult Index()
        {
            UserModel? user = _session.FindUserSession();

            if (user == null) return RedirectToAction("Index", "Login");

            List<ContactModel> contacts = _contactRepository.GetAll(user.Id);
            return View(contacts);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(int id)
        {
            ContactModel? contact = _contactRepository.GetById(id);
            if (contact == null)
            {
                return View();
            }
            return View(contact);
        }

        public IActionResult Delete(int id)
        {
            ContactModel? contact = _contactRepository.GetById(id);
            if (contact == null)
            {
                return View();
            }
            return View(contact);
        }

        public IActionResult DeleteContact(int id)
        {
            try
            {
                bool result = _contactRepository.Delete(id);
                if (result)
                {
                    TempData["successMessage"] = "Contato deletado com sucesso. ";
                }
                else
                {
                    TempData["ErrorMessage"] = "Não foi possível deletar o contato. ";
                }
                return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                TempData["ErrorMessage"] = $"Ops, não conseguimos apagar seu contato, tente novamente. Erro: {error.Message} ";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult Create(ContactModel contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel? user = _session.FindUserSession();

                    contact.UserId = user.Id;

                    _contactRepository.Add(contact);

                    // return Ok();
                    TempData["successMessage"] = "Contato cadastrado com sucesso. ";
                    return RedirectToAction("Index");
                }
                return View(contact);
            }
            catch (Exception error)
            {
                TempData["ErrorMessage"] = $"Ops, não conseguimos cadastrar seu contato, tente novamente. Erro: {error.Message} ";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Update(ContactModel contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel? user = _session.FindUserSession();

                    contact.UserId = user.Id;

                    _contactRepository.Update(contact);

                    TempData["successMessage"] = "Contato Atualizado com sucesso. ";
                    return RedirectToAction("Index");
                }
                return View(contact);
            }
            catch (Exception error)
            {
                TempData["ErrorMessage"] = $"Ops, não conseguimos atualizar seu contato, tente novamente. Erro: {error.Message} ";
                return RedirectToAction("Index");
            }


        }

    }
}
