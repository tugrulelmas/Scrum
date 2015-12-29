using AbiokaScrum.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace AbiokaScrum.Api.Data.Mock
{
    public class LabelCollection : CollectionBase<Label>
    {
        public LabelCollection()
            : base() {
            list.Add(new Label { Id = 1, Name = "Pancar", Type = "success", IsDeleted = true });
            list.Add(new Label { Id = 2, Name = "Buğday", Type = "info", IsDeleted = true });
        }

        public override Label GetByKey(object key) {
            return list.FirstOrDefault(l => l.Id == Convert.ToInt32(key));
        }
    }
}