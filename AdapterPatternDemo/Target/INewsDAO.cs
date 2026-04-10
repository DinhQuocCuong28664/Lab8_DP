// ============================================================================
// File: Target/INewsDAO.cs
// Mô tả: TARGET INTERFACE - Giao diện chuẩn của hệ thống.
//         Đây là interface mà Client (Program.cs) sẽ sử dụng.
//         Cả NewsDAO (local) và các Adapter đều phải implement interface này,
//         đảm bảo tính đa hình (polymorphism) trong Adapter Pattern.
// ============================================================================

using AdapterPatternDemo.Models;

namespace AdapterPatternDemo.Target
{
    /// <summary>
    /// TARGET INTERFACE trong Adapter Pattern.
    /// Định nghĩa giao diện chuẩn mà hệ thống sử dụng để truy vấn tin tức.
    /// - getAllCategory(): Lấy danh sách tất cả danh mục tin tức.
    /// - getNewsByCategory(int categoryId): Lấy danh sách tin theo danh mục.
    /// </summary>
    public interface INewsDAO
    {
        /// <summary>
        /// Lấy toàn bộ danh mục tin tức từ nguồn dữ liệu.
        /// </summary>
        /// <returns>Danh sách NewsCategory chuẩn của hệ thống</returns>
        List<NewsCategory> getAllCategory();

        /// <summary>
        /// Lấy danh sách tin tức theo mã danh mục.
        /// </summary>
        /// <param name="categoryId">Mã danh mục cần lọc tin</param>
        /// <returns>Danh sách NewsLocal chuẩn của hệ thống</returns>
        List<NewsLocal> getNewsByCategory(int categoryId);
    }
}
