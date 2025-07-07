using CommunityToolkit.Mvvm.ComponentModel;
using TravelExpenseTracker.Shared;
using TravelExpenseTracker.Shared.DTOs;

namespace TravelExpenseTracker.Models
{
    public partial class SaveTripModel : ObservableObject
    {
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private int _categoryId;
        [ObservableProperty]
        private string _title;
        [ObservableProperty]
        private string _location;
        [ObservableProperty]
        private string _status = nameof(TripStatus.Планирано);
        [ObservableProperty]
        private DateTime _startDate = DateTime.UtcNow;
        [ObservableProperty]
        private DateTime _endDate = DateTime.UtcNow.AddDays(1);

        public (bool IsValid, string? Error) Validate()
        {
            string? error = null;
            if (CategoryId == 0)
                error = "Моля, изберете категория.";
            else if (string.IsNullOrWhiteSpace(Title))
                error = "Моля, въведете заглавие на пътуването.";
            else if (string.IsNullOrWhiteSpace(Location))
                error = "Моля, въведете местоположение на пътуването.";
            else if (StartDate >= EndDate)
                error = "Началната дата трябва да бъде преди крайната дата.";
            else if (StartDate < DateTime.UtcNow)
                error = "Началната дата не може да бъде в миналото.";
            else if (EndDate < DateTime.UtcNow)
                error = "Крайната дата не може да бъде в миналото.";
            else if (EndDate < StartDate)
                error = "Крайната дата трябва да бъде след началната дата.";
            else if (StartDate == default || EndDate == default)
                error = "Моля, изберете валидни дати.";

            return (error == null, error);
        }

        public SaveTripDto ToDto()
            => new SaveTripDto
            {
                Id = Id,
                CategoryId = CategoryId,
                Title = Title,
                Location = Location,
                Status = Status,
                StartDate = StartDate,
                EndDate = EndDate
            };

        public static SaveTripModel FromDto(TripListDto dto)
        => new()
        {
            Id = dto.Id,
            CategoryId = dto.CategoryId,
            Title = dto.Title,
            Location = dto.Location,
            Status = dto.Status,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate
        };
    }
}
