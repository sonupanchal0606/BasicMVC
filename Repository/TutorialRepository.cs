using BasicMVC.Context;
using BasicMVC.Models;
using BasicMVC.ViewModels;

namespace BasicMVC.Repository
{
    public class TutorialRepository : ITutorialRepository
    {
        // method 1 to return data - create dummy data. 
        // private List<Tutorial> _tutorials;

        /*        public TutorialRepository()
        {
            // here were are jsut creating the dummy data. in reality we write the service layer, which fetched data from the DB and append inside the view
            _tutorials = new List<Tutorial> {
               new Tutorial{Id = 1, Name = "sonu", Description = "Blockchain developer" },
               new Tutorial{Id = 2, Name = "sonu2", Description = "ML developer" },
            };
        }*/

        // Method 2 - fetch data from DB and return from DB
        private readonly TutorialDbContext _context;
        public TutorialRepository(TutorialDbContext context) 
        {
            _context = context; 
        }


        public Tutorial Add(Tutorial tutorial)
        {
            tutorial.Id = 0; // EF treats 0 as default and generates a new ID
            _context.Tutorials.Add(tutorial);
            _context.SaveChanges();
            return tutorial;
        }


        public void Delete(int Id)
        {
            Tutorial tutorial = _context.Tutorials.Find(Id);
            if (tutorial != null)
            {
                _context.Tutorials.Remove(tutorial);
                _context.SaveChanges();
            }
        }


        public IEnumerable<Tutorial> GetAllTutorial()
        {
            // return _tutorials;
            return _context.Tutorials;
        }

        public async Task<Tutorial> GetTutorial(int Id)
        {
            return await _context.Tutorials.FindAsync(Id);
        }

        public Tutorial Update(Tutorial tutorialModified)
        {
            _context.Update(tutorialModified);
            _context.SaveChanges();
            return tutorialModified;
        }
    }
}
