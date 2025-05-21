using System;
using System.Collections.Generic;
using OdontoPrevAPI.Dtos;

namespace OdontoPrevAPI.Tools
{
    public static class DadosDeTestes
    {
        public static List<PlanoDtos> ListaPlanos = new()
        {
            new PlanoDtos { DsCodigoPlano = "ODP001", NmPlano = "Básico" },
            new PlanoDtos { DsCodigoPlano = "ODP002", NmPlano = "Intermediário" },
            new PlanoDtos { DsCodigoPlano = "ODP003", NmPlano = "Premium" },
            new PlanoDtos { DsCodigoPlano = "ODP004", NmPlano = "Empresarial" },
            new PlanoDtos { DsCodigoPlano = "ODP005", NmPlano = "Familiar" },
            new PlanoDtos { DsCodigoPlano = "ODP006", NmPlano = "Executivo" },
            new PlanoDtos { DsCodigoPlano = "ODP007", NmPlano = "Estudante" },
            new PlanoDtos { DsCodigoPlano = "ODP008", NmPlano = "Infantil" },
            new PlanoDtos { DsCodigoPlano = "ODP009", NmPlano = "Senior" },
            new PlanoDtos { DsCodigoPlano = "ODP010", NmPlano = "Master" }
        };

        public static List<DentistaDtos> ListaDentistas = new()
        {
            new DentistaDtos { NmDentista = "Dr. Otto Canino", DsCro = "CRO312544", DsEmail = "otto.canino@gmail.com", NrTelefone = "31932158752", DsDocIdentificacao = "12579320000127" },
            new DentistaDtos { NmDentista = "Dr. Ben Dente", DsCro = "CR245565", DsEmail = "ben.dente@gmail.com", NrTelefone = "62901985212", DsDocIdentificacao = "16874896000178" },
            new DentistaDtos { NmDentista = "Dr. Álvaro Canal", DsCro = "CR52865", DsEmail = "alvaro.canal@gmail.com", NrTelefone = "11999855776", DsDocIdentificacao = "59225989000184" },
            new DentistaDtos { NmDentista = "Dra. Marina Molar", DsCro = "CR986422", DsEmail = "marina.molar@gmail.com", NrTelefone = "11933255774", DsDocIdentificacao = "93908189000104" },
            new DentistaDtos { NmDentista = "Dr. Ali Vramento", DsCro = "CR098964", DsEmail = "ali.vramento@gmail.com", NrTelefone = "45988552211", DsDocIdentificacao = "75234663000170" },
            new DentistaDtos { NmDentista = "Dra. Aparecida do Sorriso", DsCro = "CRO99001", DsEmail = "aparecida.sorriso@gmail.com", NrTelefone = "84966665786", DsDocIdentificacao = "42526327000141" },
            new DentistaDtos { NmDentista = "Dra. Isa Carie", DsCro = "CR856934", DsEmail = "isa.carie@gmail.com", NrTelefone = "11955863320", DsDocIdentificacao = "07316233000147" },
            new DentistaDtos { NmDentista = "Dra. Clara Mente", DsCro = "CR125863", DsEmail = "clara.mente@gmail.com", NrTelefone = "33945633251", DsDocIdentificacao = "36816900000159" },
            new DentistaDtos { NmDentista = "Dr. Cláudio Gengiva", DsCro = "CRO88990", DsEmail = "claudio.gengiva@gmail.com", NrTelefone = "27337558861", DsDocIdentificacao = "46805824000130" },
            new DentistaDtos { NmDentista = "Dra. Sonia Brilho", DsCro = "CR203587", DsEmail = "sonia.brilho@gmail.com", NrTelefone = "63977523258", DsDocIdentificacao = "50241419000103" }
        };

