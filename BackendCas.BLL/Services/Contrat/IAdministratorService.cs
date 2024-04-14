using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendCas.DTO;

namespace BackendCas.BLL.Services.Contrat
{
    public interface IAdministratorService
    {

        Task<List<AdministratorDTO>> List();

        Task<AdministratorDTO> Create(AdministratorDTO model);

        Task<bool> Edit(AdministratorDTO model);

        Task<bool> Delete(int id);



    }
}
