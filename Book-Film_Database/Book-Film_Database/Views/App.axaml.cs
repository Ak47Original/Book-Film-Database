using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Book_Film_Database.Data;
using System;
using System.IO;

namespace Book_Film_Database;

public partial class App : Application
{
    public static AppData AppData { get; private set; } = new AppData();
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        
        //var appData = new AppData();  
        AppData.ReadCSV();
        
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}