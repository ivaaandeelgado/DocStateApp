using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace DocStateApp.UI.ViewModels;


public class MainViewModel : INotifyPropertyChanged
{



    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    public ICommand TextoCommand { get;}
    private void TextoCommand()
    {
        Console.log("TextoCommand executed");
    }




    public MainViewModel()
	{

	}
}
