using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskmanagement.api.ms.DTOs;

namespace taskmanagement.api.ms.domain.Interface
{
    public interface ITaskService
    {
        Task<CreateTaskDto> AddTask(CreateTaskDto dto);
    }
}
