// ============================================================================
// File: Adaptee/VnExpress/VENews.cs
// Mô tả: Model tin tức của thư viện VnExpress (bên thứ 3).
//         Kiểu dữ liệu KHÔNG tương thích với chuẩn NewsLocal của hệ thống.
//         Thuộc tính: id, headline, content (khác với newsId, newsTitle, 
//         newsContent, categoryId).
// ============================================================================

namespace AdapterPatternDemo.Adaptee.VnExpress
{
    /// <summary>
    /// Model tin tức của VnExpress - ADAPTEE MODEL.
    /// Có cấu trúc khác với NewsLocal chuẩn:
    /// - id (thay vì newsId)
    /// - headline (thay vì newsTitle) ← Tên khác hoàn toàn!
    /// - content (thay vì newsContent)
    /// - KHÔNG có categoryId riêng
    /// </summary>
    public class VENews
    {
        public int Id { get; set; }
        public string Headline { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public VENews() { }

        public VENews(int id, string headline, string content)
        {
            Id = id;
            Headline = headline;
            Content = content;
        }
    }
}
