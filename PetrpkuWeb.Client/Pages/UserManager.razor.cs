using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json;
using PetrpkuWeb.Shared.Contracts.V1;
using PetrpkuWeb.Shared.Models;
using PetrpkuWeb.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetrpkuWeb.Client.Pages
{
    public partial class UserManager
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public IMatToaster Toaster { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        private LdapUser LdapUser { get; set; } = new LdapUser();

        string SearchTerm { get; set; } = String.Empty;

        List<LdapUser> ldapUsers;
        ClaimsPrincipal authUser;
        List<AppUserViewModel> appUsersList;
        AppUserViewModel editAppUser;
        List<DepartmentViewModel> departments;
        List<BuildingViewModel> buildings;
        List<AppUserViewModel> filtredAppUsers;

        bool editAppUserDialogIsOpen = false;
        bool newUserDialogIsOpen = false;

        protected async override Task OnInitializedAsync()
        {
            authUser = (await AuthenticationStateTask).User;

            appUsersList = await HttpClient.GetJsonAsync<List<AppUserViewModel>>(ApiRoutes.Users.ALL);
            departments = await HttpClient.GetJsonAsync<List<DepartmentViewModel>>(ApiRoutes.Departments.ALL);
            buildings = await HttpClient.GetJsonAsync<List<BuildingViewModel>>(ApiRoutes.Buildings.ALL);
            ldapUsers = await HttpClient.GetJsonAsync<List<LdapUser>>(ApiRoutes.Account.ALL_LDAPUSERS);
        }

        async Task AddAccount()
        {
            newUserDialogIsOpen = false;

            var newUser = ldapUsers.FirstOrDefault(u => u.UserName == LdapUser.UserName);

            // Remove from dropdown list of LdapUsers collection
            ldapUsers.Remove(newUser);

            var jsonContent = JsonConvert.SerializeObject(newUser);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync(ApiRoutes.Account.ADD_IDENTITY, byteContent);
            var content = await response.Content.ReadAsStringAsync();

            //Add to UsersIdentity collection
            var appUser = JsonConvert.DeserializeObject<AppUserViewModel>(content);
            appUsersList.Add(appUser);
            appUsersList = appUsersList.OrderBy(d => d.DisplayName).ToList();
            LdapUser = new LdapUser();

            Toaster.Add($"Пользователь успешно добавлен в систему", MatToastType.Success, "Успех!");
        }


        void OpenEditAppUserDialog(string appUserId)
        {
            editAppUserDialogIsOpen = true;
            editAppUser = appUsersList.FirstOrDefault(u => u.Id == appUserId);
        }

        async Task UpdateAppUser(string appUserId)
        {
            editAppUserDialogIsOpen = false;
            var response = await HttpClient.PutJsonAsync<AppUserViewModel>($"{ApiRoutes.Users.UPDATE}/{appUserId}", editAppUser);
            var index = appUsersList.FindIndex(u => u.Id == response.Id);
            appUsersList[index] = response;
            Toaster.Add($"Информация о пользователе {response.DisplayName} успешно обновлена", MatToastType.Success, "Успех!");
        }

        void SearchKeyDown(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                SearchCommand();
            }
        }

        void SearchButton()
        {
            SearchCommand();
        }

        void ClearButton()
        {
            SearchTerm = String.Empty;
            filtredAppUsers = null;
        }

        void SearchCommand()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                string filter = SearchTerm.ToLower();
                filtredAppUsers = appUsersList
                    .Where(u => (u.LastName ?? "").ToLower().Contains(filter) || (u.IntPhone ?? "").ToLower().Contains(filter) || (u.DepartmentViewModel.Name ?? "").ToLower().Contains(filter)).ToList();
                if (filtredAppUsers.Count < 1)
                    Toaster.Add($"Пользователь не найден. Попробуйте другой критерий поиска", MatToastType.Danger, "Ошибка!");
            }
            else
            {
                filtredAppUsers = null;
            }
        }
    }
}
