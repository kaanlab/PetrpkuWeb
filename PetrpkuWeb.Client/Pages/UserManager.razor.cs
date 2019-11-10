using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json;
using PetrpkuWeb.Shared.Models;
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
        ClaimsPrincipal identityUser;
        List<AppUser> appUsersList;
        AppUser editAppUser;
        List<Department> departments;
        List<Building> buildings;
        List<AppUser> filtredAppUsers;

        bool editAppUserDialogIsOpen = false;
        bool newUserDialogIsOpen = false;

        protected async override Task OnInitializedAsync()
        {
            identityUser = (await AuthenticationStateTask).User;

            appUsersList = await HttpClient.GetJsonAsync<List<AppUser>>("api/users/all");
            departments = await HttpClient.GetJsonAsync<List<Department>>("api/departments/all");
            buildings = await HttpClient.GetJsonAsync<List<Building>>("api/buildings/all");
            ldapUsers = await HttpClient.GetJsonAsync<List<LdapUser>>("api/account/ldap/all");
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

            var response = await HttpClient.PostAsync("api/account/identity/add", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            //Add to UsersIdentity collection
            var appUserIdentity = JsonConvert.DeserializeObject<AppUserIdentity>(content);
            appUsersList.Add(appUserIdentity.AssosiatedUser);
            appUsersList = appUsersList.OrderBy(d => d.DisplayName).ToList();
            Toaster.Add($"Пользователь успешно добавлен в систему", MatToastType.Success, "Успех!");

            LdapUser = new LdapUser();
        }


        void OpenEditAppUserDialog(int appUserId)
        {
            editAppUserDialogIsOpen = true;
            editAppUser = appUsersList.FirstOrDefault(u => u.AppUserId == appUserId);
        }

        async Task UpdateAppUser(int appUserId)
        {
            editAppUserDialogIsOpen = false;
            var response = await HttpClient.PutJsonAsync<AppUser>($"api/users/update/{appUserId}", editAppUser);
            var index = appUsersList.FindIndex(u => u.AppUserId == response.AppUserId);
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
                    .Where(u => (u.LastName ?? "").ToLower().Contains(filter) || (u.IntPhone ?? "").ToLower().Contains(filter) || (u.Department.Name ?? "").ToLower().Contains(filter)).ToList();
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
