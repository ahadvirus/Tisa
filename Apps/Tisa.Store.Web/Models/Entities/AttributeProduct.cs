namespace Tisa.Store.Web.Models.Entities
{
    public class AttributeProduct
    {
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public virtual Attribute Attribute { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
