using System.ComponentModel.DataAnnotations;

namespace MS2Project.Domain.Core.Utilities.PagesSettings
{
    public class Pagable
    {
        /// <summary>
        /// صفحه
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// تعداد در هر صفحه
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// جستجو
        /// </summary>
        [StringLength(100, MinimumLength = 2, ErrorMessage = "مقدار جستجو باید بین 2 تا 100 کارکاتر باشد")]
        public string Search { get; set; } = "";
    }
}
