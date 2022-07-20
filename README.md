# ECommerce.Project.KO

Merhaba,

İstenilenler --> Katmanlı Mimari,RepositoryDesignPattern, EntityFramework Core, Microsoft SQL Server
Gerçekleştirilenler --> Katmanlı Mimari, RepositoryDesignPattern,EntityFramework Core, Microsoft SQL Server , Redis (Basket için kullanıldı diğer MSSql)

Teknoloji dışında sistemde gerçekleşmesi istenen kısımlar şunlardı :

1-)
Proje çalıştırıldığı anda tablo yapıları veritabanına yansıtılacak --> Yapıldı. (Ben kendi localimde Sql server için 6379 portunda 
ve hostunda container ayağa kaldırdım. Redis içinde 6379 port ve hostu için container ayağa kaldırdım(Redis sadece basketi kaydetmek için kullandım).
Containerlar ayağa kaldırıldıktan sonra program.cs deki fake datalar proje çalıştırıldığında eğer veritabanı oluşturulmamış ise oluşturuyor,
var ancak data yok ise dolduruyor.Data da var ise doldurmuyor)

İstenilen üç farklı role için kullanıcı dummy datası da koydum.
(mail : ahmet@gmail.com, şifre : "Ahmet3434*","SysAdmin")
(mail : mehmet@gmail.com, şifre : "Mehmet3434*","Admin")
(mail : hasan@gmail.com, şifre : "Hasan3434*","Customer")

Category ve ürünler içinde dummy datalar ekledim. Proje ayağa kalkınca rahatlıkla test edilebilir.

2-)
Identity user kullanılarak kullanıcı adı ve şifre ile sisteme giriş yapılmalı ve yanlış bilgi
girişlerinde hata mesajları ekrana yazdırılmalıdır. Giriş başarılı ise ürünlerin listelendiği bir
sayfaya yönlendirilmeli ve kullanıcı rolünün ne olduğu bu sayfada belirtilmelidir.  --> Buradaki istenilenler de gerçekleştirildi.

3-)
SysAdmin: Tüm işlemleri yapmaya yetkisi olan kullanıcı rolüdür.
Admin: Yalnızca markaya ait tüm işlemleri yapmaya yetkisi olan kullanıcı rolüdür.
Customer: Satın alım, yorum yapma gibi işlemlere yetkisi olan kullanıcı rolüdür.

AdminController'a sadece SysAdmin ve Admin role lerine sahip kuulanıcılar erişebiliyor. Bu AdminControllerının içindeki
Categoryleri sadece SysAdmin güncelleyebiliyor.


Comment ve Basket i sadece login olan kişiler kullanabiliyor. Eğer login değil ise Login ekranına yönlendiriliyor.

4-) Markaların kendi ürünlerini renk, boyut gibi çeşitli özellikleri belirterek ekleyebileceği, aynı
ürünün farklı özelliklerine göre indirimler uygulayabileceği, müşterilerin bu ürünleri satın
alabileceği, ürünler hakkında yorum yapabileceği bir e-ticaret web projesinin Entity ve Asp.net
Core Identity User altyapısı oluşturulmalıdır --> Satın alma sürecinin sadece Basket kısmını başlayıp ekleme ve listeleme kısmını yetiştirebildim.
Ürün ekleme,silme,güncelleme ve listeleme yapılıyor. Eğer kullanıcı login ise ürün detay sayfasında ürün hakkında yorum yapabiliyor. Başka kullanıcılar yorumları 
okuyabiliyor.


Proje süresince harici olarak kendim Docker Engine üzerinde veritabanını ve redis i yönettim. AutoMapper kullanarak UI ve Business servis arasındaki
Entity - Dto geçişini sağlayarak Entitylerimi sadece backend kısımda kalmasını sağlayıp UI da Dto lar ve Modeller üzerinden gittim.

Eksiklerim--
 
 Order tarafında da Basket ile başladım basketin ekleme ve listeleme kısımlarını kodladım. Order ve OrderItem entitylerini da kodlamaya başlarken oluşturdum.

 






