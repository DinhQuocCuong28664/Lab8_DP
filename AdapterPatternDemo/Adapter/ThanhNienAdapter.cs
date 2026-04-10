// ============================================================================
// File: Adapter/ThanhNienAdapter.cs
// Mô tả: ADAPTER cho Thanh Niên Service.
//         Sử dụng Object Adapter Pattern: giữ tham chiếu (object reference)
//         tới ThanhNienService và chuyển đổi dữ liệu từ TNCat/TNNews
//         sang NewsCategory/NewsLocal chuẩn của hệ thống.
//
// LUỒNG HOẠT ĐỘNG:
//   Client → INewsDAO.getAllCategory() 
//          → ThanhNienAdapter.getAllCategory()
//          → ThanhNienService.GetCategories() → List<TNCat>
//          → Adapter ánh xạ TNCat → NewsCategory
//          → Client nhận List<NewsCategory> chuẩn
// ============================================================================

using AdapterPatternDemo.Models;
using AdapterPatternDemo.Target;
using AdapterPatternDemo.Adaptee.ThanhNien;

namespace AdapterPatternDemo.Adapter
{
    /// <summary>
    /// ADAPTER - Chuyển đổi giao diện của ThanhNienService sang INewsDAO.
    /// 
    /// Áp dụng Object Adapter Pattern:
    /// - Implement INewsDAO (Target Interface)
    /// - Giữ tham chiếu đến ThanhNienService (Adaptee) qua composition
    /// - Trong mỗi phương thức, gọi API của Adaptee rồi ánh xạ kết quả
    ///   sang kiểu dữ liệu chuẩn của hệ thống
    /// 
    /// Mapping:
    ///   TNCat.Id     → NewsCategory.CategoryId
    ///   TNCat.Title  → NewsCategory.CategoryName
    ///   TNNews.Id    → NewsLocal.NewsId
    ///   TNNews.Title → NewsLocal.NewsTitle
    ///   TNNews.Content → NewsLocal.NewsContent
    /// </summary>
    public class ThanhNienAdapter : INewsDAO
    {
        // Object reference tới Adaptee (Object Adapter Pattern)
        // Sử dụng composition thay vì inheritance
        private readonly ThanhNienService _thanhNienService;

        /// <summary>
        /// Constructor: Nhận Adaptee thông qua Dependency Injection.
        /// Tuân thủ nguyên tắc Dependency Inversion (SOLID - chữ D).
        /// </summary>
        /// <param name="thanhNienService">Đối tượng ThanhNienService (Adaptee)</param>
        public ThanhNienAdapter(ThanhNienService thanhNienService)
        {
            _thanhNienService = thanhNienService;
        }

        /// <summary>
        /// Chuyển đổi danh sách danh mục từ Thanh Niên sang chuẩn hệ thống.
        /// 
        /// Luồng: 
        /// 1. Gọi _thanhNienService.GetCategories() → List&lt;TNCat&gt;
        /// 2. Ánh xạ mỗi TNCat → NewsCategory:
        ///    - TNCat.Id     → NewsCategory.CategoryId
        ///    - TNCat.Title  → NewsCategory.CategoryName
        /// 3. Trả về List&lt;NewsCategory&gt; chuẩn
        /// </summary>
        public List<NewsCategory> getAllCategory()
        {
            // Bước 1: Gọi API của Adaptee (Thanh Niên)
            List<TNCat> tnCategories = _thanhNienService.GetCategories();

            // Bước 2: Ánh xạ từ TNCat sang NewsCategory chuẩn
            List<NewsCategory> result = new List<NewsCategory>();
            foreach (var tnCat in tnCategories)
            {
                result.Add(new NewsCategory(
                    tnCat.Id,       // TNCat.Id → NewsCategory.CategoryId
                    tnCat.Title     // TNCat.Title → NewsCategory.CategoryName
                ));
            }

            // Bước 3: Trả về kiểu dữ liệu chuẩn cho Client
            return result;
        }

        /// <summary>
        /// Chuyển đổi danh sách tin tức từ Thanh Niên sang chuẩn hệ thống.
        /// 
        /// Luồng:
        /// 1. Gọi _thanhNienService.GetNewsByCat(categoryId) → List&lt;TNNews&gt;
        /// 2. Ánh xạ mỗi TNNews → NewsLocal:
        ///    - TNNews.Id      → NewsLocal.NewsId
        ///    - TNNews.Title   → NewsLocal.NewsTitle
        ///    - TNNews.Content → NewsLocal.NewsContent
        ///    - categoryId     → NewsLocal.CategoryId (bổ sung từ tham số)
        /// 3. Trả về List&lt;NewsLocal&gt; chuẩn
        /// </summary>
        public List<NewsLocal> getNewsByCategory(int categoryId)
        {
            // Bước 1: Gọi API của Adaptee (Thanh Niên)
            List<TNNews> tnNewsList = _thanhNienService.GetNewsByCat(categoryId);

            // Bước 2: Ánh xạ từ TNNews sang NewsLocal chuẩn
            List<NewsLocal> result = new List<NewsLocal>();
            foreach (var tnNews in tnNewsList)
            {
                result.Add(new NewsLocal(
                    tnNews.Id,          // TNNews.Id → NewsLocal.NewsId
                    tnNews.Title,       // TNNews.Title → NewsLocal.NewsTitle
                    tnNews.Content,     // TNNews.Content → NewsLocal.NewsContent
                    categoryId          // Bổ sung CategoryId từ tham số đầu vào
                ));
            }

            // Bước 3: Trả về kiểu dữ liệu chuẩn cho Client
            return result;
        }
    }
}
