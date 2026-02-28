// ViewModels/SupplierViewModel.cs

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.ViewModels
{
    public partial class SupplierViewModel : ObservableObject
    {
        private readonly AppDbContext _context;

        [ObservableProperty]
        private ObservableCollection<Supplier> suppliers = new();

        [ObservableProperty]
        private Supplier? selectedSupplier;

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private string? contactName;

        [ObservableProperty]
        private string? phone;

        [ObservableProperty]
        private string? email;

        [ObservableProperty]
        private string? address;

        [ObservableProperty]
        private bool isEditMode;

        public IAsyncRelayCommand LoadDataCommand { get; }
        public IRelayCommand NewSupplierCommand { get; }
        public IAsyncRelayCommand SaveSupplierCommand { get; }
        public IAsyncRelayCommand DeleteSupplierCommand { get; }
        public IRelayCommand CancelEditCommand { get; }

        public SupplierViewModel(AppDbContext context)
        {
            _context = context;

            LoadDataCommand = new AsyncRelayCommand(LoadDataAsync);
            NewSupplierCommand = new RelayCommand(NewSupplier);
            SaveSupplierCommand = new AsyncRelayCommand(SaveSupplierAsync);
            DeleteSupplierCommand = new AsyncRelayCommand(DeleteSupplierAsync);
            CancelEditCommand = new RelayCommand(CancelEdit);
        }

        private async Task LoadDataAsync()
        {
            var data = await _context.Suppliers.ToListAsync();
            Suppliers = new ObservableCollection<Supplier>(data);
        }

        private void NewSupplier()
        {
            SelectedSupplier = null;
            Name = string.Empty;
            ContactName = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
            IsEditMode = false;
        }

        private async Task SaveSupplierAsync()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return;

            if (IsEditMode && SelectedSupplier != null)
            {
                // Update existing
                SelectedSupplier.Name = Name;
                SelectedSupplier.ContactName = ContactName;
                SelectedSupplier.Phone = Phone;
                SelectedSupplier.Email = Email;
                SelectedSupplier.Address = Address;
            }
            else
            {
                // Create new
                var supplier = new Supplier
                {
                    Name = Name,
                    ContactName = ContactName,
                    Phone = Phone,
                    Email = Email,
                    Address = Address
                };

                await _context.Suppliers.AddAsync(supplier);
            }

            await _context.SaveChangesAsync();
            await LoadDataAsync();
            CancelEdit();
        }

        private async Task DeleteSupplierAsync()
        {
            if (SelectedSupplier == null)
                return;

            _context.Suppliers.Remove(SelectedSupplier);
            await _context.SaveChangesAsync();
            await LoadDataAsync();
        }

        private void CancelEdit()
        {
            SelectedSupplier = null;
            Name = string.Empty;
            ContactName = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
            IsEditMode = false;
        }
    }
}