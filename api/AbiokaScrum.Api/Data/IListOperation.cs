using AbiokaScrum.Api.Entities;
using System;
using System.Collections.Generic;

namespace AbiokaScrum.Api.Data
{
    public interface IListOperation : IOperation<List>
    {
        IEnumerable<List> GetByBoardId(Guid boardId);

        void Delete(Guid id);
    }
}