        public static List<PacienteDtos> ListaPacientes = new()
        {
            new PacienteDtos 
            { 
                NmPaciente = "Beto Mal Hálito", 
                DtNascimento = DateTime.Parse("1990-02-06"), 
                NrCpf = "09317440088", 
                DsSexo = "M", 
                NrTelefone = "27965563215", 
                DsEmail = "beto.halito@gmail.com", 
                IdPlano = 1,
                Plano = new Models.Plano { DsCodigoPlano = "ODP001", NmPlano = "Básico" }
            },
            new PacienteDtos 
            { 
                NmPaciente = "Branca Dente", 
                DtNascimento = DateTime.Parse("1987-10-31"), 
                NrCpf = "00780976061", 
                DsSexo = "F", 
                NrTelefone = "11975853524", 
                DsEmail = "branca.dente@gmail.com", 
                IdPlano = 2,
                Plano = new Models.Plano { DsCodigoPlano = "ODP002", NmPlano = "Intermediário" }
            },
            new PacienteDtos 
            { 
                NmPaciente = "Geni giva", 
                DtNascimento = DateTime.Parse("1995-01-18"), 
                NrCpf = "39600754055", 
                DsSexo = "F", 
                NrTelefone = "31963254785", 
                DsEmail = "geni.giva@gmail.com", 
                IdPlano = 3,
                Plano = new Models.Plano { DsCodigoPlano = "ODP003", NmPlano = "Premium" }
            },
            new PacienteDtos 
            { 
                NmPaciente = "João Tártaro", 
                DtNascimento = DateTime.Parse("1992-02-06"), 
                NrCpf = "12724268075", 
                DsSexo = "M", 
                NrTelefone = "35932100586", 
                DsEmail = "joao.tartaro@gmail.com", 
                IdPlano = 4,
                Plano = new Models.Plano { DsCodigoPlano = "ODP004", NmPlano = "Empresarial" }
            },
            new PacienteDtos 
            { 
                NmPaciente = "Carie Alves", 
                DtNascimento = DateTime.Parse("1983-11-25"), 
                NrCpf = "44797031018", 
                DsSexo = "N", 
                NrTelefone = "45932014563", 
                DsEmail = "carie.alves@gmail.com", 
                IdPlano = 5,
                Plano = new Models.Plano { DsCodigoPlano = "ODP005", NmPlano = "Familiar" }
            },
            new PacienteDtos 
            { 
                NmPaciente = "Sara Dente", 
                DtNascimento = DateTime.Parse("1991-08-20"), 
                NrCpf = "23953879081", 
                DsSexo = "N", 
                NrTelefone = "54946852335", 
                DsEmail = "sara.dente@gmail.com", 
                IdPlano = 6,
                Plano = new Models.Plano { DsCodigoPlano = "ODP006", NmPlano = "Executivo" }
            },
            new PacienteDtos 
            { 
                NmPaciente = "Ligia Dor", 
                DtNascimento = DateTime.Parse("1988-06-30"), 
                NrCpf = "35423149002", 
                DsSexo = "F", 
                NrTelefone = "11985652545", 
                DsEmail = "ligia.dor@gmail.com", 
                IdPlano = 7,
                Plano = new Models.Plano { DsCodigoPlano = "ODP007", NmPlano = "Estudante" }
            },
            new PacienteDtos 
            { 
                NmPaciente = "Ryan Quebrado", 
                DtNascimento = DateTime.Parse("1990-09-01"), 
                NrCpf = "67641380018", 
                DsSexo = "N", 
                NrTelefone = "25975254562", 
                DsEmail = "ryan.quebrado@gmail.com", 
                IdPlano = 8,
                Plano = new Models.Plano { DsCodigoPlano = "ODP008", NmPlano = "Infantil" }
            },
            new PacienteDtos 
            { 
                NmPaciente = "Ronaldo Banguela", 
                DtNascimento = DateTime.Parse("1994-03-22"), 
                NrCpf = "93430420008", 
                DsSexo = "M", 
                NrTelefone = "11998547565", 
                DsEmail = "ronaldo.banguela@gmail.com", 
                IdPlano = 5,
                Plano = new Models.Plano { DsCodigoPlano = "ODP005", NmPlano = "Familiar" }
            },
            new PacienteDtos 
            { 
                NmPaciente = "Josefina Quebra Queixo", 
                DtNascimento = DateTime.Parse("1985-03-09"), 
                NrCpf = "91952855047", 
                DsSexo = "F", 
                NrTelefone = "32963254777", 
                DsEmail = "josefina.quebra.queixo@gmail.com", 
                IdPlano = 10,
                Plano = new Models.Plano { DsCodigoPlano = "ODP010", NmPlano = "Master" }
            }
        };

        public static List<PerguntasDtos> ListaPerguntas = new()
        {
            new PerguntasDtos { DsPergunta = "Você é fumante?" },
            new PerguntasDtos { DsPergunta = "Você ja visitou um dentista esse ano?" },
            new PerguntasDtos { DsPergunta = "Você sente dor ao mastigar?" },
            new PerguntasDtos { DsPergunta = "Você já fez limpeza dentária esse ano?" },
            new PerguntasDtos { DsPergunta = "Você já escovou os dentes hoje?" },
            new PerguntasDtos { DsPergunta = "Você tem algum problema de gengiva?" },
            new PerguntasDtos { DsPergunta = "Você ja fez tratamento de canal?" },
            new PerguntasDtos { DsPergunta = "Você usa fio dental regularmente?" },
            new PerguntasDtos { DsPergunta = "Você já extraiu algum dente?" },
            new PerguntasDtos { DsPergunta = "Você masca chicletes com frequência?" }
        };

