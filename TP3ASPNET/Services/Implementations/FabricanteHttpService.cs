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
    public class FabricanteHttpService : IFabricanteHttpService
    {
        private readonly HttpClient _httpClient;
        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true
        };

        public FabricanteHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FabricanteViewModel> CreateAsync(FabricanteViewModel fabricanteViewModel)
        {
            var httpResponseMessage = await _httpClient
                                            .PostAsJsonAsync("create", fabricanteViewModel);

            await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var fabricanteCreated = await JsonSerializer
                                          .DeserializeAsync<FabricanteViewModel>(contentStream, JsonSerializerOptions);

            return fabricanteCreated;
        }

        public async Task DeleteAsync(int id)
        {
            var httpResponseMessage = await _httpClient
                                            .DeleteAsync($"Delete/{id}");

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task<bool> FabricanteModelExistsAsync(int id)
        {
            var fabricante = await _httpClient
                                   .GetFromJsonAsync<FabricanteViewModel>($"IsExistsId/{id}");

            return fabricante != null;
        }

        public async Task<IEnumerable<FabricanteViewModel>> GetAllAsync()
        {
            var fabricantes = await _httpClient
                                    .GetFromJsonAsync<IEnumerable<FabricanteViewModel>>(string.Empty);

            return fabricantes;
        }

        public async Task<FabricanteViewModel> GetByIdAsync(int id)
        {
            var fabricante = await _httpClient
                                   .GetFromJsonAsync<FabricanteViewModel>($"{id}");

            return fabricante;
        }

        public async Task<FabricanteViewModel> ExistAsync(string nomeFabricante, int id)
        {
            var fabricanteViewModel = await _httpClient
                                            .GetFromJsonAsync<FabricanteViewModel>($"ExistAsync/{nomeFabricante}/{id}");

            return fabricanteViewModel;
        }

        public async Task<FabricanteViewModel> UpdateAsync(FabricanteViewModel fabricanteViewModel)
        {
            var httpResponseMessage = await _httpClient
                                            .PutAsJsonAsync($"update/{fabricanteViewModel.Id}", fabricanteViewModel);

            httpResponseMessage.EnsureSuccessStatusCode();

            await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var fabricanteEdited = await JsonSerializer
                                    .DeserializeAsync<FabricanteViewModel>(contentStream, JsonSerializerOptions);

            return fabricanteEdited;
        }

        public async Task<IEnumerable<FabricanteViewModel>> SearchAsync(string search)
        {
            var fabricante = await _httpClient
                                    .GetFromJsonAsync<IEnumerable<FabricanteViewModel>>($"search/{search}");

            return fabricante;
        }
    }
}
