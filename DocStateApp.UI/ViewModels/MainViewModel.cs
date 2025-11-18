using DocStateApp.Core.Interfaces;
using DocStateApp.Core.Modelos;
using DocStateApp.UI.Ayudas;
using DocStateApp.UI.Services;
using DocStateApp.Worker.Escaner;
using DocStateApp.Worker.Reader;
using System;
using System.Collections.ObjectModel;
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

    //Lista de docsumentos filtrados
    public ObservableCollection<Item> ToDo { get;} = new();
    public ObservableCollection<Item> InProgress { get;} = new();
    public ObservableCollection<Item> Done { get;} = new();
    public ObservableCollection<Item> Historial { get;} = new();



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

    private void OnExecuteAnalizeCommand(object? parameter)
    {

        var scanner = new ScanDocuments();
        var reader = new Reader();

        //Documentos = scanner.ScanDocumentsInDirectory(RutaDirectorio);
        Documentos = reader.ReadDocuments(RutaDirectorio);
        FiltrarDocumentosPorEstado();

    }


    //-------METODOS AUXILIARES-------

    private void FiltrarDocumentosPorEstado()
    {
        ToDo.Clear();
        InProgress.Clear();
        Done.Clear();
        Historial.Clear();
        foreach (var doc in Documentos)
        {
            switch (doc.estado)
            {
                case Estado.ToDo:
                    ToDo.Add(doc);
                    break;
                case Estado.Doing:
                    InProgress.Add(doc);
                    break;
                case Estado.Done:
                    Done.Add(doc);
                    break;
                case Estado.Historial:
                    Historial.Add(doc);
                    break;
            }
        }
    }



    //-------IMPLEMENTACION DE INotifyPropertyChanged-------

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


}
