using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Application.Commands
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken token);
    }
}
