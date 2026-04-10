// ============================================================================
// File: Adaptee/ThanhNien/TNCat.cs
// Mô tả: Model danh mục của thư viện Thanh Niên (bên thứ 3).
//         Kiểu dữ liệu KHÔNG tương thích với chuẩn NewsCategory của hệ thống.
//         Thuộc tính: id, title (khác với categoryId, categoryName).
// ============================================================================

namespace AdapterPatternDemo.Adaptee.ThanhNien
{
    /// <summary>
    /// Model danh mục tin của Thanh Niên - ADAPTEE MODEL.
    /// Có cấu trúc khác với NewsCategory chuẩn:
    /// - id (thay vì categoryId)
    /// - title (thay vì categoryName)
    /// </summary>
    public class TNCat
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public TNCat() { }

        public TNCat(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
