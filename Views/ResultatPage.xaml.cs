using CalculateurAge.ViewModels;

namespace CalculateurAge.Views;

public partial class ResultatPage : ContentPage
{
    public ResultatPage(CalculateurViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}