﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlazorShop.Libraries.Services.Product.Models
{
    /// <summary>
    /// The information needed in a product listing Razor component
    /// </summary>
    public interface IProductListing
    {
        // An instance of the product
        ProductModel? Product { get; set; }

        // The quantity wishing to be added to the cart
        int? Quantity { get; set; }
    }
}
