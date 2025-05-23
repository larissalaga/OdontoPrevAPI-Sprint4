# OdontoPrevAPI

## O que é o OdontoPrevAPI?

OdontoPrevAPI é uma API para gerenciamento de saúde bucal preventiva que utiliza técnicas avançadas de Machine Learning e Inteligência Artificial para oferecer recomendações personalizadas aos pacientes com base em seus históricos de check-ins odontológicos.

A plataforma permite o registro de relacionamento entre Pacientes e Dentistas, e o registro de um diário de saúde bucal, ao responder Check Ins diários.

O sistema permite analisar os dados de check-ins dos pacientes nos últimos 5 dias e utiliza tanto modelos de Machine Learning tradicionais quanto Inteligência Artificial generativa para detectar potenciais problemas bucais e fornecer recomendações adequadas ao contexto do paciente.

## Características Principais

- Registro de Pacientes e Dentistas: Permite o gerenciamento de informações de pacientes e dentistas.
- Check-Ins Diários: Os pacientes podem registrar suas condições bucais diariamente, respondendo a perguntas sobre dor, sangramento, sensibilidade e bruxismo.
- Análise Contextual: Processamento do histórico recente de check-ins do paciente
- Dupla Abordagem de IA:
  - ML.NET: Modelo de Machine Learning para detectar padrões e problemas
  - Azure OpenAI: IA generativa para recomendações personalizadas em linguagem natural
- Comparação de Abordagens: Possibilidade de comparar recomendações dos modelos de ML e IA generativa

## Integração com Google Accounts

O sistema implementa autenticação segura utilizando contas Google para controle de acesso:

### 1. Autenticação OAuth 2.0:
- Integração nativa com o protocolo OAuth 2.0 do Google
- Configuração via ASP.NET Core Authentication Middleware
- Gerenciamento seguro de credenciais através de variáveis de ambiente

### 2. Proteção de Endpoints:
- Interface Swagger protegida com login Google
- Middleware personalizado para roteamento de requisições autenticadas
- Forçamento de seleção de conta a cada acesso com parâmetro prompt=select_account

### 3. APIs RESTful para Gerenciamento de Sessão:
- Endpoint /api/account/logout para encerramento seguro de sessão
- Preservação de estado através de cookies de autenticação
- Separação clara entre autenticação e lógica de negócio

### 4. Fluxo de Autenticação:
- Redirecionamento automático para tela de login Google
- Tratamento de tokens e credenciais em conformidade com padrões de segurança
- Injeção de JavaScript personalizado na interface Swagger após autenticação

Esta integração assegura que apenas usuários autorizados possam acessar a API e suas funcionalidades sensíveis, mantendo o padrão RESTful para todas as operações de autenticação.

## Práticas Clean Code e SOLID

### Clean Code

O projeto implementa os seguintes princípios de Clean Code:

#### 1. Nomenclatura Clara e Descritiva
- Classes como MlService, ContextualCheckInData, e PredictionResult
- Métodos que expressam claramente seu propósito: TrainModelFromDatabase, PredictRecommendationForPatient

#### 2. Documentação Completa
- Comentários XML descrevendo o propósito de métodos e classes
- Anotações Swagger para documentação da API

#### 3. Tratamento de Erros Consistente
- Blocos try-catch com mensagens de erro detalhadas
- Retorno de códigos HTTP apropriados em cada situação

### SOLID

#### 1. Princípio da Responsabilidade Única
- MlService focado em operações de Machine Learning
- Controllers que gerenciam apenas requisições HTTP
- Acesso a dados isolado em interfaces de repositório

#### 2. Princípio Aberto/Fechado
- Pipeline de ML extensível através de configuração

#### 3. Princípio da Inversão de Dependência
- Uso intenso de injeção de dependência via construtor
- Controllers dependem de abstrações (interfaces) e não implementações
- MlController recebe repositórios e serviços como dependências

#### 4. Princípio da Segregação de Interface
- Interfaces de repositório (ICheckInRepository, IPacienteRepository) focadas e específicas

## Ferramentas de Inteligência Artificial

### Integração com ML.NET

A API utiliza o framework ML.NET para processamento e análise dos dados de check-in odontológicos:

#### 1. Treinamento de Modelos:
- Método TrainModelFromDatabase() processa check-ins dos últimos 5 dias
- Agrupamento de dados por paciente para análise contextual

#### 2. Processamento Contextual:
- Classe ContextualCheckInData consolida o histórico de um paciente
- Análise de tópicos relevantes (dor, sangramento, sensibilidade, bruxismo)

#### 3. Pipeline de Machine Learning:
- Processamento de texto com FeaturizeText para perguntas e respostas
- Conversão de características booleanas e numéricas
- Algoritmo FastTree para classificação binária

#### 4. Previsões Baseadas em Regras e ML:
- Método DetermineIfPotentialIssue() implementa regras específicas de domínio
- Modelo contextual para análise de padrões mais complexos
#### 5. Dados de treinamento e teste:
- Os dados usados para treinamento do modelo são gerados aleatoriamente, com 30% dos dados reservados para teste. Esses dados estão no arquivo SQL Packages/ML/DadosTeste.sql
- Os dados de teste se encontram em SQL Packages/ML/DadosTeste.sql, e são gerados aleatoriamente

