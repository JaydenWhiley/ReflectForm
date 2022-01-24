using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FormEngine;
using Xunit;

namespace Dotnet;

public class FormGenerationTests
{

    public void AssertJson(object expected, object actual, bool log = false)
    {
        var options = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };
        var contentExpected = JsonSerializer.Serialize(expected, options);
        var contentActual = JsonSerializer.Serialize(actual, options);
        if (log)
        {
            Console.WriteLine("\n\n");
            Console.WriteLine(contentActual);
            Console.WriteLine("\n\n");
        }
        Assert.Equal(contentExpected, contentActual);
    }

    [Fact]
    public async Task DefaultFieldGeneration()
    {
        FormEngineInstance myEngine = new FormEngineInstance();
        var myFields = await myEngine.GetFields(typeof(RawClassExample));
        AssertJson(RawClassExample.ExpectedResult, myFields);
    }

    [Fact]
    public async Task BasicFieldProcessorGeneration()
    {
        FormEngineInstance myEngine = new FormEngineInstance();
        var myFields = await myEngine.GetFields(typeof(BasicClassProcessorExample));
        AssertJson(BasicClassProcessorExample.ExpectedResult, myFields);
    }

    [Fact]
    public async Task RenderBeforeGenerator()
    {
        FormEngineInstance myEngine = new FormEngineInstance();
        var myFields = await myEngine.GetFields(typeof(RenderBeforeGeneratorExample));
        AssertJson(RenderBeforeGeneratorExample.ExpectedResult, myFields);
    }

    [Fact]
    public async Task BasicFieldTypeProcessor()
    {
        FormEngineInstance myEngine = new FormEngineInstance();
        var myFields = await myEngine.GetFields(typeof(BasicFieldTypeAttributeExample));
        AssertJson(BasicFieldTypeAttributeExample.ExpectedResult, myFields);
    }

    [Fact]
    public async Task CanRenderMultipleFields()
    {
        FormEngineInstance myEngine = new FormEngineInstance();
        var myFields = await myEngine.GetFields(typeof(RenderMultipleFieldsExample));
        AssertJson(RenderMultipleFieldsExample.ExpectedResult, myFields);
    }

    [Fact]
    public async Task CanRenderUsingAsyncDelegateSource()
    {
        FormEngineInstance myEngine = new FormEngineInstance();
        var myFields = await myEngine.GetFields(typeof(RenderFieldsWithAsyncSources));
        AssertJson(RenderFieldsWithAsyncSources.ExpectedResult, myFields);
    }

    [Fact]
    public async Task CanRenderFieldsWithMultipleSteps()
    {
        FormEngineInstance myEngine = new FormEngineInstance();
        var myFields = await myEngine.GetFields(typeof(RenderFieldsWithStepper));
        AssertJson(RenderFieldsWithStepper.ExpectedResult, myFields, true);
    }
}