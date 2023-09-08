using System;

namespace MasterBiolabBFF.Entity.Dto.PharmaLink
{

    public class PharmaLink_Atendimento
    {
        public int IdCanalAtendimento { get; set; }
        public int IdCanalAtendimentoTipo { get; set; }
        public string Descricao { get; set; }
        public bool? Apagado { get; set; }
    }

    public class PharmaLink_Tema
    {
        public int IdTema { get; set; }
        public string Descricao { get; set; }
        public bool Status { get; set; }
        public string PaletaCorA { get; set; }
        public string PaletaCorB { get; set; }
        public string PaletaCorC { get; set; }
        public string PaletaCorD { get; set; }
        public string PaletaCorFonteMenu { get; set; }
        public string PaletaCorFundo { get; set; }
        public string PaletaCorPrimaria { get; set; }
        public string PaletaCorSecundaria { get; set; }
        public string PaletaCorBotao { get; set; }
        public string PaletaCorIcone { get; set; }
        public bool Padrao { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Apagado { get; set; }
    }
}
