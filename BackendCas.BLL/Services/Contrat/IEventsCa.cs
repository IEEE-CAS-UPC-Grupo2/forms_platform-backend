using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendCas.DTO;

namespace BackendCas.BLL.Services.Contrat
{
    public interface IEventsCa
    {
        Task<List<EventsCaDTO>> List();

        Task<EventsCaDTO> Create(EventsCaDTO model);

        Task<bool> Edit(EventsCaDTO model);

        Task<bool> Delete(int id);

        Task<EventsCaDTO> GetById(int id);

    }
}
