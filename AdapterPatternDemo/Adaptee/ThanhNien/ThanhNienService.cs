// ============================================================================
// File: Adaptee/ThanhNien/ThanhNienService.cs
// Mô tả: ADAPTEE - Thư viện bên ngoài của Thanh Niên.
//         Cung cấp API lấy tin tức với kiểu dữ liệu riêng (TNCat, TNNews).
//         Giao diện KHÔNG tương thích với INewsDAO chuẩn của hệ thống.
//         => Cần ThanhNienAdapter để chuyển đổi.
// ============================================================================

namespace AdapterPatternDemo.Adaptee.ThanhNien
{
    /// <summary>
    /// ADAPTEE - Service bên thứ 3 của báo Thanh Niên.
    /// Cung cấp dữ liệu tin tức với API và kiểu dữ liệu riêng biệt:
    /// - GetCategories() → List&lt;TNCat&gt; (không phải List&lt;NewsCategory&gt;)
    /// - GetNewsByCat(int catId) → List&lt;TNNews&gt; (không phải List&lt;NewsLocal&gt;)
    /// 
    /// Đây là thư viện bên ngoài mà chúng ta KHÔNG THỂ sửa đổi source code.
    /// </summary>
    public class ThanhNienService
    {
        // Dữ liệu mẫu giả lập API của Thanh Niên
        private readonly List<TNCat> _categories;
        private readonly Dictionary<int, List<TNNews>> _newsByCategory;

        public ThanhNienService()
        {
            // Khởi tạo danh mục tin của Thanh Niên
            _categories = new List<TNCat>
            {
                new TNCat(101, "Chính trị - Xã hội"),
                new TNCat(102, "Kinh tế"),
                new TNCat(103, "Giải trí")
            };

            // Khởi tạo tin tức theo từng danh mục
            _newsByCategory = new Dictionary<int, List<TNNews>>
            {
                {
                    101, new List<TNNews>
                    {
                        new TNNews(1001, "[TN] Phiên họp Chính phủ tháng 4", "Chính phủ họp bàn các giải pháp kinh tế..."),
                        new TNNews(1002, "[TN] Chống biến đổi khí hậu", "Việt Nam cam kết giảm phát thải carbon...")
                    }
                },
                {
                    102, new List<TNNews>
                    {
                        new TNNews(1003, "[TN] Thị trường chứng khoán tăng", "VN-Index vượt mốc 1300 điểm..."),
                    }
                },
                {
                    103, new List<TNNews>
                    {
                        new TNNews(1004, "[TN] Phim Việt đoạt giải quốc tế", "Bộ phim mới của đạo diễn Trần Anh Hùng...")
                    }
                }
            };
        }

        /// <summary>
        /// API của Thanh Niên - Lấy danh sách danh mục.
        /// Trả về List&lt;TNCat&gt; (KHÔNG tương thích với List&lt;NewsCategory&gt;).
        /// </summary>
        public List<TNCat> GetCategories()
        {
            return _categories;
        }

        /// <summary>
        /// API của Thanh Niên - Lấy tin theo danh mục.
        /// Trả về List&lt;TNNews&gt; (KHÔNG tương thích với List&lt;NewsLocal&gt;).
        /// </summary>
        /// <param name="catId">Mã danh mục theo hệ thống Thanh Niên</param>
        public List<TNNews> GetNewsByCat(int catId)
        {
            if (_newsByCategory.TryGetValue(catId, out var news))
                return news;
            return new List<TNNews>();
        }
    }
}
