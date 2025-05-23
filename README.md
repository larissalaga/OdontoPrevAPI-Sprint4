OdontoPrevAPI
O que � o OdontoPrevAPI?
OdontoPrevAPI � uma API para gerenciamento de sa�de bucal preventiva que utiliza t�cnicas avan�adas de Machine Learning e Intelig�ncia Artificial para oferecer recomenda��es personalizadas aos pacientes com base em seus hist�ricos de check-ins odontol�gicos.
A plataforma permite o registro de relacionamento entre Pacientes e Dentistas, e o registro de um di�rio de sa�de bucal, ao responder Check Ins di�rios.
O sistema permite analisar os dados de check-ins dos pacientes nos �ltimos 5 dias e utiliza tanto modelos de Machine Learning tradicionais quanto Intelig�ncia Artificial generativa para detectar potenciais problemas bucais e fornecer recomenda��es adequadas ao contexto do paciente.

Caracter�sticas Principais
�	Registro de Pacientes e Dentistas: Permite o gerenciamento de informa��es de pacientes e dentistas.
�	Check-Ins Di�rios: Os pacientes podem registrar suas condi��es bucais diariamente, respondendo a perguntas sobre dor, sangramento, sensibilidade e bruxismo.
�	An�lise Contextual: Processamento do hist�rico recente de check-ins do paciente
�	Dupla Abordagem de IA:
	�	ML.NET: Modelo de Machine Learning para detectar padr�es e problemas
	�	Azure OpenAI: IA generativa para recomenda��es personalizadas em linguagem natural
�	Compara��o de Abordagens: Possibilidade de comparar recomenda��es dos modelos de ML e IA generativa

Integra��o com Google Accounts
O sistema implementa autentica��o segura utilizando contas Google para controle de acesso:
1.	Autentica��o OAuth 2.0:
�	Integra��o nativa com o protocolo OAuth 2.0 do Google
�	Configura��o via ASP.NET Core Authentication Middleware
�	Gerenciamento seguro de credenciais atrav�s de vari�veis de ambiente
2.	Prote��o de Endpoints:
�	Interface Swagger protegida com login Google
�	Middleware personalizado para roteamento de requisi��es autenticadas
�	For�amento de sele��o de conta a cada acesso com par�metro prompt=select_account
3.	APIs RESTful para Gerenciamento de Sess�o:
�	Endpoint /api/account/logout para encerramento seguro de sess�o
�	Preserva��o de estado atrav�s de cookies de autentica��o
�	Separa��o clara entre autentica��o e l�gica de neg�cio
4.	Fluxo de Autentica��o:
�	Redirecionamento autom�tico para tela de login Google
�	Tratamento de tokens e credenciais em conformidade com padr�es de seguran�a
�	Inje��o de JavaScript personalizado na interface Swagger ap�s autentica��o
Esta integra��o assegura que apenas usu�rios autorizados possam acessar a API e suas funcionalidades sens�veis, mantendo o padr�o RESTful para todas as opera��es de autentica��o.

Pr�ticas Clean Code e SOLID
Clean Code
O projeto implementa os seguintes princ�pios de Clean Code:
1.	Nomenclatura Clara e Descritiva
�	Classes como MlService, ContextualCheckInData, e PredictionResult
�	M�todos que expressam claramente seu prop�sito: TrainModelFromDatabase, PredictRecommendationForPatient
2.	Documenta��o Completa
�	Coment�rios XML descrevendo o prop�sito de m�todos e classes
�	Anota��es Swagger para documenta��o da API
3.	Tratamento de Erros Consistente
�	Blocos try-catch com mensagens de erro detalhadas
�	Retorno de c�digos HTTP apropriados em cada situa��o
SOLID
1.	Princ�pio da Responsabilidade �nica
�	MlService focado em opera��es de Machine Learning
�	Controllers que gerenciam apenas requisi��es HTTP
�	Acesso a dados isolado em interfaces de reposit�rio
2.	Princ�pio Aberto/Fechado
�	Pipeline de ML extens�vel atrav�s de configura��o
3.	Princ�pio da Invers�o de Depend�ncia
�	Uso intenso de inje��o de depend�ncia via construtor
�	Controllers dependem de abstra��es (interfaces) e n�o implementa��es
�	MlController recebe reposit�rios e servi�os como depend�ncias
4.	Princ�pio da Segrega��o de Interface
�	Interfaces de reposit�rio (ICheckInRepository, IPacienteRepository) focadas e espec�ficas

Ferramentas de Intelig�ncia Artificial
Integra��o com ML.NET
A API utiliza o framework ML.NET para processamento e an�lise dos dados de check-in odontol�gicos:
1.	Treinamento de Modelos:
�	M�todo TrainModelFromDatabase() processa check-ins dos �ltimos 5 dias
�	Agrupamento de dados por paciente para an�lise contextual
2.	Processamento Contextual:
�	Classe ContextualCheckInData consolida o hist�rico de um paciente
�	An�lise de t�picos relevantes (dor, sangramento, sensibilidade, bruxismo)
3.	Pipeline de Machine Learning:
�	Processamento de texto com FeaturizeText para perguntas e respostas
�	Convers�o de caracter�sticas booleanas e num�ricas
�	Algoritmo FastTree para classifica��o bin�ria
4.	Previs�es Baseadas em Regras e ML:
�	M�todo DetermineIfPotentialIssue() implementa regras espec�ficas de dom�nio
�	Modelo contextual para an�lise de padr�es mais complexos
Integra��o com Azure OpenAI
A API integra o Azure OpenAI para gera��o de recomenda��es mais naturais e contextuais:
1.	Servi�o Generativo:
�	Classe GenerativeAIService encapsula a comunica��o com a API do Azure OpenAI
�	Configura��o do modelo via inje��o de depend�ncia
2.	Gera��o de Recomenda��es Personalizadas:
�	M�todo GenerateDentalRecommendation() envia hist�rico de check-ins para an�lise da IA
�	Formata��o de prompts detalhados incluindo datas e evolu��o do caso
3.	Compara��o com Modelos Tradicionais:
�	API permite comparar recomenda��es do ML tradicional vs IA generativa
�	Diferentes n�veis de confian�a para cada m�todo
4.	Fallback Inteligente:
�	Sistema cai para recomenda��es pr�-definidas em caso de falha na API

Arquitetura
O projeto segue um padr�o de arquitetura em camadas:
1.	Controllers: Gerenciamento de requisi��es HTTP
2.	Services: L�gica de neg�cio e integra��o com IA
3.	Repositories: Acesso a dados
4.	Models: Entidades de dom�nio e DTOs
Esta arquitetura limpa permite a evolu��o dos modelos de ML e IA sem afetar a estrutura geral da aplica��o.

Contribui��es
Este projeto demonstra a aplica��o de pr�ticas modernas de desenvolvimento com foco em IA e ML para o dom�nio da sa�de bucal, oferecendo uma abordagem h�brida que combina o poder preditivo do ML.NET com a capacidade generativa do Azure OpenAI.