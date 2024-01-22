using System;

namespace DataLayerLib
{
    public partial class Supplier
    {
        public override string ToString()
        {
            return CompanyName;
        }


    }

    public partial class Product
    {
        public override string ToString()
        {
            return ProductName;
        }
    }
}
