using System;
using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace patback.Models
{
    [FirestoreData]
    public class Patient
    {
        [FirestoreProperty]
        [Key]
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }


        [Required]
        [StringLength(4, ErrorMessage = "Poliklinik Kodu (POLICLINIC_CODE) Max 4 Karakter olmalıdır.")]
        [JsonProperty(PropertyName = "policlinicCode")]
        public string POLICLINIC_CODE { get; set; }


        [Required]
        [RegularExpression(@"^[0-9]{1,5}$", ErrorMessage = "Doktor Sicil Numarası (DOCTOR_REC_ID) Max 5 Karakter olmalıdır. ")]
        [JsonProperty(PropertyName = "doctorRecId")]
        public decimal DOCTOR_REC_ID { get; set; }


        [Required]
        [StringLength(512, ErrorMessage = "Doktor Adı Soyadı (DOCTOR_NAME_SURNAME) Max 512 Karakter olmalıdır.")]
        [JsonProperty(PropertyName = "doctorNameSurname")]
        public string DOCTOR_NAME_SURNAME { get; set; }


        [Required]
        [StringLength(255, ErrorMessage = "Hasta Adı (PATIENT_NAME) Max 255 Karakter olmalıdır.")]
        [JsonProperty(PropertyName = "patientName")]
        public string PATIENT_NAME { get; set; }


        [Required]
        [StringLength(255, ErrorMessage = "Hasta Soyadı (PATIENT_SURNAME) Max 255 Karakter olmalıdır.")]
        [JsonProperty(PropertyName = "patientSurName")]
        public string PATIENT_SURNAME { get; set; }


        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [JsonProperty(PropertyName = "patientBirthday")]
        public DateTime? PATIENT_BIRTHDAY { get; set; }


        [Required]
        [JsonProperty(PropertyName = "patientSex")]
        public decimal PATIENT_SEX { get; set; }


        [Required]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Hasta Kimlik Numarası (PATIENT_REC_ID) 11 Digit Olmalıdır.")]
        [JsonProperty(PropertyName = "patientRecId")]
        public decimal PATIENT_REC_ID { get; set; }

     
        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Telefon Numarası (PATIENT_PHONE) 10 Digit Olmalıdır.")]
        [JsonProperty(PropertyName = "patientPhone")]
        public decimal PATIENT_PHONE { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [JsonProperty(PropertyName = "visitDateTime")]
        public DateTime? VISIT_DATETIME { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [JsonProperty(PropertyName = "nextVisitDateTime")]
        public DateTime? NEXT_VISIT_DATETIME { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [JsonProperty(PropertyName = "createDataDateTime")]
        public DateTime? CREATE_DATETIME { get; set; }

        [JsonProperty(PropertyName = "doctorNote")]
        public string DOCTOR_NOTE { get; set; }

        [FirestoreProperty]
        public Timestamp createStamp { get; set; }

        [FirestoreProperty]
        public string logData { get; set; }


    }
}