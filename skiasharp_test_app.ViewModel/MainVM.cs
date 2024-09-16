using CommunityToolkit.Mvvm.ComponentModel;

namespace skiasharp_test_app.ViewModel;

public class MainVM : ObservableObject
{
    public string DebugText { get; set; } = "Test";
}