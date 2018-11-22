using System.Collections.Specialized;
using System.Threading.Tasks;
using WebApplication1.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.DAL;
using WebApplication1.ViewModels;

namespace WebApplication1.Interfaces
{
    public interface ISearchManager
    {
        IEnumerable<T> SearchPrivateConsultants<T>(IEnumerable<T> privates, string letters) where T : PrivateConsultant;
        IEnumerable<T> SearchJuridicConsultants<T>(IEnumerable<T> juridics, string letters) where T : JuridicConsultant;
    }
}