// ViewModels/SupplierViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
public partial class SupplierViewModel : ObservableObject
{
    private readonly AppDbContext _context;

    [ObservableProperty]
    private ObservableCollection<Supplier> _suppliers = new();

    [ObservableProperty]
    private Supplier? _selectedSupplier;

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string? _contactName, _phone, _email, _address;

    [ObservableProperty]
    private bool _isEditMode;

    public IAsyncRelayCommand LoadDataCommand { get; }
    public IRelayCommand NewSupplierCommand { get; }
    public IAsyncRelayCommand SaveSupplierCommand { get; }
    public IAsyncRelayCommand DeleteSupplierCommand { get; }
    public IRelayCommand CancelEditCommand { get; }

    public SupplierViewModel(AppDbContext context) { /* similar implementation */ }
}