using BasicMVC.Context;
using BasicMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicMVC.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly TutorialDbContext _context;

        public ArticleRepository(TutorialDbContext context)
        {
            _context = context;
        }

        public void AddArticle(Article article)
        {
            var newArticle = new Article()
            {
                ArticleTitle = article.ArticleTitle,
                ArticleContent = article.ArticleContent,
                TutorialId = article.TutorialId
            };
            _context.Articles.AddAsync(newArticle);
            _context.SaveChangesAsync();
        }

        public void DeleteArticle(int Id)
        {
            Article article = _context.Articles.Find(Id);
            if (article != null)
            {
                _context.Articles.Remove(article);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Article>> GetAllArticle()
        {
            return  _context.Articles;
        }

        public async Task<IEnumerable<Tutorial>> GetAllTutorials()
        {
            return await _context.Tutorials.ToListAsync();
        }

        public async Task<Article> GetArticleById(int Id)
        {
            return await _context.Articles.FindAsync(Id);
        }

        public async Task<IEnumerable<Article>> GetArticlesByTutorialId(int tutorialId)
        {
            return await _context.Articles.Where(a => a.TutorialId == tutorialId).ToListAsync();
        }

        public Article UpdateArticle(Article updatedArticle)
        {
            _context.Update(updatedArticle);
            _context.SaveChanges();
            return updatedArticle;
        }
    }
}
