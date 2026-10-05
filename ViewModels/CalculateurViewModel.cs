using System.Collections.ObjectModel;

namespace CalculateurAge.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
    // Contient l'ÉTAT de l'écran et les ACTIONS possibles.
    // Champs privés : la vraie donnée.
    private string _nom = "";
    private DateTime _dateNaissance =
        DateTime.Today.AddYears(-20);
    private string _resultat = "";
    private bool _resultatVisible;
    private string _joursRestants = "";

    // Propriétés publiques : ce que le XAML voit.
    public string Nom
    {
        get => _nom;
        set
        {
            if (SetField(ref _nom, value))
                CalculerCommand.Rafraichir();
        }
    }

    public DateTime DateNaissance
    {
        get => _dateNaissance;
        set => SetField(ref _dateNaissance, value);
    }

    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    public bool ResultatVisible
    {
        get => _resultatVisible;
        set => SetField(ref _resultatVisible, value);
    }

    //proprietes pour afficher Mineur/Majeur
    private string _message = "";

    public string Message
    {
        get => _message;
        set => SetField(ref _message, value);
    }

    public string JoursRestants
    {
        get => _joursRestants;
        set => SetField(ref _joursRestants, value);
    }
    
    // ObservableCollection prévient l'écran à chaque ajout.
    public ObservableCollection<string> Historique { get; } = new();

    // Lié à Button.Command dans le XAML.
    public RelayCommand CalculerCommand { get; }
    public RelayCommand EffacerCommand { get; }
    public RelayCommand VoirDetailCommand { get; }
    public RelayCommand RetourCommand { get; }

    public CalculateurViewModel()
    {
        CalculerCommand = new RelayCommand(
            Calculer,
            () => !string.IsNullOrWhiteSpace(Nom));
        EffacerCommand = new RelayCommand(Effacer);
        VoirDetailCommand = new RelayCommand(
            async () => await Shell.Current.GoToAsync("ResultatPage"));
        RetourCommand = new RelayCommand(
            async () => await Shell.Current.GoToAsync(".."));
    }

    // La logique métier : aucun contrôle d'interface ici.
    private void Calculer()
    {
        int age = DateTime.Today.Year - DateNaissance.Year;
        int jours = JoursAvantAnniversaire(DateNaissance);
        if (DateNaissance.Date >
            DateTime.Today.AddYears(-age)) age--;

        Resultat = $"{Nom}, vous avez {age} ans";
        Message = age >= 18 ? "Majeur" : "Mineur";
        JoursRestants = jours == 0
            ? "Joyeux anniversaire !"
            : $"Prochain anniversaire dans {jours} jour(s)";
        Historique.Insert(0, $"{DateTime.Now:HH:mm} · {Resultat}");
        ResultatVisible = true;
    }

    private void Effacer()
    {
        Nom = "";
        DateNaissance = DateTime.Today.AddYears(-20);
        Resultat = "";
        Message = "";
        JoursRestants = ""; 
        ResultatVisible = false;
    }

    private static DateTime AnniversaireEnAnnee(DateTime naissance, int annee)
    {
        // Né un 29 février : en année non bissextile, on prend le 28.
        int jour = (naissance.Month == 2 && naissance.Day == 29
                    && !DateTime.IsLeapYear(annee)) ? 28 : naissance.Day;
        return new DateTime(annee, naissance.Month, jour);
    }

    private static int JoursAvantAnniversaire(DateTime naissance)
    {
        DateTime aujourdhui = DateTime.Today;
        DateTime prochain = AnniversaireEnAnnee(naissance, aujourdhui.Year);
        if (prochain < aujourdhui)
            prochain = AnniversaireEnAnnee(naissance, aujourdhui.Year + 1);
        return (prochain - aujourdhui).Days;
    }
}
