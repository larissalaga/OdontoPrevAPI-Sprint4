using OpenAI.Chat;
using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using OdontoPrevAPI.Models;
using System;
using System.Threading.Tasks;

namespace OdontoPrevAPI.MlModels
{
    public class GenerativeAIService
    {
        private readonly AzureOpenAIClient _client;
        private readonly string _deploymentName;
        private readonly ChatClient _chatClient;

        public GenerativeAIService(IConfiguration configuration)
        {
            var apiKey = configuration["Azure:OpenAI:ApiKey"];
            var endpoint = configuration["Azure:OpenAI:Endpoint"];
            _deploymentName = configuration["Azure:OpenAI:DeploymentName"] ?? "model-router";

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentException("Azure OpenAI API configuration is missing.");
            }

            _client = new AzureOpenAIClient(new Uri(endpoint), new AzureKeyCredential(apiKey));
            _chatClient = _client.GetChatClient(_deploymentName);
        }

        public async Task<string> GenerateDentalRecommendation(List<CheckIn> checkIns)
        {
            try
            {
                var chatCompletionsOptions = new ChatCompletionOptions()
                {
                    MaxOutputTokenCount = 8192,
                    Temperature = 0.7f,
                    TopP = 0.95f,
                    FrequencyPenalty = 0.0f,
                    PresencePenalty = 0.0f,
                };

                var systemMessage = "Você é um assistente especializado em saúde bucal que fornece recomendações profissionais baseadas em respostas de pacientes. " +
                    "   Forneça recomendações concisas e clinicamente apropriadas em português do Brasil. Limite sua resposta a 1-2 frases.";

                // Adicionar perguntas e respostas de check-in dental a partir da lista de check-ins
                var userMessage = "Com base na seguinte lista de pergunta e resposta de check-in dental," +
                    "considerando as datas e evolução do caso, forneça um possível diagnostico e recomendação profissional:";
                foreach (var checkIn in checkIns)
                {
                    userMessage += $"\nData: {checkIn.DtCheckIn.ToString("dd/MM/yyyy")}: Pergunta: {checkIn.Perguntas?.DsPergunta}, " +
                        $"Resposta: {checkIn.Respostas?.DsResposta}";
                }

                    List<ChatMessage> messages = new List<ChatMessage>()
                {
                    new SystemChatMessage(systemMessage),
                    new UserChatMessage(userMessage)
                };

                var response = await _chatClient.CompleteChatAsync(messages, chatCompletionsOptions);
                string recommendation = response.Value.Content[0].Text.Trim();

                return recommendation;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gerar recomendação: {ex.Message}");
                return "Continue mantendo sua saúde bucal com escovação regular e visitas ao dentista.";
            }
        }
    }
}