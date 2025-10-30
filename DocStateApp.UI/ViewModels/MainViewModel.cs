using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DocStateApp.UI.Ayudas;
using System.Diagnostics;


namespace DocStateApp.UI.ViewModels;


public class MainViewModel : INotifyPropertyChanged
{

    // Constructor
    public MainViewModel()
    {
        TextoCommand = new Ayudas.RelayCommand(OnExecuteTextoCommand);
    }




    // PROPIEDADES


    //Ruta del directorio seleccionado
    private string _rutaDirectorio;
    public string RutaDirectorio
    {
        get => _rutaDirectorio;
        set
        {
            if (_rutaDirectorio != value)
            {
                _rutaDirectorio = value;
                OnPropertyChanged();
            }
        }
    }

    private string _texto;
    public string Texto
    {
        get => _texto;
        set
        {
            if (_texto != value)
            {
                _texto = value;
                OnPropertyChanged();
            }
        }
    }



    // Comandos

    public ICommand TextoCommand { get;}






    //Metodos de los comandos
    private void OnExecuteTextoCommand(object? parameter)
    {
        Texto = $"El archivo seleccionado es: {RutaDirectorio}";
    }







    // Implementacion de INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


}
