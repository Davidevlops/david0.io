// ViewModels/MainViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace InventoryManagement.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private object? _currentView;

    public IRelayCommand ShowProductsCommand { get; }
    public IRelayCommand ShowSuppliersCommand { get; }
    public IRelayCommand ShowAlertsCommand { get; }

    private readonly ProductViewModel _productVM;
    private readonly SupplierViewModel _supplierVM;
    private readonly AlertViewModel _alertVM;

    public MainViewModel(ProductViewModel productVM, SupplierViewModel supplierVM, AlertViewModel alertVM)
    {
        _productVM = productVM;
        _supplierVM = supplierVM;
        _alertVM = alertVM;

        ShowProductsCommand = new RelayCommand(() => CurrentView = _productVM);
        ShowSuppliersCommand = new RelayCommand(() => CurrentView = _supplierVM);
        ShowAlertsCommand = new RelayCommand(() => CurrentView = _alertVM);

        // Default view
        CurrentView = _productVM;
    }
}