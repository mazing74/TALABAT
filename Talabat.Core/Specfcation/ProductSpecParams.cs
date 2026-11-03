namespace Talabat.Core.Specfcation
{
    public class ProductSpecParams
    {
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        public int PageIndex { get; set; } = 1;
        private int pageSize = 5;

        public int PageSize
        {   
            get { return pageSize; }
            set { pageSize = value >= 10 ? 10 : value; }
        }

    }
}
