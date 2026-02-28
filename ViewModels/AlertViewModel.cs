// ViewModels/AlertViewModel.cs

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.ViewModels
{
    public partial class AlertViewModel : ObservableObject
    {
        private readonly AppDbContext _context;

        [ObservableProperty]
        private ObservableCollection<Product> lowStockProducts = new();

        public IAsyncRelayCommand LoadAlertsCommand { get; }

        public AlertViewModel(AppDbContext context)
        {
            _context = context;

            LoadAlertsCommand = new AsyncRelayCommand(LoadAlertsAsync);
        }

        private async Task LoadAlertsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Supplier)
                .Where(p => p.StockQuantity <= p.ReorderLevel)
                .ToListAsync();

            LowStockProducts = new ObservableCollection<Product>(products);
        }
    }
}