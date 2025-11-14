using DocStateApp.Core.Interfaces;
using DocStateApp.Core.Modelos;
using DocStateApp.UI.Ayudas;
using DocStateApp.UI.Services;
using DocStateApp.Worker.Escaner;
using DocStateApp.Worker.Reader;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace DocStateApp.UI.ViewModels;


public class MainViewModel : INotifyPropertyChanged
{

    //-------CONSTRUCTOR-------
    public MainViewModel(IDialogService dialogService)
    {

        _dialogService = dialogService;
        DialogCommand = new RelayCommand(ExecuteDialogCommand);
        AnalizeCommand = new RelayCommand(OnExecuteAnalizeCommand);

    }




    //-------PROPIEDADES-------

    // Servicio de dialogo
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


    //Lista de documentos
    private List<Item> _documentos;
    public List<Item> Documentos
    {
        get => _documentos;
        set
        {
            if (_documentos != value)
            {
                _documentos = value;
                OnPropertyChanged();
            }
        }
    }

    // -------COMANDOS-------


    public ICommand DialogCommand { get; }
    public ICommand AnalizeCommand { get; }





    //-------METODOS DE LOS COMANDOS-------


    private void ExecuteDialogCommand(object? parameter)
    {
        var path = _dialogService.OpenFolderDialog(RutaDirectorio);
        if (!string.IsNullOrEmpty(path))
            RutaDirectorio = path;
    }

    public void OnExecuteAnalizeCommand(object? parameter)
    {

        var scanner = new ScanDocuments();
        var reader = new Reader();

        //Documentos = scanner.ScanDocumentsInDirectory(RutaDirectorio);
        Documentos = reader.ReadDocuments(RutaDirectorio);

    }   





    //-------IMPLEMENTACION DE INotifyPropertyChanged-------

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


}
