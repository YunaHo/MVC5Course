namespace MVC5Course.Models
{
    using MVC5Course.Models.InputValidations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(ClientMetaData))]
    public partial class Client
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.DateOfBirth.Value.Year > 1980 && this.City == "Taipei")
            if (this.Longitude.HasValue != this.Latitude.HasValue)
            {
                yield return new ValidationResult("條件錯誤", new string[] { "DateOfBirth", "City" });
                yield return new ValidationResult("經緯度欄位必須一起設定", new string[] { "Longitude", "Latitude" });
            }
        }
    }
    
    public partial class ClientMetaData
    {
        [Required]
        public int ClientId { get; set; }


        [Required]
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string LastName { get; set; }

        [Required]
        [StringLength(1, ErrorMessage="欄位長度不得大於 1 個字元")]
        public string Gender { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<double> CreditRating { get; set; }
        
        [StringLength(7, ErrorMessage="欄位長度不得大於 7 個字元")]
        public string XCode { get; set; }
        public Nullable<int> OccupationId { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string TelephoneNumber { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string Street1 { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string Street2 { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string City { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string ZipCode { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<double> Latitude { get; set; }
        public string Notes { get; set; }
        
        [IDCard]
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        public string IdNumber { get; set; }
        [Required]
        public bool IsDel { get; set; }
    
        public virtual Occupation Occupation { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
