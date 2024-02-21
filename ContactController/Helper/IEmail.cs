namespace CursoYoutubeProgramadorTech.Helper
{
    public interface IEmail
    {
        bool Send(string email, string title, string message);
    }
}
