// ============================================================================
// File: Adapter/VnExpressAdapter.cs
// Mô tả: ADAPTER cho VnExpress Service.
//         Sử dụng Object Adapter Pattern: giữ tham chiếu (object reference)
//         tới VnExpressService và chuyển đổi dữ liệu từ VECat[]/VENews[]
//         sang NewsCategory/NewsLocal chuẩn của hệ thống.
//
// LUỒNG HOẠT ĐỘNG:
//   Client → INewsDAO.getAllCategory()
//          → VnExpressAdapter.getAllCategory()
//          → VnExpressService.GetVECategories() → VECat[]
//          → Adapter ánh xạ VECat → NewsCategory (và chuyển array → List)
//          → Client nhận List<NewsCategory> chuẩn
// ============================================================================

using AdapterPatternDemo.Models;
using AdapterPatternDemo.Target;
using AdapterPatternDemo.Adaptee.VnExpress;

namespace AdapterPatternDemo.Adapter
{
    /// <summary>
    /// ADAPTER - Chuyển đổi giao diện của VnExpressService sang INewsDAO.
    /// 
    /// Áp dụng Object Adapter Pattern:
    /// - Implement INewsDAO (Target Interface)
    /// - Giữ tham chiếu đến VnExpressService (Adaptee) qua composition
    /// - Trong mỗi phương thức, gọi API của Adaptee rồi ánh xạ kết quả
    ///   sang kiểu dữ liệu chuẩn của hệ thống
    /// 
    /// Điểm khác biệt so với ThanhNienAdapter:
    /// - VnExpress trả về mảng (array), cần chuyển sang List
    /// - Tên thuộc tính khác: headline (thay vì title), catID (thay vì id)
    /// 
    /// Mapping:
    ///   VECat.CatID     → NewsCategory.CategoryId
    ///   VECat.Title     → NewsCategory.CategoryName
    ///   VENews.Id       → NewsLocal.NewsId
    ///   VENews.Headline → NewsLocal.NewsTitle   ← Tên khác hoàn toàn!
    ///   VENews.Content  → NewsLocal.NewsContent
    /// </summary>
    public class VnExpressAdapter : INewsDAO
    {
        // Object reference tới Adaptee (Object Adapter Pattern)
        // Sử dụng composition thay vì inheritance
        private readonly VnExpressService _vnExpressService;

        /// <summary>
        /// Constructor: Nhận Adaptee thông qua Dependency Injection.
        /// Tuân thủ nguyên tắc Dependency Inversion (SOLID - chữ D).
        /// </summary>
        /// <param name="vnExpressService">Đối tượng VnExpressService (Adaptee)</param>
        public VnExpressAdapter(VnExpressService vnExpressService)
        {
            _vnExpressService = vnExpressService;
        }

        /// <summary>
        /// Chuyển đổi danh sách danh mục từ VnExpress sang chuẩn hệ thống.
        /// 
        /// Luồng:
        /// 1. Gọi _vnExpressService.GetVECategories() → VECat[] (mảng)
        /// 2. Ánh xạ mỗi VECat → NewsCategory:
        ///    - VECat.CatID  → NewsCategory.CategoryId
        ///    - VECat.Title  → NewsCategory.CategoryName
        ///    - VECat.Content bị bỏ qua (hệ thống không cần trường này)
        /// 3. Chuyển từ array sang List và trả về
        /// </summary>
        public List<NewsCategory> getAllCategory()
        {
            // Bước 1: Gọi API của Adaptee (VnExpress) - trả về mảng
            VECat[] veCategories = _vnExpressService.GetVECategories();

            // Bước 2: Ánh xạ từ VECat[] sang List<NewsCategory> chuẩn
            List<NewsCategory> result = new List<NewsCategory>();
            foreach (var veCat in veCategories)
            {
                result.Add(new NewsCategory(
                    veCat.CatID,    // VECat.CatID → NewsCategory.CategoryId
                    veCat.Title     // VECat.Title → NewsCategory.CategoryName
                    // VECat.Content bị bỏ qua - hệ thống không có trường tương ứng
                ));
            }

            // Bước 3: Trả về kiểu dữ liệu chuẩn cho Client
            return result;
        }

        /// <summary>
        /// Chuyển đổi danh sách tin tức từ VnExpress sang chuẩn hệ thống.
        /// 
        /// Luồng:
        /// 1. Gọi _vnExpressService.GetVENews(categoryId) → VENews[] (mảng)
        /// 2. Ánh xạ mỗi VENews → NewsLocal:
        ///    - VENews.Id       → NewsLocal.NewsId
        ///    - VENews.Headline → NewsLocal.NewsTitle  ← Chú ý: headline → newsTitle
        ///    - VENews.Content  → NewsLocal.NewsContent
        ///    - categoryId      → NewsLocal.CategoryId (bổ sung từ tham số)
        /// 3. Chuyển từ array sang List và trả về
        /// </summary>
        public List<NewsLocal> getNewsByCategory(int categoryId)
        {
            // Bước 1: Gọi API của Adaptee (VnExpress) - trả về mảng
            VENews[] veNewsList = _vnExpressService.GetVENews(categoryId);

            // Bước 2: Ánh xạ từ VENews[] sang List<NewsLocal> chuẩn
            List<NewsLocal> result = new List<NewsLocal>();
            foreach (var veNews in veNewsList)
            {
                result.Add(new NewsLocal(
                    veNews.Id,          // VENews.Id → NewsLocal.NewsId
                    veNews.Headline,    // VENews.Headline → NewsLocal.NewsTitle ★
                    veNews.Content,     // VENews.Content → NewsLocal.NewsContent
                    categoryId          // Bổ sung CategoryId từ tham số đầu vào
                ));
            }

            // Bước 3: Trả về kiểu dữ liệu chuẩn cho Client
            return result;
        }
    }
}
