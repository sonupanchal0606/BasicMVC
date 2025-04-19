using BasicMVC.Models;
using BasicMVC.ViewModels;

namespace BasicMVC.Repository
{
    public interface ITutorialRepository
    {
        Tutorial Add(Tutorial tutorial);

        Tutorial Update(Tutorial tutorial);

        void Delete(int Id);

        Task<Tutorial> GetTutorial(int Id);

        IEnumerable<Tutorial> GetAllTutorial();
    }
}
