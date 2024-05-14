using Client.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public interface IShoppingCartViewModel
    {
        ObservableCollection<Product> Products { get; set; }
    }
}
