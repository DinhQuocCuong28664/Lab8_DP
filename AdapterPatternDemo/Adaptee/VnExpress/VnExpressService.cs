// ============================================================================
// File: Adaptee/VnExpress/VnExpressService.cs
// Mô tả: ADAPTEE - Thư viện bên ngoài của VnExpress.
//         Cung cấp API lấy tin tức với kiểu dữ liệu riêng (VECat[], VENews[]).
//         Đặc biệt: Trả về MẢNG (array) thay vì List, khác biệt rõ rệt 
//         với INewsDAO trả về List<T>.
//         => Cần VnExpressAdapter để chuyển đổi.
// ============================================================================

namespace AdapterPatternDemo.Adaptee.VnExpress
{
    /// <summary>
    /// ADAPTEE - Service bên thứ 3 của VnExpress.
    /// Cung cấp dữ liệu tin tức với API và kiểu dữ liệu riêng biệt:
    /// - GetVECategories() → VECat[] (mảng, không phải List&lt;NewsCategory&gt;)
    /// - GetVENews(int catID) → VENews[] (mảng, không phải List&lt;NewsLocal&gt;)
    /// 
    /// Khác biệt chính so với hệ thống:
    /// 1. Trả về mảng (array) thay vì List.
    /// 2. Tên thuộc tính khác (headline thay vì newsTitle, catID thay vì categoryId).
    /// 3. VECat có thêm trường content mà NewsCategory không có.
    /// 
    /// Đây là thư viện bên ngoài mà chúng ta KHÔNG THỂ sửa đổi source code.
    /// </summary>
    public class VnExpressService
    {
        // Dữ liệu mẫu giả lập API của VnExpress
        private readonly VECat[] _categories;
        private readonly Dictionary<int, VENews[]> _newsByCategory;

        public VnExpressService()
        {
            // Khởi tạo danh mục tin của VnExpress
            _categories = new VECat[]
            {
                new VECat(201, "Thời sự", "Tin tức thời sự trong nước"),
                new VECat(202, "Khoa học", "Tin tức khoa học công nghệ"),
                new VECat(203, "Sức khỏe", "Tin tức y tế sức khỏe")
            };

            // Khởi tạo tin tức theo từng danh mục
            _newsByCategory = new Dictionary<int, VENews[]>
            {
                {
                    201, new VENews[]
                    {
                        new VENews(2001, "[VNE] Đại hội Đảng lần thứ XIV", "Công tác chuẩn bị Đại hội..."),
                        new VENews(2002, "[VNE] Luật Đất đai sửa đổi", "Nhiều điểm mới trong Luật Đất đai...")
                    }
                },
                {
                    202, new VENews[]
                    {
                        new VENews(2003, "[VNE] Vệ tinh NanoDragon", "Vệ tinh Made in Vietnam phóng thành công...")
                    }
                },
                {
                    203, new VENews[]
                    {
                        new VENews(2004, "[VNE] Phòng chống dịch bệnh", "Bộ Y tế khuyến cáo tiêm vaccine..."),
                        new VENews(2005, "[VNE] Dinh dưỡng mùa hè", "Chế độ ăn uống hợp lý trong mùa nóng...")
                    }
                }
            };
        }

        /// <summary>
        /// API của VnExpress - Lấy danh sách danh mục.
        /// Trả về VECat[] (mảng, KHÔNG tương thích với List&lt;NewsCategory&gt;).
        /// </summary>
        public VECat[] GetVECategories()
        {
            return _categories;
        }

        /// <summary>
        /// API của VnExpress - Lấy tin theo danh mục.
        /// Trả về VENews[] (mảng, KHÔNG tương thích với List&lt;NewsLocal&gt;).
        /// </summary>
        /// <param name="catID">Mã danh mục theo hệ thống VnExpress</param>
        public VENews[] GetVENews(int catID)
        {
            if (_newsByCategory.TryGetValue(catID, out var news))
                return news;
            return Array.Empty<VENews>();
        }
    }
}
