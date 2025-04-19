using BasicMVC.Models;
using BasicMVC.Repository;
using BasicMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BasicMVC.Controllers
{
    public class TutorialController : Controller
    {
        private readonly ITutorialRepository _repository; // DI using constructor
        public TutorialController(ITutorialRepository repository) // DI 
        {
            _repository = repository;
        }


        [ViewData]
        public string title2 { get; set; } 
        public IActionResult Index()
        {
            // here were are jsut creating the dummy data. in reality we write the service layer, which fetched data from the DB and append inside the view
            var tutorials = new List<Tutorial> {
               new Tutorial{Id = 1, Name = "C#", Description = "C# Tutorial" },
               new Tutorial{Id = 2, Name = "MVC", Description = "MVC Tutorial" },
            };

            // there are different ways to pass data from controller to the view.
            // 1) here we use view data
            ViewData["tutorials"] = tutorials;

            ViewData["title"] = "Tutorial Details";

            title2 = "Tutorial 2";

            // 2) storing and accessing values using View Bag
            ViewBag.Tutorialss = tutorials;

            // 3) we can also store data using ViewData and access it via ViewBag. and vice versa
            ViewBag.fname = "Sonu";
            ViewData["lname"] = "Panchal";

            return View(); // return the view
        }

        // 4) using view model
        public IActionResult ViewModel_example()
        {
            var newModel = new TutorialViewModel()
            {
                tutorial = new Tutorial 
                {
                    Id = 1,
                    Name = ".net",
                    Description = ".net tutorial"
                },
                title_tv = "title from view model"
            };

            return View(newModel);
        }

        // returning data using repository pattern
        public IActionResult Index2()
        {
            // one way to get the data is by creating the instance. just create the instance and call the method.
            // but this is not the best way to create the instance b’s this makes our application tightly coupled.
            // var tut = new TutorialRepository().GetAllTutorial();

            // another way to get the data is using DI
            // 1) Register the Dependency : .net support built in dependency injection container. Register it in program.cs. add service (scopped / transient / singleton)
            // 2) Inject the Dependency : inject the Dependency inside the controller using constructor injection 
            var tut = _repository.GetAllTutorial();

            return View(tut);
        }

        [HttpGet]
        public IActionResult CreateTutorial() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTutorial(Tutorial tutorial) 
        {
            if(!ModelState.IsValid)
            {
                return View(tutorial);
            }
            Tutorial newtutorial = _repository.Add(tutorial);
            return Redirect("Index2");  
        }

        [HttpGet]
        public async Task<IActionResult> EditTutorial(int id)
        {
            Tutorial tutorial = await _repository.GetTutorial(id);
            return View(tutorial);
        }

        [HttpPost]
        public async Task<IActionResult> EditTutorial(Tutorial modifiedData)
        {
            Tutorial tutorial = await _repository.GetTutorial(modifiedData.Id);
            tutorial.Name = modifiedData.Name;
            tutorial.Description = modifiedData.Description;
            Tutorial updatedTutorial = _repository.Update(tutorial);
            return RedirectToAction("Index2");
        }

        public IActionResult DeleteTutorial(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index2");
        }
    }
}
