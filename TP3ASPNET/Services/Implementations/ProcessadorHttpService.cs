using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using ATASP2021.Models;

namespace ATASP2021.Services.Implementations
{
    public class ProcessadorHttpService : IProcessadorHttpService
    {
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true
        };

        public ProcessadorHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ProcessadorModelExistsAsync(int id)
        {
            var processador = await _httpClient
                                   .GetFromJsonAsync<ProcessadorViewModel>($"IsExistsId/{id}");

            return processador != null;
        }

        public async Task<ProcessadorViewModel> CreateAsync(ProcessadorViewModel processadorViewModel)
        {
            var httpResponseMessage = await _httpClient
                                            .PostAsJsonAsync("create", processadorViewModel);

            await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var processadorCreated = await JsonSerializer
                                          .DeserializeAsync<ProcessadorViewModel>(contentStream, JsonSerializerOptions);

            return processadorCreated;
        }

        public async Task DeleteAsync(int id)
        {
            var httpResponseMessage = await _httpClient
                                            .DeleteAsync($"Delete/{id}");

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<ProcessadorViewModel>> GetAllAsync()
        {
            var processador = await _httpClient
                                  .GetFromJsonAsync<IEnumerable<ProcessadorViewModel>>(string.Empty);

            return processador;
        }

        public async Task<ProcessadorViewModel> GetByIdAsync(int id)
        {
            var processador = await _httpClient
                                 .GetFromJsonAsync<ProcessadorViewModel>($"{id}");

            return processador;
        }

        public async Task<bool> ExistOnAsync(string nomeProcessador, int id)
        {
            var processador = await _httpClient
                                   .GetFromJsonAsync<ProcessadorViewModel>($"ExistOnAsync/{nomeProcessador}/{id}");

            return processador != null;
        }

        public async Task<ProcessadorViewModel> UpdateAsync(ProcessadorViewModel processadorViewModel)
        {
            var httpResponseMessage = await _httpClient
                                            .PutAsJsonAsync($"update/{processadorViewModel.Id}", processadorViewModel);

            httpResponseMessage.EnsureSuccessStatusCode();

            await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var processadorEdited = await JsonSerializer
                                       .DeserializeAsync<ProcessadorViewModel>(contentStream, JsonSerializerOptions);

            return processadorEdited;
        }

        public async Task<IEnumerable<ProcessadorViewModel>> SearchAsync(string search)
        {
            var processadores = await _httpClient
                                    .GetFromJsonAsync<IEnumerable<ProcessadorViewModel>>($"search/{search}");

            return processadores;
        }
    }
}
