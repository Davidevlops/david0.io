// ViewModels/ProductViewModel.cs
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.ViewModels
{
    public partial class ProductViewModel : ObservableObject
    {
        private readonly AppDbContext _context;

        [ObservableProperty]
        private ObservableCollection<Product> products = new();

        [ObservableProperty]
        private ObservableCollection<Supplier> suppliers = new();

        [ObservableProperty]
        private Product? selectedProduct;

        [ObservableProperty]
        private string productName = string.Empty;

        [ObservableProperty]
        private string? description;

        [ObservableProperty]
        private decimal price;

        [ObservableProperty]
        private int stockQuantity;

        [ObservableProperty]
        private int reorderLevel;

        [ObservableProperty]
        private Supplier? selectedSupplier;

        [ObservableProperty]
        private bool isEditMode;

        public IAsyncRelayCommand LoadDataCommand { get; }
        public IRelayCommand NewProductCommand { get; }
        public IAsyncRelayCommand SaveProductCommand { get; }
        public IAsyncRelayCommand DeleteProductCommand { get; }
        public IRelayCommand CancelEditCommand { get; }

        public ProductViewModel(AppDbContext context)
        {
            _context = context;

            LoadDataCommand = new AsyncRelayCommand(LoadDataAsync);
            NewProductCommand = new RelayCommand(NewProduct);
            SaveProductCommand = new AsyncRelayCommand(SaveProductAsync, CanSaveProduct);
            DeleteProductCommand = new AsyncRelayCommand(DeleteProductAsync, CanDeleteProduct);
            CancelEditCommand = new RelayCommand(CancelEdit);
        }

        public async Task LoadDataAsync()
        {
            var suppliersData = await _context.Suppliers.ToListAsync();
            Suppliers = new ObservableCollection<Supplier>(suppliersData);

            var productsData = await _context.Products
                .Include(p => p.Supplier)
                .ToListAsync();
            Products = new ObservableCollection<Product>(productsData);
        }

        private void NewProduct()
        {
            SelectedProduct = new Product();
            IsEditMode = true;
            ClearInputFields();
        }

        private void CancelEdit()
        {
            SelectedProduct = null;
            IsEditMode = false;
            ClearInputFields();
        }

        private void ClearInputFields()
        {
            ProductName = string.Empty;
            Description = null;
            Price = 0;
            StockQuantity = 0;
            ReorderLevel = 0;
            SelectedSupplier = null;
        }

        private bool CanSaveProduct() =>
            !string.IsNullOrWhiteSpace(ProductName)
            && Price >= 0
            && StockQuantity >= 0
            && ReorderLevel >= 0
            && SelectedSupplier != null;

        private async Task SaveProductAsync()
        {
            if (SelectedProduct == null) return;

            SelectedProduct.Name = ProductName;
            SelectedProduct.Description = Description;
            SelectedProduct.Price = Price;
            SelectedProduct.StockQuantity = StockQuantity;
            SelectedProduct.ReorderLevel = ReorderLevel;
            SelectedProduct.SupplierId = SelectedSupplier!.Id;
            SelectedProduct.Supplier = SelectedSupplier;

            if (SelectedProduct.Id == 0)
                await _context.Products.AddAsync(SelectedProduct);

            await _context.SaveChangesAsync();
            await LoadDataAsync();

            IsEditMode = false;
            SelectedProduct = null;
            ClearInputFields();
        }

        private bool CanDeleteProduct() => SelectedProduct != null && !IsEditMode;

        private async Task DeleteProductAsync()
        {
            if (SelectedProduct == null) return;

            // Optional: move MessageBox to View layer
            _context.Products.Remove(SelectedProduct);
            await _context.SaveChangesAsync();
            await LoadDataAsync();
        }

        // Automatically populate fields when a product is selected
        partial void OnSelectedProductChanged(Product? value)
        {
            if (value != null)
            {
                IsEditMode = true;
                ProductName = value.Name;
                Description = value.Description;
                Price = value.Price;
                StockQuantity = value.StockQuantity;
                ReorderLevel = value.ReorderLevel;
                SelectedSupplier = value.Supplier;
            }
        }
    }
}