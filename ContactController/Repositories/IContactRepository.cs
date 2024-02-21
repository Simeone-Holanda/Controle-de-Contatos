using CursoYoutubeProgramadorTech.Models;

namespace CursoYoutubeProgramadorTech.Repository
{
    public interface IContactRepository
    {
        ContactModel Add(ContactModel contact);

        List<ContactModel> GetAll(int userId);

        ContactModel? GetById(int Id);

        ContactModel Update(ContactModel contactUpdate);

        bool Delete(int id);
    }
}
