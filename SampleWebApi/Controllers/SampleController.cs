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
        var city = await GenerateCityAsync().ConfigureAwait(false);
        logger.LogWarning($"CallBack in {Thread.CurrentThread.ManagedThreadId}");
        return city;
    }
    
    [HttpGet]
    public async Task<string> Sample2()
    {
        logger.LogWarning($"Starting execute in {Thread.CurrentThread.ManagedThreadId}");
        var city = await GenerateCityAsync();
        logger.LogWarning($"CallBack in {Thread.CurrentThread.ManagedThreadId}");
        return city;
    }
    
    [HttpGet]
    public Task<string> Sample3()
    {
        logger.LogWarning($"Starting execute in {Thread.CurrentThread.ManagedThreadId}");
        var city = GenerateCityAsync();
        logger.LogWarning($"CallBack in {Thread.CurrentThread.ManagedThreadId}");
        return city;
    }
    
    [HttpGet]
    public async ValueTask<string> Sample4()
    {
        logger.LogWarning($"Starting execute in {Thread.CurrentThread.ManagedThreadId}");
        var city = await GenerateCityAsync();
        logger.LogWarning($"CallBack in {Thread.CurrentThread.ManagedThreadId}");
        return city;
    }
    
    [HttpGet]
    public Task<string> Sample5()
    {
        logger.LogWarning($"Starting execute in {Thread.CurrentThread.ManagedThreadId}");
        var city = GenerateCity();
        logger.LogWarning($"CallBack in {Thread.CurrentThread.ManagedThreadId}");
        return city;
    }
    
    [NonAction]
    private async Task<string> GenerateCityAsync()
    {
        var response = await new HttpClient().GetStringAsync("https://ya.ru");
        logger.LogWarning($"IO in {Thread.CurrentThread.ManagedThreadId}");
        return response;
    }
    
    [NonAction]
    private Task<string> GenerateCity()
    {
        var response = new HttpClient().GetStringAsync("https://ya.ru");
        logger.LogWarning($"IO in {Thread.CurrentThread.ManagedThreadId}");
        return response;
    }
}