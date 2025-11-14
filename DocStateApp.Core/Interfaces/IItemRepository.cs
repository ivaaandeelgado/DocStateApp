using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStateApp.Core.Interfaces
{
    public interface IItemRepository
    {

        public void AddItem(Modelos.Item item);
        public void UpdateItem(Modelos.Item item);

        public void DeleteItem(int id);

        public void getAllItems();
        public void getItemsByState(Estado estado);

    }
}
