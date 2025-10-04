using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Commands
{
    // Dans Application/Common/Commands
    public interface ICreateCommand<out TDto> where TDto : class
    {
        TDto Dto { get; }
    }
}
