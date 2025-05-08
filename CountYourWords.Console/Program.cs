// See https://aka.ms/new-console-template for more information

using CountYourWords.Application.Interfaces;
using CountYourWords.Application.Services;
using CountYourWords.Console.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// Build Services
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));
builder.Services.AddSingleton<IDocumentProvider, DocumentProvider>();
builder.Services.AddSingleton<IScrambleService, ScrambleService>();
builder.Services.AddSingleton<IDocumentProcessor, ScrambledDocumentProcessorDecorator>(provider =>
{
    var documentProcessor = new DocumentProcessor();
    var scrambleService = provider.GetRequiredService<IScrambleService>();
    return new ScrambledDocumentProcessorDecorator(documentProcessor, scrambleService);
});
builder.Services.AddSingleton<CountYourWordsWorkerService>();

using IHost host = builder.Build();

// Run the application
var countYourWordsService = host.Services.GetRequiredService<CountYourWordsWorkerService>();
await countYourWordsService.ProcessDocumentAsync("input.txt");