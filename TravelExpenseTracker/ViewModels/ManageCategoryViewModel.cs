using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TravelExpenseTracker.Models;

namespace TravelExpenseTracker.ViewModels
{
    public partial class ManageCategoryViewModel : ObservableObject
    {
        public ObservableCollection<ManageCategoryModel> Categories { get; set; } = [];

        public ManageCategoryViewModel()
        {
            
        }
    }
}