        public static List<(string Data, int Pontos, string Descricao, string Cpf)> ListaExtratoPontos = new()
        {
            ("25/05/2022", 100, "Respondeu a pergunta", ListaPacientes[1].NrCpf),
            ("13/12/2019", 150, "Resgatou pontos", ListaPacientes[1].NrCpf),
            ("05/05/2024", 200, "Respondeu a pergunta", ListaPacientes[1].NrCpf),
            ("03/02/2024", 250, "Enviou Raio X", ListaPacientes[1].NrCpf),
            ("09/07/2022", 300, "Fez uma limpeza", ListaPacientes[1].NrCpf),
            ("28/04/2021", 350, "Fez uma avaliação", ListaPacientes[1].NrCpf),
            ("07/07/2023", 400, "Resgatou pontos", ListaPacientes[1].NrCpf),
            ("19/11/2023", 450, "Respondeu a pergunta", ListaPacientes[1].NrCpf),
            ("27/06/2022", 500, "Fez uma limpeza", ListaPacientes[1].NrCpf),
            ("22/07/2020", 550, "Enviou Raio X", ListaPacientes[1].NrCpf)
        };

        public static List<string> Respostas = new()
        {
            "Sim",
            "Não",
            "Sim",
            "Sim",
            "Não",
            "Sim",
            "Não",
            "Sim",
            "Não",
            "Sim"
        };

        public static List<(string Data, string Cpf, int IdPergunta, int IdResposta)> CheckIn = new()
        {
            ("02/02/2024", ListaPacientes[2].NrCpf, 1, 1),
            ("02/02/2024", ListaPacientes[2].NrCpf, 2, 2),
            ("02/02/2024", ListaPacientes[2].NrCpf, 3, 3),
            ("02/02/2024", ListaPacientes[2].NrCpf, 4, 4),
            ("02/02/2024", ListaPacientes[2].NrCpf, 5, 5),
            ("02/02/2024", ListaPacientes[2].NrCpf, 6, 6),
            ("02/02/2024", ListaPacientes[2].NrCpf, 7, 7),
            ("02/02/2024", ListaPacientes[2].NrCpf, 8, 8),
            ("02/02/2024", ListaPacientes[2].NrCpf, 9, 9),
            ("02/02/2024", ListaPacientes[2].NrCpf, 10, 10)
        };

        public static List<(string Descricao, byte[] Imagem, string Data, string Cpf)> ListaRaioX = new()
        {
            ("Raio_x do siso", null, "02/01/2024", ListaPacientes[0].NrCpf),
            ("Raio_x do molar", null, "05/02/2024", ListaPacientes[1].NrCpf),
            ("Raio_x do pré-molar", null, "08/03/2024", ListaPacientes[2].NrCpf),
            ("Raio_x do canino", null, "11/04/2024", ListaPacientes[3].NrCpf),
            ("Raio_x panorâmico", null, "15/05/2024", ListaPacientes[4].NrCpf),
            ("Raio_x da mandíbula", null, "19/06/2024", ListaPacientes[5].NrCpf),
            ("Raio_x do maxilar", null, "23/07/2024", ListaPacientes[6].NrCpf),
            ("Raio_x do incisivo", null, "28/08/2024", ListaPacientes[7].NrCpf),
            ("Raio_x do canino", null, "02/09/2024", ListaPacientes[8].NrCpf),
            ("Raio_x do molar", null, "06/10/2024", ListaPacientes[9].NrCpf)
        };

        public static List<(string Descricao, string Data, int IdRaioX)> ListaAnaliseRaioX = new()
        {
            ("Cáries nos dentes superiores", "05/01/2024", 1),
            ("Infecção no dente molar", "10/02/2024", 2),
            ("Abscesso no pré-molar", "15/03/2024", 3),
            ("Perda óssea devido à periodontite", "20/04/2024", 4),
            ("Dente impactado no siso inferior", "25/05/2024", 5),
            ("Lesão óssea na mandíbula", "30/06/2024", 6),
            ("Fratura dentária no incisivo", "05/07/2024", 7),
            ("Dente supranumerário detectado no maxilar superior", "10/08/2024", 8),
            ("Cisto dentário em formação ao redor do canino", "15/09/2024", 9),
            ("Cárie radicular no molar inferior direito", "20/10/2024", 10)
        };

        public static List<(string Cpf, string CRO)> ListaPacienteDentista = new()
        {
            (ListaPacientes[0].NrCpf,  ListaDentistas[0].DsCro),
            (ListaPacientes[1].NrCpf,  ListaDentistas[1].DsCro),
            (ListaPacientes[2].NrCpf,  ListaDentistas[2].DsCro),
            (ListaPacientes[3].NrCpf,  ListaDentistas[3].DsCro),
            (ListaPacientes[4].NrCpf,  ListaDentistas[4].DsCro),
            (ListaPacientes[5].NrCpf,  ListaDentistas[5].DsCro),
            (ListaPacientes[6].NrCpf,  ListaDentistas[6].DsCro),
            (ListaPacientes[7].NrCpf,  ListaDentistas[7].DsCro),
            (ListaPacientes[8].NrCpf,  ListaDentistas[8].DsCro),
            (ListaPacientes[9].NrCpf,  ListaDentistas[9].DsCro)
        };
    }
}