### Integração com Azure OpenAI

A API integra o Azure OpenAI para geração de recomendações mais naturais e contextuais:

#### 1. Serviço Generativo:
- Classe GenerativeAIService encapsula a comunicação com a API do Azure OpenAI
- Configuração do modelo via injeção de dependência

#### 2. Geração de Recomendações Personalizadas:
- Método GenerateDentalRecommendation() envia histórico de check-ins para análise da IA
- Formatação de prompts detalhados incluindo datas e evolução do caso

#### 3. Comparação com Modelos Tradicionais:
- API permite comparar recomendações do ML tradicional vs IA generativa
- Diferentes níveis de confiança para cada método

#### 4. Fallback Inteligente:
- Sistema cai para recomendações pré-definidas em caso de falha na API

## Arquitetura

O projeto segue um padrão de arquitetura em camadas:

1. Controllers: Gerenciamento de requisições HTTP
2. Services: Lógica de negócio e integração com IA
3. Repositories: Acesso a dados
4. Models: Entidades de domínio e DTOs

Esta arquitetura limpa permite a evolução dos modelos de ML e IA sem afetar a estrutura geral da aplicação.

## Testes Automatizados

O projeto OdontoPrevAPI implementa uma suíte completa de testes automatizados para garantir a qualidade e confiabilidade do código. A estratégia de testes segue as melhores práticas de desenvolvimento de software, com cobertura em múltiplos níveis.

### Estrutura e Organização

O projeto de testes está organizado em uma estrutura clara que separa diferentes tipos de testes:

OdontoPrevAPl.Tests/
├── UnitTests/
│   ├── Controllers/     # Testes para os controladores da API
│   ├── Repositories/    # Testes para as classes de acesso a dados
│   ├── Services/        # Testes para serviços de aplicação
│   └── Models/          # Testes para modelos e entidades
├── IntegrationTests/    # Testes que verificam a integração entre componentes
│   ├── Controllers/     # Testes para os controladores da API
│   ├── Repositories/    # Testes para as classes de acesso a dados
└── SystemTests/         # Testes end-to-end que simulam usuários reais

### Ferramentas Utilizadas

A implementação de testes utiliza um conjunto moderno de ferramentas:

- xUnit: Framework principal de testes, escolhido por sua simplicidade e extensibilidade
- FluentAssertions: Biblioteca que permite asserções mais expressivas e legíveis
- Moq: Framework de mocking para isolar componentes em testes unitários
- EntityFrameworkCore.InMemory: Permite testar repositórios sem dependência de um banco de dados real
- Microsoft.AspNetCore.Mvc.Testing: Facilita testes de integração com a API web completa
- Microsoft.AspNetCore.TestHost: Hospeda a aplicação durante os testes de integração

### Tipos de Testes

#### 1. Testes Unitários

Os testes unitários focam em componentes individuais, isolando-os de suas dependências:

- Testes de Repositórios: Verificam se os métodos de acesso a dados funcionam corretamente
  - Exemplo: CheckInRepositoryTests, PerguntasRepositoryTests, RespostasRepositoryTests
- Testes de Controllers: Validam o comportamento dos endpoints da API
- Testes de Serviços: Asseguram que a lógica de negócio funciona conforme esperado

#### 2. Testes de Integração

Os testes de integração verificam como diferentes componentes do sistema interagem:

- Integração entre Controladores e Repositórios: Validam fluxos de dados completos
- Testes de Middleware: Garantem que componentes como autenticação funcionam corretamente
- Integrações com ML.NET: Verificam se os modelos de machine learning funcionam conforme esperado

#### 3. Testes de Sistema (End-to-End)

Esses testes simulam interações reais de usuários com a API:

- Ciclos Completos: Testes que percorrem fluxos de uso completo da API
- Simulações de Cliente HTTP: Verificam se os endpoints funcionam corretamente de ponta a ponta

### Estratégia para Dependências Externas

O projeto implementa uma abordagem robusta para lidar com dependências externas:

- Banco de Dados: Utiliza banco de dados em memória para testes, evitando dependência de Oracle
- Procedimentos Armazenados: Mock de chamadas SQL para simular procedimentos Oracle
- APIs Externas: Mock de serviços externos como Azure OpenAI

### Benefícios dos Testes Automatizados

- Maior Confiança no Código: Detecção precoce de regressões ou falhas
- Facilita Refatorações: Segurança para melhorar o código sem quebrar funcionalidades
- Documentação Viva: Os testes servem como exemplos práticos de uso dos componentes
- Desenvolvimento Guiado por Testes: Possibilita a implementação de TDD (Test-Driven Development)

### Execução dos Testes

Os testes podem ser executados através:
- Do Visual Studio Test Explorer
- Linha de comando com "dotnet test"
- Integração contínua em pipelines de CI/CD

Esta abordagem abrangente de testes automatizados garante que a OdontoPrevAPI mantenha alta qualidade à medida que evolui, especialmente considerando os componentes avançados de ML e IA que requerem testes rigorosos.

## Contribuições

Este projeto demonstra a aplicação de práticas modernas de desenvolvimento com foco em IA e ML para o domínio da saúde bucal, oferecendo uma abordagem híbrida que combina o poder preditivo do ML.NET com a capacidade generativa do Azure OpenAI.