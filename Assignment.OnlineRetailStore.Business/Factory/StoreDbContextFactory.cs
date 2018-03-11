using Assignment.OnlineRetailStore.IoC;
using Assignment.OnlineRetailStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Business
{
    public class StoreDbContextFactory
    {
        public static IStoreDbContext Create()
        {
            return ObjectLocator.CreateInstance<IStoreDbContext>("Assignment.OnlineRetailStore.Repository.StoreDbContext, Assignment.OnlineRetailStore.Repository");
        }
    }
}
