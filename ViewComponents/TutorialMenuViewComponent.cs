using BasicMVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BasicMVC.ViewComponents
{
    public class TutorialMenuViewComponent : ViewComponent
    {
        private readonly ITutorialRepository _tutorialRepository;
        public TutorialMenuViewComponent(ITutorialRepository tutorialRepository)
        {
            _tutorialRepository = tutorialRepository;
        }

        // synchronous method
        public IViewComponentResult Invoke()
        {
            var tutorials = _tutorialRepository.GetAllTutorial().OrderBy(p=>p.Name);
            return View(tutorials); // C:\Users\User\Downloads\.NetProject\BasicMVC\Views\Shared\Components\TutorialMenu\Default.cshtml ---> this view is called
        }

        //Asynchronous method
        /* public async Task<IViewComponentResult> InvokeAsync()
        {
            var tutorials = await _tutorialRepository.GetAllTutorial();
            return View(tutorials);
        }*/
    }
}
