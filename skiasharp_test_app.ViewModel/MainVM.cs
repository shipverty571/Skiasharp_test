using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using skiasharp_test_app.Model;
using skiasharp_test_app.Model.Factories;

namespace skiasharp_test_app.ViewModel;

public class MainVM : ObservableObject
{
    public MainVM()
    {
        AddResistorCommand = new RelayCommand(AddResistor);
    }
    
    public ICommand AddResistorCommand { get; }

    private void AddResistor()
    {
        TestData.Add(ShapesFactory.Resistor());
    }
}