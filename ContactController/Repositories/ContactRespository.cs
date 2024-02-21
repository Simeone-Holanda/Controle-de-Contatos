using CursoYoutubeProgramadorTech.Data;
using CursoYoutubeProgramadorTech.Models;

namespace CursoYoutubeProgramadorTech.Repository
{
    public class ContactRespository : IContactRepository
    {

        private readonly ConnectionContext _connectionContext;
        public ContactRespository(ConnectionContext connectionContext)
        {
            _connectionContext = connectionContext;
        }


        public ContactModel? GetById(int Id)
        {
            return _connectionContext.Contacts.FirstOrDefault(c => c.Id == Id);
        }

        public ContactModel Update(ContactModel contactUpdate) 
        {
            ContactModel? contact = GetById(contactUpdate.Id);
            if(contact == null)
            {
                throw new Exception("Ocorreu um erro ao atualizar os dados do usuário. ");
            }
            contact.Name = contactUpdate.Name;
            contact.Email = contactUpdate.Email;
            contact.Phone = contactUpdate.Phone;

            _connectionContext.Contacts.Update(contact);
            _connectionContext.SaveChanges();
            return contact;
        }

        public List<ContactModel> GetAll(int userId)
        {
            return _connectionContext.Contacts.Where(c => c.UserId == userId).ToList();
        }

        public ContactModel Add(ContactModel contact)
        {
            _connectionContext.Contacts.Add(contact);
            _connectionContext.SaveChanges();
            return contact;
        }

        public bool Delete(int id)
        {
            ContactModel? contact = GetById(id);
            if (contact == null)
            {
                throw new Exception("Ocorreu um erro ao deletar o contato. ");
            }
            _connectionContext.Contacts.Remove(contact);
            _connectionContext.SaveChanges();
            return true;
        }
    }
}
