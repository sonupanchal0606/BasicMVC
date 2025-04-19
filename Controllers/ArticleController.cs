using BasicMVC.Models;
using BasicMVC.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasicMVC.Controllers
{
    public class ArticleController : Controller
    {

        private readonly IArticleRepository _articleRepository;
        
        public ArticleController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Article> articles = await _articleRepository.GetAllArticle();
            return View(articles);
        }

        public async Task<IActionResult> DisplayArticlesByTutorialId(int id)
        {
            IEnumerable<Article> articles = await _articleRepository.GetArticlesByTutorialId(id);
            return View(articles);
        }

        public async Task<IActionResult> GetArticleByArticleId(int id)
        {
            Article article = await _articleRepository.GetArticleById(id);
            return View(article);

        }

        [HttpGet]
        public async Task<IActionResult> AddNewArticle()
        {
            var tutorials = await _articleRepository.GetAllTutorials();
            ViewBag.Tutorials = new SelectList(tutorials, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewArticle(Article article)
        {
            if (!ModelState.IsValid)
            {
                var tutorials = await _articleRepository.GetAllTutorials();
                ViewBag.Tutorials = new SelectList(tutorials, "Id", "Name");
                return View(article);
            }

            _articleRepository.AddArticle(article);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> EditArticle(int id)
        {
            Article article = await _articleRepository.GetArticleById(id);
/*            var data = new Article()
            {
                ArticleTitle = article.ArticleTitle,
                ArticleContent = article.ArticleContent
            };*/
/*            var tutorials = await _articleRepository.GetAllTutorials();
            ViewBag.Tutorials = new SelectList(tutorials, "Id", "Name");*/
            return View(article);
        }


        [HttpPost]
        public async Task<IActionResult> EditArticle(Article modifiedData)
        {

            if (!ModelState.IsValid)
            {
                return View(modifiedData);
            }
            Article article = await _articleRepository.GetArticleById(modifiedData.ArticleId);
            article.ArticleTitle = modifiedData.ArticleTitle;
            article.ArticleContent = modifiedData.ArticleContent;
            _articleRepository.UpdateArticle(article);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteArticle(int id)
        {
            _articleRepository.DeleteArticle(id);
            return RedirectToAction("Index");
        }

    }
}
