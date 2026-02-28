// ViewModels/MainViewModel.cs

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Inventory_Management_System.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private object? currentView;

        public IRelayCommand ShowProductsCommand { get; }
        public IRelayCommand ShowSuppliersCommand { get; }
        public IRelayCommand ShowAlertsCommand { get; }

        private readonly ProductViewModel _productVM;
        private readonly SupplierViewModel _supplierVM;
        private readonly AlertViewModel _alertVM;

        public MainViewModel(
            ProductViewModel productVM,
            SupplierViewModel supplierVM,
            AlertViewModel alertVM)
        {
            _productVM = productVM;
            _supplierVM = supplierVM;
            _alertVM = alertVM;

            ShowProductsCommand = new RelayCommand(ShowProducts);
            ShowSuppliersCommand = new RelayCommand(ShowSuppliers);
            ShowAlertsCommand = new RelayCommand(ShowAlerts);

            // Default View
            CurrentView = _productVM;
        }

        private void ShowProducts()
        {
            CurrentView = _productVM;
        }

        private void ShowSuppliers()
        {
            CurrentView = _supplierVM;
        }

        private void ShowAlerts()
        {
            CurrentView = _alertVM;
        }
    }
}