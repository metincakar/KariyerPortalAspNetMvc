using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KagiderKariyerPortal.Dal.ConCreate.EntityFramework
{
    //public partial class CategoriesContext : DbContext
    //{
    //    public CategoriesContext()
    //        : base("DefaultConnection")
    //    {
    //    }

    //    //public virtual DbSet<MainCategory> MainCategories{ get; set; }
    //    //public virtual DbSet<SubCategory> SubCategories { get; set; }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<MainCategory>()
    //            .Property(e => e.MainCategoryName)
    //            .IsUnicode(false);
    //    }
    //}
    public class SubCategoryView
    {
        public string MCatName { get; set; }
        public string SCatName { get; set; }
        public string RCatName { get; set; }
    }
    public class SubCategory
    {
        [Key]
        public int SubCategoryId { get; set; }

        [Required]
        public string SubCategoryName { get; set; }

        [Required]
        public int MainCategoryId { get; set; }

        public MainCategory MainCategory { get; set; }

        public int? RelatedId { get; set; }

        public int OurCompanyId { get; set; }

       

    }

    public partial class MainCategory
    {
        public MainCategory()
        {
            this.SubCategories = new List<SubCategory>();
        }

        [Key]
        public int MainCategoryId { get; set; }

        [Required]
        [StringLength(45)]
        public string MainCategoryName { get; set; }

        public int? RelatedMainCategoryId { get; set; }

        public virtual List<SubCategory> SubCategories { get; set; }
    }
}
