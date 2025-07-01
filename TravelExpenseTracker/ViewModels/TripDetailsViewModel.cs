using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TravelExpenseTracker.Models;

namespace TravelExpenseTracker.ViewModels
{
    public partial class TripDetailsViewModel : ObservableObject
    {
        public TripDetailModel TripInfo { get; set; } = new TripDetailModel(
            "beach.png",
            "Екскурзия до плажа",
            "Анталия, Турция",
            "Плаж",
            "Планирано",
            DateTime.Now.AddDays(7),
            DateTime.Now.AddDays(14)
            );

        public ObservableCollection<ExpenseModel> Expenses { get; set; } = [];

        [ObservableProperty]
        private decimal _totalExpenses;

        [RelayCommand]
        private void AddExpenseTemp()
        {
            Expenses.Add(new ExpenseModel(1, "Самолетен Билет", "Билети", 1500, DateTime.Today));
            Expenses.Add(new ExpenseModel(2, "Дрехи, обувки и козметика", "Пазаруване", 850, DateTime.Today.AddDays(1)));
            Expenses.Add(new ExpenseModel(3, "Храна и напитки", "Храна", 300, DateTime.Today.AddDays(2)));

            TotalExpenses = Expenses.Sum(e => e.Amount);
        }
    }
}
