using CalculateurAge.Views;
using Microsoft.Maui.Controls; 

namespace CalculateurAge
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Déclare la route : sans cette ligne, GoToAsync
            // lève une exception "route inconnue".
            Routing.RegisterRoute(nameof(ResultatPage),
                                    typeof(ResultatPage));
        }
    }
}

