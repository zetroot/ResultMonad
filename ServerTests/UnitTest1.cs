using Abstractions.Providers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Filters;
using Refit;

namespace ServerTests;

public class AppTests
{
    private AppFactory _factory;
    private IMainProvider _mainProvider;
    
    [SetUp]
    public void Setup()
    {
        _factory = new AppFactory();
        var client = RestService.For<IMainClient>(_factory.CreateClient());
        _mainProvider = new MainProvider(client);
    }

    [Test] 
    public async Task Sut_WhenNegative_ReturnsProblem()
    {
        var result = await _mainProvider.GetData(-1);
        result.IsT1.Should().BeTrue();
        result.AsT1.Title.Should().Be("No negatives allowed");
    }

    [Test] 
    public async Task Sut_WhenLittle_ReturnsProblem()
    {
        var result = await _mainProvider.GetData(34);
        result.IsT1.Should().BeTrue();
        result.AsT1.Title.Should().Be("Too little");
    }

    
    [Test]
    public async Task Sut_WhenGreat_ReturnsResult()
    {
        var result = await _mainProvider.GetData(42);
        result.IsT0.Should().BeTrue();
        result.AsT0.Data.Should().Be(84);
    }
}
