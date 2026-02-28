// ViewModels/AlertViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
public partial class AlertViewModel : ObservableObject
{
    private readonly AppDbContext _context;

    [ObservableProperty]
    private ObservableCollection<Product> _lowStockProducts = new();

    public IAsyncRelayCommand LoadAlertsCommand { get; }

    public AlertViewModel(AppDbContext context)
    {
        _context = context;
        LoadAlertsCommand = new AsyncRelayCommand(LoadAlertsAsync);
        LoadAlertsCommand.Execute(null);
    }

    private async Task LoadAlertsAsync()
    {
        await _context.Products.Include(p => p.Supplier)
            .Where(p => p.StockQuantity <= p.ReorderLevel)
            .LoadAsync();

        LowStockProducts = new ObservableCollection<Product>(_context.Products.Local);
    }
}