using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CarRentalApplication.Models.ViewModels.Reservation
{
    #region Custom Validation Attributes
    public class IsStoreOpenAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime timeVar = (DateTime)value;

            if (timeVar.DayOfWeek == DayOfWeek.Sunday)
                return new ValidationResult("Date is not Available (Store is closed)");
            return ValidationResult.Success;
        }
    }

    public class IsTimeDurationValid : ValidationAttribute
    {
        private DateTime _pickupDate;
        private DateTime _returnDate;
        private string _pickupDateProperty;
        public IsTimeDurationValid(string otherProperty)
        {
            _pickupDateProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo pi = validationContext.ObjectType.GetProperty(_pickupDateProperty);
            _pickupDate = (DateTime)pi.GetValue(validationContext.ObjectInstance, null);
            _returnDate = (DateTime)value;
            if (_returnDate.CompareTo(_pickupDate) < 0)
                return new ValidationResult("Total Number of Rental days is less than 0");
            return ValidationResult.Success;
        }
    }
    #endregion
    public class ReservationLogisticsViewModel : IFormProcessing
    {
        #region private member variables

        private bool _isDirty = false;
        private string _userLocation = string.Empty;
        private string _pickupLocation = string.Empty;
        private string _returnLocation = string.Empty;
        private DateTime _pickupDate = DateTime.Now;
        private DateTime _returnDate = DateTime.Now;
        public static string SessionKey = "rlvm";
        private List<string> _storeLocations = new List<string>
        {
            "987 Johnson Str, Pawtucket RI",
            "1234 Bing Street, Boston US",
            "1111 Pawtucket Ave, Pawtucket RI",
            "2958 Gold Street, AttleBoro, MA"
        };

        #endregion
        public bool IsDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }
        }

        [Required(ErrorMessage = "Your Location is Required")]
        public string UserLocation
        {
            get { return _userLocation; }
            set
            {
                _userLocation = value;
                if (string.Compare(value, _userLocation) != 0)
                    _isDirty = true;
            }
        }

        [Required (ErrorMessage = "Please Select a Pickup Location")]
        public string PickupLocation
        {
            get { return _pickupLocation; }
            set
            {
                _pickupLocation = value;
                if (string.Compare(value, _pickupLocation) != 0)
                    _isDirty = true;
            }
        }

        [Required(ErrorMessage = "Please select a Return Location")]
        public string ReturnLocation
        {
            get { return _returnLocation; }
            set
            {
                _returnLocation = value;
                if (string.Compare(value, _returnLocation) != 0)
                    _isDirty = true;
            }
        }

        [IsStoreOpen]
        public DateTime PickupDate
        {
            get { return _pickupDate; }
            set
            {
                _pickupDate = value;
                if (DateTime.Compare(value, _pickupDate) != 0)
                    _isDirty = true;
            }
        }

        [IsStoreOpen]
        [IsTimeDurationValid("PickupDate")]
        public DateTime ReturnDate
        {
            get { return _returnDate; }
            set
            {
                _returnDate = value;
                if (DateTime.Compare(value, _pickupDate) != 0)
                    _isDirty = true;
            }
        }

        public List<string> StoreLocations
        {
            get { return _storeLocations; }
            set { _storeLocations = value; }
        }

        public FormSubmissionViewModel FormProcessing { get; set; }
    }
}
