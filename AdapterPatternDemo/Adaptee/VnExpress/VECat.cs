// ============================================================================
// File: Adaptee/VnExpress/VECat.cs
// Mô tả: Model danh mục của thư viện VnExpress (bên thứ 3).
//         Kiểu dữ liệu KHÔNG tương thích với chuẩn NewsCategory của hệ thống.
//         Thuộc tính: catID, title, content (khác với categoryId, categoryName).
// ============================================================================

namespace AdapterPatternDemo.Adaptee.VnExpress
{
    /// <summary>
    /// Model danh mục tin của VnExpress - ADAPTEE MODEL.
    /// Có cấu trúc khác với NewsCategory chuẩn:
    /// - catID (thay vì categoryId)
    /// - title (thay vì categoryName)
    /// - content (VnExpress có thêm mô tả cho danh mục)
    /// </summary>
    public class VECat
    {
        public int CatID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public VECat() { }

        public VECat(int catID, string title, string content)
        {
            CatID = catID;
            Title = title;
            Content = content;
        }
    }
}
