using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SeriesApp.Models;
using SeriesApp.Services;
using SeriesApp.Utils;

namespace SeriesApp.ViewModels;

public class UtilisateurViewModel : ObservableObject
{
    private Utilisateur utilisateurSearch;
    public Utilisateur UtilisateurSearch
    {
        get
        {
            return utilisateurSearch;
        }
        set
        {
            utilisateurSearch = value;
            OnPropertyChanged();
        }
    }

    private WSService service = WSService.GetInstance();

    public IRelayCommand BtnGetUtilisateurByEmail { get; }
    public IRelayCommand BtnModifyUtilisateurCommand { get; }
    public IRelayCommand BtnClearUtilisateurCommand { get; }
    public IRelayCommand BtnAddUtilisateurCommand { get; }

    public UtilisateurViewModel()
    {
        UtilisateurSearch = new Utilisateur();
        this.BtnGetUtilisateurByEmail = new RelayCommand(ActionGetUtilisateurByEmail);
        this.BtnModifyUtilisateurCommand = new RelayCommand(ActionSaveUtilisateur);
        this.BtnClearUtilisateurCommand = new RelayCommand(ActionClearUtilisateur);
        this.BtnAddUtilisateurCommand = new RelayCommand(ActionAddUtilisateur);
    }

    private async void ActionGetUtilisateurByEmail()
    {
        var utilisateur = await service.GetUtilisateurByEmail(UtilisateurSearch.Mail);

        if (utilisateur == null)
            return;

        UtilisateurSearch = utilisateur;
    }
    
    private async void ActionSaveUtilisateur()
    {
        var resp = await service.SaveUtilisateur(UtilisateurSearch);

        if(resp)
        {
            Dialog.DisplayDialogAsync("Information", $"Utilisateur {UtilisateurSearch.Prenom} modifié !", "Ok");
        } else
        {
            Dialog.DisplayDialogAsync("Information", $"Une erreur est survenue", "Ok");
        }
    }

    private async void ActionClearUtilisateur()
    {
        UtilisateurSearch = new Utilisateur();
    }

    private async void ActionAddUtilisateur()
    {
        var utilisateur = await service.AddUtilisateur(UtilisateurSearch);

        if (utilisateur != null)
        {
            Dialog.DisplayDialogAsync("Information", $"Utilisateur {utilisateur.Prenom} ajouté !", "Ok");
        }
        else
        {
            Dialog.DisplayDialogAsync("Information", $"Une erreur est survenue", "Ok");
        }
    }
}
