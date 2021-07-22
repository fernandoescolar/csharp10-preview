using Presentation.Statistics;
using Spectre.Console;

SlideShow.Start("slides/slides.yml");
AnsiConsole.Clear();

var chart = await PollChart.CreateAsync();
AnsiConsole.Render(chart);