using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string Added = "Aracınız eklendi";
        public static string Updated = "Güncelleme işlemi başarılı";
        public static string Deleted = "Silme işlemi başarılı";
        public static string BrandNameInvalid = "Model ismi geçersiz. Minimum 2 karakterli olmalıdır.";
        public static string CarNameInvalid = "Araç ismi geçersiz";
        public static string MaintenanceTime = "Sistem Bakımda";
        public static string CarsIsListed = "Araçlar listelendi";
        public static string CarIsNotAvailable = "Araba Teslim Edilmedi";
        public static string CarDailyPriceInvalid = "Günlük fiyat değeri 0 dan büyük olmalıdır. Ekleme işlemi başarısız.";
        public static string GetAll = "Tüm veriler listelendi";
        public static string RentalReturnDate = "Şu an istediğiniz araç kirada olduğu için işleminiz başarısız.";
    }
}
