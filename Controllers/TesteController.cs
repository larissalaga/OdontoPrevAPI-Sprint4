using Microsoft.AspNetCore.Mvc;
using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Models;
using OdontoPrevAPI.Repositories.Interfaces;
using OdontoPrevAPI.Tools;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.EntityFrameworkCore;
using OdontoPrevAPI.Data;
using System.Numerics;

namespace OdontoPrevAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciar dados de teste.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IDentistaRepository _dentistaRepository;
        private readonly IPlanoRepository _planoRepository;
        private readonly IPerguntasRepository _perguntasRepository;
        private readonly IRespostasRepository _respostasRepository;
        private readonly IExtratoPontosRepository _extratoPontosRepository;
        private readonly IRaioXRepository _raioXRepository;
        private readonly IAnaliseRaioXRepository _analiseRaioXRepository;
        private readonly ICheckInRepository _checkInRepository;
        private readonly IPacienteDentistaRepository _pacienteDentistaRepository;
        private readonly DataContext _context;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="TesteController"/>.
        /// </summary>
        public TesteController(
            IPacienteRepository pacienteRepository,
            IDentistaRepository dentistaRepository,
            IPlanoRepository planoRepository,
            IPerguntasRepository perguntasRepository,
            IRespostasRepository respostasRepository,
            IExtratoPontosRepository extratoPontosRepository,
            IRaioXRepository raioXRepository,
            IAnaliseRaioXRepository analiseRaioXRepository,
            ICheckInRepository checkInRepository,
            IPacienteDentistaRepository pacienteDentistaRepository,
            DataContext context)
        {
            _pacienteRepository = pacienteRepository;
            _dentistaRepository = dentistaRepository;
            _planoRepository = planoRepository;
            _perguntasRepository = perguntasRepository;
            _respostasRepository = respostasRepository;
            _extratoPontosRepository = extratoPontosRepository;
            _raioXRepository = raioXRepository;
            _analiseRaioXRepository = analiseRaioXRepository;
            _checkInRepository = checkInRepository;
            _pacienteDentistaRepository = pacienteDentistaRepository;
            _context = context;
        }

        /// <summary>
        /// Insere dados de teste no banco de dados.
        /// </summary>
        /// <returns>Resultado da operação de inserção de dados de teste.</returns>
        [HttpPost("populate")]
        [SwaggerOperation(Summary = "Popula o banco de dados com dados de teste", 
            Description = "Insere todos os dados pré-definidos em DadosDeTestes.")]
        [SwaggerResponse(200, "Dados de teste inseridos com sucesso")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> PopulateTestData()
        {
            try
            {
                // Contadores para registrar quantas entidades foram inseridas
                int sequenceInseridas = 0;
                int planosInseridos = 0;
                int dentistasInseridos = 0;
                int pacientesInseridos = 0;
                int perguntasInseridas = 0;
                int respostasInseridas = 0;
                int extratoPontosInseridos = 0;
                int raiosXInseridos = 0;
                int analisesRaioXInseridas = 0;
                int checkInsInseridos = 0;
                int pacientesDentistasInseridos = 0;
                
                // Dicionários para mapear IDs gerados
                Dictionary<int, int> perguntasIDs = new Dictionary<int, int>(); // index -> id real
                Dictionary<int, int> respostasIDs = new Dictionary<int, int>(); // index -> id real
                Dictionary<int, int> raiosXIDs = new Dictionary<int, int>(); // index -> id real
                                
                // 1. Insere todos os planos
                foreach (var plano in DadosDeTestes.ListaPlanos)
                {
                    try
                    {
                        await _planoRepository.Create(plano);
                        planosInseridos++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao inserir plano {plano.DsCodigoPlano}: {ex.Message}");
                    }
                }

                // 2. Insere todos os dentistas
                foreach (var dentista in DadosDeTestes.ListaDentistas)
                {
                    try
                    {
                        await _dentistaRepository.Create(dentista);
                        dentistasInseridos++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao inserir dentista {dentista.DsCro}: {ex.Message}");
                    }
                }

                // 3. Insere todos os pacientes
                foreach (var paciente in DadosDeTestes.ListaPacientes)
                {
                    try
                    {
                        var plano = await _planoRepository.GetByDsCodigoPlano(paciente.Plano.DsCodigoPlano);  
                        paciente.IdPlano = plano.IdPlano;
                        await _pacienteRepository.Create(paciente);
                        pacientesInseridos++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao inserir paciente {paciente.NrCpf}: {ex.Message}");
                    }
                }

                // 4. Insere todas as perguntas
                for (int i = 0; i < DadosDeTestes.ListaPerguntas.Count; i++)
                {
                    try
                    {
                        var perguntaDto = new PerguntasDtos { DsPergunta = DadosDeTestes.ListaPerguntas[i].DsPergunta };
                        var novaPergunta = await _perguntasRepository.Create(perguntaDto);
                        perguntasIDs[i] = novaPergunta.IdPergunta;
                        perguntasInseridas++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao inserir pergunta {i+1}: {ex.Message}");
                    }
                }

                // 5. Insere todas as respostas
                for (int i = 0; i < DadosDeTestes.Respostas.Count; i++)
                {
                    try
                    {
                        var respostaDto = new RespostasDtos { DsResposta = DadosDeTestes.Respostas[i] };
                        var novaResposta = await _respostasRepository.Create(respostaDto);
                        respostasIDs[i] = novaResposta.IdResposta;
                        respostasInseridas++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao inserir resposta {i+1}: {ex.Message}");
                    }
                }

                // 9. Insere todos os check-ins
                foreach (var checkin in DadosDeTestes.CheckIn)
                {
                    try
                    {
                        // Verifica se temos os IDs reais nos dicionários
                        int realIdPergunta = checkin.IdPergunta <= perguntasIDs.Count ? perguntasIDs[checkin.IdPergunta - 1] : checkin.IdPergunta;
                        int realIdResposta = checkin.IdResposta <= respostasIDs.Count ? respostasIDs[checkin.IdResposta - 1] : checkin.IdResposta;
                        var paciente = await _pacienteRepository.GetByNrCpf(checkin.Cpf);

                        var checkInDto = new CheckInDtos
                        {
                            DtCheckIn = DateTime.Parse(checkin.Data),
                            IdPaciente = paciente.IdPaciente,
                            IdPergunta = realIdPergunta,
                            IdResposta = realIdResposta
                        };
                        await _checkInRepository.Create(checkInDto);
                        checkInsInseridos++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao inserir check-in para paciente {checkin.Cpf}: {ex.Message}");
                    }
                }

                // 6. Insere todos os extratos de pontos
                foreach (var (Data, Pontos, Descricao, Cpf) in DadosDeTestes.ListaExtratoPontos)
                {
                    try
                    {
                        var paciente = await _pacienteRepository.GetByNrCpf(Cpf);
                        var extratoPontosDto = new ExtratoPontosDtos
                        {
                            DtExtrato = DateTime.Parse(Data),
                            NrNumeroPontos = Pontos,
                            DsMovimentacao = Descricao,
                            IdPaciente = paciente.IdPaciente
                        };
                        await _extratoPontosRepository.Create(extratoPontosDto);
                        extratoPontosInseridos++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao inserir extrato de pontos para paciente {Cpf}: {ex.Message}");
                    }
                }

                // 7. Insere todos os raios-X
                for (int i = 0; i < DadosDeTestes.ListaRaioX.Count; i++)
                {
                    try
                    {
                        var (Descricao, Imagem, Data, Cpf) = DadosDeTestes.ListaRaioX[i];
                        var paciente = await _pacienteRepository.GetByNrCpf(Cpf);
                        var raioXDto = new RaioXDtos
                        {
                            DsRaioX = Descricao,
                            ImRaioX = Imagem, // Pode ser null
                            DtDataRaioX = DateTime.Parse(Data),
                            IdPaciente = paciente.IdPaciente
                        };
                        var novoRaioX = await _raioXRepository.Create(raioXDto);
                        raiosXIDs[i] = novoRaioX.IdRaioX;
                        raiosXInseridos++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao inserir raio-X {i+1}: {ex.Message}");
                    }
                }

                // 8. Insere todas as análises de raios-X
                foreach (var (Descricao, Data, IdRaioX) in DadosDeTestes.ListaAnaliseRaioX)
                {
                    try
                    {
                        // Verifica se temos o ID do raio-X real no dicionário
                        int realIdRaioX = IdRaioX <= raiosXIDs.Count ? raiosXIDs[IdRaioX - 1] : IdRaioX;
                        
                        var analiseRaioXDto = new AnaliseRaioXDtos
                        {
                            DsAnaliseRaioX = Descricao,
                            DtAnaliseRaioX = DateTime.Parse(Data),
                            IdRaioX = realIdRaioX
                        };
                        await _analiseRaioXRepository.Create(analiseRaioXDto);
                        analisesRaioXInseridas++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao inserir análise de raio-X para raio-X {IdRaioX}: {ex.Message}");
                    }
                }

               

                // 10. Insere todas as relações Paciente-Dentista
                foreach (var (Cpf, CRO) in DadosDeTestes.ListaPacienteDentista)
                {
                    try
                    {
                        var paciente = await _pacienteRepository.GetByNrCpf(Cpf);
                        var dentista = await _dentistaRepository.GetByDsCro(CRO);
                        await _pacienteDentistaRepository.Create(dentista.IdDentista, paciente.IdPaciente);
                        pacientesDentistasInseridos++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao inserir relação paciente {Cpf} - dentista {CRO}: {ex.Message}");
                    }
                }

                // Retorna um resumo das inserções
                return Ok(new
                {
                    message = "Dados de teste inseridos com sucesso",
                    details = new
                    {
                        planos = $"{planosInseridos} de {DadosDeTestes.ListaPlanos.Count} planos inseridos",
                        dentistas = $"{dentistasInseridos} de {DadosDeTestes.ListaDentistas.Count} dentistas inseridos",
                        pacientes = $"{pacientesInseridos} de {DadosDeTestes.ListaPacientes.Count} pacientes inseridos",
                        perguntas = $"{perguntasInseridas} de {DadosDeTestes.ListaPerguntas.Count} perguntas inseridas",
                        respostas = $"{respostasInseridas} de {DadosDeTestes.Respostas.Count} respostas inseridas",
                        extratoPontos = $"{extratoPontosInseridos} de {DadosDeTestes.ListaExtratoPontos.Count} extratos de pontos inseridos",
                        raiosX = $"{raiosXInseridos} de {DadosDeTestes.ListaRaioX.Count} raios-X inseridos",
                        analisesRaioX = $"{analisesRaioXInseridas} de {DadosDeTestes.ListaAnaliseRaioX.Count} análises de raio-X inseridas",
                        checkIns = $"{checkInsInseridos} de {DadosDeTestes.CheckIn.Count} check-ins inseridos",
                        pacientesDentistas = $"{pacientesDentistasInseridos} de {DadosDeTestes.ListaPacienteDentista.Count} relações paciente-dentista inseridas"
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao inserir dados de teste: {ex.Message}");
            }
        }

        /// <summary>
        /// Limpa todos os dados do banco de dados.
        /// </summary>
        /// <returns>Resultado da operação de limpeza do banco de dados.</returns>
        [HttpDelete("clear")]
        [SwaggerOperation(Summary = "Limpa o banco de dados", 
            Description = "Remove todas as entidades do banco de dados.")]
        [SwaggerResponse(200, "Banco de dados limpo com sucesso")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> ClearDatabase()
        {
            try
            {
                // Contadores para registrar quantas entidades foram removidas
                int checkInsRemovidos = 0;
                int analisesRaioXRemovidas = 0;
                int raiosXRemovidos = 0;
                int extratoPontosRemovidos = 0;                
                int respostasRemovidas = 0;
                int perguntasRemovidas = 0;
                int pacientesDentistasRemovidos = 0;                
                int dentistasRemovidos = 0;
                int pacientesRemovidos = 0;
                int planosRemovidos = 0;

                // Remover entidades na ordem correta para respeitar as restrições de chave estrangeira

                // 1. Remover CheckIns
                var checkIns = await _context.CheckIn.ToListAsync();
                foreach (var checkIn in checkIns)
                {
                    try
                    {
                        _context.CheckIn.Remove(checkIn);
                        checkInsRemovidos++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao remover check-in ID {checkIn.IdCheckIn}: {ex.Message}");
                    }
                }

                // 2. Remover AnaliseRaioX
                var analisesRaioX = await _context.AnaliseRaioX.ToListAsync();
                foreach (var analise in analisesRaioX)
                {
                    try
                    {
                        _context.AnaliseRaioX.Remove(analise);
                        analisesRaioXRemovidas++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao remover análise de raio-X ID {analise.IdAnaliseRaioX}: {ex.Message}");
                    }
                }

                // 3. Remover RaioX
                var raiosX = await _context.RaioX.ToListAsync();
                foreach (var raioX in raiosX)
                {
                    try
                    {
                        _context.RaioX.Remove(raioX);
                        raiosXRemovidos++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao remover raio-X ID {raioX.IdRaioX}: {ex.Message}");
                    }
                }

                // 4. Remover ExtratoPontos
                var extratosPontos = await _context.ExtratoPontos.ToListAsync();
                foreach (var extrato in extratosPontos)
                {
                    try
                    {
                        _context.ExtratoPontos.Remove(extrato);
                        extratoPontosRemovidos++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao remover extrato de pontos ID {extrato.IdExtratoPontos}: {ex.Message}");
                    }
                }

                // 9. Remover Respostas
                var respostas = await _context.Respostas.ToListAsync();
                foreach (var resposta in respostas)
                {
                    try
                    {
                        _context.Respostas.Remove(resposta);
                        respostasRemovidas++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao remover resposta ID {resposta.IdResposta}: {ex.Message}");
                    }
                }

                // 8. Remover Perguntas
                var perguntas = await _context.Perguntas.ToListAsync();
                foreach (var pergunta in perguntas)
                {
                    try
                    {
                        _context.Perguntas.Remove(pergunta);
                        perguntasRemovidas++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao remover pergunta ID {pergunta.IdPergunta}: {ex.Message}");
                    }
                }

                // 5. Remover PacienteDentista
                var pacientesDentistas = await _context.PacienteDentista.ToListAsync();
                foreach (var pd in pacientesDentistas)
                {
                    try
                    {
                        _context.PacienteDentista.Remove(pd);
                        pacientesDentistasRemovidos++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao remover relação paciente {pd.IdPaciente} - dentista {pd.IdDentista}: {ex.Message}");
                    }
                }               

                // 7. Remover Dentistas
                var dentistas = await _dentistaRepository.GetAll();
                foreach (var dentista in dentistas)
                {
                    try
                    {
                        var result = await _dentistaRepository.DeleteById(dentista.IdDentista);
                        if (result)
                            dentistasRemovidos++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao remover dentista ID {dentista.IdDentista}: {ex.Message}");
                    }
                }

                // 6. Remover Pacientes
                var pacientes = await _pacienteRepository.GetAll();
                foreach (var paciente in pacientes)
                {
                    try
                    {
                        var result = await _pacienteRepository.DeleteById(paciente.IdPaciente);
                        if (result)
                            pacientesRemovidos++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao remover paciente ID {paciente.IdPaciente}: {ex.Message}");
                    }
                }

                // 10. Remover Planos por último
                var planos = await _planoRepository.GetAll();
                foreach (var plano in planos)
                {
                    try
                    {
                        var result = await _planoRepository.DeleteById(plano.IdPlano);
                        if (result)
                            planosRemovidos++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao remover plano ID {plano.IdPlano}: {ex.Message}");
                    }
                }
                
                await _context.SaveChangesAsync();

                // Retorna um resumo das remoções
                return Ok(new
                {
                    message = "Banco de dados limpo com sucesso",
                    details = new
                    {
                        checkIns = $"{checkInsRemovidos} check-ins removidos",
                        analisesRaioX = $"{analisesRaioXRemovidas} análises de raio-X removidas",
                        raiosX = $"{raiosXRemovidos} raios-X removidos",
                        extratoPontos = $"{extratoPontosRemovidos} extratos de pontos removidos",
                        pacientesDentistas = $"{pacientesDentistasRemovidos} relações paciente-dentista removidas",
                        pacientes = $"{pacientesRemovidos} pacientes removidos",
                        dentistas = $"{dentistasRemovidos} dentistas removidos",
                        perguntas = $"{perguntasRemovidas} perguntas removidas",
                        respostas = $"{respostasRemovidas} respostas removidas",
                        planos = $"{planosRemovidos} planos removidos"                        
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao limpar o banco de dados: {ex.Message}");
            }
        }
    }
}

