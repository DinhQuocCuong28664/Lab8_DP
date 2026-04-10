// ============================================================================
// File: Program.cs
// Mô tả: CLIENT - Chương trình chính minh họa Adapter Pattern.
//
// ADAPTER PATTERN - Tổng hợp tin tức từ nhiều nguồn:
// ─────────────────────────────────────────────────────────────────
//
//  ┌─────────┐      ┌───────────┐      ┌────────────────────┐
//  │ Client  │─────>│ INewsDAO  │<─────│     NewsDAO        │
//  │(Program)│      │ (Target)  │      │  (Local SQLite)    │
//  └─────────┘      └───────────┘      └────────────────────┘
//                         ▲
//                         │ implements
//              ┌──────────┴──────────┐
//              │                     │
//  ┌───────────────────┐  ┌───────────────────┐
//  │ ThanhNienAdapter  │  │ VnExpressAdapter   │
//  │  (Adapter)        │  │  (Adapter)         │
//  └────────┬──────────┘  └────────┬───────────┘
//           │ has-a                │ has-a
//  ┌────────▼──────────┐  ┌───────▼────────────┐
//  │ ThanhNienService  │  │ VnExpressService    │
//  │  (Adaptee)        │  │  (Adaptee)          │
//  │  TNCat, TNNews    │  │  VECat[], VENews[]  │
//  └───────────────────┘  └─────────────────────┘
//
// Tính đa hình: Client chỉ làm việc với INewsDAO, không cần biết
// nguồn dữ liệu thực sự là SQLite, Thanh Niên hay VnExpress.
// ============================================================================

using AdapterPatternDemo.Target;
using AdapterPatternDemo.Adapter;
using AdapterPatternDemo.Adaptee.ThanhNien;
using AdapterPatternDemo.Adaptee.VnExpress;

namespace AdapterPatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║     ADAPTER PATTERN - Tổng hợp tin tức từ nhiều nguồn      ║");
            Console.WriteLine("║     Môn: Design Pattern (523H0008) - Lab 8                  ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.WriteLine();

            // ================================================================
            // BƯỚC 1: Tạo danh sách INewsDAO chứa cả 3 nguồn tin
            // - NewsDAO: Truy vấn CSDL SQLite nội bộ (không cần Adapter)
            // - ThanhNienAdapter: Bọc ThanhNienService → INewsDAO
            // - VnExpressAdapter: Bọc VnExpressService → INewsDAO
            //
            // Tất cả đều implement INewsDAO → Tính đa hình (Polymorphism)
            // ================================================================
            List<INewsDAO> newsSources = new List<INewsDAO>
            {
                // Nguồn 1: CSDL local SQLite - implement trực tiếp INewsDAO
                new NewsDAO(),

                // Nguồn 2: Thanh Niên - sử dụng Object Adapter
                // ThanhNienAdapter giữ reference tới ThanhNienService (has-a)
                new ThanhNienAdapter(new ThanhNienService()),

                // Nguồn 3: VnExpress - sử dụng Object Adapter
                // VnExpressAdapter giữ reference tới VnExpressService (has-a)
                new VnExpressAdapter(new VnExpressService())
            };

            // Tên hiển thị cho mỗi nguồn tin
            string[] sourceNames = { "📦 Local Database (SQLite)", "📰 Thanh Niên (Adapter)", "📰 VnExpress (Adapter)" };

            // ================================================================
            // BƯỚC 2: Duyệt qua tất cả nguồn tin và in ra console
            // Client chỉ gọi getAllCategory() và getNewsByCategory() 
            // thông qua INewsDAO → KHÔNG cần biết implementation cụ thể.
            // Đây chính là sức mạnh của Adapter Pattern + Polymorphism.
            // ================================================================
            for (int i = 0; i < newsSources.Count; i++)
            {
                INewsDAO newsDAO = newsSources[i];

                Console.WriteLine("┌──────────────────────────────────────────────────────────┐");
                Console.WriteLine($"│  Nguồn {i + 1}: {sourceNames[i],-44}│");
                Console.WriteLine($"│  Kiểu thực tế: {newsDAO.GetType().Name,-40}│");
                Console.WriteLine("└──────────────────────────────────────────────────────────┘");

                // Gọi getAllCategory() - Client không biết dữ liệu đến từ đâu
                var categories = newsDAO.getAllCategory();
                Console.WriteLine($"\n  📂 Danh mục ({categories.Count} danh mục):");
                Console.WriteLine("  " + new string('─', 50));

                foreach (var category in categories)
                {
                    Console.WriteLine(category);

                    // Gọi getNewsByCategory() - Polymorphism hoạt động!
                    var newsList = newsDAO.getNewsByCategory(category.CategoryId);
                    Console.WriteLine($"     📄 Tin tức ({newsList.Count} bài):");

                    foreach (var news in newsList)
                    {
                        Console.WriteLine($"     {news}");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine(new string('═', 60));
                Console.WriteLine();
            }

            // ================================================================
            // BƯỚC 3: Minh họa tính đa hình rõ ràng hơn
            // ================================================================
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           CHỨNG MINH TÍNH ĐA HÌNH (POLYMORPHISM)           ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("  Client gọi cùng phương thức getAllCategory() trên INewsDAO,");
            Console.WriteLine("  nhưng mỗi implementation trả về dữ liệu từ nguồn khác nhau:");
            Console.WriteLine();

            foreach (var source in newsSources)
            {
                // Cùng gọi getAllCategory() nhưng kết quả đến từ nguồn khác nhau
                int count = source.getAllCategory().Count;
                Console.WriteLine($"  ✅ {source.GetType().Name,-25} → {count} danh mục");
            }

            Console.WriteLine();
            Console.WriteLine("  → Tất cả đều qua INewsDAO, không cần biết chi tiết bên trong!");
            Console.WriteLine("  → Open/Closed Principle: Thêm nguồn mới chỉ cần tạo Adapter mới.");
            Console.WriteLine();
        }
    }
}
