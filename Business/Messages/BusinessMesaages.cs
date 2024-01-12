using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Messages;

public class BusinessMesaages
{
    public static string CategoryLimit = "Kategori sayısı max 10 olabilir.";

    public static string CategoryProductLimit = "Bir kategoride  max 20 ürün olabilir.";

    public static string CityCustomerLimit = "Bir şehirde max 10 kişi olabilir.";

    public static string? ExistsContactName = "Bu iletişim adı mevcut.";

    public static string UserNotFound = "Kullanıcı bulunamadı";
    public static string PasswordError = "Şifre hatalı";
    public static string SuccessfulLogin = "Sisteme giriş başarılı";
    public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";

}
