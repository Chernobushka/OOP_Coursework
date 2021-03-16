namespace Canteen.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
        public string Name
        {
            get { return "Заказ №" + Id; }
        }
        public string DateWithoutTime
        {
            get 
            {
                return Date.Value.Date.ToShortDateString() ; 
            }
        }
        public int Price
        {
            get
            {
                int price = 0;
                foreach (var x in OrderDetails)
                {
                    price += x.Dish.Price ?? 0;
                }
                return price;
            }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual User User { get; set; }
    }
}
