using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loja.Models.ViewModels
{
    public class FornecedorViewModel
    {
        public IEnumerable<Fornecedor> Fornecedors { get; set; }
        public PagedList.IPagedList<Fornecedor> Fornece{get; set;}
    }
}