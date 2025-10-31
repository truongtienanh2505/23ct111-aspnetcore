using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using LearnAspNetCore.Models; 

[ApiController]
[Route("[controller]")] 
public class NewsController : ControllerBase
{
    private readonly IMemoryCache _cache;
    private const string NewsCacheKey = "NewsList"; 
    public NewsController(IMemoryCache cache)
    {
        _cache = cache;
    }
    private static List<NewsItem> GetNewsFromDatabase()
    {
        return new List<NewsItem>
        {
            new NewsItem { Id = 1, Title = "Tin tức 1", Content = "Nội dung tin tức số 1...", PublishDate = DateTime.Now.AddMinutes(-5) },
            new NewsItem { Id = 2, Title = "Tin tức 2", Content = "Nội dung tin tức số 2...", PublishDate = DateTime.Now.AddMinutes(-10) },
            new NewsItem { Id = 3, Title = "Tin tức 3", Content = "Nội dung tin tức số 3...", PublishDate = DateTime.Now.AddMinutes(-15) },
            new NewsItem { Id = 4, Title = "Tin tức 4", Content = "Nội dung tin tức số 4...", PublishDate = DateTime.Now.AddMinutes(-20) },
            new NewsItem { Id = 5, Title = "Tin tức 5", Content = "Nội dung tin tức số 5...", PublishDate = DateTime.Now.AddMinutes(-25) }
        };
    }
    [HttpGet]
    public IActionResult Get()
    {
        List<NewsItem> newsList;
        if (_cache.TryGetValue(NewsCacheKey, out newsList))
        {
            return Ok(new { Source = "Cache", Data = newsList, CacheTime = DateTime.Now });
        }

        newsList = GetNewsFromDatabase();
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(60)); 

        _cache.Set(NewsCacheKey, newsList, cacheEntryOptions);

        return Ok(new { Source = "Database", Data = newsList, CacheTime = DateTime.Now });
    }
    [HttpPost("clear-cache")]
    public IActionResult ClearCache()
    {
        if (_cache.TryGetValue(NewsCacheKey, out _))
        {
            _cache.Remove(NewsCacheKey);
            
            return Ok(new { Message = "đã xoá thành công.", CacheKey = NewsCacheKey });
        }
        return Ok(new { Message = "Không có dữ liệu để xoá.", CacheKey = NewsCacheKey });
    }
}