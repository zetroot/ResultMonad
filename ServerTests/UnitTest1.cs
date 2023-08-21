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
    public async Task Sut_WhenNoFailure_ReturnsData()
    {
        var result = await _mainProvider.GetData(false);
        result.IsT0.Should().BeTrue();
        result.AsT0.Data.Should().Be(42);
    }
    
    [Test]
    public async Task Sut_WhenFailureRequested_ReturnsProblemDetails()
    {
        var result = await _mainProvider.GetData(true);
        result.IsT1.Should().BeTrue();
        result.AsT1.Should().NotBeNull();
    }
}
