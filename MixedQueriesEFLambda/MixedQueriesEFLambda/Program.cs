using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedQueriesEFLambda
{
    class Program
    {
        static void Main(string[] args)
        {
            NorthwindEntities _db = new NorthwindEntities();

            //_db deki product tablomdaki tüm verileri listeye ata
            List<Product> plist = _db.Products.ToList();

            //ürünleri isme göre listele ve listeme ata
            List<Product> plist2 = _db.Products.OrderBy(p => p.ProductName).ToList();

            //ürünleri isme göre tersten listele
            List<Product> plist3 = _db.Products.OrderByDescending(p => p.ProductName).ToList();

            //ürünleri tersten listele ve ilk 5 ürünü yakalayıp listeme ata
            List<Product> plist4 = _db.Products.OrderByDescending(p => p.ProductName).Take(5).ToList();

            //id si 5 olan ürün yakala
            Product prdc = _db.Products.FirstOrDefault(p => p.ProductID == 5);

            //id si 3 olan ürünün ADINI ver
            string ad = _db.Products.FirstOrDefault(p => p.ProductID == 3).ProductName;

            //Kategori ID si 5 olan ürünlerin listesi
            List<Product> plist5 = _db.Products.Where(p => p.CategoryID == 5).ToList();

            //Kategori ID si 3 ve Supplier ID si 2 olan ürünleri isme göre sırala ilk 5 imi al
            List<Product> plist6 = _db.Products.Where(p => p.CategoryID == 3 && p.SupplierID == 2).OrderBy(p => p.ProductName).Take(5).ToList();

            //productname a harfi geçen ürünlerin listesi
            List<Product> plist7 = _db.Products.Where(p => p.ProductName.Contains("a")).ToList();

            //product name a harfi ile BAŞLAYAN ürünlerin listesi
            List<Product> plist8 = _db.Products.Where(p => p.ProductName.StartsWith("a")).ToList();

            //productname a harfi ile BİTEN ürünlerin listesi
            List<Product> plist9 = _db.Products.Where(p => p.ProductName.EndsWith("a")).ToList();

            //Kategori Var Mi
            bool VarMi = _db.Categories.Any();

            //Kategori IDsi 5 olan kategori var mı_
            bool VarMi2 = _db.Categories.Any(c => c.CategoryID == 5);

            //İsminde 'ha' içeren ürün var mı? Küçük veya büyük harf farketmez
            bool VarMi3 = _db.Products.Any(p => p.ProductName.Contains("ha"));

            //Ürün dizisine atar
            Product[] pdizi = _db.Products.ToArray();

            //ürün sayısı
            int adet = _db.Products.Count();

            //product tablomdaki QuantityPerUnit sayısı(tabloda bu kolon eğer null ise toplama eklenmeyecektir)
            int adet2 = _db.Products.Count(p => p.QuantityPerUnit != null);



            //product tablomdaki ürün fiyatlarımın toplamı
            decimal? toplamfiyat = _db.Products.Sum(p => p.UnitPrice);

            //en pahalı ürünüm
            decimal? pahaliurun = _db.Products.Max(a => a.UnitPrice);

            //isme göre sıraladığında ilk 5 ürünü atla kalan ürünleri listele
            List<Product> plist10 = _db.Products.OrderBy(p => p.ProductName).Skip(5).ToList();

            //isme göre sıraladığında ilk 5 ürünü atla 10 ürünü listele
            List<Product> plist11 = _db.Products.OrderBy(p => p.ProductName).Skip(5).Take(10).ToList();


            //order tablosundaki shipcountry kolonuna distinct uygular(sql karşılığı "select distinct o.ShipCountry //from Orders o")
            List<string> countrylist = _db.Orders.Select(a => a.ShipCountry).Distinct().ToList();

            //product tablosundaki primary key alanındaki değere eşdeğer product getirir(5 numaralı ProductID //değerini getirir)
            Product prdct = _db.Products.Find(5);

            //iki tarih arasında ki siparisleri listeler
            List<Order> OrdList = _db.Orders.ToList();
            foreach (Order item in OrdList)
            {
                if (item.OrderDate > Convert.ToDateTime("1996-07-04") && item.OrderDate < Convert.ToDateTime("1997-01-01"))
                    Console.WriteLine(item);
            }

            //yukarıda listelenmis siparis icerisinde siparisi kim aldıysa o calisani listele
            Order orderCustomer = null; //kendi uygulamaniza gore bu satiri duzenlemeyi unutmayiniz.
            Employee sprsEmp = _db.Employees.Where(t => t.EmployeeID == orderCustomer.EmployeeID).FirstOrDefault<Employee>();
            Console.WriteLine(sprsEmp.FirstName + " " + sprsEmp.LastName);

            //yukarıda listelenmis siparis icerisinde o siparisi veren musterinin ismi(CompanyName)
            Customer customerGtr = _db.Customers.Where(t => t.CustomerID == orderCustomer.CustomerID).FirstOrDefault<Customer>();
            Console.WriteLine(customerGtr.CompanyName);

            //yukarıda listelenmis siparis icerisinde o siparis icerisinde yer alan urun ve o urunun fiyati
            Order_Detail customerOrderDtl = _db.Order_Details.Where(t => t.OrderID == orderCustomer.OrderID).FirstOrDefault<Order_Detail>();

            Product customerOrderDtlProd = _db.Products.Where(t => t.ProductID == customerOrderDtl.ProductID).FirstOrDefault<Product>();
            Console.WriteLine(customerOrderDtlProd.ProductName + " " + customerOrderDtlProd.UnitPrice);

            Console.ReadLine();
        }
    }
}
