using MatBlazor;
using Microsoft.AspNetCore.Components;
using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace PetrpkuWeb.Client.Pages.Admin
{
    public partial class CatalogManager
    {
        [Inject]
        public IMatToaster Toaster { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        Department NewDepartment { get; set; } = new Department();
        Building NewBuilding { get; set; } = new Building();
        SiteSection NewSiteSection { get; set; } = new SiteSection();
        SiteSubsection NewSiteSubSection { get; set; } = new SiteSubsection();

        List<Department> departments;
        List<Building> buildings;
        List<SiteSection> siteSections;

        string ListCssClass;

        Department currentDepartment;
        private string _departmentName;
        public string DepatrmentName
        {
            get => _departmentName;
            set
            {
                _departmentName = value;
                this.StateHasChanged();
            }
        }

        Building currentBuilding;
        private string _buildingName;
        public string BuildingName
        {
            get => _buildingName;
            set
            {
                _buildingName = value;
                this.StateHasChanged();
            }
        }

        SiteSection currentSiteSection;
        private string _siteSectionName;
        public string SiteSectionName
        {
            get => _siteSectionName;
            set
            {
                _siteSectionName = value;
                this.StateHasChanged();
            }
        }

        SiteSubsection currentSiteSubSection;
        private string _siteSubSectionName;
        public string SiteSubSectionName
        {
            get => _siteSubSectionName;
            set
            {
                _siteSubSectionName = value;
                this.StateHasChanged();
            }
        }

        bool newDepartmentDialogIsOpen = false;
        bool newBuildingDialogIsOpen = false;
        bool newSiteSectionDialogIsOpen = false;
        bool newSiteSubSectionDialogIsOpen = false;

        bool editDepartmentDialogIsOpen = false;
        bool editBuildingDialogIsOpen = false;
        bool editSiteSectionDialogIsOpen = false;
        bool editSiteSubSectionDialogIsOpen = false;


        protected async override Task OnInitializedAsync()
        {
            departments = await HttpClient.GetJsonAsync<List<Department>>("api/departments/all");
            buildings = await HttpClient.GetJsonAsync<List<Building>>("api/buildings/all");
            siteSections = await HttpClient.GetJsonAsync<List<SiteSection>>("api/sections/all");
        }

        async Task AddNewDepartment()
        {
            newDepartmentDialogIsOpen = false;
            var response = await HttpClient.PostJsonAsync<Department>("api/departments/create", NewDepartment);
            Toaster.Add($"Новое подразделение успешно добавлено", MatToastType.Success, "Успех!");
            departments.Add(response);
            NewDepartment = new Department();
        }

        void OpenEditDepartmentDialog(Department department)
        {
            editDepartmentDialogIsOpen = true;
            currentDepartment = department;
            DepatrmentName = currentDepartment.Name;
        }

        void CancelUpdateDepartment()
        {
            editDepartmentDialogIsOpen = false;
            currentDepartment = null;
        }

        async Task UpdateDepartment()
        {
            editDepartmentDialogIsOpen = false;
            currentDepartment.Name = DepatrmentName;
            await HttpClient.PutJsonAsync<Department>($"api/departments/update/{currentDepartment.DepartmentId}", currentDepartment);
            Toaster.Add($"Информация о подразделении успешно обновлена", MatToastType.Success, "Успех!");
            currentDepartment = null;
        }

        async Task DeleteDepartment()
        {
            editDepartmentDialogIsOpen = false;
            var response = await HttpClient.DeleteAsync($"api/departments/delete/{currentDepartment.DepartmentId}");
            departments.Remove(currentDepartment);
            Toaster.Add($"Запись удалена", MatToastType.Warning, "Внимание!");
            currentDepartment = null;
        }

        async Task AddNewBuilding()
        {
            newBuildingDialogIsOpen = false;
            var response = await HttpClient.PostJsonAsync<Building>("api/buildings/create", NewBuilding);
            Toaster.Add($"Информация о новом здании успешно добавлена", MatToastType.Success, "Успех!");
            buildings.Add(response);
            NewBuilding = new Building();
        }

        void OpenEditBuildingDialog(Building building)
        {
            editBuildingDialogIsOpen = true;
            currentBuilding = building;
            BuildingName = currentBuilding.Name;
        }

        void CancelUpdateBuilding()
        {
            editBuildingDialogIsOpen = false;
            currentBuilding = null;
        }

        async Task UpdateBuilding()
        {
            editBuildingDialogIsOpen = false;
            currentBuilding.Name = BuildingName;
            await HttpClient.PutJsonAsync<Building>($"api/buildings/update/{currentBuilding.BuildingId}", currentBuilding);
            Toaster.Add($"Запись успешно обновлена", MatToastType.Success, "Успех!");
            currentBuilding = null;
        }

        async Task DeleteBuilding()
        {
            editBuildingDialogIsOpen = false;
            var response = await HttpClient.DeleteAsync($"api/buildings/delete/{currentBuilding.BuildingId}");
            buildings.Remove(currentBuilding);
            Toaster.Add($"Запись удалена", MatToastType.Warning, "Внимание!");
            currentBuilding = null;
        }

        async Task AddNewSiteSection()
        {
            newSiteSectionDialogIsOpen = false;
            var response = await HttpClient.PostJsonAsync<SiteSection>("api/sections/sitesection/create", NewSiteSection);
            Toaster.Add($"Новое подразделение успешно добавлено", MatToastType.Success, "Успех!");
            siteSections.Add(response);
            NewSiteSection = new SiteSection();
        }

        void CancelUpdateSiteSection()
        {
            editSiteSectionDialogIsOpen = false;
            currentSiteSection = null;
        }

        void OpenEditSiteSectionDialog(SiteSection siteSection)
        {
            editSiteSectionDialogIsOpen = true;
            currentSiteSection = siteSection;
            SiteSectionName = currentSiteSection.Name;
        }

        async Task DeleteSiteSection()
        {
            editSiteSectionDialogIsOpen = false;
            var response = await HttpClient.DeleteAsync($"api/sections/sitesection/delete/{currentSiteSection.SiteSectionId}");
            siteSections.Remove(currentSiteSection);
            Toaster.Add($"Запись удалена", MatToastType.Warning, "Внимание!");
            currentSiteSection = null;
        }

        async Task UpdateSiteSection()
        {
            editSiteSectionDialogIsOpen = false;
            currentSiteSection.Name = SiteSectionName;
            await HttpClient.PutJsonAsync<SiteSection>($"api/sections/sitesection/update/{currentSiteSection.SiteSectionId}", currentSiteSection);
            Toaster.Add($"Запись успешно обновлена", MatToastType.Success, "Успех!");
            currentSiteSection = null;
        }

        async Task AddNewSiteSubSection()
        {
            newSiteSubSectionDialogIsOpen = false;
            var siteSection = siteSections.SingleOrDefault(s => s.SiteSectionId == NewSiteSubSection.SiteSectionId);
            NewSiteSubSection.SiteSection = siteSection;
            var response = await HttpClient.PostJsonAsync<SiteSubsection>("api/sections/sitsubesection/create", NewSiteSubSection);
            Toaster.Add($"Новое подразделение успешно добавлено", MatToastType.Success, "Успех!");
            siteSection.SiteSubsections.Add(response);
            NewSiteSubSection = new SiteSubsection();
        }

        void CancelUpdateSiteSubSection()
        {
            editSiteSubSectionDialogIsOpen = false;
            currentSiteSubSection = null;
        }

        void OpenEditSiteSubSectionDialog(SiteSubsection siteSubSection)
        {
            editSiteSubSectionDialogIsOpen = true;
            currentSiteSubSection = siteSubSection;
            SiteSubSectionName = currentSiteSubSection.Title;
        }

        async Task DeleteSiteSubSection()
        {
            editSiteSubSectionDialogIsOpen = false;
            var siteSection = siteSections.SingleOrDefault(s => s.SiteSectionId == currentSiteSubSection.SiteSectionId);
            var response = await HttpClient.DeleteAsync($"api/sections/sitesubsection/delete/{currentSiteSubSection.SiteSubsectionId}");
            siteSection.SiteSubsections.Remove(currentSiteSubSection);
            Toaster.Add($"Запись удалена", MatToastType.Warning, "Внимание!");
            currentSiteSubSection = null;
        }

        async Task UpdateSiteSubSection()
        {
            editSiteSubSectionDialogIsOpen = false;
            currentSiteSubSection.Title = SiteSubSectionName;
            await HttpClient.PutJsonAsync<SiteSubsection>($"api/sections/sitesubsection/update/{currentSiteSubSection.SiteSubsectionId}", currentSiteSubSection);
            Toaster.Add($"Запись успешно обновлена", MatToastType.Success, "Успех!");
            currentSiteSubSection = null;
        }
    }
}
