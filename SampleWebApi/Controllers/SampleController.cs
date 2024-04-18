using Microsoft.AspNetCore.Mvc;

namespace SampleWebApiApp.Controllers;

[ApiController]
[Area("api")]
[Route("[area]/v1/[controller]/[action]")]
public class SampleController(ILogger<SampleController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<string> Sample1()
    {
        logger.LogWarning($"Starting execute in {Thread.CurrentThread.ManagedThreadId}");
        var city = await GenerateCity().ConfigureAwait(false);
        logger.LogWarning($"CallBack in {Thread.CurrentThread.ManagedThreadId}");
        return city;
    }
    
    [HttpGet]
    public async Task<string> Sample2()
    {
        logger.LogWarning($"Starting execute in {Thread.CurrentThread.ManagedThreadId}");
        var city = await GenerateCity();
        logger.LogWarning($"CallBack in {Thread.CurrentThread.ManagedThreadId}");
        return city;
    }
    
    [HttpGet]
    public Task<string> Sample3()
    {
        logger.LogWarning($"Starting execute in {Thread.CurrentThread.ManagedThreadId}");
        var city = GenerateCity();
        logger.LogWarning($"CallBack in {Thread.CurrentThread.ManagedThreadId}");
        return city;
    }
    
    [NonAction]
    private async Task<string> GenerateCity()
    {
        var task = new List<int>(10000);
        for (var i = 0; i < 10000; i++)
        {
            task.Add(i);
        }
        var httClient = new HttpClient();
        var response = await httClient.GetAsync("https://google.com");
        logger.LogWarning($"IO in {Thread.CurrentThread.ManagedThreadId}");
        await response.Content.ReadAsStringAsync();
        return "Hello Google";
    }
}