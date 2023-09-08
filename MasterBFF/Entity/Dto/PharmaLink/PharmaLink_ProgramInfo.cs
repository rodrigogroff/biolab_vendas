using System;

namespace MasterBiolabBFF.Entity.Dto.PharmaLink
{

    public class PharmaLink_ProgramInfo
    {
        public int IdPrograma { get; set; }
        public string CaminhoLogo { get; set; }
        public string CaminhoLogoCompleto { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Apagado { get; set; }
    }
}
