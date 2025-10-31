using DocStateApp.Core.Interfaces;
using DocStateApp.UI.Ayudas;
using DocStateApp.UI.Services;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace DocStateApp.UI.ViewModels;


public class MainViewModel : INotifyPropertyChanged
{

    // Constructor
    public MainViewModel(IDialogService dialogService)
    {

        _dialogService = dialogService;
        DialogCommand = new RelayCommand(ExecuteDialogCommand);
        TextoCommand = new RelayCommand(OnExecuteTextoCommand);

    }




    // PROPIEDADES

    // Servicio de dialogos
    private readonly IDialogService _dialogService;
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
    public ICommand DialogCommand { get; }





    //Metodos de los comandos
    private void OnExecuteTextoCommand(object? parameter)
    {
        Texto = $"El archivo seleccionado es: {RutaDirectorio}";
    }

    private void ExecuteDialogCommand(object? parameter)
    {
        var path = _dialogService.OpenFolderDialog(RutaDirectorio);
        if (!string.IsNullOrEmpty(path))
            RutaDirectorio = path;
    }






    // Implementacion de INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


}
